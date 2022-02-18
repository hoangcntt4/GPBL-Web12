<template>
  <ValidationObserver>
    <ValidationProvider ref="provider" :rules="rule" v-slot="{ errors }">
      <div :class="['m-input', icon && 'm-input-icon']">
        <input
          :type="type"
          v-bind="$attrs"
          :value="value"
          @input="onInput"
          @blur.once="$emit('touch')"
          ref="input"
          :class="{ 'is-invalid': errors[0]}"
          :title="titleErr"
          :id="idField"
        />
        <base-icon class="m-icon-input" v-if="icon" :icon="icon" :size="16" />
      </div>
    </ValidationProvider>
  </ValidationObserver>
</template>
<script>
import BaseIcon from "./BaseIcon.vue";
export default {
  components: {
    BaseIcon,
  },
  props: {
    /**
     * tên icon
     */
    icon: {
      type: String,
      default: "",
    },
    /**
     * loại input
     */
    type: String,
    /**
     * validation
     */
    rule: String,
    /**
     * @model
     */
    value: [String, Number],
    /**
     * tự động focus
     */
    autoFocus: Boolean,
    /**
     * clear lỗi
     */
    isRemoveErr: {
      type: Boolean,
      default: false
    },
    /**
     * title lỗi khi hover
     */
    titleErr: String,
    /**
     * id của input
     */
    idField: String
  },
  watch:{
    //theo dõi sự thay đổi của isRemove để làm mới provider
    isRemoveErr(){
      if(this.isRemoveErr){
        this.$refs.provider.reset();
      }
    }
  },
  methods: {
    onInput(event) {
      this.$emit("input", event.target.value);
    },
  },
  mounted() {
    if (this.autoFocus) {
        this.$refs.input.focus();
    }
  },
};
</script>
<style scoped src="../../assets/css/base/input.css"></style>
