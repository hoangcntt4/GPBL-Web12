$(document).ready(function () {
  employeePage = new EmployeePage();
});

class EmployeePage {
  PageTitle = "Danh sách nhân viên";
  Api = "http://cukcuk.manhnv.net/api/v1/Employees";
  constructor() {
    this.loadData();
    this.iniEvents();
  }

  /* * Gán các sự kiện cho thành phần
   * Bổ sung thêm sự kiện click cho button Thêm mới */

  iniEvents() {
    var me = this;
    //1. Click Thêm mới
    document.getElementById("btnThemNv").addEventListener("click", function () {
      //Xóa hết dữ liệu cũ:
      $("#dialogThemNv input").val(null);
      //Hiện thị dialog thêm mới:
      document.getElementById("dialogThemNv").style.display = "block";
    });

    //Click X => ẩn dialog:
    $("#btnCloseDialog, #btnCancel").click(function (e) {
      // ẩn form chi tiết:
      $("#dialogThemNv").hide();
      $("#dialogSuaNv").hide();
      $("#dialogXoaNv").hide();
    });

    //2. Double click trên các dòng dữ liệu (tr)
    /* $("#tbEmployee tbody tr").dblclick(function(e){
                $("#dialogThemNv").show();
            }) */
    $(document).on("dblclick", "#tbEmployee tbody tr", function () {
      try {
        $("#dialogSuaNv").show();
        //binding dữ liệu lên form chi tiết
        //Gọi API lấy dữ liệu chi tiết và binding lên form
        var employeeId = $(this).data("id");
        $(".m-loading").show();
        $.ajax({
          type: "GET",
          url: `http://cukcuk.manhnv.net/api/v1/Employees/${employeeId}`,
          success: function (response) {
            //Lấy ra tất cả các input có addtribute là fieldname:
            let inputs = $("#dialogSuaNv input[fieldName]");
            for (const input of inputs) {
              //Lấy ra fieldname:
              let fieldName = input.getAttribute("fieldName");
              //Lấy value của từng input ứng với từng fieldName của response
              let value = response[fieldName];
              if (value) {
                input.value = value;
              }
              //binding giới tính
              if(fieldName == "Gender"){
                switch(value){
                    case 0:
                        input.value = "Nữ";
                        break;
                    case 1:
                        input.value = "Nam";
                        break;
                    case 2:
                        input.value = "Khác";
                        break;
                    default: 
                        input.value = "";
                        break;
                }
                }
                //binding ngày
                if(fieldName == "DateOfBirth"||""){
                    value = new Date(value);
                    let date = value.getDate();
                    date = date < 10 ? `0${date}` : date;
                    let month = value.getMonth() + 1;
                    month = month < 10 ? `0${month}` : month;
                    let year = value.getFullYear();
                    value = `${year}-${month}-${date}`;
                    input.value = value;
                }
            }
            $(".m-loading").hide();
          },
        });
        //3. Sửa nhân viên
        $("#btnSave").click(function () {
          try {
            //Thu thập thông tin:
            //Lấy ra tất cả các input có addtribute là fieldname:
            let inputs = $("#dialogSuaNv input[fieldName]");
            let employee = {};
            for (const input of inputs) {
              //Lấy ra fieldname:
              let fieldName = input.getAttribute("fieldName");
              let value = input.value;
              //Gán value của từng input vào đối tượng employee ứng với từng fieldname
              employee[fieldName] = value;
            }
            //Gọi API thực hiện Sửa nhân viên:
            $(".m-loading").show();
            $.ajax({
              type: "PUT",
              url: `http://cukcuk.manhnv.net/api/v1/Employees/${employeeId}`,
              data: JSON.stringify(employee),
              dataType: "json",
              contentType: "application/json",
              success: function (response) {
                $("#dialogSuaNv").hide();
                me.loadData();
                $(".m-toast-success")[0].style.display = "flex";
                setTimeout(() => {
                  $(".m-toast-success").hide();
                }, 3000);
                $(".m-loading").hide();
              },
              error: function (res) {
                switch (res.status) {
                  case 400:
                    alert(res.responseJSON.userMsg);
                    break;
                  default:
                    alert("Có lỗi xảy ra!");
                    break;
                }
              },
            });
          } catch (error) {
            console.log(error);
          }
        });
      } catch (error) {
        console.log(error);
      }
    });
    //4. Thêm nhân viên
    $("#btnAdd").click(function () {
      try {
        //Thu thập thông tin:
        //Lấy ra tất cả các input có addtribute là fieldname:
        let inputs = $("#dialogThemNv input[fieldName]");
        let employee = {};
        for (const input of inputs) {
          //Lấy ra fieldname:
          let fieldName = input.getAttribute("fieldName");
          let value = input.value;
          //Gán value của từng input vào đối tượng employee ứng với từng fieldname
          employee[fieldName] = value;
        }

        //Gọi API thực hiện Thêm mới nhân viên:
        $(".m-loading").show();
        $.ajax({
          type: "POST",
          url: "http://cukcuk.manhnv.net/api/v1/Employees",
          data: JSON.stringify(employee),
          dataType: "json",
          contentType: "application/json",
          success: function (response) {
            $("#dialogThemNv").hide();
            me.loadData();
            $(".m-toast-success")[0].style.display = "flex";
            setTimeout(() => {
              $(".m-toast-success").hide();
            }, 3000);
            $(".m-loading").hide();
          },
          error: function (res) {
            switch (res.status) {
              case 400:
                alert(res.responseJSON.userMsg);
                break;
              default:
                alert("Có lỗi xảy ra!");
                break;
            }
          },
        });
      } catch (error) {
        console.log(error);
      }
    });
    //5. Xóa nhân viên
    // $(document).on("click", "#tbEmployee tbody tr", function(){
    //     var employeeId = $(this).data('id');
    // })
    $("#btnXoaNv").click(function () {
      $("#dialogXoaNv").show();
      $("#btnDelete").click(function () {
        //Mảng các checkbox có className .m-check-del
        let arrDelete = $("#tbEmployee tbody tr input.m-check-del");
        let check = 0;
        for (const item of [...arrDelete]) {
          if (item.checked) {
            check = 1;
            try {
              $.ajax({
                type: "DELETE",
                url: `http://cukcuk.manhnv.net/api/v1/Employees/${item.getAttribute(
                  "id"
                )}`,
                success: function (response) {
                  $("#dialogXoaNv").hide();
                  me.loadData();
                  $(".m-toast-success")[0].style.display = "flex";
                  setTimeout(() => {
                    $(".m-toast-success").hide();
                  }, 3000);
                },
              });
            } catch (error) {
              console.log(error)
            }
          }
        }
        if(check==0){
          $("#dialogXoaNv").hide();
          $(".m-toast-error")[0].style.display = "flex";
          setTimeout(() => {
            $(".m-toast-error").hide();
          }, 3000);
        }
      });
    });
    //6. Load lại dữ liệu
    $("#btnReload").click(function () {
      me.loadData();
    });
  }
  //Lấy dữ liệu
  loadData() {
    try {
      //Xác định table chứa dữ liệu
      /* let table = document.getElementById('tbCustomer'); */
      let table = $("#tbEmployee");
      $("#tbEmployee tbody").empty();
      //Gọi api dữ liệu
      $(".m-loading").show();
      $.ajax({
        type: "GET",
        url: "http://cukcuk.manhnv.net/api/v1/Employees",
        success: function (response) {
          let data = response;
          //Load dữ liệu lên table
          //Lấy các thông tin cần thiết từ thead th:
          let ths = $("#tbEmployee thead th");
          for (const employee of data) {
            let trHTML = $(`<tr></tr>`);
            for (const th of ths) {
              //Lấy ra thông tin propertyName sẽ map giá trị tướng ứng với từng td:
              let fieldValue = th.getAttribute("fieldValue");
              let formatType = th.getAttribute("formatType");
              let value = employee[fieldValue];
              //định dạng dữ liệu
              let td = $(`<td>${value}</td>`);
              switch (formatType) {
                case "date":
                  //Ngày sinh
                  if (value) {
                    value = new Date(value);
                    let date = value.getDate();
                    date = date < 10 ? `0${date}` : date;
                    let month = value.getMonth() + 1;
                    month = month < 10 ? `0${month}` : month;
                    let year = value.getFullYear();
                    value = `${date}/${month}/${year}`;
                    td = $(`<td class="text-align-center">${value}</td>`);
                  } else {
                    value = "";
                    td = $(`<td>${value}</td>`);
                  }
                  break;
                case "money":
                  //Tiền tệ
                  if (value) {  
                    value = new Intl.NumberFormat("vi", {
                      style: "currency",
                      currency: "VND",
                    }).format(value);
                    td = $(`<td class="text-align-right">${value}</td>`);
                  } else {
                    value = "";
                    td = $(`<td>${value}</td>`);
                  }
                  break;
                case "gender":
                  //Giới tính
                  switch (value) {
                    case 0:
                      value = "Nữ";
                      td = $(`<td>${value}</td>`);
                      break;
                    case 1:
                      value = "Nam";
                      td = $(`<td>${value}</td>`);
                      break;
                    case 2:
                      value = "Khác";
                      td = $(`<td>${value}</td>`);
                      break;
                    default:
                      value = "";
                      td = $(`<td>${value}</td>`);
                      break;
                  }
                  break;
                default:
                  if (!value) {
                    value = "";
                    td = $(`<td>${value}</td>`);
                  }
                  //checkbox để xóa employee
                  if (fieldValue == "CheckboxDelete") {
                    td =
                      $(`<td>
                            <input class="m-checkbox m-check-del" type="checkbox" name="" id="${employee.EmployeeId}">
                            <label for="${employee.EmployeeId}"></label>
                        </td>`);
                  }
                  break;
              }
              trHTML.append(td);
            }
            trHTML.data("id", employee.EmployeeId);
            trHTML.data("data", employee);
            $("table#tbEmployee tbody").append(trHTML);
          }
        },
        error: function (response) {
          alert("Có lỗi xảy ra!");
        },
      });
      $(".m-loading").hide();
    } catch (error) {
      console.log(error);
    }
  }/* 
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
  } */
}
