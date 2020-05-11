$(document).ready(function () {
    $(window).scroll(function () {
        var height = $(window).scrollTop();
        if (height > 100) {
            $('#button').fadeIn();
        } else {
            $('#button').hide();
        }
    });
    $("#button").click(function () {
        $('html, body').animate({
            scrollTop: $("#header").offset().top
        }, 2000);
    });

    // Handle Sign Up
    //Btn Sign Up Call
    $("#btnNext").on("click", function () {
        var url = "/Account/SignUp";
        var UserType = $("input[name='radio-group']:checked").val();
        var data = $("#btnNext").hasClass("isDisabled");
        if (!data) {
            $.ajax({
                url: url,
                type: 'GET',
                data: { UserType: UserType },
                success: function (response) {
                    $("#PartialVies").html(response);
                },
                error: function () {
                }
            });
        }
    });



    //====================================================================
    // Create Vendor
    $("#CreateVendor").on("click", function () {
        debugger
        var url = "/ManageAllUser/SignUp";
        var UserType = "Vendor"
        $.ajax({
            url: url,
            type: 'GET',
            data: { UserType: UserType},
            success: function (response) {
                $("#exampleModalLongTitle").html("Create Vendor");
                $("#CreateModal").html(response);
                $("#GenericModal").modal('show');
            },
            error: function () {
            }
        });
    });


    // Create Vendor
    $("#CreateDriver").on("click", function () {
        debugger
        var url = "/ManageAllUser/SignUp";
        var UserType = "Diver"
        $.ajax({
            url: url,
            type: 'GET',
            data: { UserType: UserType },
            success: function (response) {
                $("#exampleModalLongTitle").html("Create Vendor");
                $("#CreateModal").html(response);
                $("#GenericModal").modal('show');
            },
            error: function () {
            }
        });
    });


    // Create Vendor
    $("#CreateUser").on("click", function () {
        debugger
        var url = "/ManageAllUser/SignUp";
        var UserType = "User"
        $.ajax({
            url: url,
            type: 'GET',
            data: { UserType: UserType },
            success: function (response) {
                $("#exampleModalLongTitle").html("Create Vendor");
                $("#CreateModal").html(response);
                $("#GenericModal").modal('show');
            },
            error: function () {
            }
        });
    });



    //=====================================================================




    //====================================================================
    // Delete Vendor
    $("#TabVendor").delegate("#js-delete", "click", function (e) {
        e.preventDefault();
        var btn = $(this);
        var url = btn.attr('href');
        debugger
        DeleteData(url);
    });

    // Delete User
    $("#TabUser").delegate("#js-delete", "click", function (e) {
        e.preventDefault();
        var btn = $(this);
        var url = btn.attr('href');
        debugger
        DeleteData(url);
    });


    // Delete Driver
    $("#TabDriver").delegate("#js-delete", "click", function (e) {
        e.preventDefault();
        var btn = $(this);
        var url = btn.attr('href');
        debugger
        DeleteData(url);
    });
    //====================================================================


    //Btn Sign Up Call
    $("#btnSignUp").on("click", function () {
        window.location.href = "/Account/HandleSignUp";
    });

    //Btn Login Call
    $("#btnLogin").on("click", function () {
        debugger
        var url = "/Account/SignIn";
        $.ajax({
            url: url,
            type: 'GET',
            success: function (response) {
                $("#exampleModalLongTitle").html("Sign In");
                $("#CreateModal").html(response);
                $("#GenericModal").modal('show');
            },
            error: function () {

            }
        });

    });



    //===============================================================

    //Btn Add Location Call
    $("#TabVendor").delegate("#js-AddLocation", "click", function (e) {
        e.preventDefault();
        debugger
        var btn = $(this);
        var VendorId = btn.attr('data-id');
        $.ajax({
            url: '/ManageAllUser/Addlocation/',
            type: 'GET',
            data: { VendorId: VendorId },
            success: function (response) {
                $("#exampleModalLongTitle").html("Sign In");
                $("#CreateModal").html(response);
                $("#GenericModal").modal('show');
            },
            error: function () {

            }
        });

    });

    //=================================================================


    //Btn Vendor Location 
    $("#TabVendor").delegate("#js-VendorLocation", "click", function (e)
    {
        e.preventDefault();
        debugger
        var btn = $(this);
        var VendorId = btn.attr('data-id');
        $.ajax({
            url: '/ManageAllUser/ListOfLocation/',
            type: 'GET',
            data: { VendorId: VendorId},
            success: function (response) {
                $("#exampleModalLongTitle").html("List Of Other Location");
                $("#CreateModal").html(response);
                $("#GenericModal").modal('show');
            },
            error: function () {

            }
        });

    });

    //=================================================================

});


DeleteData = function (url) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this record!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes",
        cancelButtonText: "No",
        closeOnConfirm: true,
        closeOnCancel: false
    },
        function (isConfirm) {
            if (isConfirm) {
                debugger
                $("#Loader1").addClass("d-block");
                $.ajax({
                    type: 'POST',
                    url: url,
                    success: function (data) {
                        if (data.status == true) {
                            $("#Loader1").removeClass("d-block");
                            swal("Deleted!", "Your record has been deleted.", "success");
                            window.location.reload();

                        }
                    },
                    error: function (data) {
                        $("#Loader1").removeClass("d-block");
                        swal("NOT Deleted!", "Something blew up.", "error");
                    }
                });
            } else {
                $("#Loader1").removeClass("d-block");
                swal("Cancelled", "Your record is safe", "error");
            }
        });
    return false;
};



$(document).ready(function () {
    var modal = document.getElementById("apmodal");
    var btn = document.getElementById("myBtn");
    var span = document.getElementsByClassName("close")[0];
    btn.onclick = function () {
        modal.style.display = "block";
    }

    span.onclick = function () {
        modal.style.display = "none";
    }
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }
});






