import '@/assets/styles/Main.less'

// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import router from '@/router'
import store from '@/store'
import App from '@/App'
import EventBus from '@/eventBus'

// generate svg sprite from all files in /assets/svg
const files = require.context('@/assets/svg', false, /.*\.svg$/)
files.keys().forEach(files)

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  store,
  template: '<App/>',
  components: { App },
  destroyed () {
    EventBus.$off('componentLoaded')
  }
})

// window.addEventListener('message', function (event) {
//   let eventArgs = event.data
//   if (eventArgs && eventArgs.id === 'beta/contentSaved') {
//     EventBus.$emit('contentSaved', eventArgs.data)
//   }
// }, false)

// setup event that emits after every route change.
router.afterEach((to, from) => {
  if (to !== from) {
    EventBus.$emit('componentLoaded')
    window.scrollTo(0, 0)
  }
})

Vue.filter('weekday', function (value) {
  let date = new Date(value)
  let days = []
  days['en'] = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday']
  days['sv'] = ['Söndag', 'Måndag', 'Tisdag', 'Onsdag', 'Torsdag', 'Fredag', 'Lördag']
  return days[store.getters.language][date.getDay()]
})

Vue.filter('shortTime', function (value) {
  let date = new Date(value)
  let hr = date.getHours()
  let min = date.getMinutes()
  if (min < 10) {
    min = '0' + min
  }

  let ampm = 'am'
  if (store.getters.language === 'en' && hr > 12) {
    hr -= 12
    ampm = 'pm'
  }
  return (store.getters.language === 'en') ? hr + ':' + min + ampm : hr + ':' + min
})
