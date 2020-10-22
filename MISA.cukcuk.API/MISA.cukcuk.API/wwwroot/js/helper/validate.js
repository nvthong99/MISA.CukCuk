var validateForm = {
    inputRequired: function (input) {
        var value = $(input).val();
        if (!value || !(value && value.trim())) {
            $(input).addClass('required-error');
            return false;
        }
        else {
            $(input).removeClass('required-error');
            return true;
        }
    }
}