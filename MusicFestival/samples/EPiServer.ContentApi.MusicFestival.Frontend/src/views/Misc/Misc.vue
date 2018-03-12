<template>
  <div v-if="loaded" class="miscPage">
    <HeroImage :hero="heroImage" />
    <div class="top gutter">
      <h1>{{title}}</h1>
    </div>
    <div v-html="mainBody"></div>
  </div>
</template>

<script>
  import api from '@/api'
  import EventBus from '@/eventBus'
  import HeroImage from '@/components/HeroImage'

  export default {
    name: 'misc',
    data () {
      return {
        loaded: false,
        heroImage: {},
        title: {},
        mainBody: {}
      }
    },
    watch: {
      '$route': 'updateData'
    },
    created () {
      this.updateData()
      EventBus.$on('updateDataMisc', this.updateData)
    },
    destroyed () {
      EventBus.$off('updateDataMisc')
    },
    methods: {
      updateData () {
        if (!this.$route.params.id) {
          console.log('UNDEFINED!')
        }
        api.getContent(this.$route.params.id, 'HeroContentArea,MainContant', response => {
          this.heroImage = response.data.HeroContentArea.ExpandedValue[0]
          this.title = response.data.Title.Value
          this.mainBody = response.data.MainBody.Value

          this.loaded = true
        })
      }
    },
    components: {
      HeroImage
    }
  }
</script>

<style lang="less" scoped>
  .top{margin-top:-120px;}
</style>