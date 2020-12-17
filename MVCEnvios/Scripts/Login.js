function loginUser() {
    $.ajax(
        {
            "url": "http://mvcenvios.azurewebsites.net/Home/Login",
            "type": "POST",
            "data": {
                'usuario': $('#usuario').val(),
                'password': $('#password').val()
            },
            "dataType": "json",                              //tipo archivo
            "success": function (dataServer) {
                //funcion que se debe ejecutar desde javascript
                if (dataServer.error) {
                    window.location.href = 'http://mvcenvios.azurewebsites.net/Home/Error';
                } else {
                    window.location.href = 'http://mvcenvios.azurewebsites.net/Home/Index';
                }
            },
            "error": function () {
                //hacer algo
                alert("Error!!");
            }
        }
    );
}