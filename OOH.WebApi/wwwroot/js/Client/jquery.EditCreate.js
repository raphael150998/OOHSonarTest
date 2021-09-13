$().ready(function ($) {
    DropDownListMunicipio();
    DropDownListCategoria();
    llenar();
    //LLenarTextBox();
    Validate.Form("#formClient", "api/client/CEdata" ,{
        rules: {
            NombreComercial: {
                required: true
            },
            Codigo: {
                required:true
            },
            RazonSocial: {
                required:true
            },
            NRC: {
                required: true
            },
            NIT: {
                required:true
            },
            Direccion: {
                required: true
            },
            Telefono: {
                required:true
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
});
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

//function LLenarTextBox() {
//    var idCliente = $("#ClienteId").val();


//    console.log(idCliente);
//    if (idCliente == 0) {
//        $("#addbtn").css("cursor", "no-drop");
//    }
//    if (idCliente != 0) {
//        fns.CallGetAsync("api/client/find", { id: idCliente }, function (dataResult) {
//            console.log(dataResult);
//            $("#addbtn").removeClass("text-secondary").addClass("text-primary");
//            $("#ClienteId").val(dataResult["clienteId"]);
//            $("#NombreComercial").val(dataResult["nombreComercial"]);
//            $("#dropdownMunicipio option[value=" + dataResult["municipioId"] + "]").attr("selected", true);
//            $('#dropdownMunicipio ').val(dataResult["municipioId"]).trigger('change.select2');
//            $("#NRC").val(dataResult["nrc"]);
//            $("#RazonSocial").val(dataResult["razonSocial"]);
//            $("#NIT").val(dataResult["nit"]);
//            $("#Celular").val(dataResult["celular"]);
//            $("#Email").val(dataResult["email"]);
//            $("#Telefono").val(dataResult["telefono"]);
//            $("#Giro").val(dataResult["giro"]);
//            $("#dropdownCategoria option[value=" + dataResult["categoriaId"] + "]").attr("selected", true);
//            if (dataResult["personaJuridica"]) {

//                $('.switchery').trigger('click');
//            }
//            $("#Direccion").val(dataResult["direccion"]);
//            $("#Codigo").val(dataResult["codigo"]);
//        });
//    }

//}
