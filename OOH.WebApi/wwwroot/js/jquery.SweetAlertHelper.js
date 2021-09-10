var SweetAlert = {

    ConfirmForm : function (callback, showConfirm = true){
        Swal.fire({
            title: 'Confirmar?',
            text: "La informacion sera enviada!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Confirmar!'
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
    RemoveAlert: function (url, data, callback) {
        Swal.fire({
            title: 'Desea Eliminar?',
            text: "La informacion sera eliminada Permanentemente!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Confirmar!'
        }).then((result) => {
            if (result.isConfirmed) {
                fns.PostDataAsync(url, data, function (response) {
                    callback(response);       
                });
            }
              
        })
    }
}