function showToast(message) {
    console.log("show Success Toast");
    var notification = document.querySelector('#toastSuccess');
    var toastMessage = document.querySelector('#toastMessage');
    toastMessage.textContent = message;
    notification.classList.add('show');

    // Tự động ẩn thông báo sau 2 giây
    setTimeout(function () {
        notification.classList.remove('show');
    }, 2000);
}

function showErrorToast(message) {
    console.log("show Error Toast");
    var notificationError = document.querySelector('#toastError');
    var toastMessageError = document.querySelector('#toastMessageError');
    toastMessageError.textContent = message;
    notificationError.classList.add('show');

    // Tự động ẩn thông báo sau 3 giây
    setTimeout(function () {
        notificationError.classList.remove('show');
    }, 3000);
}