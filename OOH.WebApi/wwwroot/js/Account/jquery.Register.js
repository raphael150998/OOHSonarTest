$('#BtnRegister').click(function () {
 
    var Registro = {
        UserId: 0,
        Username: $("#Username").val(),
        Login: $("#Login").val(),
        Pass: $("#Pass").val(),
        Correo: $("#Correo").val(),
        Perfil: 0,
        Activo: true
    }
    if (Registro.Pass != $("#PassConfirm").val()) {
        $("#txtError").attr("hidden", false);
        $("#txtError").text("La contraseña no coincide");
    } else {

        fns.PostDataAsync("api/Account/Register", Registro, function (dataRequest) {
            if (!dataRequest["State"]) {
                switch (dataRequest["Data"]) {
                    case 1:
                        $("#txtError").attr("hidden", false);
                        $("#txtError").text(dataRequest["Message"]);
                        break
                    case 2:
                        $("#errorPass").attr("hidden", false);
                        $("#errorPass").text(dataRequest["Message"]);
                        break;
                    default:
                        break;
                }
            } else {
                $(location).attr("href", "/Account/Login");
            }

        });
    }


});