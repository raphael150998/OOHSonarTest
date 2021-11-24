$().ready(function ($) {


    Validate.Form("#formContact", "api/provider/contacts/CEcontact", {
        rules: {
            Contacto: {
                required: true
            },
            Ciudad: {
                required: true
            },
            Direccion: {
                required: true
            },
            Email: {
                required: true
            },
            Celular: {
                required: true
            } ,
            Telefono: {
                required: true
            }
        },
        messages: {
            Contacto: {
                required: "Campo requerido"
            },
            Ciudad: {
                required: "Campo requerido"
            },
            Direccion: {
                required: "Campo requerido"
            },
            Email: {
                required: "Campo requerido"
            },
            Celular: {
                required: "Campo requerido"
            },
            Telefono: {
                required: "Campo requerido"
            }
            
        }
    }, function (data) {
        if (data["state"]) {
            $("#ModalContact").modal("hide");
            GetDatatable();
        }
    });



});
function editContact(idContacto) {
    $("#formContact").trigger("reset");
    var idProveedor = $("#ProveedorId").val();
    $("#ProvId").val(idProveedor);
    console.log(idProveedor);
    console.log($("#ProvId").val());
    console.log($("#ProveedorId").val());
    //$("#ClinId").val(idCliente);
    if ($("#ProveedorId").val() != 0) {

        $("#ModalContact").modal("show");
        fns.CallGetAsync("api/provider/contacts/find", { Id: idContacto }, function (dataRquest) {
            console.log(dataRquest);
            $("#formContact").assignJsonToForm(dataRquest);
            $("#ModalContact").modal("show");
           
        });

    }
}