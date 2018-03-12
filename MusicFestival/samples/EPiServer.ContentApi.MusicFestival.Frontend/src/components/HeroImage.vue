<template>
  <div class="heroImage">
    <div class="ol">
      <img src="../assets/spacer.gif" :alt="hero.Name.Value" width="720" height="568" :class="{ loaded: isLoaded }" v-img="hero.Url" />
    </div>
  </div>
</template>

<script>
  export default {
    name: 'HeroImage',
    data () {
      return {
        isLoaded: false
      }
    },
    props: ['hero'],
    methods: {
      updateData (el, binding, vnode) {
        if (binding.value) {
          let img = new Image()
          img.src = 'https://epiheadlessui.azurewebsites.net/' + binding.value
          img.onload = function () {
            el.src = img.src
            vnode.context.isLoaded = true
          }
        }
      }
    },
    directives: {
      img: {
        isFn: true,
        bind (el, binding, vnode) {
          vnode.context.updateData(el, binding, vnode)
        },
        update (el, binding, vnode) {
          vnode.context.updateData(el, binding, vnode)
        }
      }
    }
  }
</script>

<style lang="less" scoped>
  @import "../assets/styles/common/_variables.less";

  div.heroImage{
    position:relative;
    font-size:0;
    padding-top:78.89%;
  }

  div.ol{
    position: absolute;
    top: 0;
    left: 0;

    &:after{
      content:'';
      display:block;
      position: absolute;
      top: 0;
      left: 0;
      width: 100%;
      height: 100%;
      background-image: linear-gradient(to bottom, rgba(26, 35, 126, .1) 0%, rgba(26, 35, 126, 0.6) 100%);
    }

    img{
      width:100%;
      opacity: 0.0;
      transition: opacity .5s ease-out;

      &.loaded{
        opacity: 1.0;
      }
    }
  }
</style>
