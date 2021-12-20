$().ready(function ($) {
    DropDownListMunicipio();
    DropDownListCategoria();
    DropDownListDepartments();
    DropDownListCities();
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

    $("#DepartamentoId").on('change.select2', function (e) {

        var idMunicipio = $("#MunicipioIdAux").val();

        idMunicipio == 0 ? DropDownListCities() : DropDownListCities(idMunicipio);
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
function DropDownListCities(selected) {

    var id = $("#DepartamentoId").val();

    var selectTarget = $("#MunicipioId");

    var html = "<option disabled selected>Seleccione municipio</option>";

    if (id != null) {

        selectTarget.select2('destroy');

        fns.CallGetAsync(`api/address/dropdownCities/${id}`, null, function (list) {

            list.forEach(x => {
                html += `<option value="${x.id}">${x.name}</option>`;
            });

            selectTarget.html(html);

            selectTarget.select2Validation();

            if (selected != null) {
                selectTarget.val(selected).trigger('change.select2');
            }

        })
    }
    else {
        selectTarget.html(html);

        selectTarget.select2Validation();
    }
}

function DropDownListDepartments() {
    fns.CallGetAsync("api/address/dropdownDepartments", null, function (list) {

        var selectTarget = $("#DepartamentoId");

        var html = "<option disabled selected>Seleccione departamento</option>";

        list.forEach(x => {
            html += `<option value="${x.id}">${x.name}</option>`;
        });

        selectTarget.html(html);
        selectTarget.select2Validation();
    })
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
            $('#MunicipioIdAux').val(dataResult["municipioId"]);
            $('#DepartamentoId').val(dataResult.departamentoId).trigger('change.select2');
            $("#formProveedor").assignJsonToForm(dataResult);
            $('#dropdownMunicipio ').val(dataResult["municipioId"]).trigger('change.select2');

        });
    }
}
function edit(id) {
    console.log(id);
    window.open("/Provider/AddOrUpdate/" + id, '_blank');
}
