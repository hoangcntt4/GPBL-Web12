$(document).ready(function() {
    let obj = new main();
    obj.initEvent();
})

class main {
    initEvent() {
        let objmain = this;
        // tạo sự kiện chọn 1 dòng trong  table
        $('#table-data').on('click', 'tbody tr', objmain.trDbClick.bind(this))

        // tạo sự kiện refresh dữ liệu table
        $('#btn-refresh').click(Common.loaddata)

        // nạp dữ liệu mẫu cho table
        Common.loaddata();

        // tắt button xóa dữ liệu
        objmain.DisplayBtnDelete()

        // tạo sự kiện bấm nút xóa
        $('#btn-delete').click(objmain.btnDeleteOnclick)
    }

    // đổi màu dòng được chọn
    trDbClick(parentevent) {
        let itemSelected = $(parentevent.currentTarget);

        if (itemSelected.hasClass('selected')) {
            // nếu item đã được chọn 
            itemSelected.removeClass('selected');
            itemSelected.find('input[type=checkbox]').prop('checked', false);
        } else {

            itemSelected.addClass('selected');
            itemSelected.find('input[type=checkbox]').prop('checked', true);
        }
        // kiểm tra điều kiện để bật nút xóa
        this.DisplayBtnDelete();


    }
    btnDeleteOnclick() {

            // tìm dòng được chọn

            var choice = confirm($(this).attr('data-confirm'));
            if (choice) {
                const trSelected = $('#table-data').find('tr.selected');

                if (trSelected.length > 0) {
                    for (var i = 0; i < trSelected.length; i++) {
                        let EmployeeId = $(trSelected[i]).data('EmployeeId');
                        $.ajax({
                            url: `http://cukcuk.manhnv.net/api/v1/Employees/${EmployeeId}`,
                            method: "DELETE",
                            data: null,
                            dataType: "JSON",
                            contentType: "application/json",
                            async: true
                        }).done(function(res) {
                            Toast.addNewMessage("Xóa thành công!", 1);
                            $('#btn-delete').parent().addClass('btn-delete-disabled');
                            Common.loaddata();
                        }).fail(function(res) {
                            alert("Có lỗi xảy ra:" + res.responseText);
                            console.log(res);
                        })
                        console.log(EmployeeId);
                        console.log(trSelected.length);
                    }
                }
            }

        }
        // kiểm tra điều kiện để bật nút xóa
    DisplayBtnDelete() {
        let itemsSelected = $('#table-data tr.selected');
        if (itemsSelected.length > 0) {
            $('#btn-delete').parent().removeClass('btn-delete-disabled');

        } else {
            $('#btn-delete').parent().addClass('btn-delete-disabled');

        }
    }
}