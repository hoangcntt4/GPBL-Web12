class Toast {
  constructor(toastId) {
    this.toastHtml = $(toastId);
    this.toastHtml.empty();
    this.iconHtml = $("<i></i>");
    this.textHtml = $(`<div class="c-toast-text"></div>`);
    const iconContainerHtml = $(`<div class="c-toast-icon"></div>`);
    iconContainerHtml.append(this.iconHtml);
    const btn = $(`<button class="c-toast-close"></button>`);
    btn.on("click", () => this.hideToast());
    this.toastHtml.append(iconContainerHtml);
    this.toastHtml.append(this.textHtml);
    this.toastHtml.append(btn);
  }
  hideToast() {
    this.toastHtml.animate(
      {
        opacity: 0,
        right: "-100px",
      },
      "fast",
      () => this.toastHtml.hide()
    );
  }
  showToast(text, type = "primary", timeout = 3000) {
    this.toastHtml.removeClass();
    this.toastHtml.addClass("c-toast");
    this.iconHtml.removeClass();
    switch (type) {
      case "primary":
        this.iconHtml.addClass("fas fa-check");
        this.toastHtml.addClass("c-toast-primary");
        break;
      case "danger":
        this.iconHtml.addClass("fas fa-exclamation");
        this.toastHtml.addClass("c-toast-danger");
        break;
    }
    this.textHtml.text(text);
    this.toastHtml.show();
    this.toastHtml.animate(
      {
        opacity: 1,
        right: "20px",
      },
      "fast"
    );
    setTimeout(() => this.hideToast(), timeout);
  }
}
