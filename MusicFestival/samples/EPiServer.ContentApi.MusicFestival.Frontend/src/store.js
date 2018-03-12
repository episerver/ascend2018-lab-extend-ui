import Vue from 'vue'
import Vuex from 'vuex'
import createPersistedState from 'vuex-persistedstate'
import translations from '@/translations'

Vue.use(Vuex)

export default new Vuex.Store({
  plugins: [createPersistedState()],
  state: {
    menuAvailable: true,
    language: 'en',
    accessToken: '',
    refreshToken: '',
    tokenExpire: '',
    favs: {} // to be filled by fav functionality
  },
  getters: {
    menuAvailable: state => state.menuAvailable,
    language: state => state.language,
    favs: state => state.favs,
    accessToken: state => state.accessToken,
    refreshToken: state => state.refreshToken,
    tokenExpire: state => state.tokenExpire
  },
  mutations: {
    showMenu (state, payload) {
      state.menuAvailable = payload
    },
    switchLanguage (state, payload) {
      state.language = payload
    },
    toggleFav (state, payload) {
      Vue.set(state.favs, payload.artist.ContentLink.Id, !state.favs[payload.artist.ContentLink.Id])
    },
    setAccessTokens (state, payload) { // set all tokens and expiry date from the login call
      console.log(state, payload)
      state.accessToken = payload['access_token']
      state.refreshToken = payload['refresh_token']
      state.tokenExpire = payload['.expires']
    }
  },
  actions: {

  },
  modules: {
    localization: {
      namespaced: true,
      state: {
        translations: translations
      },
      getters: {
        translation: state => state.translations
      }
    }
  }
})
