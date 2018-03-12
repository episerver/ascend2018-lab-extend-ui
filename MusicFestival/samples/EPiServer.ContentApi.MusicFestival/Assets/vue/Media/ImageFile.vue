<template>
    <div class="Page-container ImageFile">
        <div class="Grid Grid--alignMiddle Grid--gutterA">
            <div class="Grid-cell">
                <img :src="model.Url" />
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        name: 'ImageFile',
        props: ['model']
    }
</script>