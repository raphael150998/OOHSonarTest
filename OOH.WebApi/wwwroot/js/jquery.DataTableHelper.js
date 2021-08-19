
var DataTableHelper = {
    Draw: function (identify, config) {
        
        $(identify).DataTable(config);
        return {
            FilterColum: function () {
                $(identify).DataTable().destroy();
                $(identify + ' thead tr').clone(true).appendTo(identify + ' thead');

                $(identify + ' thead tr:eq(1) th').each(function (i) {
                    var title = $(this).text();
                    $(this).removeAttr("class");
                    $(this).removeAttr("aria-label");
                    $(this).removeAttr("aria-sort");
                    $(this).removeAttr("aria-controls");
                    $(this).removeAttr("style");
                    $(this).removeAttr("tabindex");
                    $(this).html('<input type="text" placeholder="" />');

                    $('input', this).on('keyup change', function () {
                        if (table.column(i).search() !== this.value) {
                            table
                                .column(i)
                                .search(this.value)
                                .draw();
                        }
                    });
                });
                var table = $(identify).DataTable(config);

            },
            ChildRow: function (format) {
                var table = $(identify).DataTable().destroy();
                nConfig = {
                    className: 'details-control',
                    orderable: false,
                    data: null,
                    defaultContent: '<i class="la la-angle-down"></i>'
                };
                config.columns.unshift(nConfig);
                $(identify + " thead tr").prepend("<th></th>");
                $(identify).DataTable().clear();
                $(identify).DataTable(config);

                $(identify + ' tbody').on('click', 'td.details-control', function () {

                    var tr = $(this).closest('tr');
                    
                    var row = table.row(tr);
                   

                    var td = $(this);
                    td = td[0];
                    if (row.child.isShown()) {
                        alert("down");
                        // This row is already open - close it
                        row.child.hide();
                        
                        $(td).html('<i class="la la-angle-down"></i>');
                    }
                    else {
                        console.log(row.data());
                        // Open this row
                        row.child(format(row.data())).show();
                        $(td).html('<i class="la la-angle-double-up"></i>');
                    }
                });
            }
        }
    }
}


