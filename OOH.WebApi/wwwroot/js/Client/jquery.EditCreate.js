$().ready(function ($) {
    LLenarTextBox();
});

function LLenarTextBox() {
    var idCliente = $("#ClienteId").val();
    console.log(idCliente);
    fns.CallGetAsync("api/client/find", { id: idCliente }, function (dataResult) {

        console.log(dataResult);

    });

}