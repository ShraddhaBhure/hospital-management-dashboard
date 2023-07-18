function ShowDeleteConfirmation(url) {
    debugger
    $("#btnContinueDelete").attr("href", url);
}

function ShowDeleteConfirmationWithResponse(url, Id, UrlList) {
    debugger
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover your profile!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    type: "GET",
                    data: { Id: Id },
                    url: url,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response == true) {
                            swal({
                                title: "Not Deleted!",
                                text: "It is in use.",
                                type: "success",
                                timer: 2000
                            });

                            //swal("Not Deleted! Customer is in use.", {
                            //    icon: "success",

                            //});
                        }
                        else if (response == false) {
                            swal({
                                title: "Deleted!",
                                text: "Your row has been deleted.",
                                type: "success",
                                timer: 2000
                            });
                        }

                        window.location.href = UrlList;
                    },
                    failure: function (response) {
                    },
                    error: function (response) {
                    }
                });

            }
            else {
                swal("Cancelled", "Your imaginary file is safe :)", "error");
            }
        });
}