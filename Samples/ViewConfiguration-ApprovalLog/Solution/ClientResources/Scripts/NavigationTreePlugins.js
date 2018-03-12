define([
    'dojo/_base/declare',
    'epi-cms/plugin-area/navigation-tree',
    // Parent class
    'epi/_Module',
    // Commands
    './ApprovalLogView/ApprovalLogCommand'
], function (
    declare,
    navigationTreePluginArea,
    // Parent class
    _Module,
    // Commands
    ApprovalLogCommand
) {

    return declare([_Module], {

        initialize: function () {
            this.inherited(arguments);

            navigationTreePluginArea.add(ApprovalLogCommand);
        }
    });
});