$(document).ready(function () {
    $('#register-button').on('click', function (e) {
        console.log("im here in register")
        let username = document.getElementById('username').value
        let first_name = $.trim($('#first_name').val());
        let last_name = $.trim($('#last_name').val());
        let email = $.trim($('#email').val());
        let pwd1 = $.trim($('#password1').val());
        let pwd2 = $.trim($('#password2').val());
        let csrf_token = document.getElementsByName('csrfmiddlewaretoken')[0].value

        $.ajax({
            headers: { "X-CSRFToken": csrf_token },
            url : "/users/register",
            type: 'POST',
            data : {
                username: username,
                first_name: first_name,
                last_name: last_name,
                email: email,
                password1: pwd1,
                password2 : pwd2
            }

        }).done(function(data) {
            if (data == "loggedin") {
                alert('Account Created')
                window.location = 'users/profile';
            }
            else {
                let error_fields = document.getElementsByClassName('error-field');

                for (let i = 0; i < error_fields.length; i++) {
                    console.log(error_fields[i])
                    error_fields[i].style.display = "none";
                }

                for (let error in data) {

                    let field_error = document.getElementById("error_id_" + error);
                    field_error.innerHTML= data[error][0]
                    field_error.style.display = "block";
                }

            }
        });
    })
})
