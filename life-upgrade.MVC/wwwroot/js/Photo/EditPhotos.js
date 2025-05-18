$(document).ready(function(s){
    let container = $("#photos-modal");
    let itemsToDelete = [];
    LoadProductPhotos();

    container.on("click","img.img-fluid","click",function (event) {
        event.preventDefault();
        
        if($(this).hasClass("selected")){
            $(this).removeClass("selected");
            itemsToDelete.splice(itemsToDelete.indexOf(event.target.id), 1);
        }
        else {
            $(this).addClass("selected");
            itemsToDelete.push(event.target.id);
        }
    });
    
    $("#delete-button").click(function (event) {
        event.preventDefault();
        $.ajax({
            url: event.currentTarget.attributes['formaction'].value,
            type: 'delete',
            data:{ photoGuids: itemsToDelete },
            success: function (data) {
                toastr["success"]("Deleted selected images")
                LoadProductPhotos();
                LoadProductPhotos();
            },
            error: function () {
                toastr["error"]("Something went wrong")
            }
        })
    })
});