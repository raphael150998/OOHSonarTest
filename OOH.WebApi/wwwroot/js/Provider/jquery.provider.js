$().ready(function ($) {
    DrawDatatable();
});
function GetDataProvideer() {
    fns.CallGetAsync("api/Provider/select", "", function (dataResult) {
        $("#tablaProveedor").DataTable().clear();
        $("#tablaProveedor").DataTable().rows.add(dataResult).draw();

    });
}

function DrawDatatable() {

    DataTableHelper.Draw("#tablaProveedor", {
        destroy: true,
        orderCellsTop: true,
        fixedHeader: true,
        data: [],
        columns: [
            {
                data: "proveedorId",
                orderable: false,
                render: function (data, type, full, meta) {
                    return `
                     <i class="fa fa-pencil-square btnDatatable text-primary" onclick="edit('` + data + `')"></i>
                     <i class="fa fa-trash btnDatatable text-danger" onclick="removeProvider('` + data + `')"></i>
                     <i onclick="GetLogs('Proveedor', 'api/provider/log', ${data})" class="fa fa-history btnDatatable text-warning"></i>
                     `;
                }
            },
            {
                data: "codigo",
                render: function (data, type, full, meta) {
                    return "<a href='index'>" + data + "</a>";
                }
            },
            { data: "nombre" },
            { data: "nrc" },
            { data: "nit" },

        ]
    }).FilterColum();
    GetDataProvideer();
}
function edit(id) {
    console.log(id);
    window.open("/Provider/AddOrUpdate/" + id, '_blank');
}

function removeProvider(idprovider) {

    SweetAlert.RemoveAlert("api/provider/remove", { Id: parseInt(idprovider) }, "", function (response) {

        if (response["state"]) {

            Swal.fire({
                icon: 'success',
                title: 'Logrado',
            });
            GetDataProvideer();
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