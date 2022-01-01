$(".c-field-input").each((_, elm) => {
  const fp = $(elm);
  const resetBtn = $(`<span class="c-field-input-reset"></span>`);
  const input = fp.find("input");
  input.on("input", () => {
    resetBtn.toggleClass("active", !!input.val());
  });
  resetBtn.on("click", () => {
    input.val("").trigger("input");
  });
  fp.append(resetBtn);
});
