export default {
  /**
   * Ham format date
   * @param {String} dateStr
   * @param {Number} type
   * @param {String} delim
   * @returns Chuoi date duoc format
   */
  formatDate(dateStr, type = 1, delim = "/") {
    if (!dateStr) return "";
    const date = new Date(dateStr);
    const y = date.getFullYear().toString();
    const m = (date.getMonth() + 1).toString().padStart(2, "0");
    const d = date.getDate().toString().padStart(2, "0");
    if (type == 1) {
      return `${d}${delim}${m}${delim}${y}`;
    }
    return `${y}${delim}${m}${delim}${d}`;
  },
  formatGender(gender) {
    if (gender == 0) return "Nữ";
    return gender == 1 ? "Nam" : "Khác";
  },
};
