<template>
  <div v-if="loaded">
    <ArtistImage :artist="tempHero" :hideInfo="true" />
    <div class="top gutter">
      <h1><span>{{ translate('todaysHeadliner') }}</span>{{tempHero.ArtistName.Value}}</h1>
      <router-link :to="'/artists/' + tempHero.ContentLink.Id" class="arrow"><svg><use xlink:href="#arrow"></use></svg> {{ translate('goToArtist') }}</router-link>
    </div>
    <div class="gutter" v-html="mainBody">
    </div>
    <div class="Grid">
      <div class="Grid-cell u-xsm-size1of2" v-for="artist in artists" :key="artist.ContentLink.Id">
        <router-link :to="'/artists/' + artist.ContentLink.Id">
          <ArtistImage :artist="artist" />
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

export default {
  name: 'home',
  mixins: [mixins],
  data () {
    return {
      loaded: false,
      tempHero: {},
      artists: [],
      mainBody: ''
    }
  },
  created () {
    this.updateData()
    EventBus.$on('updateDataDiscover', this.updateData)
    EventBus.$on('contentSaved', this.updateData)
  },
  destroyed () {
    EventBus.$off('updateDataDiscover')
  },
  methods: {
    updateData (data) {
      let id = '8'
      if (data && data.contentLink.startsWith(id)) {
        id = data.contentLink
      }
      api.getContent(id, 'HeroContentArea,MainContant', response => {
        this.artists = response.data.MainContant.ExpandedValue
        this.tempHero = response.data.HeroContentArea.ExpandedValue[0]
        this.mainBody = response.data.MainBody.Value
        this.loaded = true
      })
    }
  },
  components: {
    ArtistImage
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style lang="less" scoped>
  .top{
    position:absolute;
    top:0;
    width: 100%;
    padding-top:78.89%;
    h1{
      position: absolute;
      bottom: 45px;
      padding-right: 20px;
    }
    a{
      position: absolute;
      bottom: 20px;
      svg{
        position: relative;
        width: 40px;
        height: 40px;
        top: 15px;
        margin-right: -5px;
      }
    }
  }
</style>
