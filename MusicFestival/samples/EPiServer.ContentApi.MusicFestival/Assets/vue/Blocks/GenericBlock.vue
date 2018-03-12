<template>
    <div class="Page-container GenericBlock">
        <div class="Grid Grid--alignMiddle Grid--gutterA">
            <div class="Grid-cell">
                <img :src="model.Image.Value" data-epi-property-name="Image" />
                <span v-html="model.Content.Value" data-epi-property-name="Content"></span>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        name: 'GenericBlock',
        props: ['model']
    }
</script>