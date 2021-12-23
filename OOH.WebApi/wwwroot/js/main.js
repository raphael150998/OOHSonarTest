$(function () {

    $.validator.addMethod('greaterThan', function (value, element, param) {
        return this.optional(element) || parseFloat(value) > parseFloat(param);
    }, jQuery.validator.format("Number must be greater than {0}"));

    $.validator.addMethod('money', function (value, element, param) {
        var re = new RegExp(/^\$?\d+(,\d{3})*(\.\d{1,2})?$/);

        return this.optional(element) || re.test(value);
    }, "Please enter a valid money format");

    BuildLogDatatable();
    $("#btnCloseLog").click(function () {
        $("#logTable").DataTable().clear();
        $("#modalLogs").modal("hide");
    });

    $('.nav li a').click(function () {
        var tab_id = $(this).attr('href');
        $('#tabs-container').find('div.active').removeClass('active');
        $(tab_id).addClass('active');
    });

    $("#modalLogs").on("shown.bs.modal", function () {
        $("#logTable").DataTable().columns.adjust();
    })
});

function GetLogs(entidad, url, id) {

    fns.CallGetAsync(url, { id: id }, function (dataResponse) {
        $("#logTable").DataTable().clear();
        $("#logTable").DataTable().rows.add(dataResponse).draw();
    });

    $("#modalTitleLogs").html(`Registro de actividad de ${entidad} con identificador ${id}`);
    $("#modalLogs").modal("show");
    //$("#logTable").DataTable().columns.adjust();
}

//Metodo para crear el Datatable de clientes
function BuildLogDatatable() {
    var result = DataTableHelper.Draw("#logTable", {
        destroy: true,
        orderCellsTop: true,
        fixedHeader: true,
        data: [],
        scrollX: true,
        columns: [
            //{
            //    data: "agenciaId",
            //    render: function (data, type, full, meta) {
            //        return "<a href='index'>" + data + "</a>";
            //    }
            //},
            { data: "login" },
            { data: "nameUser" },
            { data: "description" },
            {
                data: "actionDate",
                render: function (data, type, full, meta) {
                    return moment(data).format("DD-MM-YYYY HH:mm:ss")
                }
            },
            { data: "platform" },
            { data: "version" },
            { data: "oldVersionJson" },

        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        }
    });
}

function download(url, name) {
    fetch(url)
        .then(resp => resp.blob())
        .then(blob => {
            const url = window.URL.createObjectURL(blob);
            const a = document.createElement('a');
            a.style.display = 'none';
            a.href = url;
            // the filename you want
            a.download = `${name}.pptx`;
            document.body.appendChild(a);
            a.click();
            window.URL.revokeObjectURL(url);
            $.unblockUI();
        })
        .catch(() => { alert('Parece que algo ha sucedido en la descarga, intente nuevamente'); $.unblockUI(); });
}