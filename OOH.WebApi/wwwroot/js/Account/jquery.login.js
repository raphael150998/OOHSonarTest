$('#btnLogin').click(function () {
    var UserLoginDto = {
        Login: $("#Login").val(),
        Pass: $("#Pass").val()
    }
   
    fns.PostDataAsync("api/login/Validate", UserLoginDto, function (DataRequest) {
        if (DataRequest["state"] == false) {
            console.log(DataRequest);
            $("#txtError").attr("hidden", false);
            $("#txtError").text(DataRequest["message"]);
          
        }     
    }); 
});