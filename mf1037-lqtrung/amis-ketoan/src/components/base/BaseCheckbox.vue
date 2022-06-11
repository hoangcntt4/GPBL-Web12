<template>
  <label class="m-checkbox">
    <input  type="checkbox" :value="inputValue" v-model="model" />
    <span>
      <slot />
    </span>
  </label>
</template>
<script>
export default {
  props: {
    value:{
        type: Array
    },
    inputValue: {
        type: String
    }
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
<style scoped src="../../assets/css/base/checkbox.css"></style>
