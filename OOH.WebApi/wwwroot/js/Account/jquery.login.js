$('#btnLogin').click(function () {

    var UserLoginDto = {
        Login: $("#Login").val(),
        Pass: $("#Pass").val()
    }
    UserLoginDto = $.parseJSON(UserLoginDto);
    console.log(UserLoginDto);
    fns.PostDataAsync("api/login/Validate", UserLoginDto, function (DataRequest) {
        if (DataRequest["State"] == false) {

            $("#txtError").attr("hidden", false);
            $("#txtError").text(DataRequest["Message"]);
          
        }     
    });
});