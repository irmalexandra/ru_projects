$(document).ready(function () {
    $('.return-button').on('click', function (e) {
        if(window.location.href.search("payment_information") != -1){
            let id = window.location.href.split("/")[5]
            window.location = "/carts/shipping_information/" + id
        }
        else if(window.location.href.search("shipping_information") != -1){
            window.location = "/carts"
        }
        else if(window.location.href.search("overview") != -1){
            let shipping_id = window.location.href.split("/")[5]
            let id = window.location.href.split("/")[6]
            window.location = "/carts/payment_information/"+shipping_id+"/"+id
        }
        else{
            window.history.back();
        }
    })
});