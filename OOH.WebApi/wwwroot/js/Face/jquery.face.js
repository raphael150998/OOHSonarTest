$().ready(function ($) {

    DrawDataTableFace();

});


function GetDataTableFace() {
    fns.CallGetAsync("api/caras/getLst", null, function (response) {
        $("#CarasTable").DataTable().clear();
        $("#CarasTable").DataTable().rows.add(response).draw();
    });

}


function DrawDataTableFace() {

    DataTableHelper.Draw("#CarasTable", {
        destroy: true,
        dom: "Bfrltip",
        buttons: [
            {
                text: '<i class="fa fa-plus"></i>',
                action: function (e, dt, node, config) {
                    addCara(0);
                }
            }
        ],
        orderCellsTop: true,
        fixedHeader: true,
        data: [],
        columns: [

            {
                data: "id",
                render: function (data, type, full, meta) {
                    return `<i class="fa fa-trash  btnDatatable text-danger""  onclick="RemoveDetalle('` + full.caraId + `','` + full.id + `')"></i>`;
                }
            },
            {
                data: "codigo"
            },
            {
                data: "sitio"
            },
            {
                data: "tipo"
            },
            {
                data: "refencia"
            },
            {
                data: "departamento"
            },
            {
                data: "iluminada"
            }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        }
    });
    GetDataTableFace();
}

function removeFace(idFace) {

    SweetAlert.RemoveAlert("api/face/remove", { Id: parseInt(idFace) }, "", function (response) {

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

function edit(id) {
    console.log(id);
    window.open("/Face/AddOrUpdate/" + id, '_blank');
}
