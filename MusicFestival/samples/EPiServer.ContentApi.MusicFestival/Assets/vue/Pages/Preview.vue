<template>
    <section class="Grid Preview">
        <div class="Grid-cell u-border">
            <h5>Full</h5>
        </div>
        <div class="Grid-cell u-md-sizeFull">
            <component :is="getBlockType(model)" :model="model"></component>
        </div>

        <div class="Grid-cell u-border">
            <h5>Wide</h5>
        </div>
        <div class="Grid-cell u-md-size2of3">
            <component :is="getBlockType(model)" :model="model"></component>
        </div>

        <div class="Grid-cell u-border">
            <h5>Half</h5>
        </div>
        <div class="Grid-cell u-md-size1of2">
            <component :is="getBlockType(model)" :model="model"></component>
        </div>

        <div class="Grid-cell u-border">
            <h5>Narrow</h5>
        </div>
        <div class="Grid-cell u-md-size1of3">
            <component :is="getBlockType(model)" :model="model"></component>
        </div>
    </section>
</template>

<script>
    import api from '@/Scripts/api.js'
    import GlobalMixins from '@/Scripts/Mixins/global'

    export default {
        mixins: [GlobalMixins],
        name: 'Preview',
        data() {
            return {
                model: { }
            }
        },
        created() {
            this.updateModelAsync(this.contentlink)
        },
        mounted() {
            this.registerContentSavedEvent()
        },
        props: ['contentlink', 'language']
    }
</script>

<style lang="less">
    @import '../../Styles/Common/variables.less';
    .PageHeader{
        display: none;
    }
    .Page > section{
        padding: 1em;
    }
    .u-border {
        border-bottom: 1px solid fade(@colorWhite, 50%);
    }
</style>