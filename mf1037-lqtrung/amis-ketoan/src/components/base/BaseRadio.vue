<template>
  <div class="m-radio">
    <input
      type="radio"
      name="radio-input"
      :value="option"
      :id="option"
      v-model="model"
    />
    <span>
      <slot />
    </span>
  </div>
</template>
<script>
export default {
  props: {
    value:{
      type:[String,Number]
    },
    option: {
      required: true,
      type: [String,Number]
    },
  },
  computed: {
    model: {
      get() {
        return this.value;
      },
      set(value) {
        this.$emit("input", value);
      },
    },
  },
};
</script>
<style scoped src="../../assets/css/base/radio.css"></style>
