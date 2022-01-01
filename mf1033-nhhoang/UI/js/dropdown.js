$(document).ready(function() {
    let dropdown = new Dropdown();
    // load dữ liệu cho dropdown
    dropdown.loadDataDropdown();
    // tạo sự kiện lựa click dropdown
    $(".ms-dropdown").click(dropdown.dropdownOnClick);

})
class Dropdown {

    //ẩn/hiện các item của dropdown
    dropdownOnClick() {
        let btnDropdown = $(this);
        btnDropdown.children('.ms-all-item-dropdown').toggle();
    }

    // thay đổi giá trị của dropdown
    selectValueDropdown() {
        try {
            let itemSelected = $(this);
            // lấy giá trị của item
            let value = itemSelected.attr('value');
            let text = itemSelected.text();

            // gán giá trị của item cho dropdown
            let boxDropdown = itemSelected.parents('.ms-dropdown');
            boxDropdown.attr('value', value);

            // sửa giá trị hiển thị của dropdown
            let contentDropdown = boxDropdown.children('.ms-content-dropdown');
            contentDropdown.text(text);

            // chỉnh sửa màu của các item
            let items = itemSelected.siblings();
            for (const item of items) {
                $(item).removeClass('selected');
            }
            itemSelected.addClass('selected');
        } catch (error) {
            console.log(error);
        }
    }

    // lấy dữ liệu từ API và nạp vào dropdown
    loadDataDropdown(elementHTML) {
        try {
            if (!elementHTML) elementHTML = "body .ms-dropdown";
            // lọc dropdown
            let dropdowns = $(elementHTML);

            // lưu những dữ liệu đã gọi
            var ListData = {};
            // lấy dữ liệu cho từng phần tử
            for (const dropdown of dropdowns) {
                // lấy loại dữ liệu cần gọi
                let data = $(dropdown).attr('data');
                // lấy giá trị của các item 
                let valueItem = $(dropdown).attr('valueItem');
                // lấy tên hiển thị của các item
                let textItem = $(dropdown).attr('textItem');

                // kiểm tra việc khai báo các giá trị attr
                if (data && valueItem && textItem) {
                    // kiểm tra dữ liệu đã gọi trước đó nếu chưa có thì gọi API mới
                    if (!ListData[data]) {
                        // gọi API lấy dữ liệu
                        $.ajax({
                            method: "GET",
                            url: ConstantsAPI[data].GetAll,
                            data: null,
                            dataType: "json",
                            contentType: "application/json",
                            async: false,
                            success: function(response) {
                                // lưu lại các giá trị API đã gọi để sử dụng lại
                                ListData[data] = response;

                            },
                            error: function(response) {
                                console.log(response);
                                return;
                            }
                        });
                    }
                    // lấy dữ liệu đã lưu và tạo HTML

                    let itemsHTML = ""
                    for (const item of ListData[data]) {
                        itemsHTML += `<div class="ms-item-dropdown" value=${item[valueItem]}>${item[textItem]}</div>`;

                    }
                    // nạp mã HTML vào dropdown
                    $(dropdown).find('.ms-all-item-dropdown').append(itemsHTML);
                }
            }
            // tạo sự kiện lựa chọn giá trị dropdown
            $(".ms-item-dropdown").click(this.selectValueDropdown);
        } catch (error) {
            console.log(error);
        }
    }
}