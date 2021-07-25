$(document).ready(function () {
    $('#login-button').on('click', function (e) {
        console.log("im here")
        var username = $.trim($('#id_username').val());
        var pwd = $.trim($('#id_password').val());
        $.ajax({
            url : "/users/login",
            data : {
                username: username,
                pwd : pwd
            }
        }).done(function(data) {
            if (data == "loggedin") {
                console.log("logged in")
                window.location = window.location;
            }
            else if (data == "badcredentials") {
                document.getElementById('login-error').style.display = "block"
                $( "#login-error" ).effect( "shake",{ direction: "left", times: 3, distance: 1} );
            }
        });
     })

})
