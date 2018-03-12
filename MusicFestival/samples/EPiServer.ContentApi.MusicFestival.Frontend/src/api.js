import axios from 'axios'
import store from '@/store'
import qs from 'qs'

export default {
  apiUrl: 'http://localhost:56312/api/episerver/',
  updatingToken: false,
  callQueue: [],
  get (url, callback) {
    this.callApi('get', url, callback)
  },
  getContent (pageId, expand, callback) {
    let url = 'content/' + pageId
    if (expand && typeof expand === 'string') {
      url += '?expand=' + expand
    } else {
      callback = expand
    }
    this.callApi('get', url, callback)
  },
  getChildren (pageId, expand, callback) {
    let url = 'content/' + pageId + '/children'
    if (expand && typeof expand === 'string') {
      url += '?expand=' + expand
    } else {
      callback = expand
    }
    this.callApi('get', url, callback)
  },
  search (query, callback) {
    this.callApi('get', 'search/content/?filter=' + query, callback)
  },
  post (url, body, callback) {
    axios.post(this.apiUrl + url, qs.stringify(body), {
      'Accept-Language': store.getters.language
    })
    .then(response => {
      callback(response)
    })
  },
  callStack () {
    setTimeout(() => {
      for (let args in this.callQueue) {
        this.call.apply(this, this.callQueue[args])
      }
      this.callQueue = []
      this.updatingToken = false
    }, 1)
  },
  callApi (method, url, data, callback) { // we want to check if we are logged in, and get a refresh token if it's time for that.
    if (store.getters.accessToken.length) { // we at least were logged in
      // we only want this to happen for the first call, so queue them all and execute at the end.
      this.callQueue.push([...arguments])

      if (this.updatingToken !== true) {
        console.log('we have access token.')
        this.updatingToken = true

        let now = new Date()
        let expires = new Date(store.getters.tokenExpire)

        if (now >= expires) { // it's after the token expired...
          console.log('EXPIRED TOKEN! We need a new one.')
          this.post('auth/token', {
            'grant_type': 'refresh_token',
            'refresh_token': store.getters.refreshToken,
            'client_id': 'Default'
          },
          response => {
            store.commit('setAccessTokens', response.data)
            this.callStack()
          })
        } else {
          console.log('no new token needed...')
          this.callStack()
        }
      }
    } else { // not logged in, so just send the call directly.
      this.call(method, url, data, callback)
    }
  },
  call (method, url, data, callback) {
    if (url === 'undefined') {
      console.log('UNDEFINED!')
      console.trace()
    }
    if (typeof data === 'function') {
      callback = data
    }

    axios({
      method: method,
      baseURL: this.apiUrl,
      url: url,
      data: data,
      headers: {
        'Accept-Language': store.getters.language,
        'Authorization': 'bearer ' + store.getters.accessToken
      }
    })
    .then(response => {
      console.log('requesting data, response gotten for \'' + url + '\':', response)
      callback(response)
    })
    .catch(e => {
      console.error('error from axios', e)
    })
  }
}
