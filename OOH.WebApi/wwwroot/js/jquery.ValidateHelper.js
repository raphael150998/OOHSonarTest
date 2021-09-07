var Validate = {

    Form: function (identify, url, config, callback) {

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
        config.submitHandler = function (form) {
            var dataSend = $(form).serializeFormToJson();
            fns.PostDataNoAsync(url, dataSend, function (dataResult) {
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