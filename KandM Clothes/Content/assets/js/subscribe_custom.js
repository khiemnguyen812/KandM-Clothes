$(document).ready(function () {
    $('#newsletter_submit').click(function () {
        var email = $('#newsletter_email').val();

        if (validateEmail(email)) {
            $.ajax({
                url: '/Subscribe/Subscribe',
                type: 'POST',
                data: { email: email },
                success: function (res) {
                    console.log(res);
                    if (res.success) {
                        showToast("Subscribe successfully");
                    } else {
                        showErrorToast(res.message);
                    }
                }
            });
        } else {
            showErrorToast("Invalid email address");
        }
    });

    function validateEmail(email) {
        var re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return re.test(email);
    }
});
