<template>
  <div class="artistImage">
    <div class="ol">
      <img src="../assets/spacer.gif" :alt="artist.ArtistName.Value" width="720" height="568" :class="{ loaded: isLoaded }" v-img="artist.ArtistPhoto.Value" />
    </div>
    <div class="info" v-if="!hideInfo">
      <p>{{ artist.ArtistName.Value }}</p>
    </div>
    <div class="control" v-if="!hideInfo" @click.stop.prevent="togglePlay" :class="{ 'is--playing': isPlaying }">
      <div class="border"></div>
      <div class="play"></div>
    </div>
  </div>
</template>

<script>
  import EventBus from '@/eventBus'

  export default {
    name: 'ArtistImage',
    data () {
      return {
        isLoaded: false,
        isPlaying: false,
        tmpData: {}
      }
    },
    props: ['artist', 'hideInfo', 'isImage'],
    created () {
      EventBus.$on('playingSample', source => {
        if (source !== this) {
          this.isPlaying = false
        }
      })
    },
    destroyed () {
      EventBus.$off('playingSample', this)
    },
    methods: {
      togglePlay () {
        this.isPlaying = !this.isPlaying
        EventBus.$emit('playingSample', this)
      },
      updateData (el, binding, vnode) {
        if (binding.value) {
          let img = new Image()
          img.src = 'https://epiheadlessui.azurewebsites.net' + binding.value
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

  div.artistImage{
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

  .info{
    p{
      font: 14px/20px @fontSubHeading;
      position: absolute;
      bottom: 0;
      margin-left: 10px;
      width: 140px;
    }
  }

  div.control{
    position: absolute;
    bottom: 10px;
    width: 20px;
    height: 20px;
    right: 10px;

    .border{
      width:100%;
      height: 100%;
      border: 1px solid @colorTurqoise;
      border-radius:20px;
    }

    &.is--playing .border{
      border-top: none;
      border-bottom: none;
      animation: spin 1.5s ease-in-out infinite;
    }

    .play {
      position: absolute;
      top: 5px;
      left: 8px;
      box-sizing: border-box;
      height: 7px;
      width:5px;

      border-color: transparent transparent transparent @colorTurqoise;
      transition: 100ms all ease;
      will-change: border-width;
      cursor: pointer;

      // play state
      border-style: solid;
      border-width: 5px 0 5px 5px;
    }
    &.is--playing .play {
        border-style: double;
        border-width: 0px 0 0px 6px;
        transform: translate(-1px, 1px);
      }
  }

  @keyframes spin{
    0% { transform: rotate(0deg);}
    100% { transform: rotate(360deg); }
  }
</style>
