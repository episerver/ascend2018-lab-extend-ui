/* Markdown editor based on SimpleMDE: https://simplemde.com
   SimpleMDE is provided under MIT license: https://github.com/sparksuite/simplemde-markdown-editor/blob/master/LICENSE */

define([
    'dojo/_base/declare',
    'dojo/_base/config',
    'dojo/ready',
    'dojo/aspect',
    'dojo/dom-class',

    'dijit/_Widget',
    'dijit/_TemplatedMixin',
    'dijit/_WidgetsInTemplateMixin',

    'epi/epi',
    'epi/shell/widget/_ValueRequiredMixin',

    './simplemde/simplemde.min',

    'dojo/text!./Template.html',
    'xstyle/css!./simplemde/simplemde.min.css',
    'xstyle/css!./Template.css'
],

function (
    declare,
    config,
    ready,
    aspect,
    domClass,

    _Widget,
    _TemplatedMixin,
    _WidgetsInTemplateMixin,

    epi,
    _ValueRequiredMixin,

    SimpleMDE,

    template
) {
    return declare([_Widget, _TemplatedMixin, _WidgetsInTemplateMixin, _ValueRequiredMixin], {
        // summary:
        //      This class creates a custom widget that uses the markdown editor 'SimpleMDE'. It can be used to as a text
        //      editor for string properties by setting the UIHint on model properties on the server. This is made
        //      possible by the editor descriptor MarkdownEditorDescriptor.cs. 

        // editor: [readonly] SimpleMDE
        //      The SimpleMDE editor object.
        editor: null,

        // templateString: [readonly] String
        //      The widget template HTML.
        templateString: template,

        // _onEditorValueChangeHandle: Function 
        //      The handle used with SimpleMDE to properly clean up event listeners when the widget is destroyed.
        _onEditorValueChangeHandle: null,

        onChange: function (value) {
            // summary:
            //      Notifies Episerver that the property value has changed.

            this.inherited(arguments);
        },

        buildRendering: function () {
            // summary:
            //      When the DOM has finished loading, we convert our textarea element to a SimpleMDE editor.
            //      We also wire up the 'blur' event to ensure editor changes propagate to the widget, i.e. property, value. */

            this.inherited(arguments);

            this.editor = new SimpleMDE({
                element: this.editorNode,
                initialValue: this.get('value'), // This can change after startup, so we override _setValueAttr() further down.
                placeholder: this.tooltip,
                spellChecker: false,
                status: ['lines', 'words'],
                toolbar: !this.readOnly ? ['bold', 'italic', 'heading', 'unordered-list', 'ordered-list', 'link', 'preview'] : false
            });

            this._onEditorValueChangeHandle = this._onEditorValueChange.bind(this);
            this.editor.codemirror.on('change', this._onEditorValueChangeHandle);

            this._refreshEditor();
        },

        destroy: function () {
            // summary:
            //      Make sure to destroy everything so we don't leak memory.

            this.editor.codemirror.off('change', this._onEditorValueChangeHandle);
            this._onEditorValueChangeHandle = null;

            // If the FloatingEditorWrapper hasn't destroyed the parent then we can destroy the SimpleMDE instance.
            if (this.editor.parentElement) {
                this.editor.toTextArea();
            }

            this.editor = null;

            this.inherited(arguments);
        },

        resize: function () {
            // summary:
            //      The resize() function is called when the tab strip containing this widget switches tabs.
            //      When this happens we need to refresh the editor to ensure it displays property.
            //      This is a well-known characteristic of CodeMirror, which is part of the SimpleMDE editor.

            this.inherited(arguments);

            this._refreshEditor();
        },
        
        isValid: function () {
            // summary:
            //      Needed to support properties where the value is required (_ValueRequiredMixin).
            
            return !this.required || this.editor.value();
        },

        _onEditorValueChange: function() {
            // summary:
            //      Callback for when the SimpleMDE value has changed. We need to set the 'value' property and propagate
            //      that change so it's saved by the CMS.
            
            var value = this.editor.value();
            if (!epi.areEqual(this.get('value'), value)) {
                this.set('value', value);
                this.onChange(value);
            }
        },

        _setValueAttr: function (value) {
            // summary:
            //      Needed for startup, and undo. It's not enough with only handling the "value" in buildRendering().

            this._set('value', value);

            this._refreshEditor();
        },

        _setReadOnlyAttr: function (value) {
            // summary:
            //      Needed to support the compare view.

            this._set('readOnly', value);

            this._refreshEditor();
        },

        _refreshEditor: function () {
            // summary:
            //      This function refreshes the editor, and ensures its value matches the current property value.
            //      It also switches to preview mode, making the editor read-only, if the underlying property
            //      is in read-only mode.

            if (!this.editor) {
                return;
            }

            if (typeof this.get('value') !== 'object' && !epi.areEqual(this.editor.value(), this.get('value'))) {
                this.editor.value(this.get('value'));
            }

            if (this.readOnly) {
                var previewElement = this.editor.codemirror.getWrapperElement().lastChild;

                var previewActive = domClass.contains(previewElement, 'editor-preview-active');

                if (!previewActive) {
                    this.editor.togglePreview();
                } else {
                    previewElement.innerHTML = this.editor.options.previewRender(this.editor.value(), previewElement);
                }
            }

            this.editor.codemirror.refresh();
        }
    });
});