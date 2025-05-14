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
        let formData = new FormData(this);

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

    document.querySelectorAll('.star-rating:not(.readonly) label').forEach(star => {
        star.addEventListener('click', function(event) {
            this.style.transform = 'scale(1.2)';
            setTimeout(() => {
                this.style.transform = 'scale(1)';
            }, 200);
            let formData = new FormData(document.querySelector("#ratingForm"));
            formData.set("Rating", event.currentTarget.control.value);

            $.ajax({
                url: "/Product/Rating",
                type: "POST",
                data: formData,
                processData: false,
                contentType: false,
                success: function () {
                    toastr["success"]("Product rated successfully")
                },
                error: function () {
                    toastr["error"]("Something went wrong with product rating")
                }
            })
        });
    });
});