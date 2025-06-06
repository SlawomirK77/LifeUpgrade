﻿const RenderProductWebShops = (webshops, container) => {
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
            toastr["error"]("LoadProductWebShops went wrong")
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
                        <img class="img-fluid" src="data:image/*;base64,${btoa(String.fromCharCode(...new Uint8Array(photo.bytes)))}" alt="image" id="${photo.id}">
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
                RenderProductPhotos(data, $("#photos-modal"));
            }
        },
        error: function () {
            toastr["error"]("LoadProductPhotos went wrong")
        }
    })
}

const RenderProductRating = (ratings, userId, container) => {
    container.empty();
    let rating = ratings.reduce((sum, summand) => sum + summand.rating, 0)/ratings.length;
    let userRating = ratings.find(x => x.userId === userId)?.rating ?? null;
    if (userRating)
    {
        $("#rating-form")[0].getElementsByTagName("input").namedItem("star" + userRating).checked = true;    
    }

    let ratingContainer = $("#rating-container");
    if (ratingContainer[0].children.length > 1) {
        ratingContainer[0].getElementsByTagName("h5")[0].innerHTML = `(${rating.toFixed(2)})`; 
    } else {
        ratingContainer.append(`
            <div style="max-width: 6rem;">
                    <h5 >(${rating})</h5>
            </div>`)
    }
    
}

const LoadProductRating = () => {
    const container = $("#rating")
    const productEncodedName = container.data("encodedName");
    const userId = container.data("userId");

    $.ajax({
        url: `/Product/${productEncodedName}/Rating`,
        type: 'get',
        success: function (data) {
            if (!data.length) {
                container.html("No rating yet")
            } else {
                RenderProductRating(data, userId, container);
            }
        },
        error: function () {
            toastr["error"]("LoadProductRating went wrong")
        }
    })
}