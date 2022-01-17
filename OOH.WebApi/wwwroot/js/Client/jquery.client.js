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
                    return `
                     <i class="fa fa-pencil-square btnDatatable text-primary" onclick="edit('` + data + `')"></i>
                     <i class="fa fa-trash btnDatatable text-danger" onclick="removeClient('` + data + `')"></i>
                     <i onclick="GetLogs('Cliente', 'api/client/log', ${data})" class="fa fa-history btnDatatable text-warning"></i>
                     `;
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

        ],        
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        }
    }).FilterColum();
    GetClient();
}
function refresh() {
    GetClient();
}
function edit(id) {
    console.log(id);
    window.open("/Client/CreateUpdate/" + id, '_blank');
}

function removeClient(idClient) {

    SweetAlert.RemoveAlert("api/client/remove", { Id: parseInt(idClient) }, "",function (response) {

        if (response["state"]) {
         
            Swal.fire({
                icon: 'success',
                title: 'Logrado',
            });
            GetClient();
        } else {
            if (response["condition"] == "fk") {
                Swal.fire({
                    icon: 'error',
                    title: response["message"],
                });
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'A ocurrido un error',
                });
            }
          
        }


    });
}