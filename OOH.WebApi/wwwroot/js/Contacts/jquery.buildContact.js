$().ready(function ($) {


    Validate.Form("#formContact", "api/contacts/CEcontact", {
        rules: {
            Nombres: {
                required: true
            },
            Apellidos: {
                required: true
            }, 
            Email: {
                required: true
            }, 
            Telefono: {
                required: true
            }
        },
        messages: {
            required: "Campo requerido bro"
        }
    }, function (data) {
        if (data["state"]) {
            $("#ModalContact").modal("hide");
            LLenarDatatable();
        }
    });



});

function editContact(idContacto) {
    $("#formContact").trigger("reset");
    var idCliente = $("#ClienteId").val();
    $("#ClinId").val(idCliente);

   
    if (idContacto != 0) {

        fns.CallGetAsync("api/contacts/contact", { Id: idContacto }, function (dataRquest) {
            console.log(dataRquest);
            $("#formContact").assignJsonToForm(dataRquest);
            $("#ModalContact").modal("show");
            //$("#nombre").val(dataRquest["nombres"]);
            //$("#apellido").val(dataRquest["apellidos"]);
            //$("#email").val(dataRquest["email"]);
            //$("#telefono").val(dataRquest["telefono"]);
            //$("#celular").val(dataRquest["celular"]);
            //$("#idContacto").val(dataRquest["id"]);
            //$("#rol").val(dataRquest["rol"]);

        });

    }
    if (idCliente != 0) {
        $("#ModalContact").modal("show");
        DropDown();
    }
   

}

function DropDown() {

    fns.CallGetAsync("api/Roles/call", null, function (dataResult) {
        let select = `<select class="form-control number" id="dropdownRoles" name="RolId" >`

        dataResult.forEach(rl => {
            let option = `<option value="` + rl.rolId + `"> ` + rl.nombre + `</option> `;

            select = select + option;
        });
        select = select + "</select>";

        $("#divRoles").html(select);

    });

}

