var SweetAlert = {

    ConfirmForm: function (callback, showConfirm = true) {
        Swal.fire({
            title: '¿Desea guardar?',
            text: "La información sera enviada",
            icon: 'question',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sí',
            cancelButtonText: 'No'
        }).then((result) => {
            if (result.isConfirmed) {
                if (showConfirm) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Exitoso',
                    });
                }
                callback(result);
            }
        })
    },
    RemoveAlert: function (url, data, subtitle, callback) {
        data = JSON.stringify(data);
        Swal.fire({
            title: '¿Desea eliminar?',
            text: subtitle == "" || subtitle == null ? "La información sera eliminada permanentemente" : subtitle,
            icon: 'question',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sí',
            cancelButtonText: 'No'
        }).then((result) => {
            if (result.isConfirmed) {
                fns.NPostDataAsync(url, data, function (response) {
                    callback(response);
                });
            }

        })
    }
}