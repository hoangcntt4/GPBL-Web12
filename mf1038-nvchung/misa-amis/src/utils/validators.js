const isRequired = (msg) => {
  return (val) => {
    if (val) return true;
    return msg;
  };
};
const isEmail = (msg = "Email không hợp lệ") => {
  return (val) => {
    if (!val) return true;
    return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(val) ? true : msg;
  };
};
const isPhoneNumber = (msg) => {
  return (val) => {
    if (!val) return true;
    return /^\d{10,12}$/.test(val) ? true : msg;
  };
};
const isDateLessThan = (msg, cmpVal) => {
  return (val) => {
    if (!val || isNaN(Date.parse(val))) return true;
    return Date.parse(val) < cmpVal ? true : msg;
  };
};
const isNumber = (msg) => {
  return (val) => {
    if (!val) return true;
    return /^\d+$/.test(val) ? true : msg;
  };
};
export { isRequired, isEmail, isPhoneNumber, isDateLessThan, isNumber };
