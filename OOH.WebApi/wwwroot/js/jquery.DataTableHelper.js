
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

            }
        }
    }
}


