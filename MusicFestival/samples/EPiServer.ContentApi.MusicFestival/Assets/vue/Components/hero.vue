<template>
    <section class="Hero">
        <div class="Hero-content Page-container">

            <h1
                :data-epi-property-name="propTitle"
                data-epi-property-render="none"
                v-html="title"></h1>

            <h5 
                :data-epi-property-name="propSubtitle"
                data-epi-property-render="none"
                data-epi-property-edittype="floating"
                v-html="subtitle"></h5>

            <PropertyEditor property-name="HeroImage" :modal-showing="modalShowing"></PropertyEditor>
        </div>
        <div class="Hero-image" v-bind:style="{ 'background-image': 'url(' + heroimage + ')' }"></div>
    </section>

</template>

<script>
    export default {
        name: 'hero',
        props: ['title', 'subtitle', 'heroimage', 'modalShowing'],
        computed: {
            propTitle() {
                return this.modalShowing ? null : "Title"
            },
            propSubtitle() {
                return this.modalShowing ? null : "Subtitle"
            }
        }
    }
</script>

<style lang="less" scoped>
    @import '../../Styles/Common/variables.less';
</style>