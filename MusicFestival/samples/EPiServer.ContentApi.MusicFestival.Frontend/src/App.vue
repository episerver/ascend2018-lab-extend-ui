<template>
  <div id="app">
    <MenuPage></MenuPage>
    <div id="page">
      <router-link to="/">
        <div class="logo" :class="{ 'login-is-loaded': loginLoaded }">
          <svg><use xlink:href="#logo"></use></svg>
        </div>
      </router-link>
      <div class="account" :class="{ 'login-is-loaded': loginLoaded }">
        <router-link to="/login">
          <svg><use xlink:href="#account"></use></svg>
        </router-link>
      </div>
      <router-view></router-view>
    </div>
    <div id="contextual">
    </div>
  </div>
</template>

<script>
import MenuPage from '@/components/MenuPage.vue'
import EventBus from '@/eventBus'

export default {
  name: 'app',
  data () {
    return {
      loginLoaded: false
    }
  },
  created () {
    EventBus.$on('loginLoaded', source => {
      this.loginLoaded = source
    })
  },
  components: {
    MenuPage
  }
}
</script>

<style lang="less">
  @import './assets/styles/common/_variables.less';

  body, #app{
    width:100vw;
    min-height:100vh;
    overflow-x:hidden;
  }

  #page{
    position: relative;
    top: 0;
    left: 0;
    width: 100vw;
    min-height:100vh;
    background-color: @backgroundColor;
    background-image: @backgroundGradient;
    background-attachment: fixed;
    transition: @menuTransition, opacity .4s ease-in-out;
    opacity: 1;
  }
  .menu.open + #page{
    left: 80vw;
    opacity: .3;
  }
  .logo{
    position: fixed;
    top: 14px;
    left: 50%;
    transform: translateX(-50%);
    width:40px;
    height: 36px;
    z-index: 10;
    transition: all .4s ease-in-out;

    &.login-is-loaded{
      width: 95px;
      height: 85px;
      top: 100px;
    }
  }
  .menu.open + #page .logo{ left: 130%; }
  .account{
    position: fixed;
    top: 12px;
    right: 22px;
    width: 32px;
    height: 32px;
    padding: 5px 0 12px;
    z-index: 10;
    transition: all .4s ease-in-out;
    background-color: rgba(236, 64, 112, .0);
    border-radius: 20px;

    &.login-is-loaded {
      background-color: rgba(236, 64, 112, .24);
    }
  }
  .menu.open + #page .account{ right: -50%; }
</style>