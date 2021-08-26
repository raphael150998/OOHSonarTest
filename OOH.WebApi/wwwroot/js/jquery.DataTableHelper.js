
var DataTableHelper = {
    Draw: function (identify, config) {
        
        $(identify).DataTable(config);
        return {
            FilterColum: function () {
                $(identify).DataTable().destroy();
                $(identify + ' thead tr').clone(true).appendTo(identify + ' thead');
              
                $(identify + ' thead tr:eq(1) th.filter').each(function (i) {
                    var title = $(this).text();
                    $(this).removeAttr("class");
                    $(this).removeAttr("aria-label");
                    $(this).removeAttr("aria-sort");
                    $(this).removeAttr("aria-controls");
                    $(this).removeAttr("style");
                    $(this).removeAttr("text");
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
                    console.log(row.data());
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
            }
        }
    }
}


