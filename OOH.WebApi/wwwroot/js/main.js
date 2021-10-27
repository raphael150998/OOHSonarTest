$(function () {
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
});

function GetLogs(entidad, url, id) {

    fns.CallGetAsync(url, { id: id }, function (dataResponse) {
        $("#logTable").DataTable().clear();
        $("#logTable").DataTable().rows.add(dataResponse).draw();
    });

    $("#modalTitleLogs").html(`Registro de actividad de ${entidad} con identificador ${id}`);
    $("#modalLogs").modal("show");
}

//Metodo para crear el Datatable de clientes
function BuildLogDatatable() {
    DataTableHelper.Draw("#logTable", {
        destroy: true,
        orderCellsTop: true,
        fixedHeader: true,
        data: [],
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