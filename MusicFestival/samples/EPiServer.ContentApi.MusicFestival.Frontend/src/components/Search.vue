<template>
  <div class="searchWrapper">
    <div :class="{ 'is-active': searchActive }">
      <Card :artist="result" v-for="result in results" :key="result.ContentLink.Id"></Card>
    </div>
    <input class="search" type="text" v-model="query" :placeholder="translate('search')" @input="doSearch" />
  </div>
</template>

<script>
  import api from '@/api'
  import mixins from '@/mixins'
  import Card from '@/components/Card'

  export default {
    name: 'search',
    mixins: [mixins],
    data () {
      return {
        query: '',
        searchActive: false,
        results: {}
      }
    },
    methods: {
      handleFocus (e) {
        if (e.target === document.activeElement) {
          this.searchActive = true
        } else {
          if (!e.target.value.length) this.searchActive = false
        }
      },
      doSearch (e) {
        if (e.target.value.length > 0) {
          this.searchActive = true
          // TODO: Update to show data from a number of properties, like stage etc. Also, should we get rid of personalize?
          api.search('contains(tolower(ArtistName/Value), ' + e.target.value.toLowerCase() + ')&personalize=true', response => {
            this.results = response.data.Results
          })
        } else {
          this.searchActive = false
        }
      }
    },
    components: {
      Card
    }
  }
</script>

<style lang="less" scoped>
  @import '../assets/styles/common/_variables.less';
  .searchWrapper{
    width:100%;

    > div{
      position:fixed;
      z-index:-1;
      top: 0;
      left: 0;
      width:100vw;
      height: 100vh;
      padding-top:120px;
      overflow: hidden;
      overflow-y: auto;
      background-color: @backgroundColor;
      background-image: @backgroundGradient;
      background-attachment: fixed;
      opacity: .0;
      transition: opacity .4s ease-in-out;

      &.is-active{
        z-index: 2;
        opacity: 1.0;
      }
    }
    input{
      position: relative;
      z-index:3;
    }
  }
</style>