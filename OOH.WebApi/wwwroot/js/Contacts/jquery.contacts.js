$().ready(function ($) {

    BuildDatatable();
    BuildDatatableA();
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
    
    SweetAlert.RemoveAlert("api/contacts/remove", { Id: parseInt(idContact) }, function(response) {

        if (response) {
            Swal.fire({
                icon: 'success',
                title: 'Logrado',
            });
            LLenarDatatable();
        } else {
            Swal.fire({
                icon: 'error',
                title: 'A ocurrido un error',
            });
        }


    });
}






function LLenarDatatableA() {
    console.log("wntro");
    var idCliente = $("#ClienteId").val();
    if (idCliente != 0) {

        fns.CallGetAsync("api/agency/Select", null, function (dataResponse) {
            $("#table1").DataTable().clear();
            $("#table1").DataTable().rows.add(dataResponse).draw();
        });
    }

}

function BuildDatatableA() {

    DataTableHelper.Draw("#table1", {
        destroy: true,
        orderCellsTop: true,
        fixedHeader: true,
        data: [],
        columns: [
            {
                data: "agenciaId",
                orderable: false,
                render: function (data, type, full, meta) {
                    return `<i onclick="UpdateAgency(${data})" class="fa fa-pencil-square btnDatatable text-primary"></i>
                            <i onclick="RemoveAgency(${data})" class="fa fa-trash btnDatatable text-danger"></i>
                            <i onclick="GetLogs('Agencia', 'api/agency/log', ${data})" class="fa fa-history btnDatatable text-dark"></i>`;

                }
            },
            { data: "agenciaId" },
            { data: "nombre" },
            { data: "comision" },

        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        }
    });

    LLenarDatatableA();
}
