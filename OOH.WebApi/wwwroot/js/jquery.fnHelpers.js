//serializa un formulario como string tipo json para ser enviada
$.fn.serializeFormToJson = function () {
    var rt = {};
    var typeInfo = [];
    var formData = this.serializeArray();
    this.find('input,select,textarea').each((i, value) => {
        var type = $(value).hasClass("number") ? "number" : $(value).hasClass("bool") ? "bool" : "text";
        var name = $(value).attr("name");
        typeInfo.push({
            type: type,
            name: name
        });
    });
    var $radio = $('input[type=radio],input[type=checkbox]', this);
    $.each($radio, function () {
        var valCheckbox = this.checked;
        if (!valCheckbox) formData.push({ name: this.name, value: valCheckbox });
    });
    $.each(formData, function () {
        if (rt[this.name]) {
            if (!rt[this.name].push) {
                rt[this.name] = [rt[this.name]];
            }
            rt[this.name].push(this.value || '');
        } else {
            if (typeInfo.some(x => x.name == this.name && x.type == "number")) rt[this.name] = parseFloat(this.value) || 0;
            else if (typeInfo.some(x => x.name == this.name && x.type == "bool")) {
                console.log(this);
                rt[this.name] = this.value == "on" ? true : false
            }
            else rt[this.name] = this.value || '';
        }
    });
    return JSON.stringify(rt);
};

$.fn.hasAttr = function (name) {
    return this.attr(name) !== undefined;
};

$.fn.tagType = function () {

    let type = this.prop("tagName");

    switch (this.prop("type")) {
        case "checkbox":
            type = "checkbox";
            break;
        case "radio":
            type = "radio";
            break;
        case "submit":
            type = "submit";
            break;
        case "reset":
            type = "reset";
            break;
        case "button":
            type = "button";
            break;
    }
    return type;
};

//Asigna el valor del objeto json correspondiente al controlador del formulario especificado a traves
//de la coincidencia de los nombres del json con los nombres del controlador
$.fn.assignJsonToForm = function (json) {

    this.trigger("reset");

    var typeInfo = [];

    this.find('input,select,textarea').each((i, control) => {
        var tagType = $(control).tagType();
        if (tagType != 'submit' && tagType != 'button' && tagType != 'reset') {

        var type = $(control).hasClass("number") ? "number" : $(control).hasClass("bool") ? "bool" : "text";
        var name = $(control).attr("name");
        if (tagType == "checkbox" && $(control).hasClass("js-single")) {
            if ($(control).hasAttr("checked") && $(control).prop("checked")) {
                $(control).changeSwitch(true);
            }
            else {
                $(control).changeSwitch(false);
            }
        }
        typeInfo.push({
            type: type,
            name: name,
            tagType: tagType
        });
        }
    });

    var jsonKeys = Object.keys(json);

    $.each(typeInfo, (i, controlInfo) => {
        var jKey = jsonKeys.find(x => x.toLowerCase() == controlInfo.name.toLowerCase());
        if (jKey != undefined) {
            jValue = json[jKey];
            let targetControl = $(`[name="${controlInfo.name}"]`)[0];
            switch (controlInfo.tagType) {
                case "checkbox":
                    if ($(targetControl).hasClass("js-single")) {
                        $(targetControl).changeSwitch(jValue);
                    }
                    break;
                case "select":
                    $(targetControl).val(jValue).change();
                    break;
                default:
                    $(targetControl).val(jValue);
                    break;
            }
        }
    })
};

//Cambia el switch que contenga la clase css js-single al valor especificado
$.fn.changeSwitch = function (value) {
    if (this.hasClass("js-single")) {
        if (value == this.prop("checked")) {
            this.trigger("click");
            this.trigger("click");
        }
        else {
            this.trigger("click");
        }
    }
    else {
        console.error("La función changeSwitch solo puede ser usada con checkbox que contengan la clase js-single");
        //console.error("%cLa función changeSwitch solo puede ser usada con checkbox que contengan la clase js-single", 'background: #fff; color: tomato');
    }
}