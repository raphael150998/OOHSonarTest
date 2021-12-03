$().ready(function ($) {
    LLamarSalientes();
    $(".grid").hover(function () {
        var DIV = this;
        var CaraId = $("#CaraId").val();
        if (CaraId == 0) {
            $(DIV).addClass('grid-denied');
        } else {
            $(DIV).removeClass('grid-denied');
        }
    });
    $(".grid").click(function () {
        var DIV = this;
        var Cara = $("#CaraId").val();
        var obj = {
            Id: $(DIV).attr("idsql"),
            CaraId: Cara,
            SalienteId: $(DIV).attr("idgrid")
        };

        if (Cara == 0) {
            //N0 HACE NADA
        } else {
            var flag = DIV.getAttribute("selected");
            if (flag == "true") {
                fns.PostDataAsync("api/salientes/clickRemove", JSON.stringify(obj), function (dataResult) {
                    if (dataResult) {
                        $(DIV).removeClass('item-selected');
                        DIV.setAttribute('selected', false);
                       
                    }
                });

            } else {

                fns.PostDataAsync("api/salientes/clickAdd", JSON.stringify(obj), function (dataResult) {
                    if (dataResult != 0) {
                        console.log(dataResult);
                        $(DIV).addClass('item-selected');
                        DIV.setAttribute('selected', true);
                        DIV.idsql = dataResult;
                        $(DIV).attr("idsql", dataResult);
                    }
                });
            }
        }
    });
    DropSitio();
    DropCategoria();
    DropTipo();
    DataTableMaterial();
    Llenar();
    DataTableMaterial();
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

function DropTipo() {
    fns.CallGetAsync("api/face/type/get", null, function (dataResult) {
        console.log(dataResult);
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
            console.log(dataResult.sitioId);
            $('#dropdownTipo').val(dataResult.tipoId).trigger('change.select2');
        });
    }
}

function GetDataTableMateriales() {
    fns.CallGetAsync("api/Face/Materiales/get", { id: $("#CaraId").val() }, function (callbackDT) {
        console.log(callbackDT);

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
                     <i class="fa fa-pencil-square btnDatatable text-primary" onclick="addMaterial('` + data + `')"></i>
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

function addMaterial(id =0) {
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
    });
}
function LLamarSalientes() {

    fns.CallGetAsync("api/face/salientes/get", { id: $("#CaraId").val() }, function (callback) {
        callback.forEach(x => {
            var DIV = $(".grid[idgrid='" + x.salienteId + "']");
            DIV[0].setAttribute('idsql', x.id);
            DIV[0].setAttribute('selected', true);
            $(DIV).addClass('item-selected');
            $(DIV).attr("idsql", x.id);


        });
    });
}



