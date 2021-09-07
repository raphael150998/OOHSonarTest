$().ready(function ($) {

    
});

function SwalCreateEdit(idcontacto) {
    var idCliente = $("#ClienteId").val();
    if (idCliente != 0) {
        Swal.fire({
            title: "<div>Agregar Contacto</div>",
            html: HtmlCuerpoModal(idcontacto),
            showConfirmButton: true,
            allowOutsideClick: false,
            focusConfirm: false,
            showCancelButton: true,
            cancelButtonColor: '#d33',
            confirmButtonText: 'Guardar',
            cancelButtonText: 'Cancelar',
            width: '65%'
        }).then((result) => {
            if (result.isConfirmed) {
                var objetoEnviar = {
                    Id: idcontacto,
                    ClienteId: idCliente,
                    Nombres: $("#nombre").val(),
                    Apellidos: $("#apellido").val(),
                    Rol: $("#rol").val(),
                    Email: $("#email").val(),
                    Telefono: $("#telefono").val(),
                    Celular: $("#celular").val(),
                };
                console.log(objetoEnviar);
                fns.PostDataAsync("api/contacts/CEcontact", objetoEnviar, function (dataRequest) {

                    if (dataRequest["state"] != false) {
                        LLenarDatatable();
                        Swal.fire(
                            'Logrado!',
                            'Se a guardado con exito',
                            'success'
                        );

                    } else {

                        Swal.fire(
                            'Error!',
                            'No se a podido guardar',
                            'error'
                        )

                    }

                });
            }
        });
    } 
}


function editContact(idContacto) {
    SwalCreateEdit(idContacto);
    fns.CallGetAsync("api/contacts/contact", { Id: idContacto }, function (dataRquest) {
        console.log(dataRquest);
        $("#nombre").val(dataRquest["nombres"]);
        $("#apellido").val(dataRquest["apellidos"]);
        $("#email").val(dataRquest["email"]);
        $("#telefono").val(dataRquest["telefono"]);
        $("#celular").val(dataRquest["celular"]);
        $("#idContacto").val(dataRquest["id"]);
        $("#rol").val(dataRquest["rol"]);

    });
   

}

function HtmlCuerpoModal(idcontacto) {

    var html = `
    <div class="container">

           <input class="form-control" id="idContacto" hidden />
            
            <div class="col-md-12">
                <label class="label-control">Nombre</label>
                <input class="form-control" id="nombre"/>
            </div>
            <div class="col-md-12">
                <label class="label-control">Apellido</label>
                <input class="form-control" id="apellido"/>
            </div>
            <div class="col-md-12">
                <label class="label-control">Email</label>
                <input class="form-control" id="email"/>
            </div>
        <div class="row">

            <div class="col-md-4">
                <label class="label-control">Celular</label>
                <input class="form-control" id="celular"/>
            </div>
            <div class="col-md-4">
                <label class="label-control">Telefono</label>
                <input class="form-control" id="telefono" />
            </div>

            <div class="col-md-4">
                <label class="label-control">Rol</label>
                <input class="form-control" id="rol"/>
            </div>
        </div>
        
    </div>
     `;
    return html;
}