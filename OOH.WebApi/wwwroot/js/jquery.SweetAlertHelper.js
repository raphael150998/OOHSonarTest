var SweetAlert = {

    ConfirmForm : function (callback){
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
                Swal.fire({
                    icon: 'success',
                    title: 'Logrado',
                });
                callback(result);
            } 
        })
    }




}