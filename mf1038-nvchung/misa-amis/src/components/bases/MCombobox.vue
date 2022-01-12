<template>
  <div
    :class="['dropdown combobox', error && 'error', top && 'dropdown--top']"
    ref="combobox"
  >
    <div class="combobox__main">
      <div class="combobox__input">
        <input
          ref="input"
          type="text"
          autocomplete="off"
          :value="search"
          @focus="handleFocus($event)"
          @keydown.up="handleKeyUp"
          @keydown.down="handleKeyDown"
          @input="handleInput"
          @keydown.enter="handleEnter"
          :title="error"
          :readonly="readonly"
          @blur.once="$emit('touch')"
        />
      </div>
      <div
        :class="['combobox__arrow', active && top && 'combobox__arrow--up']"
        @click="toggle"
      >
        <m-icon icon="arrow-down-black" :size="16" />
      </div>
    </div>
    <div class="dropdown__content" v-if="active">
      <m-list v-if="loading">
        <m-list-item> Loading... </m-list-item>
      </m-list>
      <div class="combobox__menu-table" v-else-if="tableMenu">
        <table>
          <thead>
            <th v-for="({ text }, index) in headers" :key="index">
              {{ text }}
            </th>
          </thead>
          <tbody v-if="hasItems()">
            <tr
              :class="{
                active: item == selected,
                hovered: hoveredIndex == index,
              }"
              v-for="(item, index) in filteredItems"
              :key="index"
              @click="setSelected(item)"
            >
              <td v-for="({ value }, index) in headers" :key="index">
                {{ item[value] }}
              </td>
            </tr>
          </tbody>
          <tbody v-else>
            <tr>
              <td :colspan="headers.length" class="combobox__no-result">
                Không có dữ liệu hiển thị
              </td>
            </tr>
          </tbody>
        </table>
      </div>
      <m-list v-else-if="hasItems()">
        <m-list-item
          v-for="(item, index) in filteredItems"
          :key="index"
          :active="item == selected"
          :hovered="index == hoveredIndex"
          @click="setSelected(item)"
          >{{ getLabel(item) }}
        </m-list-item>
      </m-list>
      <m-list v-else>
        <div class="combobox__no-result">Không có dữ liệu hiển thị</div>
      </m-list>
    </div>
  </div>
