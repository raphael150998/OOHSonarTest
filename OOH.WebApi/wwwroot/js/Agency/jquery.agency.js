//Document Ready
$().ready(function ($) {
    BuildDatatable();
});

//Llamada a la API de clientes para el llenado de la Dattable
function GetAgencies() {

    fns.CallGetAsync("Agencies/GetList", null, function (dataResponse) {
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