$().ready(function ($) {


});

function LLenarDatatable() {
    fns.CallGetAsync("api/contacts/list", null, function (dataRequest) {
        $("#tablaContact").DataTable().clear();
        $("#tablaContact").DataTable().rows.add(dataResponse).draw();

    });
}

function BuildDatatable() {

    DatatableHelper("");


}