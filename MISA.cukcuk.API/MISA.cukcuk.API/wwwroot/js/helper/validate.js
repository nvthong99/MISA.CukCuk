
/**
 * validate dữ liệu cho các input có required
 * author: NVThong (23/10/2020)
 * 
 * */

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
    },

    email: function () {

    }
}