$(function () {
    BuildDatatable();
});

function GetSites() {
    fns.CallGetAsync("api/site/select", null, function (dataResponse) {
        $("#siteTable").DataTable().clear();
        $("#siteTable").DataTable().rows.add(dataResponse).draw();
    });
}

function BuildDatatable() {
    DataTableHelper.Draw("#siteTable", {
        destroy: true,
        orderCellsTop: true,
        fixedHeader: true,
        data: [],
        scrollX: true,
        columns: [
            {
                data: "sitioId", 
                orderable: false,
                render: function (data, type, full, meta) {
                    return `<a href="Sites/CreateUpdate/${data}" target="_blank"><i class="fa fa-pencil-square btnDatatable text-primary"></i></a>
                            <i onclick="RemoveSite(${data})" class="fa fa-trash btnDatatable text-danger"></i>
                            <i onclick="GetLogs('Sitio', 'api/site/log', ${data})" class="fa fa-history btnDatatable text-dark"></i>`;

                }
            },
            { data: "codigo" },
            { data: "nombreProveedor" },
            { data: "direccion" },
            { data: "referencia" },
            { data: "latitud" },
            { data: "longitud" },
            { data: "nombreMunicipio" },
            { data: "nombreZona" },
            { data: "requierePermiso" },
            { data: "activo" },
            { data: "registroCatastral" },
            { data: "altura" },
            { data: "observaciones" },

        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        }
    }).FilterColum();
    GetSites();
}

function RemoveSite(id) {
    SweetAlert.RemoveAlert("api/site/remove", { Id: parseInt(id) }, "",function (response) {
        GetSites();
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

function refresh() {
    GetSites();
}