const SetCurrentPhotosOrder = () => {
    let photosGuids = [];
    const container = $("#photos");
    
    Array.from(container.children('.card')).forEach(x => {
        photosGuids.push(x.attributes['id'].value);
    });
    
    $.ajax({
        url: `/Photo/ChangeOrder`,
        type: 'patch',
        data: {photosGuids: photosGuids},
        success: function () {
            toastr["success"]("order changed successfully.");
        },
        error: function () {
            toastr["error"]("Changing photos order went wrong")
        }
    });
}