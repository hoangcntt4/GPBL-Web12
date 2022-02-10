<template>
  <side-bar :mini="sideBarMini" :onToggleSideBar="toggleSideBar" />
  <top-header :sideBarMini="sideBarMini" :onToggleSideBar="toggleSideBar" />
  <main :class="['main', sideBarMini && 'main--sidebar-mini']">
    <slot />
  </main>
</template>
<script>
import SideBar from "./SideBar.vue";
import TopHeader from "./TopHeader.vue";
export default {
  components: {
    SideBar,
    TopHeader,
  },
  data() {
    return {
      sideBarMini: false,
    };
  },
  methods: {
    toggleSideBar() {
      this.sideBarMini = !this.sideBarMini;
    },
  },
};
</script>
<style>
@import url(../../styles/components/layouts/layout.css);
</style>
