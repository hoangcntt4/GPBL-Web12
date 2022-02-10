<template>
  <button :class="classes">
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
    pill: Boolean,
    outlined: Boolean,
  },
  computed: {
    classes() {
      return [
        `btn btn--${this.variant} btn--${this.color} btn--${this.size}`,
        this.pill && "btn--pill",
        this.outlined && "btn--outlined",
      ];
    },
  },
};
</script>
<style scoped src="../../styles/components/bases/button.css"></style>
