<template>
  <button :class="`btn btn--${variant} btn--${color} btn--${size}`">
    <slot />
  </button>
</template>
<script>
export default {
  props: {
    /**
     * dang button
     * @values contained, text, icon
     */
    variant: {
      type: String,
      default: "contained",
    },
    /**
     * mau cua button
     * @values primary, secondary
     */
    color: {
      type: String,
      default: "primary",
    },
    /**
     * kich thuoc button
     * @values sm, md
     */
    size: {
      type: String,
      default: "md",
    },
  },
};
</script>
<style scoped src="../../styles/components/bases/button.css"></style>
