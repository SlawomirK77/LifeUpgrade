$(document).ready(function(){
    
    LoadProductWebShops();
    LoadProductPhotos();
    LoadProductRating();
    
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
                toastr["error"]("addWebShopModal went wrong")
            }
        })
    });

    $("#addPhotoModal form").submit(function (event) {
        event.preventDefault();
        let formData = new FormData(this);
        let currentFormFile = $(this)[0].querySelector("#ImageFile");
        
        let formFileCollection = document.createElement("input");
        formFileCollection.type = "file";
        formFileCollection.multiple = true;
        formFileCollection.id = "ImageFiles"
        formData.append(formFileCollection.id,currentFormFile.files[0], currentFormFile.files[0].name);

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
                toastr["error"]("addPhotoModal went wrong")
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
            formData.set("rating", event.currentTarget.control.value);

            $.ajax({
                url: "/Product/Rating",
                type: "POST",
                data: formData,
                processData: false,
                contentType: false,
                success: function () {
                    toastr["success"]("Product rated successfully")
                    LoadProductRating();
                },
                error: function () {
                    toastr["error"]("Something went wrong with product rating")
                }
            })
        });
    });
});