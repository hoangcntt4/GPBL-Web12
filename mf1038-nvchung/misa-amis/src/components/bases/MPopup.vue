<template>
  <div class="popup" :style="`--mw: ${maxW}px`">
    <div class="popup-inner">
      <div class="popup-header" v-if="showHeader">
        <div class="popup-title">
          <slot name="title" />
        </div>
        <div class="popup-icons">
          <m-icon icon="close" @click="$emit('close')" />
          <m-icon icon="help" />
        </div>
      </div>
      <div class="popup-content">
        <slot />
      </div>
      <div class="popup__divider"></div>
      <div class="popup-footer">
        <slot name="footer" />
      </div>
    </div>
  </div>
</template>
<script>
import MIcon from "./MIcon.vue";
export default {
  components: {
    MIcon,
  },
  emits: ["close"],
  props: {
    /**
     * max width popup
     */
    maxW: {
      type: Number,
      default: 900,
    },
    /**
     * co hien header ko
     */
    showHeader: {
      type: Boolean,
      default: true,
    },
  },
};
</script>
<style scoped src="../../styles/components/bases/popup.css"></style>
