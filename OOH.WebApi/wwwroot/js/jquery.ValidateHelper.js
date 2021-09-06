var Validate = {

    Form: function (identify, config) {

        config.errorPlacement = function (error, element) {
            var placement = $(element).data('error');
            if (placement) {
                $(placement).append(`<i data-toggle="tooltip" data-placement="top" data-trigger="hover" title="${error.text()}" class="fas fa-exclamation-circle error"></i>`);
            } else {
                $(element).parent().append(`<i data-toggle="tooltip" data-placement="top" data-trigger="hover" title="${error.text()}" class="fas fa-exclamation-circle error"></i>`);
                //error.insertAfter(element);
            }
        };
        $(identify).validate(config);

    }
}