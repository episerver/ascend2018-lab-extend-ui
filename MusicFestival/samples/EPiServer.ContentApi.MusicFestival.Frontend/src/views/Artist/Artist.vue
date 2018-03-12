<template>
  <div v-if="loaded">
    <div class="backButton" @click="$router.go(-1)">
      <svg><use xlink:href="#back"></use></svg>
    </div>
    <div class="u-posRelative">
      <ArtistImage :artist="artist" :hideInfo="true" />
      <div class="top gutter">
        <h1>{{artist.ArtistName.Value}}</h1>
        <p>{{artist.StageName.Value}}{{translate('scene')}}</p>
        <p>{{artist.PerformanceStartTime.Value | weekday}}</p>
        <p>{{artist.PerformanceStartTime.Value | shortTime}} - {{artist.PerformanceEndTime.Value | shortTime}}</p>
      </div>
    </div>
    <div class="gutter fav" @click.stop.prevent="toggleFav">
      <div class="star--alt">
        <svg><use :xlink:href="'#' + starsvg"></use></svg>
      </div>
      <span v-if="!isStarred">{{ translate('addToFav') }}</span>
      <span v-if="isStarred">{{ translate('addedToFav') }}</span>
    </div>
    <div class="gutter">
      <p v-html="artist.ArtistDescription.Value"></p>
    </div>
    <div class="gutter u-marginTopDouble">
      <button type="button">{{ translate('listenNow') }}</button>
    </div>
    <div class="related u-marginTopDouble">
      <h2 class="gutter">{{ translate('similarArtists') }}</h2>
      <div v-if="relatedLoaded">
        <router-link :to="'/artists/' + relatedArtist.ContentLink.Id"  v-for="relatedArtist in relatedArtists" :key="relatedArtist.ContentLink.Id" replace>
          <ArtistImageSmall :artist="relatedArtist"></ArtistImageSmall>
        </router-link>
      </div>
    </div>
  </div>
</template>

<script>
import api from '@/api'
import mixins from '@/mixins'
import EventBus from '@/eventBus'
import ArtistImage from '@/components/ArtistImage'
import ArtistImageSmall from '@/components/ArtistImageSmall'

export default {
  name: 'Artist',
  mixins: [mixins],
  data () {
    return {
      loaded: false,
      relatedLoaded: false,
      artist: { },
      relatedArtists: { ContentLink: { Id: 0 } }
    }
  },
  watch: {
    '$route': 'updateData'
  },
  computed: {
    starsvg () {
      return this.isStarred ? 'starfilled-alt' : 'star-alt'
    },
    isStarred () {
      return this.$store.getters.favs[this.artist.ContentLink.Id]
    }
  },
  created () {
    // turn off the menu to show back button instead
    this.showMenu(false)

    this.updateData()
    EventBus.$on('updateDataArtist', this.updateData)
    // EventBus.$on('contentSaved', this.updateData)
  },
  beforeDestroy () {
    this.showMenu(true)
    EventBus.$off('updateDataArtist')
  },
  methods: {
    updateData (data) {
      let id = this.$route.params.id
      // if (data && data.contentLink && data.contentLink.startsWith(id)) {
      //   id = data.contentLink
      // }
      api.getContent(id, response => {
        this.artist = response.data
        this.loaded = true

        // TODO: we do not want personalized later on, this is a workaround for relative vs absolute image paths returned.
        api.search('ContentType/any(t:t eq \'ArtistPage\') and ArtistGenre/Value eq \'' + this.artist.ArtistGenre.Value + '\' and ContentLink/Id ne ' + this.artist.ContentLink.Id + '&orderby=PerformanceStartTime/Value asc, StageName asc&top=100&personalize=true', response => {
          this.relatedArtists = response.data.Results
          this.relatedLoaded = true
        })
      })
    },
    showMenu (state) {
      this.$store.commit('showMenu', state)
    },
    toggleFav () {
      this.$store.commit('toggleFav', this)
    }
  },
  components: {
    ArtistImage,
    ArtistImageSmall
  }
}
</script>

<style lang="less" scoped>
  @import '../../assets/styles/common/_variables.less';
  .backButton{
    position: fixed;
    top: 12px;
    z-index: 10;
    left: 24px;
    width: 32px;
    height: 32px;
  }
  .top{
    position:absolute;
    bottom:30px;
    width: 100%;
    h1{
      padding-right: 20px;
    }
    p{
      margin:0;
      font: 12px @fontSubHeading;
      text-transform:uppercase;
    }
  }

  button{
    width:100%;
    background: @colorTurqoise;
    border-radius: 2em;
    color: @colorBlue;
    text-transform: uppercase;
    font: 12px @fontSubHeading;
    letter-spacing: 1.2px;
    padding: 12px;
    box-shadow: 0 0 12px 0 fade(@colorBlack, 36%);
    border:0;
  }

  .related{
    height:180px;
    h2{
      font:12px @fontSubHeading;
      text-transform: uppercase;
    }
    > div{
      position: relative;
      overflow-x: auto;
      white-space: nowrap;
      > a{
        display:inline-block;
        width:124px;
        height:144px;
        margin-left: 12px;
        &:first-child{
          margin-left: 20px;
        }
        &:last-child{
          margin-right: 20px;
        }
        >div{
          border-radius: 8px;
          overflow: hidden;
        }
      }
    }
  }
  .fav{
    display: flex;
    flex-direction: row;
    align-items: center;
    text-transform: uppercase;
    font: 12px @fontSubHeading;
    letter-spacing: 1.2px;
    margin-bottom: -10px;
    margin-top: 10px;
  }
  .star--alt{
    width: 48px;
    height: 48px;
    margin-left: -10px;
  }
</style>
