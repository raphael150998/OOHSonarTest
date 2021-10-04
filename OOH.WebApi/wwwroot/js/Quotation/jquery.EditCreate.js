var lstCaraDetalle = [];
$().ready(function ($) {
    DropDownListClientes();
    DropDownListAgencias();
    Validate.Form("#formQuotation", "api/Quotation/CEdata", {

        rules: {

        },
        messages: {

        }

    });
});
function DropDownListClientes() {

    fns.CallGetAsync("api/Client/Get", null, function (dataResult) {
        let select = `<select class="js-example-basic-single" id="dropdownClient" name="ClientId" >`

        dataResult.forEach(cli => {
            let option = `<option value="` + cli.clienteId + `"> ` + cli.nombreComercial + `</option> `;

            select = select + option;
        });
        select = select + "</select>";

        $("#divClient").html(select);
        $('#dropdownClient').select2();

    });

}

function DropDownListAgencias() {

    fns.CallGetAsync("api/agency/Select", null, function (dataResult) {
        let select = `<select class="js-example-basic-single" id="dropdownAgencia" name="AgenciaId" >`

        dataResult.forEach(agn => {
            let option = `<option value="` + agn.agenciaId + `"> ` + agn.nombre + `</option> `;

            select = select + option;
        });
        select = select + "</select>";

        $("#divAgenci").html(select);
        $('#dropdownAgencia').select2();

    });

}

function addCara(idCara) {
    $("#ModalCaras").modal("show");
    DatatableAdd();
}

function GetCarasCotizacion() {

    fns.CallGetAsync("api/facequotation/get", null, function (request) {
        console.log(request);
        $("#addCarasTable").DataTable().clear();
        $("#addCarasTable").DataTable().rows.add(request).draw();

    });

}
function DatatableAdd() {

    DataTableHelper.Draw("#addCarasTable", {
        destroy: true,
        dom: "frltip",
        orderCellsTop: true,
        fixedHeader: true,
        data: [],
        columns: [
            {
                data: "caraId ",
                render: function (data, type, full, meta) {
                    return `<button class="btn btn-primary btn-sm" onclick="addMdetalle(`+data+`)">agregar</button>`;
                }
            },
            {
                data:"codigo"
            },
            {
                data:"categoria"
            },
            {
                data:"referenciaComercial"
            },
            {
                data:"tipo"
            }
            
        ],
        "language": {
        "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
    }
    });
    GetCarasCotizacion();
}
function addMdetalle(IdCara) {
    var idcaraSend = { Id: IdCara };
    fns.PostDataAsync("api/caras/get", JSON.stringify(idcaraSend), function (caraRequest) {


         


    });
}