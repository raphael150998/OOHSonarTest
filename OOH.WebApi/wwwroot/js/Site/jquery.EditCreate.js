$(function () {
    DropDownListProviders();
})

function llenar() {

    var id = $("#SitioId").val();

    if (id == 0) {
        $("#addbtn").css("cursor", "no-drop");
    }
    else {
        fns.CallGetAsync("api/site/find", { id: id }, function (dataResult) {
            $("#addbtn").removeClass("text-secondary").addClass("text-primary");
            $("#frmSitio").assignJsonToForm(dataResult);
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

function DropDownListProviders() {
    fns.CallGetAsync("api/provider/dropdown", null, function (providers) {
        var selectTarget = $("#ProveedorId");

        var html = "<option disabled selected>Seleccione proveedor</option>";

        providers.forEach(x => {
            html += `<option value="${x.id}">${x.name}</option>`;
        });

        selectTarget.html(html);
        selectTarget.select2();
    })
}

