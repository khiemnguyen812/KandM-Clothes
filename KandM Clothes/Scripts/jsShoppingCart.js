$(document).ready(function () {
    ShowCount();
    $("body").on("click", ".add_to_cart_button", function (e) {
        e.preventDefault();
        var id = $(this).data("id");
        var quantity = parseInt($('#quantity_value').text());
        if (quantity === null || isNaN(quantity)) {
            quantity = 1;
        }

        $.ajax({
            url: "/ShoppingCart/AddToCart",
            data: {
                id: id,
                quantity: quantity
            },
            type: "POST",
            success: function (response) {
                ShowCount();
                showToast("Added to cart successfully!");
            }
        });
    });
});

function ShowCount() {
    $.ajax({
        url: "/ShoppingCart/ShowCount",
        type: "GET",
        success: function (response) {
            $("#checkout_items").text(response.count);
        }
    });
}
