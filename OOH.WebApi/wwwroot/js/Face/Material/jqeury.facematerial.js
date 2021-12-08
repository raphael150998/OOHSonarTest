$().ready(function ($) {

    DataTableMaterial();

});

function DropMaterial() {
    fns.CallGetAsync("api/Materiales/getLst", null, function (dataResult) {
        let select = `<select class="js-example-basic-single number" id="dropdownMaterial" name="MaterialId">`
        select = select + `<option value="null"></option>`;
        dataResult.forEach(mat => {

            let option = `<option value="` + mat.materialId + `"> ` + mat.mateNombre + `</option> `;
            select = select + option;
        });
        select = select + "</select>";

        $("#divMaterial").html(select);
        $('#dropdownMaterial').select2();

    });
}

function GetDataTableMateriales() {
    fns.CallGetAsync("api/Face/Materiales/get", { id: $("#CaraId").val() }, function (callbackDT) {

        $("#tablaMaterial").DataTable().clear();
        $("#tablaMaterial").DataTable().rows.add(callbackDT).draw();

    });
}


function DataTableMaterial() {
    DataTableHelper.Draw("#tablaMaterial", {
        destroy: true,
        orderCellsTop: true,
        dom: "Brtp",
        buttons: [
            {
                text: '<i class="fa fa-plus" id="addbtn" ></i>',
                action: function (e, dt, node, config) {
                    addMaterial(0);
                }
            }
        ],
        fixedHeader: true,
        data: [],
        columns: [
            {
                data: "id",
                orderable: false,
                render: function (data, type, full, meta) {
                    return `
                     <i class="fa fa-trash btnDatatable text-danger" onclick="removeCosto('`+ data + `')"></i>
                     `;
                }
            },
            {
                data: "material",
                render: function (data, type, full, meta) {
                    return "<a href='index'>" + data + "</a>";
                }
            },
            { data: "codigo" },
            { data: "costo" }

        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        }
    });
    GetDataTableMateriales();
}


function addMaterial(id = 0) {
    $("#ModalMaterial").modal("show");
    DropMaterial();
}


function guardarMaterial() {
    var idMaterial = $("#dropdownMaterial option:selected").val();
    var objetoMaterial = {
        CaraId: $("#CaraId").val(),
        MaterialId: idMaterial
    };

    fns.PostDataAsync("api/face/material/CEdata", JSON.stringify(objetoMaterial), function (dataResult) {

        console.log(dataResult);
        if (dataResult["state"]) {
            $("#ModalMaterial").modal("hide");
            GetDataTableMateriales();
        }
    });
}

function removeCosto(id) {

    SweetAlert.RemoveAlert("api/face/materiales/remove", { Id: id }, "", function (response) {

        if (response) {

            Swal.fire({
                icon: 'success',
                title: 'Logrado',
            });
            GetDataTableMateriales();
        } else {

            Swal.fire({
                icon: 'error',
                title: 'A ocurrido un error',
            });
        }

    });

}
