var lstCaraDetalle = [];
let IdCotizacion = $("#CotizacionId").val();

$().ready(function ($) {
    DropDownListClientes();
    DropDownListAgencias();
    //Validate.Form("#form-cotizacion", "api/Quotation/SaveMD", {}, function () {


    //},);
    llenarData();
});

function DropDownListClientes() {

    fns.CallGetAsync("api/Client/Get", null, function (dataResult) {
        let select = `<select class="js-example-basic-single" id="dropdownClient" name="ClientId" >`
        select = select + `<option value="N/A"></option>`;
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
        select = select + `<option value="N/A"></option>`;
        dataResult.forEach(agn => {
            let option = `<option value="` + agn.agenciaId + `"> ` + agn.nombre + `</option> `;

            select = select + option;
        });
        select = select + "</select>";

        $("#divAgenci").html(select);
        $('#dropdownAgencia').select2();

    });

}

function llenarData() {
    $("#EstadoId").val(0);
    if (IdCotizacion != 0) {
        fns.CallGetAsync("api/quotation/detail/find", { Idcotizacion: IdCotizacion }, function (dataResponse) {
            console.log(dataResponse);
            $("#dropdownClient").val(dataResponse["clienteId"]).trigger('change.select2');
            $("#dropdownAgencia").val(dataResponse["agenciaId"]).trigger('change.select2');
            $("#txtAtencionA").val(dataResponse["atencionA"]);
            $("#txtAreaComentarios").val(dataResponse["comentarios"]);
            $("#readOnlyFecha").val(dataResponse["fecha"]);
            $("#ESTADO").val(dataResponse["estado"]);
            $("#EstadoId").val(dataResponse["estadoId"]);

            var yy = CutString(CutString(dataResponse["fecha"], 0, " "), 2, "/").substring(2, 4);
            $("#CodigoCotizacion").val(`COT` + AddCeros(IdCotizacion, 6) + "/" + yy);
            if (dataResponse["consolidaCostos"]) {
                $("#ConsolidaCosto").trigger("click");
            }
            else {

            }
            $.each(dataResponse["lstCaras"], function (index, value) {

                var DetalleCara = {
                    id: value.id,
                    caraId: value.caraId,//
                    cotizacionId: IdCotizacion,//
                    codigo: value.codigo,
                    referencia: value.referencia,
                    direccion: value.direccion,
                    precio: value.costoArrendamiento,
                    departamento: "",
                    iluminada: true,
                    costoArrendamiento: value.costoArrendamiento,    //
                    costoImpresion: value.costoImpresion,        //
                    costoInstalacion: value.costoInstalacion,      //
                    costoSaliente: value.costoSaliente,         //
                    fechaDesde: value.fechaDesde,//
                    fechaHasta: value.fechaHasta //
                };
                lstCaraDetalle.clear();
                lstCaraDetalle.push(DetalleCara);
                console.log(index);
                console.log(lstCaraDetalle);
            });
            DetalleDT();




        });
    } else {
        DetalleDT();

    }
}

function DetalleDT() {
 
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
                    return `<button class="btn btn-danger btn-sm" onclick="RemoveDetalle('` + full.caraId + `','` + full.id + `')"><i class="fa fa-trash"></i></button>`;
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
 
    $("#CarasTable").DataTable().clear();
    $("#CarasTable").DataTable().rows.add(lstCaraDetalle).draw();
}

function AddArray() {
    
    var send = JSON.stringify({ Id: $("#idCaraAdd").val() });
    
    var DetalleCara = {
        id:0,
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
    $("#formularioCostos").trigger("reset");
    fns.PostDataAsync("api/caras/get", send, function (caraRequest) {

        DetalleCara.codigo = caraRequest.codigo;
        DetalleCara.referencia = caraRequest.referenciaComercial;
        DetalleCara.direccion = caraRequest.direccion;
        DetalleCara.precio = DetalleCara.costoArrendamiento;
        DetalleCara.departamento = "";
        DetalleCara.iluminada = caraRequest.iluminada;

        lstCaraDetalle.push(DetalleCara);
        $("#ModalCarasDetalle").modal("hide");
        GetCarasCotizacion();
        DetalleDT();

    });
   
}
//Quitar de las caras seleccionadas
function RemoveDetalle(IdCara, Iddetalle) {
    console.log(Iddetalle);
    if (Iddetalle != 0) {
        eliminarDetalle(Iddetalle, IdCara);
    } else {
       
        lstCaraDetalle = Remove(lstCaraDetalle, "caraId", IdCara);
        GetDetailCaras();
    }
   
}

//Post Maestro Detalle
function PostMaestroDetalle() {

    var MD = {
        CotizacionId: IdCotizacion,
        EstadoId: $("#EstadoId").val(),
        Fecha: IdCotizacion == 0 ? "" : $("#readOnlyFecha").val(),
        ClienteId: $("#dropdownClient option:selected").val(),
        AgenciaId: $("#dropdownAgencia option:selected").val(),
        AtencionA: $("#txtAtencionA").val(),
        Comentarios: $("#txtAreaComentarios").val(),
        ConsolidaCostos: $("#PersonaJuridica").is(':checked') ,
        LstCaras: lstCaraDetalle
    };
    console.log(JSON.stringify(MD));
    console.log(MD);

    SweetAlert.ConfirmForm(function () {
        fns.PostDataAsync("api/Quotation/SaveMD", JSON.stringify(MD), function (dataRequest) {

            console.log(dataRequest);
            if (dataRequest["state"]) {
                $("#CotizacionId").val(dataRequest["data"]);
                IdCotizacion = dataRequest["data"];
                llenarData();
                Swal.fire(
                    'Guardado',
                    'Se guardo correctamente',
                    'success'
                );
            } else {
                Swal.fire(
                    'Error',
                    'Ocurrio un error',
                    'error'
                );
            }

        });

    }, false);
  


}


$("#btnDetalleCaraClose").click(function () {

    $("#formularioCostos").trigger("reset");

});

function eliminarDetalle(iddetalle, IdCara) {

    SweetAlert.RemoveAlert("api/quotation/detail/remove", { Id: iddetalle }, "El detalle sera removido", function (response) {
        if (response) {
            lstCaraDetalle = Remove(lstCaraDetalle, "caraId", IdCara);

            GetDetailCaras();
        } else {
            //
        }
    });

}

function edit(id) {

    console.log(id);
    window.open("/Quotation/CreateUpdate/" + id, '_blank');

}


function AddCeros(parametro, cantidad) {


    var BaseContatenar = parametro.toString().length;
    var Contador = parseInt(cantidad) - parseInt(BaseContatenar);
    var DataConcatenada = "";

    for (var i = 0; i < Contador; i++) {

        DataConcatenada = DataConcatenada + "0";
    }
    DataConcatenada = DataConcatenada + parametro;
    return DataConcatenada;
}

