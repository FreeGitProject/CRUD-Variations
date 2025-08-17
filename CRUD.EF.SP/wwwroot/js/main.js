$(document).ready(function () {
    let typingTimer;
    const delay = 400; // wait a little after typing stops

    $("#ProductName").on("keyup", function () {
        clearTimeout(typingTimer);
        var name = $(this).val();

        if (name.length >= 3) {
            typingTimer = setTimeout(function () {
                //    console.log("hello");
                $.ajax({
                    url: '/Product/IsProductNameAvailable',
                    type: 'POST',
                    data: { name: name },
                    success: function (response) {
                        if (response) {
                            $('#NameValidationMessage').removeClass('text-success').addClass('text-danger');
                            $("#NameValidationMessage").text("Product name already exists.");
                            console.log("ProductExixt");
                        }
                        else {
                            $('#NameValidationMessage').removeClass('text-danger').addClass('text-success');
                            $("#NameValidationMessage").text("Product available to create.");
                        }
                           
                    }
                });
            }, delay);
        }


    });

});