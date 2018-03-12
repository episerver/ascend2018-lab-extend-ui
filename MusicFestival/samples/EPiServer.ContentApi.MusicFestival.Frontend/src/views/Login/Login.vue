<template>
  <div class="login">
    <div class="backButton" @click="$router.go(-1)">
      <svg><use xlink:href="#back"></use></svg>
    </div>

    <div class="form gutter" v-if="showLogin">
      <input type="email" v-model="username" placeholder="Email" />
      <input type="password" v-model="password" :placeholder="translate('password')" />
      <button type="button" :class="{ 'animation': readyToSend }" @click.stop.prevent="doLogin">{{translate('login')}}</button>
    </div>
    <div class="logout" v-if="!showLogin">
      <button type="button" @click.stop.prevent="doLogout">{{translate('logout')}}</button>
    </div>
    <div class="waves">
      <svg><use xlink:href="#waves"></use></svg>
    </div>
  </div>
</template>

<script>
import api from '@/api'
import mixins from '@/mixins'
import EventBus from '@/eventBus'

export default {
  name: 'Login',
  mixins: [mixins],
  data () {
    return {
      username: '',
      password: ''
    }
  },
  computed: {
    showLogin () {
      return !this.$store.getters.accessToken.length
    },
    readyToSend () {
      return !!this.username.length && !!this.password.length
    }
  },
  created () {
    // turn off the menu to show back button instead
    this.showMenu(false)
    EventBus.$emit('loginLoaded', true)
  },
  beforeDestroy () {
    this.showMenu(true)
    EventBus.$emit('loginLoaded', false)
  },
  methods: {
    showMenu (state) {
      this.$store.commit('showMenu', state)
    },
    doLogin () {
      api.post('auth/token', {
        'grant_type': 'password',
        username: this.username,
        password: this.password,
        'client_id': 'Default'
      }, response => {
        this.$store.commit('setAccessTokens', response.data)
        this.updateAllData()
        this.$router.push({name: 'discover'})
      })
    },
    doLogout () {
      this.$store.commit('setAccessTokens', {
        'access_token': '',
        'refresh_token': '',
        '.expires': ''
      })
      this.updateAllData()
      this.$router.push({name: 'discover'})
    }
  }
}
</script>

<style lang="less" scoped>
  @import '../../assets/styles/common/_variables.less';
  .login{
    position: fixed;
    top: 0;
    left: 0;
    width: 100vw;
    height: 100vh;
    z-index: 9;
    background-color: @backgroundColor;
    background-image: @backgroundGradient;
    background-attachment: fixed;

    .backButton{
      position: fixed;
      top: 12px;
      z-index: 10;
      left: 24px;
      width: 32px;
      height: 32px;
    }

    .form, .logout{
      position: absolute;
      width: 100%;
      top: 240px;

      button{
        color: @colorBlue;
        width: 100%;
        text-transform: uppercase;
        border:0;
        height: 40px;
        font:12px @fontSubHeading;
        background: @colorTurqoise;
        border-radius:20px;
        padding:0 20px;
        display:block;
        margin-top:20px;
        transform: scale3d(0,0,0);

        &.animation {
          animation: rubberBand .6s 0s 1 linear;
          animation-fill-mode:forwards;
        }
      }
    }

    .logout button{
      width: 90%;
      margin-left:20px;
      transform: none!important;
    }

    .waves{
      position:absolute;
      bottom:0;
      left:0;
      width:100vw;
      padding-bottom:50%;
      svg{
        position: absolute;
        bottom:0;
      }
    }
  }

  @keyframes rubberBand {
  from {
    transform: scale3d(0, 0, 0);
  }

  30% {
    transform: scale3d(1.20, 0.75, 1);
  }

  40% {
    transform: scale3d(0.75, 1.20, 1);
  }

  50% {
    transform: scale3d(1.15, 0.85, 1);
  }

  65% {
    transform: scale3d(.95, 1.05, 1);
  }

  75% {
    transform: scale3d(1.05, .95, 1);
  }

  to {
    transform: scale3d(1, 1, 1);
  }
}
</style>
