<template>
  <div class="table-container">
    <table class="table">
      <thead>
        <th class="th-check-all" v-if="showCheck">
          <m-check-box @change="checkAll($event)" :checked="checkAllChecked" />
        </th>
        <th
          v-for="{ text, classes, propName, width } in computedHeaders"
          :key="propName"
          :class="classes"
          :style="{ minWidth: `${width}px` }"
        >
          {{ text }}
        </th>
        <th v-if="showActions" class="td-actions">chức năng</th>
      </thead>
      <tbody>
        <tr v-for="item in computedItems" :key="item[keyName]">
          <td class="td-check" v-if="showCheck">
            <m-check-box v-model="selectedValues" :value="item[keyName]" />
          </td>
          <td
            v-for="{ propName, classes, width } in computedHeaders"
            :key="propName"
            :class="classes"
            :style="{ minWidth: `${width}px` }"
          >
            {{ item[propName] }}
          </td>
          <td v-if="showActions" class="td-actions">
            <div class="actions">
              <slot name="actions" :item="item" />
            </div>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
<script>
import MCheckBox from "./MCheckBox.vue";
export default {
  components: {
    MCheckBox,
  },
  props: {
    /**
     * header quy dinh cot nao duoc hien thi
     */
    headers: {
      type: Array,
      required: true,
    },
    /**
     * du lieu hien thi
     */
    items: {
      type: Array,
      default: () => [],
    },
    /**
     * khoa chinh cua item
     */
    keyName: {
      type: String,
      required: true,
    },
    /**
     * cac gia tri cua keyName duoc checked
     * @model
     */
    modelValue: {
      type: Array,
      default: () => [],
    },
  },
  emits: ["update:modelValue"],
  methods: {
    //xu ly su kien check all item
    checkAll(evt) {
      if (evt.target.checked) {
        this.selectedValues = this.items.map((item) => item[this.keyName]);
      } else {
        this.selectedValues = [];
      }
    },
  },
  computed: {
    //cac gia tri duoc chon (boi checkbox)
    selectedValues: {
      get() {
        return this.modelValue;
      },
      set(val) {
        this.$emit("update:modelValue", val);
      },
    },
    //header sau khi ap dung them format
    computedHeaders() {
      return this.headers.map((header) => {
        const newHeader = { ...header };
        switch (header.format) {
          case "date":
            newHeader.classes = "text-center";
            break;
          case "currency":
            newHeader.classes = "text-right";
            break;
        }
        return newHeader;
      });
    },
    //items sau khi format (or loc?)
    computedItems() {
      return this.items.map((item) => {
        const newItem = { ...item };
        for (const { propName, format } of this.headers) {
          switch (format) {
            case "date":
              newItem[propName] = this.$formatters.formatDate(
                newItem[propName]
              );
              break;
          }
        }
        return newItem;
      });
    },
    //kiem tra tat ca checkbox checked
    checkAllChecked() {
      if (this.modelValue.length == this.items.length) return true;
      return false;
    },
    //co show cot checkbox ko
    showCheck() {
      return this.items.length > 0 && this.headers.length > 0;
    },
    //co show cot actions ko
    showActions() {
      return !!this.$slots.actions && this.headers.length > 0;
    },
  },
};
</script>
<style scoped src="../../styles/components/bases/table.css"></style>
