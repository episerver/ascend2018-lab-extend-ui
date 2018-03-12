<template>
  <div class="menu" :class="{open: open}" v-if="menuAvailable">
    <div class="hamburger" @click="toggle"></div>
    <ul class="main">
      <li>
        <router-link to="/">{{ translate('discover') }}</router-link>
      </li>
      <li>
        <router-link to="/schedule">{{ translate('schedule') }}</router-link>
      </li>
      <li>
        <router-link to="/artists">{{ translate('artists') }}</router-link>
      </li>
      <li v-for="item in items" :key="item.Title.Value">
        <router-link :to="'/misc/' + item.ContentLink.Id">{{item.Title.Value}}</router-link>
      </li>
    </ul>
    <ul class="lang">
      <li v-for="item in languages" :key="item.Name" :class="{ active: isCurrentLanguage(item) }" @click.stop.prevent="switchLang(item)">
        <a :href="item.Name">{{item.DisplayName}}</a>
      </li>
    </ul>
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
  name: 'menu',
  mixins: [mixins],
  data () {
    return {
      open: false,
      items: [],
      languages: []
    }
  },
  created () {
    this.updateData()
    EventBus.$on('componentLoaded', source => { this.close() })
    EventBus.$on('updateDataMenu', this.updateData)
  },
  destroyed () {
    EventBus.$off('componentLoaded')
    EventBus.$off('updateDataMenu')
  },
  computed: {
    menuAvailable () {
      return this.$store.getters.menuAvailable
    }
  },
  methods: {
    updateData () {
      api.getContent('7', 'MenuItems', response => {
        this.items = response.data.MenuItems.ExpandedValue
      })

      api.get('site', response => {
        this.languages = response.data[0].Languages
      })
    },
    toggle () {
      this.open = !this.open
    },
    close () {
      this.open = false
    },
    isCurrentLanguage (item) {
      return item.Name === this.$store.getters.language
    },
    switchLang (item) {
      this.$store.commit('switchLanguage', item.Name)
      this.updateAllData()
    }
  }
}
</script>

<style lang="less" scoped>
  @import '../assets/styles/common/_variables.less';
  div.menu{
    position:fixed;
    top:0;
    padding:65px 0 0 24px;
    left:-85vw;
    width:80vw;
    min-height:100vh;
    background-color:@backgroundColor;
    background-image: @backgroundGradient;
    transition: @menuTransition;
    z-index:10;
    box-shadow: 0 0 50px rgba(0, 0, 0, .7);
    &.open{
      left:0px;
    }
  }
  .hamburger{
    position:fixed;
    top: 10px;
    left: 22px;
    width:36px;
    height:36px;
    background-color:rgba(236,64,122,0);
    border-radius: 50px;
    transition: background-color .4s ease-in-out;
    .open &{
      background-color:rgba(236,64,122,.24);

      &:before{transform: translate(-50%, 0px) rotate(45deg);}
      &:after{transform: translate(-50%, 0px) rotate(-45deg);}
    }
    &:before, &:after{
      content:'';
      display:block;
      position:absolute;
      top: 16px;
      left: 50%;
      width: 16px;
      height: 4px;
      background: #fff;
      transform: translate(-50%, -4px) rotate(0deg);
      transition: transform .3s ease-in-out;
    }
    &:after{
      transform: translate(-50%, 4px) rotate(0deg);
    }
  }
  ul{
    list-style:none;
    margin:0;
    padding:0;
    li{
      a{
        display:block;
        font: 36px/56px @fontHeading;
        text-transform: lowercase;
      }
    }

    &.main{
      min-height:55vh;
      margin-bottom:50px;
    }

    &.lang{
      li{
        display:inline-block;
        position:relative;
        margin-right:30px;

        &.active:after{
          content:'';
          display:block;
          position: absolute;
          width: 100%;
          height: 4px;
          background: @colorTurqoise;
          top: 17px;
        }

        a{
          font: 12px @fontSubHeading;
          text-transform:uppercase;
        }
      }
    }
  }
  .waves{
    position:absolute;
    bottom:0;
    left:0;
    width:80vw;
    padding-bottom:50%;
    z-index:-1;
    svg{
      position: absolute;
      bottom:0;
    }
  }
</style>
