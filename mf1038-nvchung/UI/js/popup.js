/**
 * Popup class
 * @author nvchung
 */
class Popup {
  /**
   * @param {string} popupId id of popup element
   */
  constructor(popupId) {
    this.popupElm = $(popupId);
    this.popupElm.find(".c-popup-close")?.on("click", () => this.hide());
    this.popupElm.on("click", (e) => {
      if (e.target == e.currentTarget) {
        this.hide();
      }
    });
  }
  /**
   * show popup
   */
  show() {
    this.popupElm.addClass("active");
    this.popupElm.animate(
      {
        opacity: 1,
      },
      "fast"
    );
  }
  /**
   * hide popup
   */
  hide() {
    this.popupElm.animate(
      {
        opacity: 0,
      },
      "fast",
      () => this.popupElm.removeClass("active")
    );
  }
}

class ConfirmPopup extends Popup {
  constructor(popupId, confirmBtnId, cancelBtnId) {
    super(popupId);
    this.popupElm.find(cancelBtnId).on("click", () => this.hide());
    this.confirmBtnId = confirmBtnId;
  }
  /**
   * @callback onAcceptCallback
   */
  /**
   * show confirm popup
   * @param {string} text
   * @param {string} title
   * @param {onAcceptCallback} onAccept callback to run when confirm button clicked
   */
  show(title, text, onAccept) {
    super.show();
    this.popupElm.find(".c-popup-title").text(title);
    this.popupElm.find(".c-popup-text").text(text);
    if (this.confirmBtnId) {
      this.popupElm
        .find(this.confirmBtnId)
        .off()
        .on("click", () => {
          this.hide();
          onAccept();
        });
    }
  }
}
