
// we cannot use for that beacause @categoryId in is not geted in editproductfine so
//we use direect in html file scripte 
$(document).ready(function () {
    var categoryId = '@categoryId';
    console.log(categoryId);
    var selectedCategoryId = Number(categoryId);

    $.getJSON('@Url.Action("GetCategories", "Product")', function (data) {
        var ddl = $('#CategoryId');
        ddl.empty();
        ddl.append($('<option></option>').val("").html("Select Category"));

        $.each(data, function (i, category) {
            var option = $('<option></option>').val(category.id).html(category.name);
            if (category.id == selectedCategoryId) {
                option.attr("selected", "selected");
            }

            ddl.append(option);
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