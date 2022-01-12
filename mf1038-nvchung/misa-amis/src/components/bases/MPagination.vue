<template>
  <ul class="page-list">
    <li
      @click="prevPage"
      :class="['page-item', page == 1 && 'page-item--disabled']"
    >
      Trước
    </li>
    <li
      :class="[
        'page-item',
        p == page && 'page-item--active',
        p == '...' && 'page-item--dot',
      ]"
      v-for="(p, index) in pages"
      :key="index"
      @click="setPage(p)"
    >
      {{ p }}
    </li>
    <li
      @click="nextPage"
      :class="['page-item', page == pageCount && 'page-item--disabled']"
    >
      Sau
    </li>
  </ul>
</template>
<script>
export default {
  props: {
    /**
     * tong so item
     */
    itemCount: Number,
    /**
     * item/trang
     */
    perPage: Number,
    /**
     * so trang toi da hien thi
     */
    maxDisplayPage: {
      type: Number,
      default: 3,
    },
    /**
     * trang hien tai
     */
    page: {
      type: Number,
      default: 1,
    },
  },
  emits: ["pageChange"],
  methods: {
    /**
     * nhay den page p
     * @param {Number} p
     */
    setPage(p) {
      this.$emit("pageChange", p);
    },
    // nhay den page ke tiep
    nextPage() {
      this.setPage(Math.min(this.page + 1, this.pageCount));
    },
    //nhay den page truoc do
    prevPage() {
      this.setPage(Math.max(this.page - 1, 1));
    },
  },
  computed: {
    //get so trang
    pageCount() {
      return Math.ceil(this.itemCount / this.perPage);
    },
    //tinh toan mang page de hien thi
    pages() {
      let sp = Math.max(1, this.page - ~~(this.maxDisplayPage / 2)); //trang bat dau hien thi
      let ep = Math.min(this.pageCount, sp + this.maxDisplayPage - 1); //trang ket thuc hien thi
      if (ep - sp + 1 < this.maxDisplayPage) {
        //neu so trang hien thi chua du maxDisplayPage
        if (sp == 1) {
          ep = Math.min(this.pageCount, sp + this.maxDisplayPage - 1);
        } else if (ep == this.pageCount) {
          sp = Math.max(1, ep - this.maxDisplayPage + 1);
        }
      }
      const pages = Array.from({ length: ep - sp + 1 }, (_, i) => sp + i); //tao ra mang pages [sp..ep]
      if (sp > 2) {
        //them ... neu sp>2
        pages.unshift("...");
      }
      if (sp > 1) {
        //them 1 neu sp>1
        pages.unshift(1);
      }
      if (ep < this.pageCount - 1) {
        //them ... vao cuoi neu ep<pagecount-1
        pages.push("...");
      }
      if (ep < this.pageCount) {
        //them trang cuoi cung neu ep<pagecount
        pages.push(this.pageCount);
      }
      return pages;
    },
  },
  watch: {
    //so item/trang thay doi
    perPage() {
      this.setPage(1);
    },
  },
};
</script>
<style>
@import url(../../styles/components/bases/pagination.css);
</style>
