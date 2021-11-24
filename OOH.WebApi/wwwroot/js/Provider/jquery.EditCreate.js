$().ready(function ($) {
    DropDownListMunicipio();
    DropDownListCategoria();
    llenar();
    Validate.Form("#formProveedor", "api/provider/CEdata", {

        rules: {
            Nombre: {
                required: true
            },
            Codigo: {
                required: true
            },
            RazonSocial: {
                required: true
            },
            NRC: {
                required: function (element) {
                    return $("#PersonaJuridica").is(':checked')
                }
            },
            NIT: {
                required: true
            },
            Direccion: {
                required: true
            },
            Telefono: {
                required: true
            }
        },
        messages: {

            NombreComercial: {
                required: "Campo requerido"

            },
            Codigo: {
                required: "Campo requerido"

            },
            RazonSocial: {
                required: "Campo requerido"

            },
            NRC: {
                required: "Campo requerido"

            },
            NIT: {
                required: "Campo requerido"

            },
            Direccion: {
                required: "Campo requerido"

            },
            Telefono: {
                required: "Campo requerido"

            }
        }

    }, function (data) {
        if (data["state"] == false) {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: dataResult["message"]
            })
        } else {
            $(".id").removeClass("btn-add-dissable").addClass("btn-add-success");
            $(".id").css("cursor", "pointer");
            $("#ProveedorId").val(data["data"]);
        }
    });
});
function DropDownListMunicipio() {

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
function DropDownListCategoria() {

    fns.CallGetAsync("api/provider/category", null, function (dataResult) {
        let select = `<select class="form-control number" id="dropdownCategoria" name="CategoriaId" >`
        console.log(dataResult);
        dataResult.forEach(cat => {
            let option = `<option value="` + cat.categoriaId + `"> ` + cat.nombre + `</option> `;

            select = select + option;
        });
        select = select + "</select>";

        $("#divCategoria").html(select);

    });

}
function llenar() {

    var idProvider= $("#ProveedorId").val();

    if (idProvider == 0) {
        $(".dt-button.id").css("cursor", "no-drop");
    }
    if (idProvider != 0) {
        fns.CallGetAsync("api/provider/find", { id: idProvider }, function (dataResult) {
            console.log(dataResult);
            $("#formProveedor").assignJsonToForm(dataResult);
            $('#dropdownMunicipio ').val(dataResult["municipioId"]).trigger('change.select2');

        });
    }
}
function edit(id) {
    console.log(id);
    window.open("/Provider/AddOrUpdate/" + id, '_blank');
}
