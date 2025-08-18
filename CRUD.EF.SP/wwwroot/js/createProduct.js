$(document).ready(function () {
    console.log("Hello")


    //$.ajax({
    //    url: '/Product/GetCategories',
    //    type: 'GET',
    //    success: function (response) {
    //        console.log(response);
    //    }
    //});
    $.getJSON('/Product/GetCategories', function (data) {
        var ddl = $('#CategoryId');
        ddl.empty();
        $.each(data, function (i, category) {
            ddl.append($('<option></option>').val(category.id).html(category.name));
        });
    });

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