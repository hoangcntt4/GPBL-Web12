<template>
  <div ref="dropdown" class="dropdown">
    <div class="dropdown__trigger" @click.stop="toggle">
      <slot name="trigger" :active="active" />
    </div>
    <teleport to="body" v-if="active && appendToBody">
      <div role="menu" :style="styles">
        <slot />
      </div>
    </teleport>
    <div class="dropdown__content" v-else-if="active">
      <slot />
    </div>
  </div>
</template>
<script>
export default {
  props: {
    /**
     * dong dropdown khi click
     */
    closeOnClick: {
      type: Boolean,
      default: true,
    },
    /**
     * teleport den body
     */
    appendToBody: Boolean,
  },
  methods: {
    //toggle an/hien dropdown
    toggle() {
      this.active = !this.active;
      if (this.active && this.appendToBody) {
        //tinh toan vi tri hien thi trong truong hop teleport den body
        const { right, bottom, width } =
          this.$refs.dropdown.getBoundingClientRect();
        //hien thi ve phia ben trai duoi so voi mep phai cua element dropdown
        this.styles = {
          left: `${right}px`,
          top: `${bottom}px`,
          minWidth: `${width}px`,
          position: "absolute",
          transform: "translateX(-100%)",
        };
      }
    },
    handleClickOutside(evt) {
      const target = evt.target;
      const isClickOutside = !(
        this.$refs.dropdown == target || this.$refs.dropdown.contains(target)
      );
      if (isClickOutside || this.closeOnClick) {
        this.active = false;
      }
    },
  },
  data() {
    return {
      active: false,
      styles: {},
    };
  },
  created() {
    document.addEventListener("click", this.handleClickOutside);
  },
  unmounted() {
    document.removeEventListener("click", this.handleClickOutside);
  },
};
</script>

<style>
@import url(../../styles/components/bases/dropdown.css);
</style>
