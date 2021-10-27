$().ready(function ($) {
    var inputs = $(":input");
    AplicarMask(inputs);

});
function cadenaMa(numero) {
    var mask = "";
    var nuevo = "9";
    for (var i = 1; i <= numero - 1; i++) {
        mask = mask + nuevo;
    }
    return mask;
}
function AplicarMask(inputs) {
    $.each(inputs, function (i, val) {
        var attr = $(val).attr("data-mask");
        if (attr != undefined && attr != "") {

            if (attr == "Email") { }
            else if (attr == "RightInt") {
                $("#" + val.id).inputmask("9999", {
                    rightAlign: true,
                    placeholder: '',
                    insertMode: true,
                    showMaskOnHover: false,
                    autoGroup: true
                });
            }
            else if (attr == "NumExp") {
                $("#" + val.id).inputmask("9999", {
                    placeholder: '',
                    insertMode: true,
                    showMaskOnHover: false,
                    autoGroup: true
                });
            }
            else if (attr == "9999") {
                $("#" + val.id).inputmask("9999", {
                    placeholder: '',
                    insertMode: true,
                    showMaskOnHover: false,
                    autoGroup: true
                });
            }
            else if (attr == "NumEntero") {
                $("#" + val.id).inputmask("9999", {
                    placeholder: '',
                    insertMode: true,
                    showMaskOnHover: false,
                    autoGroup: true
                });
            }
            else if (attr == "N9") {
                $("#" + val.id).inputmask("N9", {
                    placeholder: '',
                    insertMode: true,
                    showMaskOnHover: true,
                    definitions: {
                        'N': { validator: "[0-1]", cardinality: 0 }
                    }
                });
            }
            else if (attr == "N99") {
                $("#" + val.id).inputmask("N99", {
                    placeholder: '',
                    insertMode: true,
                    showMaskOnHover: true,
                    definitions: {
                        'N': { validator: "[0-1]", cardinality: 0 }
                    }
                });
            }
            else if (attr == "N999") {
                $("#" + val.id).inputmask("N999", {
                    placeholder: '',
                    insertMode: true,
                    showMaskOnHover: true,
                    definitions: {
                        'N': { validator: "[0-1]", cardinality: 0 }
                    }
                });
            }
            else if (attr == "Fecha") {
                $("#" + val.id).inputmask("99/99/9999", {
                    placeholder: '01/01/2000',
                    insertMode: true,
                    showMaskOnHover: true
                });
            }
            else if (attr == "DUI") {
                $("#" + val.id).attr("maxlength", "10");
                $("#" + val.id).attr("minlength", "10");
                $("#" + val.id).inputmask("99999999-9", {
                    placeholder: '________-_',
                    insertMode: true,
                    showMaskOnHover: true
                });
                $("#" + val.id).prop("disabled", false);
            }
            else if (attr == "NIT") {
                $("#" + val.id).attr("maxlength", "17");
                $("#" + val.id).attr("minlength", "17");
                $("#" + val.id).inputmask("9999-999999-999-9", {
                    placeholder: '____-______-___-_',
                    insertMode: true,
                    showMaskOnHover: true
                });
                $("#" + val.id).prop("disabled", false);
            }
            else if (attr == "Pasaporte") {
                $("#" + val.id).attr("maxlength", "8");
                $("#" + val.id).attr("minlength", "8");
                $("#" + val.id).inputmask("NNNNNNNN", {
                    placeholder: '',
                    insertMode: true,
                    showMaskOnHover: true,
                    definitions: {
                        'N': { validator: "[a-zA-Z0-9]", cardinality: 0 }
                    }
                });
                $("#" + val.id).attr("onkeyup", "toUpper(this)");
                $("#" + val.id).prop("disabled", false);
            } else if (attr == "Registro") {
                $("#" + val.id).inputmask("NMW999999", {
                    placeholder: '',
                    insertMode: true,
                    showMaskOnHover: true,
                    definitions: {
                        'N': { validator: "[aitoAITOZ]", cardinality: 0 },
                        'M': { validator: "[aitoecAITOZCE -]", cardinality: 0 },
                        'W': { validator: "[0-9 -]", cardinality: 0 }
                    }
                });
                $("#" + val.id).attr("onkeyup", "toUpper(this)");
                $("#" + val.id).prop("disabled", false);
            }
            else if (attr == "Carnet") {
                $("#" + val.id).attr("maxlength", "5");
                $("#" + val.id).attr("minlength", "5");
                $("#" + val.id).val($("#" + val.id).val().replace(/[^0-9]/g, ''));
                $("#" + val.id).inputmask("", {
                    placeholder: '',
                    insertMode: false,
                    showMaskOnHover: false
                });
                $("#" + val.id).prop("disabled", false);
            } else if (attr == "DetalleSolicitud") {

                $("#" + val.id).prop("disabled", true);
            }
            else if (attr == "Seleccionar") {

                $("#" + val.id).inputmask("", {
                    placeholder: '',
                    insertMode: false,
                    showMaskOnHover: false
                });
                $("#" + val.id).val("");
                $("#" + val.id).prop("disabled", true);
            }
            else if (attr == "Decimal") {
                $("#" + val.id).inputmask("", {
                    placeholder: '',
                    insertMode: false,
                    showMaskOnHover: false
                });

                $("#" + val.id).addClass('decimales');

                $('decimales').on('input', function () {
                        this.value = this.value.replace(/[^0-9,.]/g, '').replace(/,/g, '.');
                    });

                    let yaesta = false;
                    $('.decimales').on('input', function () {
                        if (yaesta != true) {
                            $(this).attr("maxlength", "8");
                            if (this.value.indexOf(".") > -1) {

                                let n = this.value.length;
                                $(this).attr("maxlength", n + 2);
                                let decimal = ".99";


                                $(this).inputmask(cadenaMa(n) + decimal, {
                                    placeholder: '',
                                    insertMode: false,
                                    showMaskOnHover: false
                                });


                                yaesta = true;

                            }
                        }
                        if (this.value.indexOf(".") < 1) {
                            $(this).attr("maxlength", "8");
                            $(this).inputmask("");
                            yaesta = false;
                        }
                    });
                
            }
            else {
                $("#" + val.id).inputmask(attr);
            }
        }

    });
}