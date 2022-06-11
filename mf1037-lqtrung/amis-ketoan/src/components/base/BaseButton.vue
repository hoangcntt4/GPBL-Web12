<template>
  <button
    :class="`m-button m-btn-${color} m-btn-${size}`"
    @click="$emit('click')"
  >
    <slot />
  </button>
</template>
<script>
export default {
  emits: ["click"],
  props: {
    /**
     * màu button
     * @values  primary, secondary, high
     */
    color: {
      type: String,
      default: "primary",
    },
    /**
     * kích thước button
     * @values sm, md
     */
    size: {
      type: String,
      default: "md",
    },
  },
};
</script>
<style scoped src="../../assets/css/base/button.css"></style>
