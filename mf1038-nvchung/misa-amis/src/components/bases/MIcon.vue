<template>
  <div
    :class="[`icon icon-${size} icon-${icon}`, anim && `animation-${anim}`]"
    @click="$emit('click')"
  ></div>
</template>
<script>
export default {
  emits: ["click"],
  props: {
    /**
     * kich thuoc icon
     */
    size: {
      type: Number,
      default: 24,
    },
    /**
     * ten icon
     */
    icon: {
      type: String,
      required: true,
    },
    /**
     * hieu ung
     */
    anim: String,
  },
};
</script>
<style scoped src="../../styles/components/bases/icon.css"></style>
