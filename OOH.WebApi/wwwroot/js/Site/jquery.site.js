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
                    return `<i onclick="UpdateAgency(${data})" class="fa fa-pencil-square btnDatatable text-primary"></i>
                            <i onclick="RemoveAgency(${data})" class="fa fa-trash btnDatatable text-danger"></i>
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