var SweetAlert = {

    ConfirmForm : function (callback, showConfirm = true){
        Swal.fire({
            title: 'Confirmar?',
            text: "La informacion sera enviada!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Confirmar',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.isConfirmed) {
                if (showConfirm) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Logrado',
                    });
                }
                callback(result);
            } 
        })
    },
    RemoveAlert: function (url, data, subtitle ,callback) {
        data = JSON.stringify(data);
        console.log(data);
        Swal.fire({
            title: 'Desea Eliminar?',
            text: subtitle == "" ? "La informacion sera eliminada Permanentemente!" : subtitle,
            icon: 'question',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Confirmar',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.isConfirmed) {
                fns.NPostDataAsync(url, data, function (response) {
                    callback(response);       
                });
            }
              
        })
    }
}