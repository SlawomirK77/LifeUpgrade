$(document).ready(function(s){
    let input = $("#file-input")[0];
    let previewContainer = $("#preview-image-container");

    input.addEventListener("change", function(event){
        if (event.target.files.length > 0) {
            handleFilesPreview(event.target.files);
            previewContainer.empty();
        }
    })

    let handleFilesPreview = function (files) {
        for (var i = 0; i < files.length; i++) {
            const reader = new FileReader();
            reader.onload = function(event) {

                let div = document.createElement("div");
                div.classList.add("card-body");
                let img = document.createElement("img");
                img.classList.add("img-fluid");
                img.src = event.target.result.toString();

                previewContainer[0].append(div.appendChild(img));
            }
            reader.readAsDataURL(files[i]);
        }
    }
    
    $("#addPhotos form").submit(function (event) {
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
                toastr["success"]("Added multiple photos")
                LoadProductPhotos();
            },
            error: function () {
                toastr["error"]("adding multiple photos went wrong")
            }
        })
    });
});