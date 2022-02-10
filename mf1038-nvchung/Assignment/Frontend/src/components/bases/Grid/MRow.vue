<template>
  <div class="row" :style="style">
    <slot />
  </div>
</template>
<script>
export default {
  props: {
    /**
     * khoang cach giua cac cot
     */
    gx: {
      type: Number,
      default: 0,
    },
    /**
     * khoang cach giua cac dong
     */
    gy: {
      type: Number,
      default: 0,
    },
  },
  computed: {
    style() {
      return {
        "--gx": `${this.gx}px`,
        "--gy": `${this.gy}px`,
      };
    },
  },
};
</script>
<style scoped src="../../../styles/components/bases/grid.css"></style>
