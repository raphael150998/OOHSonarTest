$().ready(function ($) {

    DrawDataTable();

});

function GetDatatable() {
    fns.CallGetAsync("api/provider/contacts/get", { Id: $("#ProveedorId").val() }, function (contactsResult) {

        $("#ContactsDT").DataTable().clear();
        $("#ContactsDT").DataTable().rows.add(contactsResult).draw();

    });

}

function DrawDataTable() {

    DataTableHelper.Draw("#ContactsDT", {
        destroy: true,
        orderCellsTop: true,
        dom: "Bfrltip",
        buttons: [
            {
                text: '<i class="fa fa-plus" id="addbtn" ></i>',
                action: function (e, dt, node, config) {
                    editContact(0);
                }
            }
        ],
        fixedHeader: true,
        data: [],
        columns: [
            {
                data: "id",
                orderable: false,
                render: function (data, type, full, meta) {
                    return `
                     <i class="fa fa-pencil-square btnDatatable text-primary" onclick="editContact('` + data + `')"></i>
                     <i class="fa fa-trash btnDatatable text-danger" onclick="removeContact('`+ data + `')"></i>
                     `;
                }
            },
            {
                data: "contacto",
                
            },
            { data: "celular" },
            { data: "telefono" },
            { data: "email" },

        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        }
    });
    GetDatatable();
}

function removeContact(idContact) {

    SweetAlert.RemoveAlert("api/provider/contacts/remove", { Id: parseInt(idContact) }, "", function (response) {

        if (response) {
            Swal.fire({
                icon: 'success',
                title: 'Logrado',
            });
            GetDatatable();
        } else {
            Swal.fire({
                icon: 'error',
                title: 'A ocurrido un error',
            });
        }


    });
}

