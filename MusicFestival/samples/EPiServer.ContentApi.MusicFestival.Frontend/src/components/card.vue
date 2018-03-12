<template>
  <router-link :to="'/artists/' + artist.ContentLink.Id">
    <div class="card">
      <div class="round" :class="{ big: !hideInfo}">
        <img :src="'https://epiheadlessui.azurewebsites.net' + artist.ArtistPhoto.Value" />
      </div>
      <div class="info">
        <div>
          <p>{{artist.ArtistName.Value}}</p>
          <span v-if="!hideInfo">{{artist.StageName.Value}}{{translate('scene')}}</span>
          <span v-if="!hideInfo">{{artist.PerformanceStartTime.Value | weekday}} | {{artist.PerformanceStartTime.Value | shortTime}}-{{artist.PerformanceEndTime.Value | shortTime}}</span>
        </div>
      </div>
      <div class="star" @click.stop.prevent="toggleFav"><svg><use :xlink:href="'#' + starsvg"></use></svg></div>
    </div>
  </router-link>
</template>

<script>
  import mixins from '@/mixins'

  export default {
    name: 'card',
    mixins: [mixins],
    data () {
      return {

      }
    },
    props: [
      'artist',
      'hideInfo'
    ],
    computed: {
      starsvg () {
        return this.isStarred ? 'starfilled' : 'star'
      },
      isStarred () {
        return this.$store.getters.favs[this.artist.ContentLink.Id]
      }
    },
    methods: {
      toggleFav () {
        this.$store.commit('toggleFav', this)
      }
    }
  }
</script>

<style lang="less" scoped>
  @import "../assets/styles/common/_variables.less";

  .card{
    display: flex;
    flex-direction: row;
    align-items: center;
    padding: 10px 30px 10px 25px
  }
  .round{
    width: 40px;
    height: 40px;
    border-radius: 40px;
    overflow:hidden;
    margin-right:10px;
    img{
      max-width:150%;
      margin-left:-15px
    }

    &.big{
      width: 60px;
      height: 60px;
    }
  }
  .info{
    flex-grow: 1;
    p{margin:0;font:14px/20px @fontSubHeading;}
    span{
      display: block;
      font: 12px/16px @fontBody
    }
  }
  .star{
    margin-left:10px;
    width:15px;
    height:15px;
  }
</style>
