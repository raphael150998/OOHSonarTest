$().ready(function ($) {
    LLamarSalientes();

    $(".grid").hover(function () {
        var DIV = this;
        var CaraId = $("#CaraId").val();
        if (CaraId == 0) {
            $(DIV).addClass('grid-denied');
        } else {
            $(DIV).removeClass('grid-denied');
        }
    });

    $(".grid").click(function () {
        var DIV = this;
        var Cara = $("#CaraId").val();
        var obj = {
            Id: $(DIV).attr("idsql"),
            CaraId: Cara,
            SalienteId: $(DIV).attr("idgrid")
        };

        if (Cara == 0) {
            //N0 HACE NADA
        } else {
            var flag = DIV.getAttribute("selected");
            if (flag == "true") {
                fns.PostDataAsync("api/salientes/clickRemove", JSON.stringify(obj), function (dataResult) {
                    if (dataResult) {
                        $(DIV).removeClass('item-selected');
                        DIV.setAttribute('selected', false);

                    }
                });

            } else {

                fns.PostDataAsync("api/salientes/clickAdd", JSON.stringify(obj), function (dataResult) {
                    if (dataResult != 0) {
                        console.log(dataResult);
                        $(DIV).addClass('item-selected');
                        DIV.setAttribute('selected', true);
                        DIV.idsql = dataResult;
                        $(DIV).attr("idsql", dataResult);
                    }
                });
            }
        }
    });

});

function LLamarSalientes() {

    fns.CallGetAsync("api/face/salientes/get", { id: $("#CaraId").val() }, function (callback) {
        callback.forEach(x => {
            var DIV = $(".grid[idgrid='" + x.salienteId + "']");
            DIV[0].setAttribute('idsql', x.id);
            DIV[0].setAttribute('selected', true);
            $(DIV).addClass('item-selected');
            $(DIV).attr("idsql", x.id);


        });
    });
}
