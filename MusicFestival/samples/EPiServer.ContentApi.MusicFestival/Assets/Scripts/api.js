import axios from 'axios'

export default {
    apiUrl: '/api/',

    get(url, language, parameters, data) {
        return this.callApi('get', url, language, parameters, data)
    },

    callApi(method, url, language, parameters, data) {
        return axios({
            method: method,
            baseURL: this.apiUrl,
            url: url,
            data: data,
            responseType: 'json',
            params: parameters,
            headers: {
                'Accept-Language': language,
                'Cache-Control': 'no-cache'
            }
        })
        .catch(e => {
            console.error(e)
        })
    }
}