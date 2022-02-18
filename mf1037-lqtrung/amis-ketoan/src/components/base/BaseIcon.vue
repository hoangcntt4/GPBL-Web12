<template>
  <div
    :class="`m-icon mi-${size} mi-${icon}`"
    @click="$emit('click')"
  ></div>
</template>
<script>
export default {
  emits: ["click"],
  props: {
    /**
     * kích thước
     */
    size: {
      type: Number,
      default: 24,
    },
    /**
     * tên
     */
    icon: {
      type: String,
      required: true,
    },
  },
};
</script>
<style scoped src="../../assets/css/base/icon.css"></style>
