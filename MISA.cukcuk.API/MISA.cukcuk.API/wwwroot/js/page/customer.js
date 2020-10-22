

$(document).ready(() => {
    var customer = new CustomerJS();
})
/**
 * Class quản lý các function cho trang customer
 * author: nvthong ( 27/09/2020)
 */
class CustomerJS extends baseJS {
    constructor() {
        super();
        this.loadCustomerGroupData();
    }
    /**
     * khởi tạo event cho trang customer
     * author: nvthong ( 01/10/2020)
     * 
     * */
    initEvents() {
        super.initEvents();
        $('#keep-tab').focus(this.returnFocus);
        $('#customer-code').focus(this.backFocus);

        /*$('#edit-customer').click(this.btnEditOnclick.bind(this));*/
        
    }


    /**
     * Lấy url  thông tin của khách hàng
     * author: nvthong(30/09/2020)
     * */
    getUrlData() {
        return '/api/customer';
    }

    /**
     * validate trường mã khách hàng và tên khách hàng
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
        return $('#dialog-customer input,#dialog-customer select,#dialog-customer textarea');
    }

    //#region điều hướng focus
    /**
   * điều hướng focus trở về ô nhập Mã khách hàng
   * author: nvthong ( 24/09/2020)
   */
    returnFocus() {
        $('#customer-code').focus();
    }

    /**
     * điều hướng focus trở về button hủy bỏ khi nhần shift + tab ở ô Mã khách hàng
     * author: nvthong ( 24/09/2020)
     */
    backFocus() {
        $('#customer-code').keydown((e) => {
            if (e.which == 16) {
                $('#customer-code').keydown((event) => {
                    if (event.which == 9) {
                        console.log('shift + tab');
                        $('#btn-help').focus();
                    }
                })
            }
        })
    }
    //#endregion
    /**
     * hàm lấy dữ liệu của các nhóm khách hàng
     * author: NVThong (14/10/2020)
     * */
    loadCustomerGroupData() {
        // lấy dữ liệu từ sevice
        $.ajax({
            
            url: '/api/customer/customer-group',
            method: 'GET',
            contentType: "application/json"
        }).done(function (res) {
            var sel = $('#customer-group');
            $.each(res, function (index, item) {
                var opt = $(`<option>` + item.CustomerGroupName + `</option>`);
                $(opt).val(item.CustomerGroupId);
                sel.append(opt);
            })
            
        })
        //binding lên form nhập.
    }

}
//TODO : config data
