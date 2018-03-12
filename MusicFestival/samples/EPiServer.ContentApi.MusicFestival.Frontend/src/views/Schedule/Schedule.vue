<template>
  <div>
    <div class="top gutter">
      <Search></Search>
      <h1>{{ translate('schedule') }}</h1>
    </div>
    <div class="buttons">
      <button :class="{active: sorting == 'time'}" @click="sortArtists('time')">{{ translate('byTime') }}</button>
      <button :class="{active: sorting == 'place'}" @click="sortArtists('place')">{{ translate('byPlace') }}</button>
    </div>
    <div class="buttons is--alternative">
      <button :class="{active: day == 6}" @click="changeDay(6)">{{ translate('saturday') }}</button>
      <button :class="{active: day == 0}" @click="changeDay(0)">{{ translate('sunday') }}</button>
    </div>
    <div class="list">
      <div v-for="(value, key, index) in artists" :key="key">
        <h3>{{key}}</h3>
        <Card :artist="value" v-for="(value, key) in value" :key="key" v-if="new Date(value.PerformanceStartTime.Value).getDay() === day" />
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
    name: 'schedule',
    mixins: [mixins],
    data () {
      return { // defaults when opening the page.
        sorting: 'time',
        day: 6,
        response: {},
        artists: {}
      }
    },
    created () {
      this.updateData()
      EventBus.$on('updateDataSchedule', this.updateData)
    },
    destroyed () {
      EventBus.$off('updateDataSchedule')
    },
    methods: {
      updateData () {
        api.getChildren(11, response => {
          this.response = response.data
          this.sortArtists(this.sorting)
        })
      },
      sortArtists (sorting) {
        let artistList = null
        let artists = this.response

        this.sorting = sorting

        if (this.sorting === 'time') {
          // sort by start performance time, to get 2pm before 7pm and so on. Also, sort on Stage Name to get same ordering in list.
          let sorted = _.sortBy(artists, [artist => { return artist.PerformanceStartTime.Value }, artist => { return artist.StageName.Value }])
          // group artists by start time, creating a loopable object that we just throw at Vue.
          artistList = _.groupBy(sorted, artist => { return this.$options.filters.shortTime(artist.PerformanceStartTime.Value) })
        } else {
          // sort by start time
          let sorted = _.sortBy(artists, artist => { return artist.PerformanceStartTime.Value })
          // group by stage.
          artistList = _.groupBy(sorted, artist => { return artist.StageName.Value + this.translate('scene') })
        }

        this.artists = artistList
      },
      changeDay (day) {
        this.day = day
      },
      ...mapFilters(['shortTime'])
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
