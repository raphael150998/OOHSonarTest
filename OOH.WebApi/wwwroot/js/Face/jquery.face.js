$().ready(function ($) {

    DrawDataTableFace();

});


function GetDataTableFace() {
    fns.CallGetAsync("api/caras/getLst", null, function (response) {
        console.log(response);
        $("#CarasTable").DataTable().clear();
        $("#CarasTable").DataTable().rows.add(response).draw();
    });

}


function DrawDataTableFace() {

    DataTableHelper.Draw("#CarasTable", {
        destroy: true,
        dom: "frltip",
        orderCellsTop: true,
        fixedHeader: true,
        data: [],
        columns: [

            {
                data: "id",
                render: function (data, type, full, meta) {
                    console.log(full);
                    return ` <i class="fa fa-pencil-square btnDatatable text-primary" onclick="edit('` + full["caraId"] + `')"></i>
                             <i class="fa fa-trash  btnDatatable text-danger"  onclick="removeFace('` + full.caraId + `','` + full.id + `')"></i>`;
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
                data: "referencia"
            },
            {
                data: "municipio"
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
    console.log(idFace);
    SweetAlert.RemoveAlert("api/face/remove", { Id: parseInt(idFace) }, "", function (response) {

        if (response["state"]) {

            Swal.fire({
                icon: 'success',
                title: 'Logrado',
            });
            GetDataTableFace();
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

function refresh() {
    GetDataTableFace();
}