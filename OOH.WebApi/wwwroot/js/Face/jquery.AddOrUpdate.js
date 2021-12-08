$().ready(function ($) {
   
    DropSitio();
    DropCategoria();
    DropTipo();
    Llenar();
    Validate.Form("#formFaces", "api/caras/CEdata", {

        rules: {
            ReferenciaComercial: {
                required: true
            },
            //CategoriaId: {
            //    required: true
            //},
            //TipoId: {
            //    required: true
            //},
            //SitioId: {
            //    required: true
            //},
            MetodoInstalacion: {
                required: true
            },
            Observaciones: {
                required: true
            },
            NotaInstalacion: {
                required: true
            },
            Sentido: {
                required: true
               }
            //,
            //AlturaPiso: {
            //    required: function (element) {
            //        console.log(parseFloat($(element).val()) > 0.00);
            //        return parseFloat($(element).val()) > 0.00;
            //    }
            //},
            //Ancho: {
            //     required: function (element) {
            //        console.log(parseFloat($(element).val()));
            //        return parseFloat($(element).val()) > 0.0;
            //    }
            //},
            //Alto: {
            //    required: function (element) {
            //        console.log(parseFloat($(element).val()));
            //        return parseFloat($(element).val()) > 0.0;
            //    }
            //}
        },
        messages: {

            ReferenciaComercial: {
                required:  "Campo requerido"
            },
            CategoriaId: {
                required:  "Campo requerido"
            },
            TipoId: {
                required:  "Campo requerido"
            },
            SitioId: {
                required:  "Campo requerido"
            },
            MetodoInstalacion: {
                required:  "Campo requerido"
            },
            Observaciones: {
                required:  "Campo requerido"
            },
            NotaInstalacion: {
                required:  "Campo requerido"
            },
            Sentido: {
                required:  "Campo requerido"
            }
            //,
            //AlturaPiso: {
            //    required:  "Campo requerido"
            //},
            //Ancho: {
            //    required:  "Campo requerido"
            //},
            //Alto: {
            //    required: "Campo requerido"
            //}
        }

    }, function (data) {
        if (data["state"] == false) {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: dataResult["message"]
            })
        } else {
            //$(".id").removeClass("btn-add-dissable").addClass("btn-add-success");
            //$(".id").css("cursor", "pointer");
            $("#CaraId").val(data["data"]);
            Llenar();
        }
    });
});

////Dropdonlist de filtros para usar en el modal de add caras
//function DropSitio() {
//    $("#dropdownSitio").select2Paged("/api/site/select2");

//}

function DropSitio() {
    let tt = "-";
    fns.CallGetAsync("api/site/select", null, function (dataResult) {
        let select = `<select class="js-example-basic-single number" id="dropdownSitio" name="SitioId">`
        select = select + `<option value="null"></option>`;
        dataResult.sort().forEach(sit => {

            let option = `<option value="` + sit.sitioId + `"> ` + sit.codigo + tt + sit.referencia + tt + sit.nombreMunicipio + `</option> `;
            select = select + option;
        });
        select = select + "</select>";

        $("#divSitio").html(select);
        $('#dropdownSitio').select2Validation();

    });
}
function DropCategoria() {
    fns.CallGetAsync("api/face/category/get", null, function (dataResult) {
        let select = `<select class="js-example-basic-single number" id="dropdownCategoria" name="CategoriaId">`
        select = select + `<option value="null"></option>`;
        dataResult.forEach(cat => {

            let option = `<option value="` + cat.categoriaId + `"> ` + cat.nombre + `</option> `;
            select = select + option;
        });
        select = select + "</select>";

        $("#divCategoria").html(select);
        $('#dropdownCategoria').select2Validation();

    });
}

function DropTipo() {
    fns.CallGetAsync("api/face/type/get", null, function (dataResult) {
        let select = `<select class="js-example-basic-single number" id="dropdownTipo" name="TipoId">`
        select = select + `<option value="null"></option>`;
        dataResult.forEach(tipo => {

            let option = `<option value="` + tipo.tipoId + `"> ` + tipo.nombre + `</option> `;
            select = select + option;
        });
        select = select + "</select>";

        $("#divTipo").html(select);
        $('#dropdownTipo').select2Validation();

    });
}

function Llenar() {
    if ($("#CaraId").val() != 0) {

        fns.CallGetAsync("api/caras/get", { id: $("#CaraId").val() }, function (dataResult) {
            $("#formFaces").assignJsonToForm(dataResult);
           
            $('#dropdownCategoria').val(dataResult.categoriaId).trigger('change.select2');
            $('#dropdownSitio').val(dataResult.sitioId).trigger('change.select2');
            $('#dropdownTipo').val(dataResult.tipoId).trigger('change.select2');
        });
    }
}



