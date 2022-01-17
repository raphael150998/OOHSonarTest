

//Metodo para crear el Datatable de clientes
var ButtonExcel = [{
    extend: 'excelHtml5',
    text: 'Archivo Excel',
    titleAttr: 'Exportar a Excel',
    className: 'btn btn-success',
    exportOptions: {
        columns: [1, 2, 3]
    }
},];

var DataTableHelper = {
    Draw: function (identify, config, execelBackend = false) {
        if (execelBackend) {
            config.dom = "Bfrltip";
            config.buttons = ButtonExcel;
        }
        var table = $(identify).DataTable(config);
        return {
            FilterColum: function () {
                $(identify + ' thead tr').clone(true).appendTo(identify + ' thead');
                $(identify + ' thead tr:eq(1) th.filter-action').each(function (e) {
                    $(this).html("");
                    $(this).removeAttr("class");

                });
                $(identify + ' thead tr:eq(1) th.filter').each(function (i) {
                    i = i + 1;

                    var title = $(this).text();
                    $(this).removeAttr("class");
                    $(this).removeAttr("aria-label");
                    $(this).removeAttr("aria-sort");
                    $(this).removeAttr("aria-controls");
                    $(this).removeAttr("style");
                    $(this).removeAttr("text");
                    $(this).removeAttr("tabindex");
                    $(this).html('<input type="text" placeholder="" class="form-control input-viva-filter"/>');

                    $('input', this).on('keyup change', function () {
                        if (table.column(i).search() !== this.value) {
                            table
                                .column(i)
                                .search(this.value)
                                .draw();
                        }
                    });
                });

            },
            ChildRow: function (format) {
                $(identify).DataTable().destroy();
                nConfig = {
                    className: 'details-control',
                    orderable: false,
                    data: null,
                    defaultContent: '<i class="la la-angle-down"></i>'
                };
                config.columns.unshift(nConfig);
                $(identify + " thead tr").prepend("<th></th>");
                var table = $(identify).DataTable(config);

                $(identify + ' tbody').on('click', 'td.details-control', function () {

                    var tr = $(this).closest('tr');

                    var row = table.row(tr);


                    //var td = $(this);
                    //td = td[0];
                    if (row.child.isShown()) {
                        // This row is already open - close it
                        row.child.hide();
                        tr.removeClass('shown');
                    }
                    else {
                        // Open this row
                        row.child(format(row.data())).show();
                        tr.addClass('shown');
                    }
                });
            },
            Table: table
        }
    }
}


