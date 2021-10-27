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
        "scrollX": true,
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
                data: "cotizacionId",
                render: function (data, type, full, meta) {

                    var yy = CutString(CutString(full.fecha, 0, " "), 2, "/").substring(2,4);
                   
                    return `COT` + AddCeros(data, 6) + "/" + yy;
                }
            },
            {
                data:"cliente"
            },
            {
                data: "fecha",
                render: function (data, type, full, meta) {
                    return CutString(data, 0, " ");
                }
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
        },
        buttons: [
            {
                extend: 'excelHtml5',
                text: '<i class="fa fa-file-excel-o"></i> <b>Excel</b>',
                titleAttr: 'Excel'
            }
        ]
       
    }).FilterColum();
    LLenarDataTable();
}

function edit(id) {

    console.log(id);
    window.open("/Quotation/CreateUpdate/" + id, '_blank');

}

function removeQuotation(idQutotaion) {
    SweetAlert.RemoveAlert("api/quotation/remove", { Id: idQutotaion }, "La cotización sera removido", function (response) {
        console.log(response);
        if (response["data"]) {
            Swal.fire(
                'Removido',
                'Se removio correctamente',
                'success'
            );
        } else {
            Swal.fire(
                'Error',
                'Ocurrio un error',
                'error'
            );
        }
        LLenarDataTable();
    });
   
}

function refresh() {
    LLenarDataTable();
}

function AddCeros(parametro, cantidad) {

  
    var BaseContatenar = parametro.toString().length;
    var Contador = parseInt(cantidad) - parseInt(BaseContatenar);
    var DataConcatenada = "";

    for (var i = 0; i < Contador; i++) {

        DataConcatenada = DataConcatenada + "0";
    }
    DataConcatenada = DataConcatenada + parametro;
    return DataConcatenada;
}

