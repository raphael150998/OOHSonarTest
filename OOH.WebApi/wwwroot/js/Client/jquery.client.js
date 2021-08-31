//Document Ready
$().ready(function ($) {
    BuilDatatable();
});

//Llamada a la API de clientes para el llenado de la Dattable
function GetClient() {

    fns.CallGetAsync("api/Client/Get", null, function (dataResponse) {
        console.log(dataResponse);
        $("#tablaClient").DataTable().clear();
        $("#tablaClient").DataTable().rows.add(dataResponse).draw();
    }); 
}

//Metodo para crear el Datatable de clientes
function BuilDatatable() {
    DataTableHelper.Draw("#tablaClient", {
        destroy: true,
        orderCellsTop: true,
        fixedHeader: true,
        data: [],
        columns: [
            {
                data: "clienteId",
                orderable: false,
                render: function (data, type, full, meta) {
                    return `<i class="fa fa-pencil-square" onclick="edit('`+data+`')"></i>`;
                }
            },
            {
                data: "codigo",
                render: function (data, type, full, meta) {
                    return "<a href='index'>" + data + "</a>";
                }
            },
            { data: "nombreComercial" },
            { data: "razonSocial" },
            { data: "nit" },

        ]
    }).FilterColum();
    GetClient();
}

function edit(id) {
    console.log(id);
    window.open("/Client/CreateUpdate/" + id, '_blank');
}