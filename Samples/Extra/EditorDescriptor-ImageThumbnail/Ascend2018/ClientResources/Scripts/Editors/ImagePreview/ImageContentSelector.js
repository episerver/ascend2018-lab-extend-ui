define([
    'dojo/_base/declare',
    'dojo/dom-attr',
    'dojo/dom-style',
    'dojo/dom-construct',
    
    'epi-cms/widget/ContentSelector'
],
function (
    declare,
    domAttr,
    domStyle,
    domConstruct,
    
    ContentSelector
) {
    return declare([ContentSelector], {
        // summary:
        //      This class extends ContentSelector and adds a thumbnail preview which makes the image selector
        //      more helpful for users. The ImageEditorDescriptor.cs on the server sets this up as the default
        //      editor for image properties.

        // thumbnailNode: [readonly] Element
        //      The thumbnail node.
        thumbnailNode: null,

        // content: [readonly] Object
        //      The selected image.
        content: null,

        buildRendering: function () {
            // summary:
            //      buildRendering happens after the DOM has been created.
            //      We add a thumbnail element that will serve as the preview image.

            this.inherited(arguments);

            // Create the thumbnail element
            var attributes = {
                style: {
                    'borderBottom': '1px solid #929ba4',
                    'width': '195px', // The image should preferably be resized on the server instead, because previewUrl is slightly bigger than this :)
                    'display': 'none'
                }
            };
            this.thumbnailNode = domConstruct.create('img', attributes, this.displayNode, 'first');
        },

        _updateDisplayNode: function (content) {
            // summary:
            //      This method is inherited from _SelectorBase, through ContentSelector.
            //      When the display node is updated we update the thumbnail.

            this.inherited(arguments);

            this.set('content', content);

            this.updateThumbnail();
        },

        updateThumbnail: function () {
            // summary:
            //      Update the thumbnail preview node based on the selected content.

            var content = this.content;

            var hasPreview = content && content.previewUrl;

            domAttr.set(this.thumbnailNode, 'src', hasPreview ? content.previewUrl : '');
            domStyle.set(this.thumbnailNode, 'display', hasPreview ? '' : 'none');
        }
    });
});