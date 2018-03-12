<template>
    <div class="Page-container ContentBlock">
        <div class="Grid Grid--alignMiddle Grid--gutterA" :class="imageAlignment(model.ImageAlignment.Value)">
            <div class="Grid-cell u-md-size1of2" v-if="model.Image.Value">
                <img :src="model.Image.Value" data-epi-property-name="Image" />
            </div>
            <div class="Grid-cell" :class="{'u-md-size1of2': model.Image.Value}">
                <h2 data-epi-property-name="Title">{{model.Title.Value}}</h2>
                <span v-html="model.Content.Value" data-epi-property-name="Content"></span>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        name: 'ContentBlock',
        props: ['model'],
        methods: {
            imageAlignment(direction) {
                if (direction === 'Right') {
                    return 'Grid--rowReverse'
                }
            }
        }
    }
</script>

<style lang="less" scoped>
    @import '../../Styles/Common/variables.less';

</style>