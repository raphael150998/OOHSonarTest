$().ready(function ($) {
    DropDownListMunicipio();
    DropDownListCategoria();
    llenar();
    Validate.Form("#formClient", "api/client/CEdata", {

        rules: {
            NombreComercial: {
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
                required: "Campo requerido bro"

            },
            Codigo: {
                required: "Campo requerido bro"

            },
            RazonSocial: {
                required: "Campo requerido bro"

            },
            NRC: {
                required: "Campo requerido bro"

            },
            NIT: {
                required: "Campo requerido bro"

            },
            Direccion: {
                required: "Campo requerido bro"

            },
            Telefono: {
                required: "Campo requerido bro"

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
            $("#addbtn").removeClass("text-secondary").addClass("text-primary");
            $("#addbtn").css("cursor", "pointer");
            $("#ClienteId").val(data["data"]);
        }
    });
    //LLenarTextBox();
});

function edit(id) {
    console.log(id);
    window.open("/Client/CreateUpdate/" + id, '_blank');
}

function llenar() {

    var idCliente = $("#ClienteId").val();

    if (idCliente == 0) {
        $("#addbtn").css("cursor", "no-drop");
    }
    if (idCliente != 0) {
        fns.CallGetAsync("api/client/find", { id: idCliente }, function (dataResult) {
            //console.log(JSON.stringify(dataResult));
            $("#addbtn").removeClass("text-secondary").addClass("text-primary");
            $("#formClient").assignJsonToForm(dataResult);
            $('#dropdownMunicipio ').val(dataResult["municipioId"]).trigger('change.select2');

        });
    }
}
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

    fns.CallGetAsync("api/category/call", null, function (dataResult) {
        let select = `<select class="form-control number" id="dropdownCategoria" name="CategoriaId" >`
        
        dataResult.forEach(cat => {          
            let option = `<option value="` + cat.categoriaId + `"> ` + cat.nombre + `</option> `;

            select = select + option;
        });
        select = select + "</select>";

        $("#divCategoria").html(select);

    });

}


function PostData() {

    var ObjetSend = {

        ClienteId: $("#ClienteId").val(),
        NombreComercial: $("#NombreComercial").val(),
        MunicipioId: $("#dropdownMunicipio option:selected").val(),
        NRC: $("#NRC").val(),
        RazonSocial: $("#RazonSocial").val(),
        NIT: $("#NIT").val(),
        Celular: $("#Celular").val(),
        Email: $("#Email").val(),
        Telefono: $("#Telefono").val(),
        Giro: $("#Giro").val(),
        CategoriaId: $("#dropdownCategoria option:selected").val(),
        PersonaJuridica: $("PersonaJuridica").attr('checked'),
        Direccion: $("#Direccion").val(),
        Codigo: $("#Codigo").val()
    }
}
