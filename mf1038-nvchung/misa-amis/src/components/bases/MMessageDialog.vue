<template>
  <div class="message-dialog">
    <div class="message-dialog__inner">
      <div class="message-dialog__body">
        <m-icon :size="48" :icon="icon" />
        <span class="message-dialog__text">{{ text }}</span>
      </div>
      <hr class="message-dialog__line" />
      <div
        :class="[
          'message-dialog__footer',
          confirm && 'message-dialog__footer--confirm',
        ]"
      >
        <m-button v-if="confirm" color="primary" @click="$emit('yes')"
          >Có</m-button
        >
        <m-button v-if="confirm" color="secondary" @click="$emit('no')"
          >Không</m-button
        >
        <m-button v-if="info" color="primary" @click="$emit('close')"
          >Đóng</m-button
        >
      </div>
    </div>
  </div>
</template>
<script>
import MButton from "./MButton.vue";
import MIcon from "./MIcon.vue";
export default {
  components: { MButton, MIcon },
  props: {
    /**
     * ten icon
     */
    icon: String,
    /**
     * noi dung
     */
    text: String,
    /**
     * dang confirm
     */
    confirm: Boolean,
    /**
     * dang info
     */
    info: Boolean,
  },
  emits: ["yes", "no", "close"],
};
</script>
<style scoped src="../../styles/components/bases/message-dialog.css"></style>
