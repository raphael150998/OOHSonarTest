function DropDownListClientes() {

    fns.CallGetAsync("api/category/call", null, function (dataResult) {
        let select = `<select class="form-control" id="dropdownClient" name="CategoriaId" >`

        dataResult.forEach(cat => {
            let option = `<option value="` + cat.categoriaId + `"> ` + cat.nombre + `</option> `;

            select = select + option;
        });
        select = select + "</select>";

        $("#divClient").html(select);

    });

}