var Validate = {

    Form: function (identify, url, config, callback, dataSend = null) {

        var rt;

        config.errorPlacement = function (error, element) {
            var placement = $(element).data('error');
            if (placement) {
                $(placement).append(`<i data-toggle="tooltip" data-placement="top" data-trigger="hover" title="${error.text()}" class="fas fa-exclamation-circle error"></i>`);
            } else {
                $(element).parent().append(`<i data-toggle="tooltip" data-placement="top" data-trigger="hover" title="${error.text()}" class="fas fa-exclamation-circle error"></i>`);
                //error.insertAfter(element);
            }
        };

        //config.highlight = function (element) {

        //};

        //config.success = function (element) {

        //    console.log($(element).closest("i.error"));
        //    //$(element).parents().find($("i.error")[0]).remove();
        //};

        config.submitHandler = function (form) {

            dataSend = (dataSend == null || dataSend == undefined) ? $(form).serializeFormToJson() : JSON.stringify(dataSend);
            console.log(dataSend);
            fns.PostDataNoAsync(url, dataSend, function (dataResult) {
                dataSend = null;
                if (dataResult.state == false) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: dataResult.message
                    });
                } else {
                    if (callback != undefined) {
                        callback(dataResult);
                    }
                    Swal.fire({
                        icon: 'success',
                        title: 'Logrado',

                    })
                }
            })
            return false;
        }
        $(identify).validate(config);
    }
}