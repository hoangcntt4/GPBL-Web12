<template>
  <div id="app">
    <div id="navbar">
      <the-navbar/>
    </div>
    <div class="wrap">
        <div id="header">
          <the-header/>
        </div>
        <div id="content">
          <router-view/>
        </div>
    </div>
  </div>
</template>

<script>
import TheHeader from './components/layout/TheHeader.vue'
import TheNavbar from './components/layout/TheNavbar.vue'
export default {
  components: { TheHeader, TheNavbar },
  
}
</script>
<style>
@import url('./assets/css/main.css');
#app{
  display: flex;
}
#navbar{
  width: auto;
}
.wrap{
  width: 100%;
}
#content{
  background-color: #EAECEF;
}
.mi-icon{
    width: 24px;
    height: 24px;
    min-width: 24px;
    min-height: 24px;
    background: url('./assets/img/Sprites.svg') no-repeat;
    cursor: pointer;
}
</style>
