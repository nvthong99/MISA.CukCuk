

$(document).ready(() => {
    var employee = new EmployeeJS();
})
/**
 * Class quản lý các function cho trang customer
 * author: nvthong ( 27/09/2020)
 */
class EmployeeJS extends baseJS {
    constructor() {
        super();
        this.loadSelectData('#possition-select', '/api/possition', 'Possition');
        this.loadSelectData('#department-select', '/api/department', 'Department');
    }

    initEvents() {
        super.initEvents();
        $('#keep-tab').focus(this.returnFocus);

    }
    /**
    * Lấy dữ liệu thông tin của nhân viên
    * author: nvthong(30/09/2020)
    * */
    getUrlData() {
        return '/api/Employee';
    }
    /**
   * validate trường mã nhân viên và tên nhân viên
   * author: nvthong ( 30/09/2020)
   * */

    validateDialog() {
        return super.validateDialog();
    }

    /**
     * chọn các trường trong dialog để lấy dữ liệu từ dialog
     * author: nvthong(30/09/2020)
     * */
    selectDialog() {
       
        return $('#dialog-employee input,select,textarea');
    }

    /**
     * biding dữ liệu cho các select
     * @param {any} selector id của ô select
     * @param {any} url đường dẫn tới api
     * @param {any} name tên của select
     */
    loadSelectData(selector, url, name) {
        //Lấy data từ sevice

        $.ajax({
            url: url,
            method: "GET",
            contentType: "application/json",
            
        }).done(function (response) {
            
            var sel = $(selector);
            sel.empty();
            $.each(response, function (index, item) {
                var optionText = item[`${name}Name`]
                var optionValue = item[`${name}Id`]
                var opt = $(`<option>` + optionText + `</option>`);
                $(opt).val(optionValue);
                sel.append(opt);
            })
        })
    }

    returnFocus() {
        $('#employee-code').focus();
    }

}
