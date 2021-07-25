const csrf_token = document.getElementsByName("csrfmiddlewaretoken")[0].value
$(".alert").hide()
$(document).ready(function () {
    $(".add_to_cart_button").click(function (e) {
        e.preventDefault()
        let status
        if(window.location.href == "http://127.0.0.1:8000/"){
            let position = e.target.parentNode.parentNode.parentNode.parentNode.childNodes[1].innerText
            if(position == "Top Sellers"){
                status = document.getElementById("alert-div-above")
            }
            else if(position == "Latest Releases"){
                status = document.getElementById("alert-div-middle")
            }
            else{
                status = document.getElementById("alert-div-below")
            }
        }
        else{
            status = document.getElementById("status-"+e.target.id)
        }
        message = document.createElement('p')
        message.className = "alert-link"
        $.ajax({
            headers:{ "X-CSRFToken": csrf_token } ,
            type:'POST',
            url: '/carts/add',
            data: {'id': e.target.id}

        }).done(function (data) {

            if (data) {
                if(window.location.href == "http://127.0.0.1:8000/"){
                    if(status.childNodes.length > 0){
                        status.removeChild(status.childNodes[0])
                    }
                }
                status.style.display = "block"
                if(data == "1"){
                    message.innerText = 'Added to cart'
                }
                else{
                    if(status.childNodes.length > 0){
                        status.removeChild(status.childNodes[0])
                    }

                    message.innerText = 'Added to cart (' + data + ')'
                }

                status.appendChild(message)
                console.log("Cart Updated")
                function sleeper(){
                    $(status).fadeOut(2000)
                }
                setTimeout(sleeper, 1500);
            }
        })
    })
})
