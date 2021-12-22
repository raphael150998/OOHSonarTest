$(function () {

    $('#schedule').jqs({
        periodOptions: false,
        periodDuration: 60,
        periodBackgroundColor: 'rgba(0, 255, 0, 0.5)',
        periodBorderColor: 'rgb(0, 255, 0)',
    });

    getTimes();

    $("#Latitud").val('');
    $("#Longitud").val('');
    $("#Altura").val('');

    DropDownListProviders();
    DropDownListDepartments();
    DropDownListCities();
    DropDownListZones();
    DropDownListCategories();
    DropDownListStructures();

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
            EstructuraTipo: {
                required: true
            },
            CategoriaSitio: {
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
            },
            ProveedorElectricidadId: {
                required: true
            },
            Porcentaje: {
                required: true,
                number: true,
                min: 0,
                max: 100
            },
            ContadorElectrico: {
                required: true
            },
            NIC: {
                required: true
            },
            DepartamentoId: {
                required: true
            },
            MunicipioId: {
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
            EstructuraTipo: {
                required: "Seleccione un tipo de estructura"
            },
            CategoriaSitio: {
                required: "Selecciones una categoría"
            },
            Latitud: {
                required: "Latitud requerida, seleccione punto en mapa",
                number: "Solo se permiten números"
            },
            Longitud: {
                required: "Longitud requerida, seleccione punto en mapa",
                number: "Solo se permiten números"
            },
            Altura: {
                number: "Solo se permiten números",
                greaterThan: "El número debe ser mayo a cero"
            },
            Direccion: {
                required: "Direccion requerida"
            },
            ProveedorElectricidadId: {
                required: "Seleccione proveedor de electricidad"
            },
            Porcentaje: {
                required: "Defina un porcentaje",
                number: "Solo se permiten números",
                min: "El porcentaje debe ser mayor o igual a cero",
                max: "El porcentaje debe ser menor o igual a cien"
            },
            ContadorElectrico: {
                required: "Digite el número de contado eléctrico"
            },
            NIC: {
                required: "Digite el NIC"
            },

            DepartamentoId: {
                required: "Seleccione un departamento"
            },
            MunicipioId: {
                required: "Seleccione un municipio"
            }
        }
    }, function (data) {
        if (data.state) {

            var id = $("#SitioId").val();

            if (id == 0) {
                id = data.data;
                location.href = `/Sites/CreateUpdate/${id}`;
            }
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: data.message
            });
        }
    });

    $("#DepartamentoId").on('change.select2', function (e) {

        var idMunicipio = $("#MunicipioIdAux").val();

        idMunicipio == 0 ? DropDownListCities() : DropDownListCities(idMunicipio);
    });

    llenar();

    loadMap();

    $("#FechaActivacion").datetimepicker({ format: 'DD/MM/YYYY HH:mm', showClose: true });

})

function getTimes() {

    var id = $("#SitioId").val();

    fns.CallGetAsync(`api/time/times/${id}`, null, function (dataResponse) {
        $('#schedule').jqs('import', dataResponse);
    });
}

function setTimes() {
    var id = $("#SitioId").val();

    var exportData = $('#schedule').jqs('export');

    SweetAlert.ConfirmForm(function () {

        fns.PostDataNoAsync(`api/time/times/${id}`, exportData, function (dataResult) {
            if (!dataResult) {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'Intente nuevamente o contacte al administrador'
                });
            } else {
                Swal.fire({
                    icon: 'success',
                    title: 'Logrado',
                })
            }
        })
    }, false);

}

function loadMap() {

    var lat = parseFloat($("#Latitud").val());
    var long = parseFloat($("#Longitud").val());

    lat = !isNaN(lat) ? lat : 13.794185;
    long = !isNaN(long) ? long : -88.89653;

    var mapOptions = {
        center: new google.maps.LatLng(lat, long),
        zoom: 15
    }

    var map = new google.maps.Map(document.getElementById("sample"), mapOptions);

    var marker = new google.maps.Marker({
        position: new google.maps.LatLng(lat, long),
        map: map,
        draggable: true,
    });

    marker.setMap(map);

    google.maps.event.addListener(marker, 'click', function (event) {
        latPosition = marker.getPosition().lat();
        longPosition = marker.getPosition().lng();

        $("#Latitud").val(latPosition);
        $("#Longitud").val(longPosition);

        $("#frmSitio").valid();
    });

    google.maps.event.addListener(marker, 'dragend', function (event) {
        latPosition = marker.getPosition().lat();
        longPosition = marker.getPosition().lng();

        $("#Latitud").val(latPosition);
        $("#Longitud").val(longPosition);

        $("#frmSitio").valid();
    });


    //map.addListener("click", (mapsMouseEvent) => {
    //    var latLong = mapsMouseEvent.latLng.toJSON();
    //    $("#Latitud").val(latLong.lat);
    //    $("#Longitud").val(latLong.lng);
    //});
}

