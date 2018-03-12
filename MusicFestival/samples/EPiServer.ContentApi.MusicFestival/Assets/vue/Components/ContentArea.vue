<template>
    <section class="Grid Grid--alignMiddle Grid--gutterA ContentArea">
        <div v-for="(block) in model.Value" class="Grid-cell" :class="getDisplayOption(block.DisplayOption)">
            <component :is="getBlockType(model, block.ContentLink.Id)" :model="getBlockModel(model, block.ContentLink.Id)"></component>
        </div>
    </section>
</template>

<script>
    import GlobalMixins from "@/Scripts/Mixins/global"

    export default {
        mixins: [GlobalMixins],
        name: 'ContentArea',
        props: ['model']
    }
</script>