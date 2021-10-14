$().ready(function ($) {
    quotationDataTable();

});

function LLenarDataTable() {
    fns.CallGetAsync("api/quotation/get", null, function (dataResult) {

        $("#tablaQuotation").Datatable().DataTable().clear();
        $("#tablaQuotation").Datatable().DataTable().rows.add(dataResult).draw();

    });
}

function quotationDataTable() {
    DataTableHelper.Draw("#tablaQuotation", {
        destroy: true,
        dom: "Bfrltip",
        orderCellsTop: true,
        fixedHeader: true,
        data: [],
        columns: [
            {
                data: "clienteId",
                orderable: false,
                render: function (data, type, full, meta) {
                    return `
                     <i class="fa fa-pencil-square btnDatatable text-primary" onclick="edit('` + data + `')"></i>
                     <i class="fa fa-trash btnDatatable text-danger" onclick="removeClient('` + data + `')"></i>
                     <i onclick="GetLogs('Cliente', 'api/client/log', ${data})" class="fa fa-history btnDatatable text-warning"></i>
                     `;
                }
            }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        }
    });
    LLenarDataTable();
}