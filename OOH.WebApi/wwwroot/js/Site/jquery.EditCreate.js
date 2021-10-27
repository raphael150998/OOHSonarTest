$(function () {

    DropDownListProviders();
    DropDownListMunicipio();
    DropDownListZones();

    $.validator.addMethod('greaterThan', function (value, element, param) {
        return this.optional(element) || parseFloat(value) > parseFloat(param);
    }, jQuery.validator.format("Number must be greater than {0}"));

    var validator = Validate.Form("#frmSitio", "api/site/CreateUpdate", {
        rules: {
            Codigo: {
                required: true
            },
            ProveedorId: {
                required: true
            },
            ZonaId: {
                required: true
            },
            Latitud: {
                required: true,
                number: true
            },
            Longitud: {
                required: true,
                number: true
            },
            Altura: {
                number: true,
                greaterThan: 0
            },
            Direccion: {
                required: true
            }
        },
        messages: {
            Codigo: {
                required: "El código es requerido"
            },
            ProveedorId: {
                required: "Seleccione un proveedor"
            },
            ZonaId: {
                required: "Seleccione una zona"
            },
            Latitud: {
                required: "Latitud requerida",
                number: "Solo se permiten números"
            },
            Longitud: {
                required: "Longitud requerida",
                number: "Solo se permiten números"
            },
            Altura: {
                number: "Solo se permiten números",
                greaterThan: "El número debe ser mayo a cero"
            },
            Direccion: {
                required: "Direccion requerida"
            }
        }
    }, function (data) {
        if (data.state) {
            $("#SitioId").val(data.data);
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: data.message
            });
        }
    });

    llenar();
})

function loadMap() {

    var lat = parseFloat($("#Latitud").val());
    var long = parseFloat($("#Longitud").val());

    var mapOptions = {
        center: new google.maps.LatLng(lat, long),
        zoom: 15
    }

    var map = new google.maps.Map(document.getElementById("sample"), mapOptions);

    var marker = new google.maps.Marker({
        position: new google.maps.LatLng(lat, long),
        map: map,
    });

    marker.setMap(map);
}

function llenar() {

    var id = $("#SitioId").val();

    if (id == 0) {
        $("#addbtn").css("cursor", "no-drop");
    }
    else {
        fns.CallGetAsync("api/site/find", { id: id }, function (dataResult) {
            $("#frmSitio").assignJsonToForm(dataResult);
            $('#dropdownMunicipio').val(dataResult.municipioId).trigger('change.select2');
            $("#ProveedorId").val(dataResult.proveedorId).trigger('change.select2');
            $("#ZonaId").val(dataResult.zonaId).trigger('change.select2');
            $("#Codigo").attr("readonly", true);
            loadMap();
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
        selectTarget.select2Validation();
    })
}


function DropDownListZones() {
    fns.CallGetAsync("api/zone/dropdown", null, function (zones) {

        var selectTarget = $("#ZonaId");

        var html = "<option disabled selected>Seleccione zona</option>";

        zones.forEach(x => {
            html += `<option value="${x.id}">${x.name}</option>`;
        });

        selectTarget.html(html);
        selectTarget.select2Validation();
    })
}
