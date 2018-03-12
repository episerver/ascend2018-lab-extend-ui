define([
    'dojo/_base/declare',
    'dojo/topic',
    // Parent class and mixins
    'epi/shell/command/_Command'
], function (
    declare,
    topic,
    // Parent class and mixins
    _Command
) {
    return declare([_Command], {
        // summary:
        //      Redirects to the content approval log view.
        // tags:
        //      internal

        // canExecute: [readonly] Boolean
        //      Flag which indicates whether this command is able to be executed.
        canExecute: true,

        // isAvailable: [readonly] Boolean
        //      Flag which indicates whether this command is available in the current context.
        isAvailable: true,

        // iconClass: [public] String
        //      The icon class of the command to be used in visual elements.
        iconClass: 'epi-iconList',

        // label: [public] String
        //      The action text of the command to be used in visual elements.
        label: 'Approval Audit Log',

        _execute: function () {
            // summary:
            //      Redirects to the content approval log view.

            // This sets the view to our log, and the 'true' parameter sets it to sticky-view. 
            topic.publish('/epi/shell/action/changeview', 'approvalLog', null, null, true);
            
            // This changes the context to the content that the user selected in the navigation tree.
            topic.publish('/epi/shell/context/request', { uri: 'epi.cms.contentdata:///' + this.model.contentLink });
        }
    });
});