</template>
<script>
import MList from "./List/MList.vue";
import MListItem from "./List/MListItem.vue";
import MIcon from "./MIcon.vue";
export default {
  components: { MList, MListItem, MIcon },
  props: {
    /**
     * loi
     */
    error: String,
    /**
     * hien loading ko
     */
    loading: Boolean,
    /**
     * du lieu cua combobox
     */
    items: {
      type: Array,
      default: () => [],
    },
    /**
     * thuoc tinh duoc chon hien thi tren input
     */
    labelMember: String,
    /**
     * @model
     */
    modelValue: [String, Number],
    /**
     * ham loc du lieu
     */
    filterFn: Function,
    /**
     * du lieu hien thi dang table
     */
    tableMenu: Boolean,
    /**
     * headers cua table
     */
    headers: Array,
    /**
     * combobox chi doc
     */
    readonly: Boolean,
    /**
     * gia tri duoc chon khoi tao
     */
    initialSelected: Object,
    /**
     * hien phia tren
     */
    top: Boolean,
  },
  emits: ["update:modelValue", "select", "open", "close", "touch"],
  data() {
    return {
      active: false, //hien dropdown item
      selected: this.initialSelected, //item duoc chon
      hoveredIndex: -1, //con tro chon
      showAll: false, //hien tat ca item
      search: this.modelValue || "", //gia tri input
    };
  },
  computed: {
    filteredItems() {
      //items sau khi ap dung ham loc
      if (this.showAll || !this.search || this.readonly) {
        //neu showAll, ko co search hoac la readonly thi bo qua ham loc
        return this.items;
      }
      return this.items.filter((item) => this.filterFn(item, this.search));
    },
  },
  methods: {
    //toggle dong/mo dropdown
    toggle() {
      if (this.active) {
        this.showAll = false;
        this.closeDropdown();
      } else {
        this.showAll = true;
        this.openDropdown(true);
      }
    },
    //co item nao ko
    hasItems() {
      return this.filteredItems.length > 0;
    },
    //xu ly bam nut enter
    handleEnter() {
      if (
        this.hoveredIndex != -1 &&
        this.hoveredIndex < this.filteredItems.length
      ) {
        //neu con tro hop le
        this.setSelected(this.filteredItems[this.hoveredIndex]);
      } else {
        this.setSelected(null);
      }
    },
    //xu ly su kien nhap tren input
    handleInput(evt) {
      this.setSearch(evt.target.value); //set search
      this.setHoveredIndex(this.items); //update lai con tro chon
      if (!this.readonly) {
        this.setSelected(null); //set selected la null khi nhap lieu
      }
      if (!this.active) {
        this.openDropdown(); //mo dropdown
      }
      if (this.showAll) {
        this.showAll = false; //tat showall
      }
    },
    //ham cap nhat gia tri input
    setSearch(text) {
      this.search = text;
      this.$emit("update:modelValue", this.search); //emit update model
    },
    /**
     * ham mo dropdown
     * @param {Boolean} focus - co focus vao input ko
     */
    openDropdown(focus = false) {
      this.active = true;
      this.setHoveredIndex(this.items);
      this.$emit("open");
      focus && this.focusInput();
    },
    //ham dong dropdown
    closeDropdown() {
      this.active = false;
      this.hoveredIndex = -1;
      this.$emit("close");
      this.$nextTick(() => {
        //thao tac voi dom that phai nexttick de dong bo reactive data
        this.$refs.input.blur();
      });
    },
    //ham focus input
    focusInput() {
      this.$nextTick(() => {
        this.$refs.input.focus(); //focus input
        this.$refs.input.select(); //boi den chu trong input
      });
    },
    //ham lay text hien thi tren input
    getLabel(item) {
      return typeof item == "object" ? item?.[this.labelMember] : item;
    },
    //ham update gia tri duoc chon
    setSelected(item) {
      this.selected = item;
      this.$emit("select", this.selected); //emit event chon item
      if (item != null) {
        this.setSearch(this.getLabel(this.selected));
        this.closeDropdown();
      }
    },
    //ham xu ly su kien focus tren input
    handleFocus(evt) {
      evt.target.select();
    },
    //xu ly bam mui ten len
    handleKeyUp() {
      this.hoveredIndex = Math.max(0, this.hoveredIndex - 1);
    },
    //xu ly bam mui ten xuong
    handleKeyDown() {
      this.hoveredIndex = Math.min(
        this.items.length - 1,
        this.hoveredIndex + 1
      );
    },
    //xu ly kich ra ben ngoai
    handleClickOutside(evt) {
      const target = evt.target;
      const isClickOutside = !(
        this.$refs.combobox == target || this.$refs.combobox.contains(target)
      ); //neu ko phai kich vao combobox va con cua no
      if (isClickOutside) {
        this.closeDropdown();
      }
    },
    //cap nhat lai con tro
    setHoveredIndex(items) {
      this.hoveredIndex = items.length > 0 ? 0 : -1;
    },
  },
  created() {
    document.addEventListener("click", this.handleClickOutside);
  },
  unmounted() {
    document.removeEventListener("click", this.handleClickOutside);
  },
  watch: {
    //items thay doi cap nhat lai con tro
    items(val) {
      this.setHoveredIndex(val);
    },
    //gia tri duoc chon ban dau thay doi
    initialSelected: {
      immediate: true,
      handler(val) {
        this.setSelected(val);
      },
    },
    //model thay doi
    modelValue(val) {
      this.setSearch(val);
    },
  },
};
</script>
<style>
@import url(../../styles/components/bases/dropdown.css);
@import url(../../styles/components/bases/combobox.css);
</style>
