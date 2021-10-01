var Validate = {

    Form: function (identify, url, config, callback, dataSend = null) {

        var rt;

        config.errorPlacement = function (error, element) {
            var placement = $(element).data('error');
            if (placement) {
                $(placement).append(`<i data-toggle="tooltip" data-placement="top" data-trigger="hover" title="${error.text()}" class="fas fa-exclamation-circle error"></i>`);
            } else {
                if ($(element).parent().hasClass("input-group")) {
                    $(element).parent().append(`<i data-toggle="tooltip" data-placement="top" data-trigger="hover" title="${error.text()}" class="fas fa-exclamation-circle error error-group"></i>`);
                }
                else {
                    $(element).parent().append(`<i data-toggle="tooltip" data-placement="top" data-trigger="hover" title="${error.text()}" class="fas fa-exclamation-circle error"></i>`);
                }

                //error.insertAfter(element);
            }
        };

        config.success = function (error, element) {
            $(element).parent().find($("i.error")).remove();
        };

        config.submitHandler = function (form) {
            SweetAlert.ConfirmForm(function () {
                dataSend = (dataSend == null || dataSend == undefined) ? $(form).serializeFormToJson() : JSON.stringify(dataSend);
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
            }, false);
            return false;
        }
        return $(identify).validate(config);
    }
}