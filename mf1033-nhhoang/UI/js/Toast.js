class Toast {
    static addNewMessage(textMess, type) {
        let typeToast = "";

        // kiểm tra dạng toast
        switch (type) {
            case Enum.Toast.Success:
                typeToast = "ms-success"
                break;
            case Enum.Toast.Info:
                typeToast = "ms-info"
                break;
            case Enum.Toast.Warning:
                typeToast = "ms-warning"
                break;
            case Enum.Toast.Error:
                typeToast = "ms-error"
                break;
            default:
                break;
        }
        // tạo mã html cho toast
        let idObj = Math.floor(Math.random() * 10000000);
        let objHTML = $(`<div id="${idObj}" class="ms-toast-mess ${typeToast}">
                        <div class="ms-toast-icon "></div>
                        <div class="ms-toast-content">${textMess}</div>
                        <div class="ms-toast-btn-close">
                            <i class="fas fa-times"></i>
                        </div>
                    </div>`)

        // hiển thị toast
        $('#ms-toast-box').append(objHTML);
        setTimeout(function() {
            $(`#${idObj}`).remove();

        }, 3000)
    }
}