$().ready(function ($) {
    DataTablePrecios();

});

function GetDataTablePrecios() {
    fns.CallGetAsync("api/priceface/get", { id: $("#CaraId").val() }, function (callbackDT) {

        $("#tablaPrecio").DataTable().clear();
        $("#tablaPrecio").DataTable().rows.add(callbackDT).draw();

    });
}

function DataTablePrecios() {
    DataTableHelper.Draw("#tablaPrecio", {
        destroy: true,
        orderCellsTop: true,
        dom: "Brtp",
        buttons: [
            {
                text: '<i class="fa fa-plus" id="addbtn" ></i>',
                action: function (e, dt, node, config) {
                    addPrecio(0);
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
                    <i class="fa fa-pencil-square btnDatatable text-primary" onclick="edit('` + full["id"] + `')"></i>
                     <i class="fa fa-trash btnDatatable text-danger" onclick="removePriceFace('`+ data + `')"></i>
                     `;
                }
            },
            { data: "tipo"},
            { data: "precio" },
     

        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        }
    });
    GetDataTablePrecios();
}

function DropDownTipoPrecio() {

    fns.CallGetAsync("api/PriceType/getLst", null, function (dataResult) {
        let select = `<select class="js-example-basic-single number" id="dropdownTypesPrice" name="TipoId">`
        select = select + `<option value="null"></option>`;
        dataResult.forEach(pri => {

            let option = `<option value="` + pri.id + `"> ` + pri.codigo +"-"+ pri.nombre + `</option> `;
            select = select + option;
        });
        select = select + "</select>";

        $("#divPrecios").html(select);
        $('#dropdownTypesPrice').select2();

    });
}

function addPrecio(id) {
    $("#ModalPrecio").modal("show");
    DropDownTipoPrecio();
}

function guardarPriceFace() {

    var sendObject = {
        TipoId: $("#dropdownTypesPrice option:selected").val(),
        CaraId: $("#CaraId").val(),
        Precio: $("#precio").val()
    }

    fns.PostDataAsync("api/priceface/post", JSON.stringify(sendObject), function (callback) {

        if (callback["state"]) {

            Swal.fire({
                icon: 'success',
                title: 'Logrado',
            });
            $("#ModalPrecio").modal("hide");
            GetDataTablePrecios();
        } else {

            Swal.fire({
                icon: 'error',
                title: 'A ocurrido un error',
            });
        }
    })

}

function removePriceFace(id) {

    SweetAlert.RemoveAlert("api/priceface/remove", { Id: id }, "", function (response) {

        if (response) {

            Swal.fire({
                icon: 'success',
                title: 'Logrado',
            });
            GetDataTablePrecios();
        } else {

            Swal.fire({
                icon: 'error',
                title: 'A ocurrido un error',
            });
        }

    });



}