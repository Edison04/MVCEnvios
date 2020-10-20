function loginUser() {
    $.ajax(
        {
            "url": "http://localhost:55671//Home/Login",
            "type": "POST",
            "data": {
                'usuario': $('#usuario').val(),
                'password': $('#password').val()
            },
            "dataType": "json",                              //tipo archivo
            "success": function (dataServer) {
                //funcion que se debe ejecutar desde javascript
                if (dataServer.error) {
                    window.location.href = 'http://localhost:55671/Home/Error';
                } else {
                    window.location.href = 'http://localhost:55671/Home/Index';
                }
            },
            "error": function () {
                //hacer algo
                alert("Error!!");
            }
        }
    );
}