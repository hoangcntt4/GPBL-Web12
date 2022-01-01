$(document).ready(function() {
    let dialog = new Dialog();
    dialog.initEvent();
})

class Dialog {
    Mode = Enum.Mode.Create
    EmployeeIdEdit = null;

    constructor() {}

    // Khai báo các sự kiện
    initEvent() {
            let dialogthis = this;
            // tạo sự kiện click button thêm mới nhân viên
            $("#btn-add-employee").click(function() {
                dialogthis.Mode = Enum.Mode.Create
                let form = $("#dialog-add-employee .ms-dialog-form").children().find('input');
                // duyệt từng trường thông tin
                $.each(form, function(index, input) {

                    $(input).val("").trigger("input");
                })
                Common.show_dialog();
            });

            // tạo sự kiện đóng cửa sổ dialog thêm nhân viên
            $("#close_dialog").click(Common.hide_dialog); // với nút "hủy"
            $(".ms-dialog-btn-close").click(Common.hide_dialog); //với nút x

            // tạo sự kiện khi người dùng ra khỏi input
            $(".ms-dialog-form input.ms-input, .ms-dialog-form .ms-dropdown").focusout(function() {
                // loại bỏ cảnh báo validate
                $(this).removeClass('ms-valid-error-input');
            })

            // tạo sự kiện keypress cho trường lương dialog
            $('.ms-dialog-form').find('[formatSalary]').keyup(dialogthis.inputMoneyPress);


            // tạo sự kiện submit form
            $('#btnSubmit').click(dialogthis.submitForm.bind(this));

            // tạo sự kiện double click 1 dòng trong  table
            $('#table-data').on('dblclick', 'tbody tr', function() {
                dialogthis.EmployeeIdEdit = $(this).data('EmployeeId');
                // chỉnh sửa trạng thái dialog sang edit
                dialogthis.Mode = Enum.Mode.Edit;
                // lấy dữ liệu edit từ API
                dialogthis.getEmployeeEditData();
                // hiển thị dialog
                Common.show_dialog();
            })

        }
        // lấy dữ liệu Edit từ API và điền vào form
    getEmployeeEditData() {
            try {
                $.ajax({
                    url: `http://cukcuk.manhnv.net/api/v1/Employees/${this.EmployeeIdEdit}`,
                    method: "GET",
                    data: null,
                    dataType: "JSON",
                    contentType: "application/json",
                    async: false
                }).done(function(res) {
                    console.log(res);
                    // điền dữ liệu vào form
                    let form = $("#dialog-add-employee .ms-dialog-form").children().find('input');
                    // duyệt từng trường thông tin
                    $.each(form, function(index, input) {
                        // lấy tên trường thông tin
                        let nameProperty = $(input).attr('name');
                        let value = res[nameProperty];

                        // định dạng dữ liệu
                        let nameFomat = $(input).attr('format');
                        switch (nameFomat) {
                            case 'Money': // dạng tiền
                                value = Common.formatTextToVND(value);
                                break;
                            case 'ddmmyyyy': // dạng date
                                value = Common.formatDateJS(value);
                                break;

                            default:
                                break;
                        }

                        $(input).val(value).trigger("input");
                    })
                }).fail(function(res) {
                    alert("Có lỗi xảy ra");
                    console.log(res);
                })
            } catch (error) {
                alert("Có lỗi xảy ra");
                console.log(error);
            }
        }
        //thực hiện submit form
    submitForm() {
            let form = $("#dialog-add-employee .ms-dialog-form").children().find('input');

            // tạo obj
            let objData = {}
                // duyệt từng trường thông tin
            $.each(form, function(index, input) {
                // lấy tên trường thông tin
                let nameProperty = $(input).attr('name');
                let nameFormat = $(input).attr('format');
                let value = $(input).val();
                // kiểm tra thông tin
                if (value) {
                    switch (nameFormat) {
                        case 'Money':
                            value = Common.formatVNDToText(value)
                            break;
                        case 'ddmmyyyy':
                            value = Common.formatDate(value)
                            break;
                        default:
                            break;
                    }
                    objData[nameProperty] = value
                }
            })
            console.log('Object Submit: ', objData);
            try {
                if (this.checkValidateInput()) {
                    if (this.Mode == Enum.Mode.Create) {
                        this.sendDataCreate();
                    } else if (this.Mode == Enum.Mode.Edit) {
                        this.sendDataEdit();
                    }
                    Common.loaddata();
                }
            } catch (error) {
                alert("Có lỗi xảy ra");
                console.log(error);
            }
        }
        //kiểm tra trạng thái validate của các trường trong form
    checkValidateInput() {
            try {
                let isValid = true
                    // lấy tất cả field có điều kiện required
                let requiredItems = $('.ms-dialog-form').find('[required]');

                for (var item of requiredItems) {
                    item = $(item);
                    if (item.attr('value') || item.val()) {
                        item.removeClass('ms-valid-error-input');
                    } else {
                        isValid = false;
                        item.addClass('ms-valid-error-input');
                    }
                }

                // lấy tất cả field có điều kiện isEmail
                let EmailItems = $('.ms-dialog-form').find('[isEmail]');

                for (var item of EmailItems) {
                    item = $(item);
                    // kiểm tra định dạng email
                    const regexEmail = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
                    const value = item.val();

                    if (regexEmail.test(String(value).toLowerCase())) {
                        item.removeClass('ms-valid-error-input');
                    } else {
                        isValid = false;
                        item.addClass('ms-valid-error-input');
                    }
                }

                // lấy tất cả field có điều kiện formatVND
                let VNDItems = $('.ms-dialog-form').find('[formatVND]');

                for (var item of VNDItems) {
                    item = $(item);
                    // chuyển đổi text dạng  tiền (VND) sang text dạng số 
                    let textVND = formatVNDToText(item.val())
                    if (textVND) {
                        item.removeClass('ms-valid-error-input');
                    } else {
                        isValid = false;
                        item.addClass('ms-valid-error-input');
                    }
                }
                return isValid;
                //
            } catch (error) {
                alert("Có lỗi xảy ra");
                console.log(error);
            }
        }
        //sự kiện khi trường tiền được nhập từ bàn phím
    inputMoneyPress() {
            let item = $(this);
            // đổi dạng VND sang text number
            item.val(Common.formatVNDToText(item.val()))
                // thay đổi giá trị hiển thị sang VND
            let textVND = Common.formatTextToVND(item.val())
            if (textVND) {
                item.val(textVND)
                item.removeClass('ms-valid-error-input');
            } else {
                item.addClass('ms-valid-error-input');
            }
        }
        // lấy dữ liệu trong form

