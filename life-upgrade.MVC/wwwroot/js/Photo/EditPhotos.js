$(document).ready(function(){
    let itemsToDelete = []; 

    $("#editPhotosModal img").click(function (event) {
        event.preventDefault();
        
        if($(this).hasClass("selected")){
            $(this).removeClass("selected");
            itemsToDelete.splice(itemsToDelete.indexOf(event.target.id), 1);
        }
        else {
            $(this).addClass("selected");
            itemsToDelete.push(event.target.id);
        }
        
        // $.ajax({
        //     url: $(this).attr('action'),
        //     type: $(this).attr('method'),
        //     data: $(this).serialize(),
        //     success: function (data) {
        //         toastr["success"]("Added web shop")
        //         LoadProductWebShops();
        //     },
        //     error: function () {
        //         toastr["error"]("Something went wrong")
        //     }
        // })
    });
});