import * as CONSTANTS from '@/Scripts/constants.js'
import api from '@/Scripts/api.js'

export default {
    methods: {
        getBlockType: function (model, contentLinkId) {

            if (Object.keys(model).length === 0)
                return

            let blockModel
            let vm = this
            if (contentLinkId !== undefined) {
                for (let i = 0; i < model.ExpandedValue.length; i++) {
                    if (model.ExpandedValue[i].ContentLink.Id === contentLinkId) {
                        blockModel = model.ExpandedValue[i]
                    }
                }
            } else {
                blockModel = model
            }

            for (let i = 0; i < blockModel.ContentType.length; i++) {

                let contentType = blockModel.ContentType[i]
                if (vm.$options.components[contentType]) {
                    return contentType
                }
            }

            //Fallback: Load the "GenericBlock" in the case that no block is found.
            return "GenericBlock"
        },
        getBlockModel: function (model, contentLinkId) {
            for (let i = 0; i < model.ExpandedValue.length; i++){
                if (model.ExpandedValue[i].ContentLink.Id === contentLinkId) {
                    return model.ExpandedValue[i]
                }
            }
        },
        getDisplayOption: function (value) {
            let displayoption = value

            for (var key in CONSTANTS.DISPLAY_OPTIONS) {
                if (CONSTANTS.DISPLAY_OPTIONS.hasOwnProperty(key)) {
                    if (displayoption === key) {
                        return CONSTANTS.DISPLAY_OPTIONS[key]
                    }
                }
            }
        },
        updateModelAsync: function (contentLink) {
            let vm = this

            const parameters = {
                'expand': '*'
            }

            return api.get('episerver/content/' + contentLink, vm.language, parameters).then(response => {
                let data = response.data
                vm.model = data
                vm.$forceUpdate()
            })
        },
        registerContentSavedEvent: function () {
            if (!document.documentElement.classList.contains('epi-editMode'))
                return;

            window.addEventListener("load", () => {
                window.epi.subscribe("beta/contentSaved", (details) => {
                    console.log('beta/contentSaved', details)

                    this.updateModelAsync(details.contentLink)
                })
            })
        }
    }
}