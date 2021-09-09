//Document Ready
$().ready(function ($) {

    BuildDatatable();

    $("#modalAgency").modal({
        backdrop: "static",
        keyboard: false,
        show: false
    });

    $("#btnAdd").click(function () {
        $("#Active").changeSwitch(false);
        $('#frmAgency').trigger("reset");
        $("#modalAgency").modal("show");
    });

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
        $("#Active").changeSwitch(false);
        $('#frmAgency').trigger("reset");
        $("#modalAgency").modal("hide");
        refresh();
    });
});

//Llamada a la API de clientes para el llenado de la Dattable
function GetAgencies() {

    fns.CallGetAsync("api/agency/Select", null, function (dataResponse) {
        $("#agencyTable").DataTable().clear();
        $("#agencyTable").DataTable().rows.add(dataResponse).draw();
    });
}

function UpdateAgency(id) {
    fns.CallGetAsync(`api/agency/Find`, { id: id }, function (response) {
        $("#frmAgency").assignJsonToForm(response);
    })
    $("#modalAgency").modal("show");
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
                    return `<i onclick="UpdateAgency(${data})" class="fa fa-pencil-square btnDatatable text-primary"></i>
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