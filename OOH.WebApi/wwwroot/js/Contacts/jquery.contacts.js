$().ready(function ($) {

    BuildDatatable();

});

function LLenarDatatable() {
    var idCliente = $("#ClienteId").val();
    if (idCliente != 0) {

        fns.CallGetAsync("api/contacts/list", { clientId: idCliente }, function (dataRequest) {
            console.log(dataRequest);
            $("#tablaContact").DataTable().clear();
            $("#tablaContact").DataTable().rows.add(dataRequest).draw();

        });
    }
   
}

function BuildDatatable() {

    DataTableHelper.Draw("#tablaContact", {
        destroy: true,
        orderCellsTop: true,
        fixedHeader: true,
        data: [],
        columns: [
            {
                data: "id",
                orderable: false,
                render: function (data, type, full, meta) {
                    return `
                     <i class="fa fa-pencil-square btnDatatable text-primary" onclick="editContact('` + data + `')"></i>
                     <i class="fa fa-trash btnDatatable text-danger" onclick="removeContact('`+data+`')"></i>
                     `;
                }
            },
            {
                data: "nombres",
                render: function (data, type, full, meta) {
                    return "<a href='index'>" + data + "</a>";
                }
            },
            { data: "apellidos" },
            { data: "celular" },

        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        }
    });

    LLenarDatatable();
}

function removeContact(idContact) {
    SweetAlert.RemoveAlert("api/contacts/remove", { Id: idContact }, function (response) {

        if (response) {
            Swal.fire({
                icon: 'success',
                title: 'Logrado',
            });
        } else {
            Swal.fire({
                icon: 'error',
                title: 'A ocurrido un error',
            });
        }


    });
}