    getFieldForm() {
            let form = $("#dialog-add-employee .ms-dialog-form");

            let EmployeeCode = form.find('[name=EmployeeCode]').val();
            let FullName = form.find('[name=FullName]').val();
            let DateOfBirth = Common.formatDateJS(form.find('[name=DateOfBirth]').val());
            let Gender = form.find('[name=Gender]').attr('value');
            let IdentityNumber = form.find('[name=IdentityNumber]').val();
            let IdentityDate = form.find('[name=IdentityDate]').val();
            let IdentityPlace = form.find('[name=IdentityPlace]').val();
            let Email = form.find('[name=Email]').val();
            let PhoneNumber = form.find('[name=PhoneNumber]').val();
            let PositionId = form.find('[name=PositionId]').attr('value');
            let DepartmentId = form.find('[name=DepartmentId]').attr('value');
            let PersonalTaxCode = form.find('[name=PersonalTaxCode]').val();
            let Salary = parseFloat(form.find('[name=Salary]').val().replaceAll('.', ''));
            let CreatedDate = form.find('[name=CreatedDate]').val();
            let WorkStatus = form.find('[name=WorkStatus]').attr('value');

            const newData = {

                EmployeeCode: EmployeeCode,
                FullName: FullName,
                Gender: Gender, // 1 : nam ,  0 : nữ
                DateOfBirth: DateOfBirth,
                PhoneNumber: PhoneNumber,
                Email: Email,
                IdentityNumber: IdentityNumber,
                IdentityDate: IdentityDate,
                IdentityPlace: IdentityPlace,
                PositionId: PositionId,
                DepartmentId: DepartmentId,
                PersonalTaxCode: PersonalTaxCode,
                Salary: Salary,
                CreatedDate: Common.formatDateJS(CreatedDate),
                WorkStatus: WorkStatus // 0: chuẩn bị làm việc, 1: đang làm việc, 2: đã nghỉ việc

            };
            console.log(newData);
            return newData;
        }
        // tạo dữ liệu nhân viên mới qua API
    sendDataCreate() {
            const DataCreate = this.getFieldForm();
            $.ajax({
                url: "http://cukcuk.manhnv.net/api/v1/Employees",
                method: "POST",
                data: JSON.stringify(DataCreate),
                dataType: "JSON",
                contentType: "application/json",
                async: false
            }).done(function(res) {
                console.log(res);
                Toast.addNewMessage("Thêm thành công", 1);
                Common.hide_dialog();

            }).fail(function(res) {
                alert("Có lỗi xảy ra: " + res.responseText);
            })
        }
        // gửi dữ liệu đã sửa qua API
    sendDataEdit() {
        let DataEdit = this.getFieldForm();
        DataEdit.EmployeeId = this.EmployeeIdEdit;
        $.ajax({
            url: `http://cukcuk.manhnv.net/api/v1/Employees/${DataEdit.EmployeeId}`,
            method: "PUT",
            data: JSON.stringify(DataEdit),
            dataType: "JSON",
            contentType: "application/json",
            async: false
        }).done(function(res) {
            // console.log(res);
            Toast.addNewMessage("Sửa thành công!", 1);
            Common.hide_dialog();
            Common.loaddata();
        }).fail(function(res) {
            alert("Có lỗi xảy ra: " + res.responseText);
            console.log('loi');
            console.log(res);
        })
    }
}