/**
 * Lop EmployeePage
 * @author nvchung
 */
class EmployeePage {
  GET_EMP_LIST_API = "http://cukcuk.manhnv.net/api/v1/Employees";
  GET_DEPARTMENT_LIST_API = "http://cukcuk.manhnv.net/api/v1/Departments";
  GET_POSITION_LIST_API = "http://cukcuk.manhnv.net/api/v1/Positions";
  constructor() {
    this.popup = new Popup("#empPopup");
    this.confirmDeletePopup = new ConfirmPopup(
      "#confirmDelete",
      "#confirmDeleteOkBtn",
      "#confirmDeleteCancelBtn"
    );
    this.formValidationErrorPopup = new ConfirmPopup(
      "#formValidationError",
      null,
      "#confirmFormValidationErrorBtn"
    );
    $("#comboPerPage").combobox({
      items: [
        { text: "10 nhân viên/trang", value: 10 },
        { text: "20 nhân viên/trang", value: 20 },
        { text: "30 nhân viên/trang", value: 30 },
        { text: "50 nhân viên/trang", value: 50 },
        { text: "100 nhân viên/trang", value: 100 },
      ],
      selectedValue: 10,
      onChange: (perPage) => this.handlePerPageChange(perPage),
    });
    this.toast = new Toast("#toast");
    this.loadFilter();
    this.loadEmp();
    this.bindEvents();
  }
  /**
   * gan event cho cac element
   */
  bindEvents() {
    // gan event cho input tim kiem
    $("#idOrNameFilter").on("input", () => this.handleFilter());
    //gan event cho nut huy
    $("#cancelBtn").on("click", () => {
      this.popup.hide();
    });
    // gan event cho nut luu
    $("#saveEmployeeBtn").on("click", () => {
      this.handleSave();
    });
    // gan event cho nut xoa
    $("#delEmployeeBtn").on("click", () => {
      this.handleDelete();
    });
    // gan event nut lam moi
    $("#refreshBtn").on("click", () => {
      $("#idOrNameFilter").val("").trigger("input");
      this.loadFilter();
      this.loadEmp();
    });
    // gan event nut mo form them
    $("#addEmployeeBtn").on("click", () => {
      this.popup.show();
      this.initFormAdd();
    });
    // bat su kien kich dup vao hang trong bang
    $("#empTb").on("dblclick", "tbody tr", (e) => this.handleRowClick(e));
    // bat su kien chon nhieu
    $("#empTb thead input[type='checkbox']").on("change", (e) => {
      $("#empTb tbody tr input[type='checkbox']:first-child").prop(
        "checked",
        e.target.checked
      );
    });
    // tu dong format input tien te
    $("#empPopup input[input-format='currency']").on("input", (evt) => {
      const input = $(evt.target);
      if (input.val() != "") {
        input.addClass("text-right");
        input.val(Formatter.formatCurrency(input.val().replace(/\D/g, "")));
      } else {
        input.removeClass("text-right");
      }
    });
  }
  /**
   * ham chay khi so ban ghi tren trang thay doi
   * @param {number} perPage
   */
  handlePerPageChange(perPage) {
    this.pagination.setPerPage(perPage);
    this.renderEmpTable();
  }
  /**
   * ham chay khi kich nut xoa
   */
  handleDelete() {
    //lay ra cac id duoc chon
    const ids = $("#empTb tbody tr input[type='checkbox']:first-child:checked")
      .map((_, cb) => cb.value)
      .toArray();
    if (ids.length == 0) {
      this.toast.showToast("Chọn một dòng để xóa", "danger");
    } else {
      //hien xac nhan xoa
      this.confirmDeletePopup.show(
        "Xác nhận xóa",
        `Bạn có chắc muốn xóa ${ids.length} bản ghi được chọn?`,
        () => {
          this.showLoader();
          // tao ra cac promise xoa
          const tasks = ids.map((id) =>
            $.ajax(`${this.GET_EMP_LIST_API}/${id}`, {
              method: "DELETE",
            })
          );
          Promise.all(tasks)
            .then(() => {
              this.toast.showToast(`${tasks.length} bản ghi đã được xóa!`);
              $("#empTb thead input[type='checkbox']").prop("checked", false);
              this.loadEmp();
            })
            .catch((err) => this.handleAjaxError(err));
        }
      );
    }
  }
  // reset form loi
  clearValidations() {
    $("#empPopup .c-field-input.invalid").removeClass("invalid");
  }
  /**
   * validate du lieu tren form
   * @returns string - thong bao loi
   */
  validateForm() {
    let err;
    $("#empPopup input").each((_, input) => {
      //neu input la required
      if (input.hasAttribute("is-required") && input.value == "") {
        input.parentElement.classList.add("invalid");
        if (!err) err = input.getAttribute("required-message");
      } else {
        input.parentElement.classList.remove("invalid");
      }
      //neu input la email
      if (input.type == "email" && input.value != "") {
        if (!/^.+@.+\..+$/.test(input.value)) {
          input.parentElement.classList.add("invalid");
          if (!err) err = "Email không đúng định dạng!";
        } else {
          input.parentElement.classList.remove("invalid");
        }
      }
    });
    return err;
  }
  /**
   * khoi tao form them
   */
  initFormAdd() {
    this.clearValidations();
    $("#saveEmployeeBtn").text("Thêm");
    this.currentId = null;
    this.showLoader();
    //reset cac input
    $("#empPopup input[name]:not([type='radio'])").val("").trigger("input");
    // khoi tao combobox
    const comboboxes = [
      {
        id: "#employeePosition",
        url: this.GET_POSITION_LIST_API,
        text: "PositionName",
        value: "PositionId",
        hint: "Chọn một vị trí",
      },
      {
        id: "#employeeDepartment",
        url: this.GET_DEPARTMENT_LIST_API,
        text: "DepartmentName",
        value: "DepartmentId",
        hint: "Chọn một phòng ban",
      },
      {
        id: "#employeeWorkStatus",
        hint: "Chọn trạng thái làm việc",
        items: [
          { text: "Đang làm việc", value: 1 },
          { text: "Đã nghỉ việc", value: 0 },
        ],
        selectedValue: -1,
      },
    ];
    this.initComboboxes(comboboxes);
    this.hideLoader();
  }
  //render thong tin phan trang
  renderPageInfo() {
    const perPage = $("#comboPerPage").data("selectedValue");
    $("#paginationInfo")
      .empty()
      .append(`Hiển thị <b>${perPage}</b> nhân viên/trang.`);
  }
  /**
   * hien loading
   */
  showLoader() {
    $("#loader").addClass("active");
  }
  /**
   * an loading
   */
  hideLoader() {
    $("#loader").removeClass("active");
  }
  /**
   * ham luu/them moi phu thuoc vao thuoc tinh currentId
   */
  handleSave() {
    const error = this.validateForm();
    if (error) {
      this.formValidationErrorPopup.show("Lỗi", error);
      return;
    }
    const data = {};
    // get du lieu tren form
    // get gia tri input la radio
    $("#empPopup input[name]").each((_, { name, value, type }) => {
      if (type == "radio") {
        data[name] = $(`#empPopup input[name='${name}']:checked`).val();
      } else {
        data[name] = value;
      }
    });
    //get gia tri input co format
    $("#empPopup input[input-format]").each((_, input) => {
      data[input.name] = input.value.replace(/\D/g, "");
    });
    //get gia tri combobox
    data.DepartmentId = $("#employeeDepartment").data("selectedValue");
    data.PositionId = $("#employeePosition").data("selectedValue");
    data.WorkStatus = $("#employeeWorkStatus").data("selectedValue");
    // kiem tra la form them hay sua
    if (this.currentId) {
      // la form sua
      $.ajax(`${this.GET_EMP_LIST_API}/${this.currentId}`, {
        method: "PUT",
        data: JSON.stringify(data),
        contentType: "application/json",
        success: () => {
          this.popup.hide();
          this.toast.showToast("Dữ liệu được cập nhật!");
          this.loadEmp();
        },
        error: (err) => {
          this.handleAjaxError(err);
        },
      });
    } else {
      // la form them
      $.ajax(this.GET_EMP_LIST_API, {
        method: "POST",
        data: JSON.stringify(data),
        contentType: "application/json",
        success: () => {
          this.popup.hide();
          this.toast.showToast("Đã thêm mới nhân viên!");
          this.loadEmp();
        },
        error: (err) => {
          this.handleAjaxError(err);
        },
      });
    }
  }
  /**
   * xu ly loi ajax
   * @param {JQuery.jqXHR<any>} err
   */
  handleAjaxError(err) {
    console.log(err);
    const res = err.responseJSON;
    if (res && res.userMsg) {
      this.toast.showToast(res.userMsg, "danger");
    } else {
      this.toast.showToast("Có lỗi xảy ra! Vui lòng thử lại.", "danger");
    }
    this.hideLoader();
  }
  /**
   * ham xu ly su kien kich dup vao hang tren table
   * @param {MouseEvent} e
   */
  handleRowClick(e) {
    this.clearValidations();
    $("#saveEmployeeBtn").text("Lưu");
    this.popup.show();
    this.showLoader();
    this.currentId = $(e.currentTarget).data("id");
    // get data tu api voi id=currentId
    $.get(`${this.GET_EMP_LIST_API}/${this.currentId}`)
      .then((data) => {
        $("#empPopup input[name]").each((_, input) => {
          //gan du lieu vao cac input
          input = $(input);
          const name = input.attr("name");
          const value = data[name];
          switch (input.attr("type")) {
            case "date":
              input.val(Formatter.formatDate(value, 2, "-")).trigger("input");
              break;
            case "radio":
              input.prop("checked", input.val() == value);
              break;
            default:
              input.val(value).trigger("input");
          }
        });
        return data;
      }) //khoi tao cac combobox
      .then((data) => {
        const comboboxes = [
          {
            id: "#employeePosition",
            url: this.GET_POSITION_LIST_API,
            text: "PositionName",
            value: "PositionId",
            selectedValue: data.PositionId,
            hint: "Chọn một vị trí",
          },
          {
            id: "#employeeDepartment",
            url: this.GET_DEPARTMENT_LIST_API,
            text: "DepartmentName",
            value: "DepartmentId",
            selectedValue: data.DepartmentId,
            hint: "Chọn một phòng ban",
          },
          {
            id: "#employeeWorkStatus",
            selectedValue: data.WorkStatus,
            hint: "Chọn trạng thái làm việc",
            items: [
              { text: "Đang làm việc", value: 1 },
              { text: "Đã nghỉ việc", value: 0 },
            ],
          },
        ];
        this.initComboboxes(comboboxes);
        this.hideLoader();
      })
      .catch((err) => this.handleAjaxError(err));
  }
  /**
   * ham khoi tao combobox
   * @param {Array<{id:string,url?:string,text?:string,value?:string,selectedValue?:string,hint?:string,items?:Array<{text:string,value:string}}} comboboxes
   * @param id - #id cua combobox
   * @param url url cua api
   * @param text ten cua thuoc tinh lam text hien thi
   * @param value ten cua thuoc tinh lam value
   * @param selectedValue gia tri duoc chon
   * @param hint goi y
   * @param items mang items cua combobox trong truong hop ko load tu api
   */
  initComboboxes(comboboxes) {
    comboboxes.forEach(
      ({ id, url, text, value, selectedValue, hint, items, onChange }) => {
        // neu combobox duoc load tu api
        if (url) {
          $.get(url, (list) => {
            const items = list.map((item) => ({
              text: item[text],
              value: item[value],
            }));
            $(id).combobox({
              items,
              selectedValue,
              hint,
              onChange,
            });
          }); //combobox binh thuong
        } else {
          $(id).combobox({
            items,
            selectedValue,
            hint,
          });
        }
      }
    );
  }
  //ham render bang employee
  renderEmpTable() {
    const tbody = $("#empTb tbody");
    // lam trong bang
    tbody.empty();
    // lay ten cac thuoc tinh can hien thi
    const props = $("#empTb thead th[propname]")
      .map((_, th) => ({
        name: $(th).attr("propname"),
        format: $(th).attr("format"),
      }))
      .toArray();
    //render data cua page hien tai
    this.pagination.getCurrentPageData().forEach((emp) => {
      const trHtml = $("<tr></tr>");
      trHtml.data("id", emp.EmployeeId);
      props.forEach(({ name, format }) => {
        const tdHtml = $("<td></td>");
        switch (format) {
          case "gender":
            tdHtml.text(Formatter.formatGender(emp[name]));
            break;
          case "date":
            tdHtml.text(Formatter.formatDate(emp[name]));
            tdHtml.addClass("text-center");
            break;
          case "currency":
            tdHtml.text(Formatter.formatCurrency(emp[name]));
            tdHtml.addClass("text-right");
            break;
          case "workStatus":
            tdHtml.text(Formatter.formatWorkStatus(emp[name]));
            break;
          default:
            tdHtml.text(emp[name] || "");
        }
        trHtml.append(tdHtml);
      });
      const tdCheck = $(
        `<td><input class="c-check-box" type="checkbox" value="${emp.EmployeeId}"></td>`
      );
      trHtml.prepend(tdCheck);
      tbody.append(trHtml);
    });
    this.renderPageInfo();
  }
  // xu ly tim kiem, loc thong tin
  handleFilter() {
    const idOrName = $("#idOrNameFilter").val();
    const departmentId = $("#departmentFilter").data("selectedValue");
    const positionId = $("#positionFilter").data("selectedValue");
    // clone mang du lieu
    let filterData = [...this.data];
    if (idOrName) {
      filterData = filterData.filter(
        (d) => d.EmployeeId.includes(idOrName) || d.FullName.includes(idOrName)
      );
    }
    if (departmentId) {
      filterData = filterData.filter((d) => d.DepartmentId == departmentId);
    }
    if (positionId) {
      filterData = filterData.filter((d) => d.PositionId == positionId);
    }
    this.pagination.setData(filterData);
  }
  // khoi tao combobox loc du lieu
  loadFilter() {
    this.initComboboxes([
      {
        id: "#departmentFilter",
        url: this.GET_DEPARTMENT_LIST_API,
        text: "DepartmentName",
        value: "DepartmentId",
        hint: "Chọn một phòng ban",
        onChange: () => this.handleFilter(),
      },
      {
        id: "#positionFilter",
        url: this.GET_POSITION_LIST_API,
        text: "PositionName",
        value: "PositionId",
        hint: "Chọn một vị trí",
        onChange: () => this.handleFilter(),
      },
    ]);
  }
  /**
   * load du lieu len bang
   */
  loadEmp() {
    this.showLoader();
    $.get(this.GET_EMP_LIST_API, (data) => {
      this.data = data;
      this.pagination = new Pagination({
        paginationId: "#pagination",
        perPage: 10,
        data,
        onPageChange: () => this.renderEmpTable(), // render lai table khi page thay doi
      });
      this.renderEmpTable();
      this.hideLoader();
    }).fail((err) => this.handleAjaxError(err));
  }
}
$(() => {
  new EmployeePage();
});
