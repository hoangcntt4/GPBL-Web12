class Common {
    //đổi text dạng số sang text dạng tiền (VND)
    static formatTextToVND(text) {
            // nếu là dạng số thì trả về text định dạng VND, nếu không thì trả về null 
            let textNumber = parseFloat(text);
            if (textNumber) {
                // return textNumber.toLocaleString('it-IT', { style: 'currency', currency: 'VND' });
                return textNumber.toLocaleString('de-DE');
            }
            return null;
        }
        //đổi text dạng tiền (VND) sang text dạng số
    static formatVNDToText(textVND) {
            textVND = textVND.toLowerCase();
            let textNumber = textVND.replaceAll(',', '').replaceAll('.', '').replaceAll(" ", "").replaceAll('vnd', '')
            return textNumber;
        }
        //hiển thị cửa sổ dialog thêm nhân viên
    static show_dialog() {
            $("#dialog-add-employee").show();
        }
        //ẩn cửa sổ dialog thêm nhân viên
    static hide_dialog() {
            $("#dialog-add-employee").hide();
        }
        //chuyển định dạng Date sang dd/mm/yyyy
    static formatDate(DateOfBirth) {
        let BirthFormat = new Date(DateOfBirth);
        // thông tin ngày
        let day = BirthFormat.getDate();
        day = day < 10 ? `0${day}` : day;

        // thông tin tháng
        let month = BirthFormat.getMonth() + 1;
        month = month < 10 ? `0${month}` : month;

        // định dạng lại thông tin ngày sinh
        BirthFormat = `${day}/${month}/${BirthFormat.getFullYear()}`;

        return BirthFormat
    }
    static formatDateJS(DateOfBirth) {

        let BirthFormat = new Date(DateOfBirth);
        // thông tin ngày
        let day = BirthFormat.getDate();
        if (isNaN(day))
            return null;
        day = day < 10 ? `0${day}` : day;

        // thông tin tháng
        let month = BirthFormat.getMonth() + 1;
        month = month < 10 ? `0${month}` : month;

        // định dạng lại thông tin ngày sinh
        BirthFormat = `${BirthFormat.getFullYear()}-${month}-${day}`;

        return BirthFormat

    }
    static formatWorkStatus(status) {
            if (status == null) return "";
            switch (status) {
                case 0:
                    return "Đã nghỉ việc";
                case 1:
                    return "Đang làm việc";
            }
            return "Không rõ";
        }
        /**
         * lấy dữ liệu từ API, đưa dữ liệu vào bảng
         */
    static loaddata() {
            try {
                // let table =$('#table-data').children().remove()
                let table = $('#table-data');
                // xóa dữ liệu hiện có trên bảng
                table.find('tbody').children().remove();
                // lấy dữ liệu khai báo từ các cột để map với dữ liệu trả về
                let cols = table.find('thead tr').children();

                // lấy dữ liệu từ API và nạp vào bảng
                $.ajax({
                    url: "http://cukcuk.manhnv.net/api/v1/Employees",
                    method: "GET",
                    data: null,
                    dataType: "JSON",
                    contentType: "application/json",
                    async: false
                }).done(function(res) {
                    let objs = res;

                    for (let i = 0; i < objs.length; i++) {
                        const item = objs[i];
                        // tạo thẻ tr
                        var trbody = $("<tr></tr>");

                        // tạo dữ liệu cho từng dòng theo các cột
                        for (const col of cols) {
                            // lấy dữ liệu hiển thị(colData) từ các cột th 
                            let colData = $(col).attr('colData');
                            // lấy kiểu dữ liệu định dạng
                            let typeData = $(col).attr('typeData');
                            // lấy dữ liệu từ API trả về
                            let value = item[colData];
                            // định dạng dữ liệu theo kiểu(colData) đã khai báo
                            value = Common.formatTypeData(value, typeData);
                            // lưu giá trị title của cột
                            let title = value;
                            // định dạng dữ liệu sang HTML
                            value = Common.formatHTMLData(value, typeData, i + 1);
                            // chỉnh sửa hiển thị các giá trị null, undefined
                            if (!title) title = ""
                            if (!value) value = "";
                            // if (isNaN(value)) value = "";
                            // tạo cột HTML
                            trbody.append($(`<td title='${title}'>${value}</td>`));
                        }
                        // nạp dữ liệu vào bảng
                        $(table).find('tbody').append(trbody);
                        // lưu lại id của đối tượng
                        let keyRow = $(table).attr("keyRow");
                        $(trbody).data(keyRow, item[keyRow])
                    }
                }).fail(function(res) {
                    console.log(res);
                })
            } catch (error) {
                console.log(error);
            }
        }
        /**
         * định dạng lại  dữ liệu theo từng kiểu
         */
    static formatTypeData(value, type) {
            switch (type) {
                case "ddmmyyyy":
                    // định dạng ngày tháng
                    value = Common.formatDate(value);
                    break;
                case "Money":
                    // định dạng tiền
                    value = parseFloat(value)
                    if (value) {
                        value = value.toLocaleString('it-IT', { style: 'currency', currency: 'VND' });
                    }

                    break;
                case "WorkStatus":
                    value = this.formatWorkStatus(value);

                    break;
                default:
                    break;
            }
            return value;
        }
        /**
         * định dạng dữ liệu HTML
         */
    static formatHTMLData(value, type, stt) {
        switch (type) {
            case "stt":
                value = stt;
                break;
            case "checkbox":
                value = "<input type='checkbox'>";
                break;
            case "ddmmyyyy":
                // định dạng ngày tháng
                value = `<div class="align-center">${value}</div>`;
                break;
            case "Money":
                value = `<div class="align-right">${value}</div>`;

            default:
                break;
        }
        return value;
    }
}