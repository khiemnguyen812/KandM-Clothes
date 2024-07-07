function showToast(message) {
    var notification = document.querySelector('.notification');
    var toastMessage = document.querySelector('#toastMessage');
    toastMessage.textContent = message;
    notification.classList.add('show');

    // Tự động ẩn thông báo sau 2 giây
    setTimeout(function () {
        notification.classList.remove('show');
    }, 2000);
}

function showErrorToast(message) {
    var notificationError = document.querySelector('.notification--error');
    var toastMessageError = document.querySelector('#toastMessageError');
    toastMessageError.textContent = message;
    notificationError.classList.add('show');

    // Tự động ẩn thông báo sau 3 giây
    setTimeout(function () {
        notificationError.classList.remove('show');
    }, 3000);
}