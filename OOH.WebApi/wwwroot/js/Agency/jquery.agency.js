//Document Ready
$().ready(function ($) {

    BuildDatatable();

    $("#exampleModalCenter").modal({
        backdrop: "static",
        keyboard: false,
        show: false
    });

    $("#btnAdd").click(function () {
        $("#exampleModalCenter").modal("show");
    });

    //var obj = { "Id": 0, "Name": "prueba data send 3", "Rate": 15 };

    Validate.Form("#frmAgency", "api/agency/CreateUpdate", {
        rules: {
            Name: {
                required: true
            },
            Rate: {
                required: true,
                number: true
            }
        },
        messages: {
            Name: {
                required: "Debes ingresar un nombre"
            },
            Rate: {
                required: "La comisión es requerida",
                number: "Solo se permiten números"
            }
        }
    }, function (data) {
        $('#frmAgency').trigger("reset");
        $("#exampleModalCenter").modal("hide");
        refresh();
    });
});

//Llamada a la API de clientes para el llenado de la Dattable
function GetAgencies() {

    fns.CallGetAsync("api/agency/GetList", null, function (dataResponse) {
        $("#agencyTable").DataTable().clear();
        $("#agencyTable").DataTable().rows.add(dataResponse).draw();
    });
}

//Metodo para crear el Datatable de clientes
function BuildDatatable() {
    DataTableHelper.Draw("#agencyTable", {
        destroy: true,
        orderCellsTop: true,
        fixedHeader: true,
        data: [],
        columns: [
            {
                data: "agenciaId",
                orderable: false,
                render: function (data, type, full, meta) {
                    return `<a href="/Agencies/CreateUpdate/${data}"><i class="fa fa-pencil-square btnDatatable text-primary"></i></a>
                            <i class="fa fa-trash btnDatatable text-danger"></i>`;

                }
            },
            {
                data: "agenciaId",
                render: function (data, type, full, meta) {
                    return "<a href='index'>" + data + "</a>";
                }
            },
            { data: "nombre" },
            { data: "comision" },

        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        }
    }).FilterColum();
    GetAgencies();
}
function refresh() {
    GetAgencies();
}