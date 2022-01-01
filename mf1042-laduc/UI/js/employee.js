$('document').ready(function() {
        EmployeePage = new EmployeePage();
    })
    // xây dựng class EmployeePage
class EmployeePage {
    PageTittle = "Danh sách nhân viên.";
    Api = "http://cukcuk.manhnv.net/api/v1/Employees";
    currentId = null;
    //Viết hàm khởi tạo class
    constructor() {
            this.loadData();
            this.initEvents();
        }
        /**
         * xây dựng các sự kiện
         */
    initEvents() {
            var me = this;

            //sự kiện click button thêm mới để hiển thị form
            $('#btnRefresh').click(function(e) {
                me.loadData();
            });
            //Sự kiện hiển thị form khi click nút "thêm nhân viên"
            $('#btnAddEmployee').click(function(e) {
                //clear những input trong foem
                $('input').val(null);
                $('#EmployeeForm').show();
                //gán crrentId để phân biệt là thêm hay sửa nhân viên
                me.currentId = null;
                //focus vào ô input nhập mã nhân viên khi khởi tạo form
                document.getElementById("txtEmployeeCode").focus();
                //xóa class invalid để các css của ô input trở lại bình thường
                $('#txtEmployeeCode').removeClass("m-invalid");
                $('#txtFullName').removeClass("m-invalid");
            });
            // sự kiện để ẩn form
            $('#FormClose').click(function(e) {
                $('#EmployeeForm').hide();
            });
            $('#btnCloseFormEmployee').click(function(e) {
                $('#EmployeeForm').hide();
            });
            //sự kiện để ẩn popup thông báo
            $('#btnCloseForm,.m-form-close').click(function(e) {
                $('.m-confirm-delete').hide();
            });
            //gỡ class invalid của 2 ô input khi focus vào
            $('#txtEmployeeCode').focus(function(e) {
                $('#txtEmployeeCode').removeClass("m-invalid");
            });
            $('#txtFullName').focus(function(e) {
                $('#txtFullName').removeClass("m-invalid");
            });
            // sự kiện hiển thị form khi double click vào 1 hàng trên table
            $(document).on('dblclick', 'table#tbEmployees tbody tr', function() {

                $('#EmployeeForm').show();
                $('#txtEmployeeCode').removeClass("m-invalid");
                $('#txtFullName').removeClass("m-invalid");
                //focus vào ô nhập mã nhân viên
                document.getElementById("txtEmployeeCode").focus();
                $('.m-loader').show();
                // lấy ra bản ghi tương ứng khi click vào dòng 
                var employee = $(this).data('data');

                //fill dữ liệu lên form bằng Api
                let employeeId = employee.EmployeeId;
                //gán currentId để kiểm tra là thao tác sửa hay là thêm nhân viên
                me.currentId = employeeId;
                //gọi api để lấy dữ liệu điền vào form
                $.ajax({
                    type: "GET",
                    url: `http://cukcuk.manhnv.net/api/v1/Employees/${employeeId}`,
                    success: function(response) {
                        //lấy ra tất cả input có thuộc tính fieldName
                        let inputs = $('input[fieldName]');

                        //duyệt các thẻ input trong mảng inputs
                        for (const i of inputs) {
                            //định dạnh ngày tháng trước khi hiển thị lên form
                            if (i.getAttribute("type") == "date") {
                                //lấy ra giá trị trong thuộc tính fieldName
                                let valueName = i.getAttribute('fieldName');
                                //lấy giá trị trong đối tương employee bằng thuộc tính 
                                let value = response[valueName];
                                if (value) {
                                    value = new Date(value);
                                    let date = value.getDate();
                                    date = date < 10 ? `0${date}` : date;
                                    let month = value.getMonth() + 1;
                                    month = month < 10 ? `0${month}` : month;
                                    let year = value.getFullYear();
                                    value = `${year}-${month}-${date}`;
                                }
                                if (value != null) {
                                    i.value = value;
                                }
                            } else {
                                //lấy ra giá trị trong thuộc tính fieldName
                                let valueName = i.getAttribute('fieldName');
                                //lấy giá trị trong đối tương employee bằng thuộc tính 
                                let value = response[valueName];
                                if (value != null) {
                                    i.value = value;
                                }
                            }

                        }
                        //ẩn form thêm nhân viên
                        $('.m-loader').hide();
                    }
                });

            });
            //hàm báo lỗi khi nhập dữ liệu
            function err(s) {
                $('div.m-confirm-text').html("");
                let spanHTML = `<span>${s} không được để trống</span>`;
                $('div.m-confirm-text').append(spanHTML);
                $('#AlertBox').show();
            };
            //sự kiện thêm mới nhân viên khi nhấn nút lưu
            $('#btnSaveEmployee').click(function(e) {
                //lấy thông tin tất cả các thẻ input có thuộc tính fieldName
                let inputs = $('input[fieldName]');
                //kiểm tra xem ô mã nhân viên và họ tên đã được nhập hay chưa
                if (!($('#txtEmployeeCode').val())) {
                    $('#txtEmployeeCode').addClass("m-invalid");
                    err("Mã nhân viên");
                    return;
                }
                if (!($('#txtFullName').val())) {
                    $('#txtFullName').addClass("m-invalid");
                    err("Tên nhân viên");
                    return;
                }
                //khởi tạo đối tượng rỗng
                let employee = {};
                //thêm các thuộc tính cho đối tượng
                for (const input of inputs) {
                    let valueName = input.getAttribute('fieldName');
                    let value = input.value;
                    employee[valueName] = value;
                }
                $('.m-loader').show();
                //gọi Api để thêm hoặc sửa employee
                //sửa dữ liệu nhân viên
                if (me.currentId) {
                    $.ajax({
                        type: "PUT",
                        url: `http://cukcuk.manhnv.net/api/v1/Employees/${me.currentId}`,
                        data: JSON.stringify(employee),
                        dataType: "json",
                        contentType: 'application/json',
                        success: function(response) {
                            $('.m-toast-msg').show();
                            setTimeout(() => {
                                $('.m-toast-msg').hide();
                            }, 4000);
                            $('#EmployeeForm').hide();
                            me.loadData();

                        },
                        error: function(response) {
                            switch (response.status) {
                                case 400:
                                    alert(response.responseJSON.userMsg);
                                    break;
                                default:
                                    alert("Có lỗi xảy ra vui lòng liên hệ hotline để được tư vấn chi tiết");
                                    break;
                            }
                        }
                    });
                }
                //thêm nhân viên mới
                else {
                    $.ajax({
                        type: "POST",
                        url: "http://cukcuk.manhnv.net/api/v1/Employees",
                        data: JSON.stringify(employee),
                        dataType: "json",
                        contentType: 'application/json',
                        success: function(response) {
                            $('.m-toast-msg').show();
                            setTimeout(() => {
                                $('.m-toast-msg').hide();
                            }, 4000);
                            $('#EmployeeForm').hide();
                            me.loadData();

                        },
                        error: function(response) {
                            switch (response.status) {
                                case 400:
                                    alert(response.responseJSON.userMsg);
                                    break;
                                default:
                                    alert("Có lỗi xảy ra vui lòng liên hệ hotline để được tư vấn chi tiết");
                                    break;
                            }
                        }
                    });
                }

                $('.m-loader').hide();
            });
            //click chọn bản ghi
            $(document).on('click', 'table#tbEmployees tbody tr', function() {
                this.style.backgroundColor = '#cccccc';
                var employee = $(this).data('data');
                let employeeId = employee.EmployeeId;
                //click nút xóa nhân viên
                $('#btnDeleteEmployee').click(function(e) {
                    //thêm nội dung cho popup cảnh báo
                    $('div.m-confirm-text').html("");
                    let spanHTML = `<span>Bạn có chắc chắn muốn xóa nhân viên<b>[${employeeId}]</b> không?</span>`;
                    $('div.m-confirm-text').append(spanHTML);
                    //hiển thị popup cảnh báo
                    $('#confirmDeleteBox').show();
                    //xác nhận xóa
                    $('#confirmYes').click(function(e) {
                        //gọi api để xóa
                        $('.m-loader').show();
                        $.ajax({
                            type: "DELETE",
                            url: `http://cukcuk.manhnv.net/api/v1/Employees/${employeeId}`,
                            success: function(response) {
                                debugger
                                $('.m-toast-msg').show();
                                //cài đăth thời gian ẩn cho toast message
                                setTimeout(() => {
                                    $('.m-toast-msg').hide();
                                }, 3000);
                                $('.m-confirm-delete').hide();
                                me.loadData();
                                $('.m-loader').hide();
                            },
                            error: function(response) {
                                switch (response.status) {
                                    case 400:
                                        alert(response.responseJSON.userMsg);
                                        break;
                                    default:
                                        alert("có lỗi xảy ra vui lòng liên hệ hotline để được tư vấn");
                                        break;
                                }
                                $('.m-loader').hide();
                            }
                        });
                    });
                });
            });

        }
        //xây dựng hàm để lấy dữ liệu từ Api
    loadData() {
        //xác định table sẽ chứa dữ liệu
        let table = $('#tbEmployees');
        //trước khi load dữ liệu thì xóa đi dữ liệu cũ
        $('#tbEmployees>tbody').empty();
        $('.m-loader').show();
        //gọi api lấy dữ liệu
        $.ajax({
            type: "GET",
            url: "http://cukcuk.manhnv.net/api/v1/Employees",
            success: function(response) {
                console.log(response);
                //load dữ liệu lên table
                //lấy ra các thẻ th đăng có trên table
                let ths = $('#tbEmployees thead th');
                for (const employee of response) {
                    //tạo ra 1 bản ghi rỗng
                    var trHTML = $(`<tr></tr>`);
                    //duyệt các th để tạo ra các td
                    for (const th of ths) {
                        //lấy ra formatType và property của th
                        let formatType = th.getAttribute('formatType');
                        let property = th.getAttribute('property');
                        //lấy ra giá trị của đối tượng tương ứng với cột th
                        let value = employee[property];
                        // định dạng dữ liệu hiển thị
                        if (value != null) {
                            switch (formatType) {
                                case 'gender':
                                    //1. định dạng giới tính
                                    if (value != null) {
                                        switch (value) {
                                            case 0:
                                                value = "Nữ";
                                                break;
                                            case 1:
                                                value = "Nam";
                                                break;
                                            default:
                                                value = "Khác";
                                                break;
                                        }
                                    }

                                    break;
                                case 'date': //2. định dạng ngày tháng
                                    if (value) {
                                        value = new Date(value);
                                        let date = value.getDate();
                                        date = date < 10 ? `0${date}` : date;
                                        let month = value.getMonth() + 1;
                                        month = month < 10 ? `0${month}` : month;
                                        let year = value.getFullYear();
                                        value = `${date}/${month}/${year}`;
                                    }
                                    break;
                                case 'money': //6. định dạng tiền tệ
                                    if (value) {
                                        value = new Intl.NumberFormat('vi', {
                                            style: 'currency',
                                            currency: 'VND',
                                            minimumFractionDigits: 0
                                        }).format(value);
                                    } else {
                                        value = "";
                                    }
                                    break;
                                case 'workStatus': //7. định dạng tình trạng công việc
                                    if (value != null) {
                                        switch (value) {
                                            case 0:
                                                value = "Đăng thử việc";
                                                break;
                                            case 1:
                                                value = "Đăng làm việc";
                                                break;
                                            case 2:
                                                value = "Đã nghỉ việc";
                                                break;
                                            case 3:
                                                value = "Đã nghỉ hưu";
                                                break;
                                            default:
                                                value = "";
                                        }
                                    }
                                    break;
                                default:

                                    break;
                            }
                        } else {
                            value = "";
                        }
                        //gán giá trị vào td
                        if (formatType == 'money') {
                            var td = $(`<td style="text-align: right;">${value}</td>`);
                        } else {
                            td = $(`<td>${value}</td>`);
                        }
                        //gán td vào tr
                        trHTML.append(td);
                    }
                    //gán tr vào tbody của table
                    trHTML.data('data', employee);
                    $('table#tbEmployees tbody').append(trHTML);
                };
                $('.m-loader').hide();
            },
            error: function(response) {
                alert("có lỗi xảy ra!");
                $('.m-loader').hide();
            }
        });

    }
}