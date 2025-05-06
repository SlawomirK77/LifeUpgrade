const RenderProductWebShops = (webshops, container) => {
    container.empty();

    for(const webshop of webshops) {
        container.append(`
                <div class="card border-secondary mb-3" style="max-width: 18rem;">
                    <div class="card-header">${webshop.country}</div>
                    <div class="card-body">
                        <h5 class="card-title">${webshop.name}</h5>
                    </div>
                </div>`)
    }
}

const LoadProductWebShops = () => {
    const container = $("#webshops")
    const productEncodedName = container.data("encodedName");

    $.ajax({
        url: `/Product/${productEncodedName}/WebShop`,
        type: 'get',
        success: function (data) {
            if (!data.length) {
                container.html("There are no webshops for this product")
            } else {
                RenderProductWebShops(data, container);
            }
        },
        error: function () {
            toastr["error"]("Something went wrong")
        }
    })
}

const RenderProductPhotos = (photos, container) => {
    container.empty();

    for(const photo of photos) {
        container.append(`
                <div class="card border-secondary mb-3" style="max-width: 18rem;">
                    <div class="card-header">${photo.description}</div>
                    <div class="card-body">
<!--                        <h5 class="card-title">${photo}</h5>-->
                        <img class="img-fluid" src="data:image/png;base64,${photo.bytes}" alt="image">
                    </div>
                </div>`)
    }
}

const LoadProductPhotos = () => {
    const container = $("#photos")
    const productEncodedName = container.data("encodedName");

    $.ajax({
        url: `/Product/${productEncodedName}/Photo`,
        type: 'get',
        success: function (data) {
            if (!data.length) {
                container.html("There are no photos for this product")
            } else {
                RenderProductPhotos(data, container);
            }
        },
        error: function () {
            toastr["error"]("Something went wrong")
        }
    })
}