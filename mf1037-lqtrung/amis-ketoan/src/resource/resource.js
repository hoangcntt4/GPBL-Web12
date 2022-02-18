var resource = {
  /**
     - Định dạng ngày tháng
     - Author: LQTrung (13/02/2022)
     */
  formatDate(val,mode) {
    if (val != null) {
      const date = new Date(val);
      const y = date.getFullYear().toString();
      const m = (date.getMonth() + 1).toString().padStart(2, "0");
      const d = date.getDate().toString().padStart(2, "0");
      if(mode==1){
        return `${y}-${m}-${d}`;
      }
      else{
        return `${d}/${m}/${y}`;
      }
    }
  },

  /**
   - Định dạng giới tính
     - Author: LQTrung (13/02/2022)
   */
   formatGender(gender) {
    if (gender == 0) {
      return "Nữ";
    } else if (gender == 1) {
      return "Nam";
    } else return "Khác";
  },

  /**
   - Thông báo
   - Author: LQTrung (14/02/2022)
   */
   VI: {
    /**Thông báo xóa nhiều nhân viên*/
    msgDeleteMany: "Bạn có thực sự muốn xóa những nhân viên đã chọn không?",
    /**Thông báo xóa 1 nhân viên */
    msgDelete(val) {
      return `Bạn có thực sự muốn xóa nhân viên <${val}> không?`;
    },
    /**Thông báo lỗi server */
    errorMsg: "Có lỗi xảy ra, vui lòng liên hệ Misa để được hỗ trợ.",
    /**Thông báo lỗi trùng mã nhân viên phía server */
    userMsgDuplicate:
      "Thông tin Mã nhân viên không được trùng lặp với Mã nhân viên khác.",
    /**Thông báo lỗi trùng mã nhân viên phía client */
    msgDuplicate(val) {
      return `Mã nhân viên <${val}> đã tồn tại trong hệ
            thống, vui lòng kiểm tra lại.`;
    },
    /**Thông báo lỗi ngày sinh không được lớn hơn ngày hiện tại */
    errorMsgDob: "Ngày cấp không được lớn hơn ngày hiện tại.",
    /**Thông báo lỗi mã đơn vị không được để trống */
    errorMsgDepartment: "Đơn vị không được để trống.",
    /**Thông báo lỗi tên không được để trống */
    errorMsgFullName: "Tên không được để trống.",
    /**Thông báo lỗi mã nhân viên không được để trống*/
    errorEmployeeCodeEmpty: "Mã không được để trống.",
    /**Thông báo lỗi mã nhân viên sai định dạng*/
    errorEmployeeCode:
      "Mã nhân viên cần đúng định dạng NV-x (x là số tự nhiên).",
  },
};

export default resource;
