<template>
  <div class="m-dialog">
    <div class="m-dialog-inner">
      <div class="m-dialog-body">
        <base-icon :size="48" :icon="icon" />
        <span class="m-dialog-text">{{ text }}</span>
      </div>
      <div class="hr-line"></div>
      <div
        :class="[
          'm-dialog-footer',
          confirm && 'm-dialog-footer-confirm',
          ok && 'm-dialog-footer-ok',
          change && 'm-dialog-footer-change',
        ]"
      >
        <base-button
          v-if="change"
          color="secondary"
          size="sm"
          @click="$emit('cancel')"
          >Hủy</base-button
        >

        <base-button
          class="btn-no"
          v-if="confirm"
          color="secondary"
          size="sm"
          @click="$emit('no')"
          >Không</base-button
        >
        <base-button
          v-if="confirm"
          color="primary"
          size="sm"
          @click="$emit('yes')"
          >Có</base-button
        >

        <base-button
          v-if="notic"
          color="primary"
          size="sm"
          @click="$emit('close')"
          >Đóng</base-button
        >
        <base-button v-if="ok" color="primary" size="sm" @click="$emit('close')"
          >Đồng ý</base-button
        >
      </div>
    </div>
  </div>
</template>
<script>
import BaseButton from "./BaseButton.vue";
import BaseIcon from "./BaseIcon.vue";
export default {
  components: { BaseButton, BaseIcon },
  props: {
    /**
     * tên icon
     */
    icon: String,
    /**
     * message
     */
    text: String,
    /**
     * dạng yes - no
     */
    confirm: Boolean,
    /**
     * dạng thông báo button Đóng
     */
    notic: Boolean,
    /**
     * dạng thông báo button Đồng ý
     */
    ok: Boolean,
    /**
     * thông báo thay đổi
     */
    change: Boolean,
  },
  emits: ["yes", "no", "close"],
};
</script>
<style scoped src="../../assets/css/base/dialog.css"></style>
