var lstCaraDetalle = [];
$().ready(function ($) {
    DropDownListClientes();
    DropDownListAgencias();
    DetalleDT();
    Validate.Form("#formQuotation", "api/Quotation/CEdata", {

        rules: {

        },
        messages: {

        }

    });
});
function DropDownListClientes() {

    fns.CallGetAsync("api/Client/Get", null, function (dataResult) {
        let select = `<select class="js-example-basic-single" id="dropdownClient" name="ClientId" >`

        dataResult.forEach(cli => {
            let option = `<option value="` + cli.clienteId + `"> ` + cli.nombreComercial + `</option> `;

            select = select + option;
        });
        select = select + "</select>";

        $("#divClient").html(select);
        $('#dropdownClient').select2();

    });

}

function DropDownListAgencias() {

    fns.CallGetAsync("api/agency/Select", null, function (dataResult) {
        let select = `<select class="js-example-basic-single" id="dropdownAgencia" name="AgenciaId" >`

        dataResult.forEach(agn => {
            let option = `<option value="` + agn.agenciaId + `"> ` + agn.nombre + `</option> `;

            select = select + option;
        });
        select = select + "</select>";

        $("#divAgenci").html(select);
        $('#dropdownAgencia').select2();

    });

}

function addCara(idCara) {
    $("#ModalCaras").modal({
        show: true,
        backdrop: 'static',
        keyboard: false
    });
    DatatableAdd();
}

function DropSitio() {
    fns.CallGetAsync("api/municipio/call", null, function (dataResult) {
        let select = `<select class="js-example-basic-single number" id="dropdownMunicipio" name="MunicipioId">`
        var departamento = 0;
        dataResult.forEach(mun => {

            let optionGrp = "";
            if (mun.departamentoId != departamento) {
                departamento = mun.departamentoId;
                optionGrp = `<optgroup label="` + mun.departamento + `" group-id="` + mun.departamentoId + `" >`;
            }
            let option = optionGrp + `<option value="` + mun.municipioId + `"> ` + mun.departamento + "/" + mun.nombre + `</option> `;

            if (mun.departamentoId != departamento) {
                option = option + "  </optgroup>";
            }
            select = select + option;
        });
        select = select + "</select>";

        $("#divMunicipio").html(select);
        $('#dropdownMunicipio').select2();

    });
}

function DropCategoria() {
    fns.CallGetAsync("api/municipio/call", null, function (dataResult) {
        let select = `<select class="js-example-basic-single number" id="dropdownMunicipio" name="MunicipioId">`
        var departamento = 0;
        dataResult.forEach(mun => {

            let optionGrp = "";
            if (mun.departamentoId != departamento) {
                departamento = mun.departamentoId;
                optionGrp = `<optgroup label="` + mun.departamento + `" group-id="` + mun.departamentoId + `" >`;
            }
            let option = optionGrp + `<option value="` + mun.municipioId + `"> ` + mun.departamento + "/" + mun.nombre + `</option> `;

            if (mun.departamentoId != departamento) {
                option = option + "  </optgroup>";
            }
            select = select + option;
        });
        select = select + "</select>";

        $("#divMunicipio").html(select);
        $('#dropdownMunicipio').select2();

    });
}

function DropTipo() {
    fns.CallGetAsync("api/municipio/call", null, function (dataResult) {
        let select = `<select class="js-example-basic-single number" id="dropdownMunicipio" name="MunicipioId">`
        var departamento = 0;
        dataResult.forEach(mun => {

            let optionGrp = "";
            if (mun.departamentoId != departamento) {
                departamento = mun.departamentoId;
                optionGrp = `<optgroup label="` + mun.departamento + `" group-id="` + mun.departamentoId + `" >`;
            }
            let option = optionGrp + `<option value="` + mun.municipioId + `"> ` + mun.departamento + "/" + mun.nombre + `</option> `;

            if (mun.departamentoId != departamento) {
                option = option + "  </optgroup>";
            }
            select = select + option;
        });
        select = select + "</select>";

        $("#divMunicipio").html(select);
        $('#dropdownMunicipio').select2();

    });
}

function GetCarasCotizacion() {

    fns.CallGetAsync("api/facequotation/get", null, function (request) {
        console.log(request);
        $("#addCarasTable").DataTable().clear();
        
        lstCaraDetalle.forEach(x => {
            request = Remove(request, "caraId", x.caraId);
        });
        $("#addCarasTable").DataTable().rows.add(request).draw();

    });

}
function DatatableAdd() {

    DataTableHelper.Draw("#addCarasTable", {
        destroy: true,
        dom: "frltip",
        orderCellsTop: true,
        fixedHeader: true,
        data: [],
        columns: [
            {
                data: "caraId ",
                render: function (data, type, full, meta) {
                    return `<button class="btn btn-primary btn-sm" onclick='addMdetalle("` + full.caraId+`")'>agregar</button>`;
                }
            },
            {
                data:"codigo"
            },
            {
                data:"categoria"
            },
            {
                data:"referenciaComercial"
            },
            {
                data:"tipo"
            }
            
        ],
        "language": {
        "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
    }
    });
    GetCarasCotizacion();
}
function addMdetalle(IdCara) {
   
    $("#idCaraAdd").val(IdCara);
    $("#ModalCarasDetalle").modal({
        show: true,
        backdrop: 'static',
        keyboard: false
    });
}

$("#btnDetalleCaraClose").click(function () {
    $("#ModalCarasDetalle").modal("hide");

});


function DetalleDT() {

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
                    return `<button class="btn btn-danger btn-sm" onclick="RemoveDetalle('` + full.caraId +`')"><i class="fa fa-trash"></i></button>`;
                }
            },
            {
                data: "codigo"
            },
            {
                data: "referencia"
            },
            {
                data: "direccion"
            },
            {
                data: "precio"
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
    GetDetailCaras();
}

function GetDetailCaras() {
    console.log(lstCaraDetalle);
    $("#CarasTable").DataTable().clear();
    $("#CarasTable").DataTable().rows.add(lstCaraDetalle).draw();
}
function AddArray() {
    
    var send = JSON.stringify({ Id: $("#idCaraAdd").val() });
    
    var DetalleCara = {
        id:"A"+lstCaraDetalle.length,
        caraId: $("#idCaraAdd").val(),
        cotizacionId: 0,
        codigo: "ABC",
        referencia: "1234",
        direccion: "IZALCO",
        precio: "20.00",
        departamento: "SONSONATE",
        iluminada: true,
        costoArrendamient: "",
        costoImporesion: "",
        costoInstalacion: "",
        costoSaliente: "",
        fechaDesde: "",
        fechaHasta: ""
    }
    fns.PostDataAsync("api/caras/get", send, function (caraRequest) {

        console.log(caraRequest);
        DetalleCara.codigo = caraRequest.codigo;
        DetalleCara.referencia = caraRequest.referenciaComercial;
        DetalleCara.direccion = caraRequest.direccion;
        DetalleCara.precio = caraRequest.precio;
        DetalleCara.departamento = "";
        DetalleCara.iluminada = caraRequest.iluminada;

        console.log(DetalleCara);
        lstCaraDetalle.push(DetalleCara);
        $("#ModalCarasDetalle").modal("hide");
        GetCarasCotizacion();
        DetalleDT();

    });
   
}

function RemoveDetalle(Id) {
    

    lstCaraDetalle = Remove(lstCaraDetalle, "caraId", Id);

    GetDetailCaras();
}