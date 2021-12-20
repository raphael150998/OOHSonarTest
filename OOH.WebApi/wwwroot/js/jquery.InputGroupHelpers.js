$().ready(function () {
    var inputs = $(":input");
    InsertarIcono(inputs);
});


function InsertarIcono(inputs) {
    $.each(inputs, function (i, val) {
        var attr = $(val).attr("data-icon");
        if (attr != undefined && attr != "") {
            HtmlInsert(val, attr);
        }

    });

}

function HtmlInsert(input, icono) {
    let html = $(input)[0].outerHTML;
    var inputGroup = `
     <div class="input-group input-group-default">
         <span class="input-group-addon text-dark">`+ icono + `</span>
         ` + html + `
     </div>                     
                   
     `;
    $(input).replaceWith(inputGroup);
}