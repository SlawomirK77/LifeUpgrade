$(document).ready(function(s){
    let container = $("#photos-modal");
    let itemsToDelete = [];

    container.on("click",".card-img-bottom","click",function (event) {
        event.preventDefault();
        
        if($(this).hasClass("selected")){
            $(this).removeClass("selected");
            itemsToDelete.splice(itemsToDelete.indexOf(event.target.offsetParent.id), 1);
        }
        else {
            $(this).addClass("selected");
            itemsToDelete.push(event.target.offsetParent.id);
        }
    });
    
    $("#delete-button").click(function (event) {
        event.preventDefault();
        $.ajax({
            url: event.target.attributes['formaction'].value,
            type: 'delete',
            data:{ photoGuids: itemsToDelete },
            success: function (data) {
                toastr["success"]("Deleted selected images")
                LoadProductPhotos();
            },
            error: function () {
                toastr["error"]("Something went wrong")
            }
        })
    })
});