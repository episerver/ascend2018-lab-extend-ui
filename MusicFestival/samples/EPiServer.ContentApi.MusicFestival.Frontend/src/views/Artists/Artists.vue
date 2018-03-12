<template>
  <div>
    <div class="top gutter">
      <Search></Search>
      <h1>{{ this.translate('artists') }}</h1>
    </div>
    <div class="buttons">
      <button class="alpha" :class="{active: sorting == 'alpha'}" @click="changeSorting('alpha')">{{ this.translate('alphabetical') }}</button>
      <button class="headliners" :class="{active: sorting == 'headliners'}" @click="changeSorting('headliners')">{{ this.translate('headliners') }}</button>
    </div>
    <div class="list">
      <div v-for="(value, key, index) in artists" :key="key">
        <h3>{{key}}</h3>
        <Card :artist="value" v-for="(value, key) in value" :key="key" :hideInfo="true"></Card>
      </div>
    </div>
  </div>
</template>

<script>
  import Search from '@/components/Search'
  import Card from '@/components/Card'
  import api from '@/api'
  import mixins from '@/mixins'
  import EventBus from '@/eventBus'
  import mapFilters from '@/mapFilters'
  import _ from 'lodash'

  export default {
    name: 'artists',
    mixins: [mixins],
    data () {
      return {
        sorting: 'alpha',
        getChildrenResponse: {},
        getHeadlinersResponse: {},
        artists: {}
      }
    },
    created () {
      this.updateData()
      EventBus.$on('updateDataArtists', this.updateData)
    },
    destroyed () {
      EventBus.$off('updateDataArtists')
    },
    methods: {
      updateData () {
        api.getChildren(11, response => {
          // sort response alphabetically
          let ordered = _.orderBy(response.data, [artist => artist.ArtistName.Value.toLowerCase()], ['asc'])
          // group them by first letter of artist name and store in data.artists object
          this.getChildrenResponse = _.groupBy(ordered, artist => { return artist.ArtistName.Value.substring(0, 1) })
          // this is our initial sorting, so push this data as soon as possible.
          this.changeSorting(this.sorting)
        })

        // TODO: we do not want personalized later on, this is a workaround for relative vs absolute image paths returned.
        api.search('ContentType/any(t:t eq \'ArtistPage\') and ArtistIsHeadliner/Value eq true&orderby=PerformanceStartTime/Value asc, StageName asc&top=4&personalize=true', response => {
          // since we can get the response filtered and ordered, we only need to group it when using the search api. the heavy lifting is done with EPi Find.
          this.getHeadlinersResponse = _.groupBy(response.data.Results, artist => { return this.$options.filters.weekday(artist.PerformanceStartTime.Value) })
        })
      },
      changeSorting (sorting) {
        this.sorting = sorting
        console.log('in changeSort', sorting)
        this.artists = (sorting === 'alpha') ? this.getChildrenResponse : this.getHeadlinersResponse
      },
      ...mapFilters(['weekday'])
    },
    components: {
      Search,
      Card
    }
  }
</script>

<style lang="less" scoped>
  .top{padding-top:80px;}
  h3{text-transform:uppercase;}
</style>
