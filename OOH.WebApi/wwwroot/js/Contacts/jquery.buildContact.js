﻿$().ready(function ($) {


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
    });



});

function editContact(idContacto) {
    $("#ModalContact").modal({
        show: true
    });
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

