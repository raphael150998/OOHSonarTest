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
            contentType: "application/json; charset=utf-8"
        }).done(function (data) {
            $.unblockUI();
            rt = data;
            callBack(data);
        }).fail(function (xhr, textStatus, errorThrown) {
            alert(xhr.responseText);
            alert(textStatus);
            $.unblockUI();
        });
        return rt;
    }
}