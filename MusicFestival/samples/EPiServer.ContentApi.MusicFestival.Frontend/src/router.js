import Vue from 'vue'
import Router from 'vue-router'

import Discover from '@/views/Discover/Discover'
import Schedule from '@/views/Schedule/Schedule'
import Artists from '@/views/Artists/Artists'
import Artist from '@/views/Artist/Artist'
import Misc from '@/views/Misc/Misc'
import Login from '@/views/Login/Login'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'discover',
      component: Discover
    },
    {
      path: '/login',
      component: Login
    },
    {
      path: '/schedule',
      component: Schedule
    },
    {
      path: '/artists',
      component: Artists
    },
    {
      path: '/artists/:id',
      component: Artist
    },
    {
      path: '/misc/:id',
      component: Misc
    }
  ]
})
