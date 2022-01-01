/**
 * ComboBox form control class
 * @author nvchung
 */
class ComboBox {
  /**
   * @callback onChange
   * @param {string} selectedValue
   */
  /**
   * @param {object} config
   * @param {JQuery<HTMLElement>} config.root - Combobox container
   * @param {Array<{text:string,value:string}} config.items - Combobox items
   * @param {string} config.selectedValue - initial value
   * @param {string} config.hint - hint
   * @param {onChange} config.onChange - callback to run when selected item changed
   * @author nvchung
   */
  constructor({ root, items, selectedValue, hint, onChange }) {
    this.root = root;
    this.onChange = onChange;
    this.selectedValue = selectedValue;
    root.data("selectedValue", selectedValue);
    this.input = $(`<input readonly type="text" placeholder="${hint}" />`);
    this.ul = $(`<ul class="c-combo-box-options"></ul>`);
    items.forEach(({ text, value }) => {
      const li = $(`<li class="c-combo-box-item">${text}</li>`);
      if (selectedValue == value) {
        li.addClass("selected");
        this.input.val(text);
      }
      li.on("click", (e) => this.handleItemClick(e));
      li.data("value", value);
      li.data("text", text);
      this.ul.append(li);
    });
    const arrow = $(`<span class="c-combo-box-arrow"></span>`);
    arrow.on("click", () => this.toggle());
    root.empty();
    root.append(this.input);
    root.append(arrow);
    root.append(this.ul);
    root.addClass("c-combo-box");
  }
  /**
   * show/hide combobox items
   */
  toggle() {
    this.root.toggleClass("active");
  }
  /**
   * show combobox items
   */
  show() {
    this.root.addClass("active");
  }
  /**
   * hide combobox items
   */
  hide() {
    this.root.removeClass("active");
  }
  /**
   * handle item select
   * @param {MouseEvent} e
   */
  handleItemClick(e) {
    const li = $(e.currentTarget);
    const newVal = li.data("value");
    this.input.val(li.data("text"));
    this.root.data("selectedValue", newVal);
    this.ul.children(".selected").removeClass("selected");
    li.addClass("selected");
    if (this.selectedValue != newVal) {
      this.selectedValue = newVal;
      this.onChange?.(this.selectedValue);
    }
    this.hide();
  }
}
/**
 * extend jquery object
 */
$.fn.extend({
  combobox({ items = [], selectedValue = "", hint = "", onChange }) {
    return new ComboBox({ root: this, items, selectedValue, hint, onChange });
  },
});
