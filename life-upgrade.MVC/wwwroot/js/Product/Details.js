$(document).ready(function(){
    
    LoadProductWebShops();
    LoadProductPhotos();
    
    $("#addWebShopModal form").submit(function (event) {
        event.preventDefault();
        
        $.ajax({
            url: $(this).attr('action'),
            type: $(this).attr('method'),
            data: $(this).serialize(),
            success: function (data) {
                toastr["success"]("Added web shop")
                LoadProductWebShops();
            },
            error: function () {
                toastr["error"]("Something went wrong")
            }
        })
    });

    $("#addPhotoModal form").submit(function (event) {
        event.preventDefault();
        var formData = new FormData(this);

        $.ajax({
            url: $(this).attr('action'),
            type: $(this).attr('method'),
            data: formData,
            enctype: 'multipart/form-data',
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                toastr["success"]("Added photo")
                LoadProductPhotos();
            },
            error: function () {
                toastr["error"]("Something went wrong")
            }
        })
    });
});