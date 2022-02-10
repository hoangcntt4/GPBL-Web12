<template>
  <div class="radio">
    <input type="radio" v-bind="$attrs" v-model="model" />
    <span>
      <slot />
    </span>
  </div>
</template>
<script>
export default {
  inheritAttrs: false,
  props: {
    /**
     * @model
     */
    modelValue: [String, Number],
  },
  emits: ["update:modelValue"],
  computed: {
    model: {
      get() {
        return this.modelValue;
      },
      set(val) {
        this.$emit("update:modelValue", val);
      },
    },
  },
};
</script>
<style scoped src="../../styles/components/bases/radio.css"></style>
