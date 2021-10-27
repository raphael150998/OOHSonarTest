var path = "/";
var fns = {
    PostData: function (ruta, data) {
        var ruta = path + ruta;
        var rt;
        $.ajax({
            url: ruta,
            type: "POST",
            data: data,
            async: false,
            dataType: "json",
            contentType: "application/json; charset=utf-8"
        }).done(function (data) {
            rt = data;
        }).fail(function (xhr, textStatus, errorThrown) {
            alert(xhr.responseText);
            alert(textStatus);
        });
        return rt;
    },

    PostDataAsync: function (ruta, data, callBack) {
        var ruta = path + ruta;
        var rt;
        $.blockUI({ message: "Procesando, favor espere." });
        $.ajax({
            url: ruta,
            type: "POST",
            data: data,
            async: true,
            dataType: "json",
            contentType: "application/json; charset=utf-8"
        }).done(function (data) {
            rt = data;
            $.unblockUI();
            callBack(data);
        }).fail(function (xhr, textStatus, errorThrown) {
            alert(xhr.responseText);
            alert(textStatus);
            $.unblockUI();
        });
        return rt;
    },
    NPostDataAsync: function (ruta, data, callBack) {
        var ruta = path + ruta;
        var rt;
        $.blockUI({ message: "Procesando, favor espere." });
        $.ajax({
            url: ruta,
            type: "POST",
            data: data,
            async: true,
            dataType: "json",
            contentType: "application/json; charset=utf-8"
        }).done(function (data) {
            rt = data;
            $.unblockUI();
            callBack(data);
        }).fail(function (xhr, textStatus, errorThrown) {
            Swal.fire({
                icon: 'error',
                title: 'A ocurrido un error',
            });
            $.unblockUI();
        });
        return rt;
    },
    PostDataNoAsync: function (ruta, data, callBack) {
        var ruta = path + ruta;
        var rt;
        $.blockUI({ message: "Procesando, favor espere." });
        $.ajax({
            url: ruta,
            type: "POST",
            data: data,
            dataType: "json",
            contentType: "application/json; charset=utf-8"
        }).done(function (data) {
            rt = data;
            $.unblockUI();
            callBack(data);
        }).fail(function (xhr, textStatus, errorThrown) {
            alert(xhr.responseText);
            alert(textStatus);
            $.unblockUI();
        });
        return rt;
    },
    CallGet: function (ruta, data) {
        var ruta = path + ruta;
        var rt;
        $.ajax({
            url: ruta,
            type: "GET",
            data: data,
            async: false,
            contentType: "application/json; charset=utf-8"
        }).done(function (data) {
            rt = data;
        }).fail(function (xhr, textStatus, errorThrown) {
            alert(xhr.responseText);
            alert(textStatus);
        });
        return rt;
    },
    CallGetAsync: function (ruta, data, callBack) {
        var ruta = path + ruta;
        var rt;
        $.blockUI({ message: "Procesando, favor espere." });
        $.ajax({
            url: ruta,
            type: "GET",
            data: data,
            async: true,
            contentType: "application/json; charset=utf-8",
            statusCode: {
                403: function() {
                    Swal.fire({
                        icon: 'warning',
                        title: 'Usuario sin acceso a esta sección',
                    });
                }
            }
        }).done(function (data) {
            $.unblockUI();
            rt = data;
            callBack(data);
        }).fail(function (xhr, textStatus, errorThrown) {
            //alert(xhr.responseText);
            //alert(textStatus);
            $.unblockUI();
        });
        return rt;
    }
}

//Metodo Remove de lista
function Remove(Lista, Campo, Dato) {

    var ListaTemporal = [];
    $.each(Lista, function (index, item) {
        if (item[Campo] != Dato) {

            ListaTemporal.push(item);

        } else {
            //Nada
        }
    });
    return ListaTemporal;
}

//Metodo Where para las listas([]) 
function Where(Lista, Campo, Dato) {

    var entradas = 0;
    var obWhere = null;
    $.each(Lista, function (index, item) {
        if (item[Campo] == Dato) {
            if (entradas < 1) {
                entradas++;
                obWhere = item;

            }

        } else {
            //Nada
        }
    });
    return obWhere;
}

//Cortar Texto con split
function CutString(Cadena, IndiceReturn, ParametroCorte) {
    let TextoACortar = Cadena.split(ParametroCorte);
    return TextoACortar[IndiceReturn];
}

//Pasar a mayusculas
function toUpper(control) {
    if (/[a-z]/.test(control.value)) {
        control.value = control.value.toUpperCase();
    }
}