/**
 * lop format du lieu
 * @author nvchung
 */
class Formatter {
  static currencyFormat = new Intl.NumberFormat("vi", {
    currency: "vnd",
    style: "currency",
  });
  static isNullOrEmpty(s) {
    return s == null || s === "";
  }
  /**
   * @param {string} cur currency string
   * @returns formatted currency string
   */
  static formatCurrency(cur) {
    return this.isNullOrEmpty(cur) ? "" : this.currencyFormat.format(cur);
  }
  /**
   *
   * @param {string} dateStr date string
   * @param {number} type 1-dd-MM-yyyy; 2-yyyy-MM-dd
   * @param {string} delim delimiter character
   * @returns formatted date string
   */
  static formatDate(dateStr, type = 1, delim = "/") {
    if (this.isNullOrEmpty(dateStr)) return "";
    const date = new Date(dateStr);
    const y = date.getFullYear().toString();
    const m = (date.getMonth() + 1).toString().padStart(2, "0");
    const d = date.getDate().toString().padStart(2, "0");
    if (type == 1) {
      return `${d}${delim}${m}${delim}${y}`;
    }
    return `${y}${delim}${m}${delim}${d}`;
  }
  /**
   *
   * @param {number} g gender number 1/0
   * @returns Nam or Nữ
   */
  static formatGender(g) {
    return this.isNullOrEmpty(g) ? "" : g == 1 ? "Nam" : "Nữ";
  }
  /**
   *
   * @param {number} status work status number
   * @returns work status name
   */
  static formatWorkStatus(status) {
    if (this.isNullOrEmpty(status)) return "";
    switch (status) {
      case 0:
        return "Đã nghỉ việc";
      case 1:
        return "Đang làm việc";
    }
    return "Không rõ";
  }
}
