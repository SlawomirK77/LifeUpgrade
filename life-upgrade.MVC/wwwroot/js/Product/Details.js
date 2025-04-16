$(document).ready(function(){
    
    LoadProductWebShops();
    
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
});