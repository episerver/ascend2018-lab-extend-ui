<template>
    <div>
        <Hero :title="model.Title.Value" :subtitle="model.Subtitle.Value" :heroimage="model.HeroImage.Value" :modal-showing="modalShowing"></Hero>

        <main>
            <div data-epi-property-name="MainContentArea" data-epi-property-render="none">
                <ContentArea :model="model.MainContentArea"></ContentArea>
            </div>
        </main>

        <footer>
            <div data-epi-property-name="FooterContentArea" data-epi-property-render="none">
                <ContentArea :model="model.FooterContentArea"></ContentArea>
            </div>
            <div class="FooterBottom">
                <h6>&copy; Music Festival 2018</h6>
            </div>
        </footer>
    </div>
</template>

<script>
    import api from '@/Scripts/api.js'
    import GlobalMixins from "@/Scripts/Mixins/global"

    export default {
        mixins: [GlobalMixins],
        name: 'LandingPage',
        data() {
            return {
                model: {
                    Title: { Value: '' },
                    Subtitle: { Value: '' },
                    HeroImage: { Value: '' },
                    MainContentArea: { Value: [], ExpandedValue: [] },
                    FooterContentArea: { Value: [], ExpandedValue: [] }
                }
            }
        },
        created() {
            this.updateModelAsync(this.contentlink)
        },
        mounted() {
            this.registerContentSavedEvent()
        },
        props: ['contentlink', 'language', 'modalShowing']
    }
</script>

<style lang="less">
    @import '../../Styles/Common/variables.less';

    main, footer {
        overflow: hidden;
        width: 100%;
    }
</style>