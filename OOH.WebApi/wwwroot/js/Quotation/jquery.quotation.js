$().ready(function ($) {
    quotationDataTable();

});

function LLenarDataTable() {
    fns.CallGetAsync("api/quotation/get", null, function (dataResult) {
        console.log(dataResult);
        $("#tablaQuotation").DataTable().clear();
        $("#tablaQuotation").DataTable().rows.add(dataResult).draw();

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
                data: "cotizacionId",
                orderable: false,
                render: function (data, type, full, meta) {
                    return `
                     <i class="fa fa-pencil-square btnDatatable text-primary" onclick="edit('` + data + `')"></i>
                     <i class="fa fa-trash btnDatatable text-danger" onclick="removeQuotation('` + data + `')"></i>
                     <i onclick="GetLogs('Cotizaciones', 'api/quotation/log', ${full.cotizacionId})" class="fa fa-history btnDatatable text-warning"></i>
                     `;
                }
            },
            {
                data:"cliente"
            },
            {
                data:"fecha"
            },
            {
                data:"agencia"
            },
            {
                data:"estado"
            }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        }
    });
    LLenarDataTable();
}

function edit(id) {

    console.log(id);
    window.open("/Quotation/CreateUpdate/" + id, '_blank');

}

function removeQuotation(idQutotaion) {
    SweetAlert.RemoveAlert("api/quotation/remove", { Id: idQutotaion }, "La cotización sera removido", function (response) {
        console.log(response);
        LLenarDataTable();
    });
   
}