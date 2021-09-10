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
                number: true,
                min: 0,
                max: 100
            }
        },
        messages: {
            Name: {
                required: "Debes ingresar un nombre"
            },
            Rate: {
                required: "La comisión es requerida",
                number: "Solo se permiten números",
                min: "La comisión debe ser mayor o igual a cero",
                max: "La comisión debe ser menor o igual a cien"
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

function RemoveAgency(id) {
    SweetAlert.RemoveAlert("api/agency/remove", { Id: parseInt(id) }, function (response) {
        GetAgencies();
        if (response) {
            Swal.fire({
                icon: 'success',
                title: 'Logrado',
            });
        } else {
            Swal.fire({
                icon: 'error',
                title: 'A ocurrido un error',
            });
        }
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
                    return `<i onclick="UpdateAgency(${data})" class="fa fa-pencil-square btnDatatable text-primary"></i>
                            <i onclick="RemoveAgency(${data})" class="fa fa-trash btnDatatable text-danger"></i>`;

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