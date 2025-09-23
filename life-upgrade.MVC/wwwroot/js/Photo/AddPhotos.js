$(document).ready(function(){
    let input = $("#file-input")[0];
    let previewContainer = $("#preview-image-container");

    input.addEventListener("change", function(event){
        if (event.target.files.length > 0) {
            handleFilesPreview(event.target.files);
            previewContainer.empty();
        }
        
    })

    let handleFilesPreview = function (files) {
        for (let i = 0; i < files.length; i++) {
            const reader = new FileReader();
            reader.onload = function(event) {

                let div = document.createElement("div");
                div.classList.add("card", "border-secondary", "mb-3");
                let img = document.createElement("img");
                img.classList.add("card-img-bottom");
                img.src = event.target.result.toString();
                div.appendChild(img)
                previewContainer[0].append(div);
            }
            reader.readAsDataURL(files[i]);
        }
    }
    
    $("#button-create").click(function () {
        let form = $("#addPhotos");
        let formData = new FormData(form[0]);
        formData.append('existingPhotosCount', $('#photos-modal').children('.card').length)

        $.ajax({
            url: form.attr('action'),
            type: form.attr('method'),
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