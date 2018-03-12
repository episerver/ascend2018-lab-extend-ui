<template>
  <button
    v-if="inEditMode"
    :data-epi-property-name="conditionalPropertyName"
    data-epi-property-render="none"
    type="button">Edit property: {{propertyName}}</button>
</template>

<script>
  export default {
    name: 'PropertyEditor',
    props: ['propertyName', 'modalShowing'],
    computed: {
      inEditMode() {
        return document.documentElement.classList.contains('epi-editMode')
      },
      conditionalPropertyName() {
          return this.modalShowing ? null : this.propertyName
      }
    }
  }
</script>

<style lang="less" scoped>
  button {
    background: gray;
    color: white;
  }
</style>