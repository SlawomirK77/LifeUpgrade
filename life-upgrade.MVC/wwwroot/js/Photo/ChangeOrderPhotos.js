$(document).ready(function(){
    let bytes;

    const container = $("#photos");
    container.on("click","#change-order","click",function (event) {
        event.preventDefault();
        bytes = Array.from(container.children('.card')).map(x => {
            let src = x.lastElementChild.currentSrc;
            src = src.replace("data:image/*;base64,", "");
            return src;
        });

        let formData = new FormData();
        formData.set("photos", bytes);
        formData.set("photos", new Blob([bytes[0]], {type: "image/jpeg"}));

        $.ajax({
            url: `/Photo/ChangeOrder`,
            type: 'patch',
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                if (!data.length) {
                    container.html("There are no photos for this product")
                } else {
                    toastr["success"]("git gut");
                }
            },
            error: function () {
                toastr["error"]("Changing photos order went wrong")
            }
        });
    });
})