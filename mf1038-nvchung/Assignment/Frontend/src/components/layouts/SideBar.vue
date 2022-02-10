<template>
  <div :class="['sidebar', mini && 'sidebar--mini']">
    <div class="sidebar__header">
      <a href="#" class="sidebar__options">
        <m-icon icon="toggle" />
      </a>
      <a href="#" class="sidebar__logo">
        <img src="@/assets/imgs/logo.svg" alt="logo" />
      </a>
      <div class="sidebar__bars">
        <m-icon icon="bars" @click="onToggleSideBar" />
      </div>
    </div>
    <ul class="sidebar__menu">
      <li v-for="({ icon, text }, index) in items" :key="index">
        <a href="#" class="sidebar__menu-item">
          <div class="sidebar__menu-item-icon">
            <m-icon :icon="icon" />
          </div>
          <div class="sidebar__menu-item-text">{{ text }}</div>
        </a>
      </li>
    </ul>
  </div>
</template>
<script>
import MIcon from "../bases/MIcon.vue";
export default {
  components: {
    MIcon,
  },
  props: {
    mini: {
      type: Boolean,
      default: false,
    },
    onToggleSideBar: {
      type: Function,
      required: true,
    },
  },
  data() {
    return {
      items: [
        { icon: "dashboard", text: "Tổng quan" },
        { icon: "cash", text: "Tiền mặt" },
        { icon: "bank", text: "Tiền gửi", active: true },
        { icon: "bag", text: "Mua hàng" },
        { icon: "sale", text: "Bán hàng" },
        { icon: "invoice", text: "Quản lý hóa đơn" },
        { icon: "stock", text: "Kho" },
        { icon: "tools", text: "Công cụ dụng cụ" },
        { icon: "car", text: "Tài sản cố định" },
        { icon: "tax", text: "Thuế" },
        { icon: "price", text: "Giá thành" },
        { icon: "general", text: "Tổng hợp" },
        { icon: "budge", text: "Ngân sách" },
        { icon: "report", text: "Báo cáo" },
        { icon: "finance", text: "Phân tích tài chính" },
      ],
    };
  },
};
</script>
<style scoped src="../../styles/components/layouts/sidebar.css"></style>
