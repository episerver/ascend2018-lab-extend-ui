import EventBus from '@/eventBus'

export default {
  methods: {
    translate (string) {
      return this.$store.getters['localization/translation'][string][this.$store.getters.language]
    },
    updateAllData () {
      EventBus.$emit('updateDataMenu')
      EventBus.$emit('updateDataDiscover')
      EventBus.$emit('updateDataSchedule')
      EventBus.$emit('updateDataArtists')
      EventBus.$emit('updateDataArtist')
      EventBus.$emit('updateDataMisc')
    }
  }
}