function llenar() {

    var id = $("#SitioId").val();

    if (id == 0) {
        $("#addbtn").css("cursor", "no-drop");
    }
    else {
        fns.CallGetAsync("api/site/find", { id: id }, function (dataResult) {
            $("#frmSitio").assignJsonToForm(dataResult);
            $('#MunicipioIdAux').val(dataResult.municipioId);
            $('#DepartamentoId').val(dataResult.departamentoId).trigger('change.select2');
            $("#ProveedorId").val(dataResult.proveedorId).trigger('change.select2');
            $("#ProveedorElectricidadId").val(dataResult.proveedorElectricidadId).trigger('change.select2');
            $("#ZonaId").val(dataResult.zonaId).trigger('change.select2');
            $("#CategoriaSitio").val(dataResult.categoriaSitio).trigger('change.select2');
            $("#EstructuraTipo").val(dataResult.estructuraTipo).trigger('change.select2');
            $("#Codigo").attr("readonly", true);

            parseInt($("#DiasSolicitudPermiso").val()) == 0 ? $("#DiasSolicitudPermiso").val('') : $("#DiasSolicitudPermiso").val();

            parseInt($("#Altura").val()) == 0 ? $("#Altura").val('') : $("#Altura").val();

            parseInt($("#Porcentaje").val()) == 0 ? $("#Porcentaje").val('') : $("#Porcentaje").val();

            loadMap();

            BuildReferencesDatatable();
            BuildRestrictionDatatable();
            BuildInsurancesDatatable();
            BuildPermissionsDatatable();
            BuildProvidersDatatable();
            BuildCostsDatatable();
        });
    }
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

function DropDownListProviders() {
    fns.CallGetAsync("api/provider/dropdown", null, function (providers) {

        var selectTarget = $("#ProveedorId");
        var selectTargetElectric = $("#ProveedorElectricidadId");
        var selectTargetList = $("#ProviderListId");

        var html = "<option disabled selected>Seleccione proveedor</option>";

        providers.forEach(x => {
            html += `<option value="${x.id}">${x.name}</option>`;
        });

        selectTarget.html(html);
        selectTarget.select2Validation();

        selectTargetElectric.html(html);
        selectTargetElectric.select2Validation();

        selectTargetList.html(html);
        selectTargetList.select2Validation();
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

function DropDownListCategories() {
    fns.CallGetAsync("api/siteCategory/dropdown", null, function (zones) {

        var selectTarget = $("#CategoriaSitio");

        var html = "<option disabled selected>Seleccione categoría</option>";

        zones.forEach(x => {
            html += `<option value="${x.id}">${x.name}</option>`;
        });

        selectTarget.html(html);
        selectTarget.select2Validation();
    })
}

function DropDownListStructures() {
    fns.CallGetAsync("api/structureType/dropdown", null, function (zones) {

        var selectTarget = $("#EstructuraTipo");

        var html = "<option disabled selected>Seleccione estructura</option>";

        zones.forEach(x => {
            html += `<option value="${x.id}">${x.name}</option>`;
        });

        selectTarget.html(html);
        selectTarget.select2Validation();
    })
}

//tabla referencias

$(function () {
    $("#modalReference").modal({
        backdrop: "static",
        keyboard: false,
        show: false
    });

    $("#btnAddReference").click(function () {

        var id = parseInt($("#SitioId").val());

        if (id > 0) {
            $("#modalTitleReference").html("Agregando nueva referencia");
            $('#ReferenciaId').val('').trigger('change.select2');
            $('#frmReference').trigger("reset");
            $("#sitioIdReference").val(id);
            $("#modalReference").modal("show");
        }
    });


    var frmReference = Validate.Form("#frmReference", "api/referenceSite/CreateUpdate", {
        rules: {
            ReferenciaId: {
                required: true,
                min: 1,
            },
            Comentarios: {
                required: true
            }
        },
        messages: {
            ReferenciaId: {
                required: "Seleccione una referencia",
                min: "Seleccione una referencia",
            },
            Comentarios: {
                required: "Digite el comentario"
            }
        }
    }, function (data) {
        $('#frmReference').trigger("reset");
        $("#modalReference").modal("hide");
        GetReferences();
    });

    $("#btnCloseReference").click(function () {
        frmReference.resetForm();
        $('#ReferenciaId').val('').trigger('change.select2');
        $("#modalReference").modal("hide");
    });

    DropDownListReferences();
});

function DropDownListReferences() {
    fns.CallGetAsync("api/reference/dropdown", null, function (zones) {

        var selectTarget = $("#ReferenciaId");

        var html = "<option disabled selected>Seleccione referencia</option>";

        zones.forEach(x => {
            html += `<option value="${x.id}">${x.name}</option>`;
        });

        selectTarget.html(html);
        selectTarget.select2Validation();
    })
}

function GetReferences() {

    var id = $("#SitioId").val();

    fns.CallGetAsync("api/referenceSite/selectReferences", { id: id }, function (dataResponse) {
        $("#referencesTable").DataTable().clear();
        $("#referencesTable").DataTable().rows.add(dataResponse).draw();
    });
}

function UpdateReference(id) {
    fns.CallGetAsync(`api/referenceSite/Find`, { id: id }, function (response) {
        $("#frmReference").assignJsonToForm(response);
        $('#ReferenciaId').val(response.referenciaId).trigger('change.select2');
    })
    $("#modalTitleReference").html("Modificando referencia");
    $("#modalReference").modal("show");
}

function RemoveReference(id) {
    SweetAlert.RemoveAlert("api/referenceSite/remove", { Id: parseInt(id) }, null, function (response) {
        GetReferences();
        if (response) {
            Swal.fire({
                icon: 'success',
                title: 'Logrado',
            });
        } else {
            Swal.fire({
                icon: 'error',
                title: 'A ocurrido un error',
            });
        }
    })
};

function BuildReferencesDatatable() {
    DataTableHelper.Draw("#referencesTable", {
        destroy: true,
        orderCellsTop: true,
        fixedHeader: true,
        data: [],
        //scrollX: true,
        columns: [
            {
                data: "id",
                orderable: false,
                render: function (data, type, full, meta) {
                    return `<i onclick="UpdateReference(${data})" class="fa fa-pencil-square btnDatatable text-primary"></i>
                            <i onclick="RemoveReference(${data})" class="fa fa-trash btnDatatable text-danger"></i>
                            <i onclick="GetLogs('SitiosReferenciasComerciales', 'api/referenceSite/log', ${data})" class="fa fa-history btnDatatable text-dark"></i>`;

                }
            },
            { data: "nombre" },
            { data: "comentarios" }

        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        }
    }).FilterColum();
    GetReferences();
}

//fin tabla referencias


//tabla restricciones

$(function () {
    $("#modalRestriction").modal({
        backdrop: "static",
        keyboard: false,
        show: false
    });

    $("#btnAddRestriction").click(function () {

        var id = parseInt($("#SitioId").val());

        if (id > 0) {
            $("#modalTitleRestriction").html("Agregando nueva restriccion");
            $('#RestriccionId').val('').trigger('change.select2');
            $('#frmRestriction').trigger("reset");
            $("#sitioIdRestriction").val(id);
            $("#modalRestriction").modal("show");
        }
    });


    var frmRestriction = Validate.Form("#frmRestriction", "api/restrictionSite/CreateUpdate", {
        rules: {
            RestriccionId: {
                required: true,
                min: 1,
            },
            Comentarios: {
                required: true
            }
        },
        messages: {
            RestriccionId: {
                required: "Seleccione una restriccion",
                min: "Seleccione una restriccion",
            },
            Comentarios: {
                required: "Digite el comentario"
            }
        }
    }, function (data) {
        $('#frmRestriction').trigger("reset");
        $("#modalRestriction").modal("hide");
        GetRestrictions();
    });

    $("#btnCloseRestriction").click(function () {
        frmRestriction.resetForm();
        $('#RestriccionId').val('').trigger('change.select2');
        $("#modalRestriction").modal("hide");
    });

    DropDownListRestrictions();
});

function DropDownListRestrictions() {
    fns.CallGetAsync("api/restriction/dropdown", null, function (zones) {

        var selectTarget = $("#RestriccionId");

        var html = "<option disabled selected>Seleccione restriccion</option>";

        zones.forEach(x => {
            html += `<option value="${x.id}">${x.name}</option>`;
        });

        selectTarget.html(html);
        selectTarget.select2Validation();
    })
}

function GetRestrictions() {

    var id = $("#SitioId").val();

    fns.CallGetAsync("api/restrictionSite/selectRestrictions", { id: id }, function (dataResponse) {
        $("#restrictionsTable").DataTable().clear();
        $("#restrictionsTable").DataTable().rows.add(dataResponse).draw();
    });
}

function UpdateRestriction(id) {
    fns.CallGetAsync(`api/restrictionSite/Find`, { id: id }, function (response) {
        $("#frmRestriction").assignJsonToForm(response);
        $('#RestriccionId').val(response.restriccionId).trigger('change.select2');
    })
    $("#modalTitleRestriction").html("Modificando restriccion");
    $("#modalRestriction").modal("show");
}

function RemoveRestriction(id) {
    SweetAlert.RemoveAlert("api/restrictionSite/remove", { Id: parseInt(id) }, null, function (response) {
        GetRestrictions();
        if (response) {
            Swal.fire({
                icon: 'success',
                title: 'Logrado',
            });
        } else {
            Swal.fire({
                icon: 'error',
                title: 'A ocurrido un error',
            });
        }
    })
};

function BuildRestrictionDatatable() {
    DataTableHelper.Draw("#restrictionsTable", {
        destroy: true,
        orderCellsTop: true,
        fixedHeader: true,
        data: [],
        //scrollX: true,
        columns: [
            {
                data: "id",
                orderable: false,
                render: function (data, type, full, meta) {
                    return `<i onclick="UpdateRestriction(${data})" class="fa fa-pencil-square btnDatatable text-primary"></i>
                            <i onclick="RemoveRestriction(${data})" class="fa fa-trash btnDatatable text-danger"></i>
                            <i onclick="GetLogs('Restriccion', 'api/restrictionSite/log', ${data})" class="fa fa-history btnDatatable text-dark"></i>`;

                }
            },
            { data: "nombre" },
            { data: "comentarios" }

        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        }
    }).FilterColum();
    GetRestrictions();
}

//fin tabla restricciones

//tabla seguros

$(function () {
    $("#modalInsurance").modal({
        backdrop: "static",
        keyboard: false,
        show: false
    });

    $("#btnAddInsurance").click(function () {

        var id = parseInt($("#SitioId").val());

        if (id > 0) {
            $("#modalTitleInsurance").html("Agregando nuevo seguro");
            $('#SeguroId').val('').trigger('change.select2');
            $('#frmInsurance').trigger("reset");
            $("#sitioIdInsurance").val(id);
            $("#modalInsurance").modal("show");
        }
    });


    var frmInsurance = Validate.Form("#frmInsurance", "api/insuranceSite/CreateUpdate", {
        rules: {
            SeguroId: {
                required: true,
                min: 1,
            }
        },
        messages: {
            SeguroId: {
                required: "Seleccione un seguro",
                min: "Seleccione un seguro",
            }
        }
    }, function (data) {
        $('#frmInsurance').trigger("reset");
        $("#modalInsurance").modal("hide");
        GetInsurances();
    });

    $("#btnCloseInsurance").click(function () {
        frmInsurance.resetForm();
        $('#SeguroId').val('').trigger('change.select2');
        $("#modalInsurance").modal("hide");
    });

    DropDownListInsurances();
});

function DropDownListInsurances() {
    fns.CallGetAsync("api/insurance/dropdown", null, function (zones) {

        var selectTarget = $("#SeguroId");

        var html = "<option disabled selected>Seleccione seguro</option>";

        zones.forEach(x => {
            html += `<option value="${x.id}">${x.name}</option>`;
        });

        selectTarget.html(html);
        selectTarget.select2Validation();
    })
}

function GetInsurances() {

    var id = $("#SitioId").val();

    fns.CallGetAsync("api/insuranceSite/selectInsurances", { id: id }, function (dataResponse) {
        $("#insurancesTable").DataTable().clear();
        $("#insurancesTable").DataTable().rows.add(dataResponse).draw();
    });
}

function UpdateInsurance(id) {
    fns.CallGetAsync(`api/insuranceSite/Find`, { id: id }, function (response) {
        $("#frmInsurance").assignJsonToForm(response);
        console.log(response);
        $('#SeguroId').val(response.seguroId).trigger('change.select2');
    })
    $("#modalTitleInsurance").html("Modificando seguro");
    $("#modalInsurance").modal("show");
}

function RemoveInsurance(id) {
    SweetAlert.RemoveAlert("api/insuranceSite/remove", { Id: parseInt(id) }, null, function (response) {
        GetInsurances();
        if (response) {
            Swal.fire({
                icon: 'success',
                title: 'Logrado',
            });
        } else {
            Swal.fire({
                icon: 'error',
                title: 'A ocurrido un error',
            });
        }
    })
};

function BuildInsurancesDatatable() {
    DataTableHelper.Draw("#insurancesTable", {
        destroy: true,
        orderCellsTop: true,
        fixedHeader: true,
        data: [],
        //scrollX: true,
        columns: [
            {
                data: "id",
                orderable: false,
                render: function (data, type, full, meta) {
                    return `<i onclick="UpdateInsurance(${data})" class="fa fa-pencil-square btnDatatable text-primary"></i>
                            <i onclick="RemoveInsurance(${data})" class="fa fa-trash btnDatatable text-danger"></i>
                            <i onclick="GetLogs('seguro', 'api/insuranceSite/log', ${data})" class="fa fa-history btnDatatable text-dark"></i>`;

                }
            },
            { data: "nombre" }

        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        }
    }).FilterColum();
    GetInsurances();
}

//fin tabla seguros

//tabla permisos

$(function () {
    $("#modalPermission").modal({
        backdrop: "static",
        keyboard: false,
        show: false
    });

    $("#btnAddPermission").click(function () {

        var id = parseInt($("#SitioId").val());

        if (id > 0) {
            $("#modalTitlePermission").html("Agregando nuevo permiso");
            $('#PermisoId').val('').trigger('change.select2');
            $('#frmPermission').trigger("reset");
            $("#sitioIdPermission").val(id);
            $("#modalPermission").modal("show");
        }
    });


    var frmPermission = Validate.Form("#frmPermission", "api/permissionSite/CreateUpdate", {
        rules: {
            PermisoId: {
                required: true,
                min: 1,
            },
            EstadoId: {
                required: true,
                min: 1,
            },
            FechaInicio: {
                required: true
            },
            FechaFin: {
                required: true
            },
            Monto: {
                required: true
            },
            FrecuenciaPago: {
                required: true
            },
            FechaInicioCuotas: {
                required: true
            }
        },
        messages: {
            PermisoId: {
                required: "Seleccione un permiso",
                min: "Seleccione un permiso",
            },
            EstadoId: {
                required: "Seleccione estado",
                min: 'Seleccione estado',
            },
            FechaInicio: {
                required: "Seleccione fecha"
            },
            FechaFin: {
                required: "Seleccione fecha"
            },
            Monto: {
                required: 'Digite el monto'
            },
            FrecuenciaPago: {
                required: "Indique frecuencia de pago"
            },
            FechaInicioCuotas: {
                required: "Seleccione fecha"
            }
        }
    }, function (data) {
        $('#frmPermission').trigger("reset");
        $("#modalPermission").modal("hide");
        GetPermissions();
    });

    $("#btnClosePermission").click(function () {
        frmPermission.resetForm();
        $('#PermisoId').val('').trigger('change.select2');
        $('#EstadoId').val('').trigger('change.select2');
        $("#FechaInicioPermiso").data("DateTimePicker").maxDate(false);
        $("#FechaInicioPermiso").data("DateTimePicker").clear();
        $("#FechaFinPermiso").data("DateTimePicker").minDate(false);
        $("#FechaFinPermiso").data("DateTimePicker").clear();
        $("#modalPermission").modal("hide");
    });

    DropDownListPermissions();
    DropDownListStates();


    $("#FechaInicioCuotasPermiso").datetimepicker({
        format: 'DD/MM/YYYY HH:mm',
        showClose: true,
        useCurrent: false
    });

    $("#FechaFinPermiso").datetimepicker({
        format: 'DD/MM/YYYY HH:mm',
        showClose: true,
        useCurrent: false
    });


    $("#FechaInicioPermiso").datetimepicker({
        format: 'DD/MM/YYYY HH:mm',
        showClose: true,
        useCurrent: false
    });

    $("#FechaFinPermiso").on("dp.change", function () {

        var maxDate = $("#FechaFinPermiso").val();

        console.log("maxDate" + maxDate);

        if (maxDate != null && maxDate != "") {
            $("#FechaInicioPermiso").data("DateTimePicker").maxDate(maxDate)
        }
    })

    $("#FechaInicioPermiso").on("dp.change", function () {

        var minDate = $("#FechaInicioPermiso").val();

        console.log("minDate" + minDate);

        if (minDate != null && minDate != "") {
            $("#FechaFinPermiso").data("DateTimePicker").minDate(minDate)
        }
    })

    //Funcion para solucionar error de que las columnas del datatable aparecen desajustadas al seleccionar el tab
    $("#tabPermissionLink").click(function () {
        $("#permissionsTable").DataTable().columns.adjust();
    })
});

function DropDownListPermissions() {
    fns.CallGetAsync("api/permission/dropdown", null, function (zones) {

        var selectTarget = $("#PermisoId");

        var html = "<option disabled selected>Seleccione permiso</option>";

        zones.forEach(x => {
            html += `<option value="${x.id}">${x.name}</option>`;
        });

        selectTarget.html(html);
        selectTarget.select2Validation();
    })
}

function DropDownListStates() {
    fns.CallGetAsync("api/state/dropdown", null, function (zones) {

        var selectTarget = $("#EstadoId");

        var html = "<option disabled selected>Seleccione estado</option>";

        zones.forEach(x => {
            html += `<option value="${x.id}">${x.name}</option>`;
        });

        selectTarget.html(html);
        selectTarget.select2Validation();
    })
}

function GetPermissions() {

    var id = $("#SitioId").val();

    fns.CallGetAsync("api/permissionSite/selectPermissions", { id: id }, function (dataResponse) {
        $("#permissionsTable").DataTable().clear();
        $("#permissionsTable").DataTable().rows.add(dataResponse).draw();
    });
}

function UpdatePermission(id) {
    fns.CallGetAsync(`api/permissionSite/Find`, { id: id }, function (response) {
        $("#frmPermission").assignJsonToForm(response);
        $('#PermisoId').val(response.permisoId).trigger('change.select2');
        $('#EstadoId').val(response.estadoId).trigger('change.select2');
    })
    $("#modalTitlePermission").html("Modificando permiso");
    $("#modalPermission").modal("show");
}

function RemovePermission(id) {
    SweetAlert.RemoveAlert("api/permissionSite/remove", { Id: parseInt(id) }, null, function (response) {
        GetPermissions();
        if (response) {
            Swal.fire({
                icon: 'success',
                title: 'Logrado',
            });
        } else {
            Swal.fire({
                icon: 'error',
                title: 'A ocurrido un error',
            });
        }
    })
};

function BuildPermissionsDatatable() {
    DataTableHelper.Draw("#permissionsTable", {
        destroy: true,
        orderCellsTop: true,
        fixedHeader: true,
        data: [],
        scrollX: true,
        columns: [
            {
                data: "id",
                orderable: false,
                render: function (data, type, full, meta) {
                    return `<i onclick="UpdatePermission(${data})" class="fa fa-pencil-square btnDatatable text-primary"></i>
                            <i onclick="RemovePermission(${data})" class="fa fa-trash btnDatatable text-danger"></i>
                            <i onclick="GetLogs('permiso', 'api/permissionSite/log', ${data})" class="fa fa-history btnDatatable text-dark"></i>`;

                }
            },
            { data: "nombrePermiso" },
            { data: "monto" },
            { data: "frecuenciaPago" },
            {
                data: "fechaInicio",
                render: function (data, type, full, meta) {
                    return moment(data).format("DD-MM-YYYY HH:mm:ss")
                }
            },
            {
                data: "fechaFin",
                render: function (data, type, full, meta) {
                    return moment(data).format("DD-MM-YYYY HH:mm:ss")
                }
            },
            {
                data: "fechaInicioCuotas",
                render: function (data, type, full, meta) {
                    return moment(data).format("DD-MM-YYYY HH:mm:ss")
                }
            },
            { data: "nombreEstado" },
            { data: "activo" }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        }
    }).FilterColum();
    GetPermissions();
}

//fin tabla permisos

//tabla proveedores

var providerHasTypeData = false;
var providerTypeData = "porcentaje";

$(function () {
    $("#modalProvider").modal({
        backdrop: "static",
        keyboard: false,
        show: false
    });

    $("#btnAddProvider").click(function () {

        var id = parseInt($("#SitioId").val());

        if (id > 0) {
            $("#modalTitleProvider").html("Agregando nuevo proveedor");
            $('#ProviderListId').val('').trigger('change.select2');
            $('#frmProvider').trigger("reset");
            $("#sitioIdProvider").val(id);
            $("#modalProvider").modal("show");
        }
    });


    var frmProvider = Validate.Form("#frmProvider", "api/providerSite/CreateUpdate", {
        rules: {
            ProveedorId: {
                required: true,
                min: 1,
            },
            Porcentaje: {
                required: true,
                number: true,
                min: 0,
                max: 100
            },
            Monto: {
                required: true,
                money: true
            },
        },
        messages: {
            ProveedorId: {
                required: "Seleccione un proveedor",
                min: "Seleccione un proveedor",
            },
            Porcentaje: {
                required: "Digite el porcentaje",
                number: "Solo se permiten números",
                min: "El porcentaje debe ser mayor o igual a cero",
                max: "El porcentaje debe ser menor o igual a cien"
            },
            Monto: {
                required: "Digite el monto",
                money: "Digite un formato válido de dinero"
            },
        }
    }, function (data) {
        $('#frmProvider').trigger("reset");
        $("#modalProvider").modal("hide");
        GetProviders();
    });

    $("#btnCloseProvider").click(function () {
        frmProvider.resetForm();
        $('#ProviderListId').val('').trigger('change.select2');
        $("#modalProvider").modal("hide");
    });

    $("#TipoValor").change(function () {
        var control = $(this);
        var isTypeValuePorcentage = control.prop("checked");

        var porcentaje = $("#divPorcentajeProveedor");
        var monto = $("#divMontoProveedor");

        var inputPorcentaje = $("#porcentajeProveedor");
        var inputMonto = $("#montoProveedor");


        if (isTypeValuePorcentage) {

            inputMonto.rules("remove", "required");
            inputMonto.val('');
            inputMonto.valid();
            monto.hide();
            porcentaje.show();
            inputPorcentaje.rules("add", { required: true });

        }
        else {
            inputPorcentaje.rules("remove", "required");
            inputPorcentaje.val('');
            inputPorcentaje.valid();
            porcentaje.hide();
            monto.show();
            inputMonto.rules("add", { required: true });
        }
    });

});

function GetProviders() {

    var id = $("#SitioId").val();

    fns.CallGetAsync("api/providerSite/selectProviders", { id: id }, function (dataResponse) {
        providerTypeData = dataResponse.some(item => item.monto != "" && item.monto != null && item.monto != 0) ? "monto" : "porcentaje"
        providerHasTypeData = dataResponse.some(item => (item.porcentaje != "" && item.porcentaje != null && item.porcentaje != 0) || (item.monto != "" && item.monto != null && item.monto != 0));

        if (providerHasTypeData) {
            $("#TipoValor").siblings("span.switchery").addClass("disabled-switch");
            if (providerTypeData == "monto") {
                $("#TipoValor").changeSwitch(false);
            }
            else {
                $("#TipoValor").changeSwitch(true);
            }
        }

        $("#providersTable").DataTable().clear();
        $("#providersTable").DataTable().rows.add(dataResponse).draw();
    });
}

function UpdateProvider(id) {
    fns.CallGetAsync(`api/providerSite/Find`, { id: id }, function (response) {
        $("#frmProvider").assignJsonToForm(response);
        $('#ProviderListId').val(response.proveedorId).trigger('change.select2');

        if (providerTypeData == "monto") {
            $("#TipoValor").changeSwitch(false);
        }
        else {
            $("#TipoValor").changeSwitch(true);
        }
    })
    $("#modalTitleProvider").html("Modificando proveedor");
    $("#modalProvider").modal("show");
}

function RemoveProvider(id) {
    SweetAlert.RemoveAlert("api/providerSite/remove", { Id: parseInt(id) }, null, function (response) {
        GetProviders();
        if (response) {
            Swal.fire({
                icon: 'success',
                title: 'Logrado',
            });
        } else {
            Swal.fire({
                icon: 'error',
                title: 'A ocurrido un error',
            });
        }
    })
};

function BuildProvidersDatatable() {
    DataTableHelper.Draw("#providersTable", {
        destroy: true,
        orderCellsTop: true,
        fixedHeader: true,
        data: [],
        //scrollX: true,
        columns: [
            {
                data: "id",
                orderable: false,
                render: function (data, type, full, meta) {
                    return `<i onclick="UpdateProvider(${data})" class="fa fa-pencil-square btnDatatable text-primary"></i>
                            <i onclick="RemoveProvider(${data})" class="fa fa-trash btnDatatable text-danger"></i>
                            <i onclick="GetLogs('proveedor', 'api/providerSite/log', ${data})" class="fa fa-history btnDatatable text-dark"></i>`;

                }
            },
            { data: "nombre" },
            { data: "porcentaje" },
            { data: "monto" }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        }
    }).FilterColum();
    GetProviders();
}

//fin tabla proveedores

//tabla centro de costos

$(function () {
    $("#modalCost").modal({
        backdrop: "static",
        keyboard: false,
        show: false
    });

    $("#btnAddCost").click(function () {

        var id = parseInt($("#SitioId").val());

        if (id > 0) {
            $("#modalTitleCost").html("Agregando nuevo costo");
            $('#CostoId').val('').trigger('change.select2');
            $('#frmCost').trigger("reset");
            $("#sitioIdCost").val(id);
            $("#modalCost").modal("show");
        }
    });


    var frmCost = Validate.Form("#frmCost", "api/costSite/CreateUpdate", {
        rules: {
            CostoId: {
                required: true,
                min: 1,
            },
            Porcentaje: {
                required: true
            },
            Monto: {
                required: true
            },
        },
        messages: {
            CostoId: {
                required: "Seleccione un costo",
                min: "Seleccione un costo",
            },
            Porcentaje: {
                required: "Digite el porcentaje"
            },
            Monto: {
                required: "Digite el monto"
            },
        }
    }, function (data) {
        $('#frmCost').trigger("reset");
        $("#modalCost").modal("hide");
        GetCosts();
    });

    $("#btnCloseCost").click(function () {
        frmCost.resetForm();
        $('#CostoId').val('').trigger('change.select2');
        $("#modalCost").modal("hide");
    });

    DropDownListCosts();
});

function DropDownListCosts() {
    fns.CallGetAsync("api/cost/dropdown", null, function (zones) {

        var selectTarget = $("#CostoId");

        var html = "<option disabled selected>Seleccione costo</option>";

        zones.forEach(x => {
            html += `<option value="${x.id}">${x.name}</option>`;
        });

        selectTarget.html(html);
        selectTarget.select2Validation();
    })
}

function GetCosts() {

    var id = $("#SitioId").val();

    fns.CallGetAsync("api/costSite/selectCosts", { id: id }, function (dataResponse) {
        $("#costCenterTable").DataTable().clear();
        $("#costCenterTable").DataTable().rows.add(dataResponse).draw();
    });
}

function UpdateCost(id) {
    fns.CallGetAsync(`api/costSite/Find`, { id: id }, function (response) {
        $("#frmCost").assignJsonToForm(response);
        $('#CostoId').val(response.costoId).trigger('change.select2');
    })
    $("#modalTitleCost").html("Modificando costo");
    $("#modalCost").modal("show");
}

function RemoveCost(id) {
    SweetAlert.RemoveAlert("api/costSite/remove", { Id: parseInt(id) }, null, function (response) {
        GetCosts();
        if (response) {
            Swal.fire({
                icon: 'success',
                title: 'Logrado',
            });
        } else {
            Swal.fire({
                icon: 'error',
                title: 'A ocurrido un error',
            });
        }
    })
};

function BuildCostsDatatable() {
    DataTableHelper.Draw("#costCenterTable", {
        destroy: true,
        orderCellsTop: true,
        fixedHeader: true,
        data: [],
        //scrollX: true,
        columns: [
            {
                data: "id",
                orderable: false,
                render: function (data, type, full, meta) {
                    return `<i onclick="UpdateCost(${data})" class="fa fa-pencil-square btnDatatable text-primary"></i>
                            <i onclick="RemoveCost(${data})" class="fa fa-trash btnDatatable text-danger"></i>
                            <i onclick="GetLogs('costo', 'api/costSite/log', ${data})" class="fa fa-history btnDatatable text-dark"></i>`;

                }
            },
            { data: "nombre" },
            { data: "porcentaje" },
            { data: "monto" }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        }
    }).FilterColum();
    GetCosts();
}

//fin tabla proveedores