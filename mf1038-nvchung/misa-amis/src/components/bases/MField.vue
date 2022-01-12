<template>
  <div class="field">
    <label class="field__label">
      <span class="field__label-title">{{ label }}</span>
      <span class="field__label-required" v-if="required">*</span>
    </label>
    <slot />
  </div>
</template>
<script>
export default {
  props: {
    /**
     * label cua input
     */
    label: String,
    /**
     * truong bat buoc
     */
    required: Boolean,
  },
};
</script>
<style scoped src="../../styles/components/bases/field.css"></style>
