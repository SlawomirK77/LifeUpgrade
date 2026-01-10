$(document).ready(function(){
    const container = $("#photos");
    container.on("click","#change-order","click",function (event) {
        SetCurrentPhotosOrder();
    });
})