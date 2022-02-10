<template>
  <employee />
</template>

<script>
import Employee from "./views/Employee.vue";
export default {
  name: "App",
  components: { Employee },
};
</script>

<style>
@import url(~@/styles/base.css);
</style>
