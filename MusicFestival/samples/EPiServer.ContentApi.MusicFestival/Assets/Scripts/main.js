import Vue from 'vue';

// generate svg sprite from all files in /Assets/Images/SVG
const files = require.context('../Images/SVG', false, /.*\.svg$/)
files.keys().forEach(files)

import LandingPage from "@/vue/Pages/LandingPage.vue";
import Preview from "@/vue/Pages/Preview.vue";

import ContentBlock from "@/vue/Blocks/ContentBlock.vue"
import ImageFile from "@/vue/Media/ImageFile.vue"
import GenericBlock from "@/vue/Blocks/GenericBlock.vue"

import BuyTickets from "@/vue/Components/BuyTickets.vue"
import ContentArea from "@/vue/Components/ContentArea.vue"
import Hero from "@/vue/Components/Hero.vue"
import Modal from "@/vue/Components/Modal.vue"

import PropertyEditor from "@/vue/Utility/PropertyEditor.vue"

Vue.component('LandingPage', LandingPage);
Vue.component('Preview', Preview);

Vue.component('ContentBlock', ContentBlock);
Vue.component('ImageFile', ImageFile);
Vue.component('GenericBlock', GenericBlock);

Vue.component('BuyTickets', BuyTickets);
Vue.component('ContentArea', ContentArea);
Vue.component('Hero', Hero);
Vue.component('Modal', Modal);

Vue.component('PropertyEditor', PropertyEditor);

let App = new Vue({
    el: '#App',
    data: {
        showModal: false
    }
});
