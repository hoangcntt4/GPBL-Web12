$(document).ready(function() {
    employeePage = new EmployeePage(1, 2, 3, 4, 5);
})
var isInsert = true;
var employeeId = "";
class EmployeePage {
    PageTitle = "Danh sách khách hàng";
    Api = "http://cukcuk.manhnv.net/api/v1/Employees";
    constructor() {

        this.loadData();
        this.iniEvents();
    }

    /**
     * Gán các sự kiện
     * Author-Hoàng Văn Thắng - 27/12/2021
     * editby----------
     */
    iniEvents() {

        var me = this;

        //click thêm mới
        document.getElementById('btnAddEmployee').addEventListener('click', function() {
                //hiển thị dialog thông tin nhân viên
                isInsert = true;
                // xóa dữ liệu cũ
                $('input').val(null);
                // hiển thị form thông tin người dùng
                document.getElementById('dialogAdd').style.display = 'block';
            })
            //ẩn form thêm mới
        $('#btnClose,#btnCancel').click(function(e) {
                $('#dialogAdd').hide();
            })
            //nhấn đúp chuột
        $('.m-table').on('dblclick', 'tbody tr', function() {
                isInsert = false;
                $('#dialogAdd').show();
                // load dữ liệu lên form bằng api
                employeeId = $(this).data('id');
                $.ajax({
                    type: "GET",
                    url: `http://cukcuk.manhnv.net/api/v1/Employees/${employeeId}`,
                    success: function(response) {
                        //lấy ra tất cả input có thuộc tính là fieldname
                        let inputs = $('input[fieldName]')
                            // duyệt các thẻ input trong mảng
                        for (const input of inputs) {
                            // lấy ra các giá trị trong thuộc tính fieldName
                            let fieldName = input.getAttribute('fieldName');
                            let value = response[fieldName];
                            if (value) {
                                input.value = value;
                            }
                        }

                    }
                });

            })
            // sự kiện cho nút lưu dữ liệu
        $('#btnSave').click(function() {
            //lấy tất cả input có attribute là fieldname
            let inputs = $('input[fieldName]');
            //ktao đối tượng rỗng
            let employee = {};
            for (const input of inputs) {
                // lấy ra các fieldName
                let fieldName = input.getAttribute('fieldName');
                let value = input.value;
                employee[fieldName] = value;
            }
            //xét trường hợp thêm mới
            if (isInsert) {
                //goị Api thực hiện lưu dữ liệu
                $.ajax({

                    type: "POST",
                    url: "http://cukcuk.manhnv.net/api/v1/Employees",
                    data: JSON.stringify(employee),
                    dataType: "json",
                    contentType: 'application/json',
                    success: function(response) {
                        $('#dialogAdd').hide();

                        me.loadData();
                        $('.m-toast-box.add').show();
                        setTimeout(() => {
                            $('.m-toast-box.add').hide();
                        }, 3000);

                    },
                    error: function(res) {
                        switch (res) {
                            case 400:
                                alert(res.responseJSON.userMsg);
                                break;

                            default:
                                alert("có lỗi xảy ra, vui lòng kiểm tra lại!");
                                break;
                        }
                    }
                })
            } else { //xét trường hợp chỉnh sửa
                $.ajax({

                    type: "PUT",
                    url: `http://cukcuk.manhnv.net/api/v1/Employees/${employeeId}`,
                    data: JSON.stringify(employee),
                    dataType: "json",
                    contentType: 'application/json',
                    success: function(response) {
                        $('#dialogAdd').hide();
                        me.loadData();
                        $('.m-toast-box.add').show();
                        setTimeout(() => {
                            $('.m-toast-box.add').hide();
                        }, 3000);

                    },
                    error: function(res) {
                        switch (res) {
                            case 400:
                                alert(res.responseJSON.userMsg);
                                break;

                            default:
                                alert("có lỗi xảy ra, vui lòng kiểm tra lại!");
                                break;
                        }
                    }
                });
            }

        });
        //click chọn dòng nhân viên
        $(document).on('click', 'table#tbCustomer tbody tr', function() {
            this.style.backgroundColor = '#2FBE8E';
            var employee = $(this).data('data');
            let employeeId = employee.EmployeeId;
            // sự kiên cho nút xóa nút xóa nhân viên
            $('#btnDelete').click(function(e) {

                //gọi api thực hiện xóa
                $.ajax({
                    type: "DELETE",
                    url: `http://cukcuk.manhnv.net/api/v1/Employees/${employeeId}`,
                    success: function(response) {

                        $('#dialogAdd').hide();
                        me.loadData();
                        $('.m-toast-box.delete').show();
                        setTimeout(() => {
                            $('.m-toast-box.delete').hide();
                        }, 3000);
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
                    }
                });

            });

        });
    }
    loadData() {
        try {
            //xác định table 
            let table = $('.m-table');
            //api lấy dữ liệu
            $.ajax({
                type: "GET",
                url: "http://cukcuk.manhnv.net/api/v1/Employees",
                success: function(response) {
                    let data = response;
                    console.log(data);
                    //lấy các thông tin từ thead th
                    let ths = $('table.m-table thead th');
                    for (const employee of data) {

                        let trHTML = $('<tr></tr>');
                        for (const th of ths) {
                            //lấy ra property map với từng cell
                            let fieldValue = th.getAttribute('fieldValue');

                            let formatType = th.getAttribute('formatType');
                            let value = employee[fieldValue];
                            let td = $(`<td>${value}</td>`);
                            switch (formatType) {
                                case 'date':
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
                                        td = $(`<td></td>`);
                                    }
                                    break;
                                case 'money':
                                    if (value) {
                                        value = new Intl.NumberFormat('vi', { style: 'currency', currency: 'VND' }).format(value);
                                        td = $(`<td class="text-align-right">${value}</td>`);
                                    } else {
                                        td = $(`<td></td>`);
                                    }
                                    break;
                                case 'workstatus':
                                    switch (value) {

                                        case 1:
                                            value = "Đang Thử Việc";
                                            td = $(`<td>${value}</td>`);
                                            break;
                                        case 2:
                                            value = "Nhân viên chính thức";
                                            td = $(`<td>${value}</td>`);
                                            break;

                                        default:
                                            value = "";
                                            td = $(`<td>${value}</td>`);
                                            break;
                                    }
                                    break;
                                    // case 'gender':
                                    //     switch (value) {
                                    //         case 0:
                                    //             value = "Nữ";
                                    //             td = $(`<td>${value}</td>`);
                                    //             break;
                                    //         case 1:
                                    //             value = "Nam";
                                    //             td = $(`<td>${value}</td>`);
                                    //             break;
                                    //         case 2:
                                    //             value = "Khác";
                                    //             td = $(`<td>${value}</td>`);
                                    //             break;

                                    //         default:
                                    //             value = "";
                                    //             td = $(`<td>${value}</td>`);
                                    //             break;
                                    //     }

                                    //     break;

                                default:
                                    if (!value) {
                                        td = $(`<td></td>`);
                                    } else {
                                        td = $(`<td>${value}</td>`);
                                    }
                                    break;
                            }

                            trHTML.append(td);
                        }

                        trHTML.data('id', employee.EmployeeId);
                        trHTML.data('data', employee);
                        $('table.m-table tbody').append(trHTML);
                    }


                },
                error: function(response) {
                    alert('có lỗi')
                }
            });
        } catch (error) {
            console.log(error);
        }


    }
}

function loadData() {
    // alert('load data');
}