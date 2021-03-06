$("#tabcaras").click(function () {

    DatatableAdd();
    DropSitio();
    DropCategoria();
    DropTipo();

});

//Dropdonlist de filtros para usar en el modal de add caras
function DropSitio() {
    $("#dropdownSitio").select2Paged("/api/site/select2");

}

function DropCategoria() {
    fns.CallGetAsync("api/face/category/get", null, function (dataResult) {
        let select = `<select class="js-example-basic-single number" id="dropdownCategoria">`
        dataResult.forEach(cat => {

            let option = `<option value="` + cat.categoriaId + `"> ` + cat.nombre + `</option> `;
            select = select + option;
        });
        select = select + "</select>";

        $("#divDropCategoria").html(select);
        $('#dropdownCategoria').select2();

    });
}

function DropTipo() {
    fns.CallGetAsync("api/face/type/get", null, function (dataResult) {

        let select = `<select class="js-example-basic-single number" id="dropdownTipo">`
        dataResult.forEach(tipo => {

            let option = `<option value="` + tipo.Id + `"> ` + tipo.nombre + `</option> `;
            select = select + option;
        });
        select = select + "</select>";

        $("#divDropTipo").html(select);
        $('#dropdownTipo').select2();

    });
}


//Datatle Modal
function GetCarasCotizacion() {

    fns.CallGetAsync("api/facequotation/get", null, function (request) {
        $("#addCarasTable").DataTable().clear();
        console.log("GetCarasCotizacion");
        console.log(request);

        lstCaraDetalle.forEach(x => {
            request = Remove(request, "caraId", x.caraId);
        });
        $("#addCarasTable").DataTable().rows.add(request).draw();

    });

}
function DatatableAdd() {

    DataTableHelper.Draw("#addCarasTable", {
        destroy: true,
        dom: "rltip",
        orderCellsTop: true,
        fixedHeader: true,
        data: [],
        columns: [
            {
                data: "caraId ",
                render: function (data, type, full, meta) {
                    return `<button class="btn btn-primary btn-sm" onclick='addMdetalle("` + full.caraId + `")'>agregar</button>`;
                }
            },
            {
                data: "codigo"
            },
            {
                data: "categoria"
            },
            {
                data: "referenciaComercial"
            },
            {
                data: "tipo"
            }

        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        }
    });
    GetCarasCotizacion();
}
function addMdetalle(IdCara) {
    console.log(IdCara);
    $("#idCaraAdd").val(IdCara);
    //$("#ModalCarasDetalle").modal({
    //    show: true,
    //    backdrop: 'static',
    //    keyboard: false
    //});
    AddArray(IdCara);
}
function EditMdetalle(IdCara, IdDetalle) {

    var cara = Where(lstCaraDetalle, "caraId", IdCara);
    $("#CostoArrendamiento").val(cara.precio);
    $("#CostoImpresion").val(0.00);
    $("#CostoInstalacion").val(0.00);
    $("#CostoSaliente").val(0.00);
    $("#idCaraAdd").val(IdCara);
    $("#idDetalle").val(IdDetalle);
    $("#ModalCarasDetalle").modal({
        show: true,
        backdrop: 'static',
        keyboard: false
    });
  
}

function editArray(IdCara, IdDetalle) {
    console.log(id);
    var send = id;

    var DetalleCara = {
        id: $("#idDetalle").val(),
        caraId: $("#idCaraAdd").val(),
        cotizacionId: 0,
        codigo: "ABC",
        referencia: "1234",
        direccion: "IZALCO",
        precio: "20.00",
        departamento: "SONSONATE",
        iluminada: true,
        costoArrendamiento: $("#CostoArrendamiento").val(),
        costoImpresion: $("#CostoImpresion").val(),
        costoInstalacion: $("#CostoInstalacion").val(),
        costoSaliente: $("#CostoSaliente").val(),
        fechaDesde: $("#FechaDesde").val(),
        fechaHasta: $("#FechaHasta").val()
    }

    lstCaraDetalle.push(DetalleCara);
    $("#ModalCarasDetalle").modal("hide");
    GetCarasCotizacion();
    DetalleDT();
    $("#formularioCostos").trigger("reset");
}


$("#btnDetalleCaraClose").click(function () {
    $("#ModalCarasDetalle").modal("hide");
    $("#formularioCostos").trigger("reset");
});

