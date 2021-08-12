$('form').submit(function (e) {
    e.preventDefault();
    var LoginClass = {
        Login: $("#Login").val(),
        Password: $("#Password").val()
    }

    fns.PostDataAsync("api/login/Valid", LoginClass, function (DataRequest) {
        if (DataRequest["State"] == false) {
            console.log(DataRequest);

            $("#txtError").attr("hidden", false);
            $("#txtError").text(DataRequest["Message"]);
            return 0;
        }
        fns.PostDataAsync("Account/GuardarPermisos", DataRequest["Data"], function (dataMVC) {
            if (dataMVC == 1) {
                $(location).attr("href", "/Home/Index")
            }

        });
    });
});