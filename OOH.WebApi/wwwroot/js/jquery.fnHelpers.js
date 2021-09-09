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