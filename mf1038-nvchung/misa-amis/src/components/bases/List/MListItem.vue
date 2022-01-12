<template>
  <li
    :class="['list-item', active && 'active', hovered && 'hovered']"
    @click="$emit('click')"
  >
    <slot />
  </li>
</template>
<script>
export default {
  props: {
    /**
     * trang thai active
     */
    active: Boolean,
    /**
     * trang thai hovered
     */
    hovered: Boolean,
  },
  emits: ["click"],
};
</script>
<style scoped src="../../../styles/components/bases/list.css"></style>
