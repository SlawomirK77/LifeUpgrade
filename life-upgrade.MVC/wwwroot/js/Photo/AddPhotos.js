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

    // $("#delete-button").click(function (event) {
    //     event.preventDefault();
    //     $.ajax({
    //         url: event.currentTarget.attributes['formaction'].value,
    //         type: 'delete',
    //         data:{ photoGuids: itemsToDelete },
    //         success: function (data) {
    //             toastr["success"]("Deleted selected images")
    //             LoadProductPhotos();
    //         },
    //         error: function () {
    //             toastr["error"]("Something went wrong")
    //         }
    //     })
    // })
});