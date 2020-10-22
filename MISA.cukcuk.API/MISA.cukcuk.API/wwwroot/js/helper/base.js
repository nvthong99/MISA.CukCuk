

$(document).ready(() => {

})
/**
 * Class quản lý các function chung
 * author: nvthong ( 27/09/2020)
 */
class baseJS {
    constructor() {
        try {
            this.loadData();
            this.initEvents();
        }
        catch (err) {
            console.log(err);
        }

    }

    /**
     * Khởi tạo sự kiện cho trang 
     * author: nvThong ( 27/09/2020)
     */
    initEvents() {
        $(".body-btn-exit").click(this.btnExitOnclick.bind(this));
        $("#cancel").click(this.btnExitOnclick.bind(this));
        $("#add-btn").click(this.btnAddOnclick.bind(this));
        $('#dialog-btn-save').click(this.bttSaveOnClick.bind(this));
        $('#reload-table').click(this.btnReloadOnclick.bind(this));
        $('#tbListData').on('click', 'tr', function () {
            if ($(this).hasClass('tr-selected')) {
                $(this).removeClass('tr-selected');
            }
            else {
                $(this).siblings().removeClass('tr-selected');
                $(this).addClass('tr-selected')
            }

        })
        $('#save-and-add').click(function () { console.log('click  cat va them')})
        $('input[required]').blur(this.validate);
        $('#delete-btn').click(this.btnDeleteOnclick.bind(this));
        $('#edit-btn').click(this.btnEditOnclick.bind(this));
        $('#err-ok').click(this.btnOkErrorOnClick.bind(this));
        $('#err-cancel').click(this.btnCancelErrorOnClick.bind(this));
        $('#copy-btn').click(this.btnCopyOnClick.bind(this));
        $('.base-modal').keydown(function (e) {
            
            if (e.ctrlKey && e.which == 83) {
            
                if (e.shiftKey) $('#save-and-add').click();
                else $('#dialog-btn-save').click();
            }
            else if (e.which == 27) $(".body-btn-exit").click();
            
        });

        // format cho unput type date
        //#region TODO: sử dụng datepicker cho ô nhập ngày tháng năm
        //TODO: sử dụng datepicker cho ô nhập ngày tháng năm
       /* $("#date-of-birth").datepicker({
            dateFormat: 'dd/mm/yy',
            changeMonth: true, //Tùy chọn này cho phép người dùng chọn tháng
            changeYear: true, //Tùy chọn này cho phép người dùng lựa chọn từ phạm vi năm
            showAnim: "slideDown",
            yearRange: "1800:2020",
            
        });
        $("#date-of-birth").datepicker($.datepicker.regional["vi"]);

        $("#date-of-birth").keyup(this.autoCompleteDate);*/
        //TODO: xử lý sự kiện thoát và submit bằng bàn phím của dialog
        // $(".base-modal").keydown(function (e){
        //     switch(e.which){
        //         case 27:
        //              this.hideBaseModal;
        //              debugger
        //             break;
        //         case 13: 
        //             this.bttSaveOnClick.bind(this);
        //     }
        // })

        //#endregion
    }
    /**
     * hàm lấy url  data
     * author: nvthong ( 28/09/2020)
     * */

    getUrlData() {
        return;
    }



    /**
     * load dữ liệu lên trang web
     * author: nvthong ( 27/09/2020)
     * edit: thêm try..catch  nvthong ( 30/09/2020)
     */
    loadData() {
        // làm trống bảng
        $(".content-table table tbody").empty();
        // lấy data từ sevice
        var urlData = this.getUrlData();
        
        self = this;
        $.ajax({
            url: urlData,
            method: "GET",
            data: "",
            contentType: "application/json",
            dataType: "",
           
        }).done(function (response) {
            
            try {
                // load data lên table và gán id cho từng thẻ <tr> tương ứng
                
                var fields = $('#tbListData thead th');
                
                $.each(response, (index, item) => {
                    var tr = $(`<tr></tr>`);// tạo thẻ <tr>
                    $.each(fields, function (index, field) {
                        var fieldName = $(field).attr('fieldName');
                        var format = $(field).attr('format');
                        var value = self.formatData(format, item[fieldName]);
                        var className = $(field).attr('class');
                        var title = item[fieldName];
                        // tạo thẻ <td> với những trường dữ liệu cần show cho người dùng
                        var td = $(`<td ${className} title = " ` + title + ` " >` + (value || "") + `</td>`);

                        $(tr).append(td);// thêm vào thẻ <tr>

                    })
                    // gán id và data tương ứng cho thẻ <tr>
                    tr.data('id', item[$(".content-table table").attr('fieldId')]);
                    tr.data('data', item);
                    
                    $(".content-table table tbody").append(tr);
                })
            }
            catch (err) {
                console.log(err);
            }
        })


    }

    //#region format dữ liệu

    /**
     * format dữ liệu 
     * author: NVthong (02/10/2020)
     * @param {string} type loại dữ liệu cần format(ngày/số điện thoại/tiền)
     * @param {any} data dữ liệu cần format.
     */
    formatData(type, data) {
        if (data) {
            switch (type) {
                case "phone":
                    return data.formatPhoneNumber();
                    break;
                case "date":
                    return data.formatDate();
                    break;
                case "money":
                    return data.formatMoney();
                    break;
                case "gender":
                    if (data == 1) return 'Nam';
                    else return 'Nữ';
                    break;
                default:
                    return data;
            }
        } else {
            if (data == 0 && type == 'gender') return 'Nữ';
            else return data;
        }

    }
    //#endregion

    //#region sự kiện của các button

    /**
     * sự kiện click cho nút exit của base-modal
     * author: nvthong ( 24/09/2020)
     */
    btnExitOnclick() {
        this.hideBaseModal();
    }


    /**
     * sự kiện cho nút Thêm
     * author: nvthong ( 24/09/2020)
     */
    btnAddOnclick() {
        this.state = 'add';
        this.showBaseModal();
        $('#customer-code').focus();
    }

    /**
       * sự kiên cho nút reload
       * author: nvthong ( 30/09/2020)
       * */
    btnReloadOnclick() {
        this.loadData();
    }

    // #endregion

    //#region Thêm thông tin khách hàng


    /**
     * hàm thực thi khi nhấn nút cất trên dialog
     * lấy thông tin trên dialog, lưu trữ và hiển thị thông tin 
     * author: nvthong(3/09/2020)
     * edit: thêm try catch - nvthong ( 30/09/2020)
     * edit: NVThong ( 02/10/2020) - thêm value radio xác định giới tính, khởi tạo Date khi lấy dữ liệu dạng date
     * edit: NVThong ( 03/10/2020) - thêm xử lý khi thực hiện thao tác sửa.
     * edit: NVThong ( 07/10/2020) - gửi thông tin sửa hoặc xóa lên sever bằng ajax
     * edit: NVThong( 08/10/2020 ) - Dùng datepicker để lấy dữ liệu dạng date
     * */

    //TODO: thêm ID cho mỗi opject được thêm
    bttSaveOnClick() {
        var self = this;
        try {
            //validate

            if (this.validateDialog()) {
                var notification;
                var method;
                //thu thập thông tin trên form
                var fields = this.selectDialog(); // chọn các trường để lấy dữ liệu
                switch (this.state) {
                    case 'edit':
                        var dataForm = {};
                        dataForm[$(".content-table table").attr('fieldId')] = this.idEditData;
                        
                        notification = 'Sửa';
                        method = 'PUT';
                        break;
                    case 'add':
                        var dataForm = {};
                        notification = 'Thêm';
                        method = 'POST';
                        
                        break;
                    default:
                        var dataForm = {};
                }

                $.each(fields, function (index, field) {
                    // lấy ra fieldName tương ứng với từng input trên form
                    var fieldName = $(field).attr('name');
                    // nếu là radio thì  kiểm tra checked
                  
                    var typeOfField = $(field).attr('type');
                    if ((typeOfField == 'radio' && field.checked) || (typeOfField != 'radio')) {
                        // lấy value khi: radio thì phải checked hoặc không phải là radio
                        var value = $(field).val(); // lấy giá trị của field

                        // chuyển dữ liệu của field về dữ liệu trùng với DB

                        var dataType = $(field).attr('dataType');
                        switch (dataType) {
                            case 'number':
                                value = parseInt(value) || 0;
                                break;
                            case 'Date':
                                value = $(field).datepicker('getDate');
                                break;
                        }
                      
                        dataForm[fieldName] = value;
                    }
                    
                })
                // thêm vào data
                
                var urlData = self.getUrlData();
                
                $.ajax({
                    url: urlData,
                    method: method,
                    data: JSON.stringify(dataForm),
                    contentType: "application/json",

                }).done(function (res) {
                    self.loadData();
                    self.hideBaseModal();
                    self.showMessageError(`${notification} thành công!`);

                }).fail(function (res) {
                    self.showMessageError(`${notification} thất bại!`);
                });
                
                
                
                // cất dữ liệu vào database
            }
            else {
                self.showMessageError('Bạn điền thiếu thông tin');
            }

        }
        catch (err) {
            console.log(err);
        }

    }

    /**
    * sự kiện cho nút xóa - xóa bản ghi đã chọn trên table
    * author: NVThong (03/10/2020)
    * */
    btnDeleteOnclick() {
        //lấy bản ghi cần xóa dữ liệu
        var recordDeletes = $('#tbListData tr[class = tr-selected]');
        // lấy id tương ứng với bản ghi
        var idDeleteData = recordDeletes.data('id');
        
        if (!idDeleteData) {

            this.showMessageError('Bạn Chưa chọn bản ghi cần xóa');
            return;
        }
        
        // xác nhận lại việc xóa
        var isDelete = confirm("Bạn có chắc chắn xóa bản ghi không!");
        if (isDelete) {
            debugger
            // gửi id lên sever và xóa dữ liệu
            var self = this;
            var url = `${this.getUrlData()}/` + idDeleteData;
            $.ajax({
                url: url,
                method: 'DELETE',

            }).done(function (res) {
                
                self.showMessageError('xóa thành công');
                self.loadData();

            });

            
            
        }



    }



    /**
     * xử lý sư kiện click cho nút sửa
     * author: NVThong (02/10/2020)
     * edit: NVThong ( 03/10/2020) - lấy id của bản ghi đã sửa
     * edit : NVThong (08/10/2020) - dùng datepicker để gán dữ liệu dạng date
     * */
    //TODO: xử lý dữ liệu sau khi sửa
    btnEditOnclick() {
        this.state = 'edit';
        var self = this;
        //lấy bản ghi cần sửa dữ liệu
        var urlData = this.getUrlData();
        var recordEdit = $('#tbListData tr[class = tr-selected]');
        // lấy dữ liệu tương ứng với bản ghi
        
        var idData = recordEdit.data('id');
        if (!idData) {
          
            this.showMessageError('Bạn Chưa chọn bản ghi');
            return;
        }

        $.ajax(
            {
                url: `${urlData}/${idData}`,
                method: "GET",
                contentType: "application/json"
            }
            
        ).done(function (response) {
            debugger
            //binding dữ liệu lên dialog để sửa 
            var editData = response;
            self.showBaseModal();
            var fields = self.selectDialog();
            $.each(fields, function (index, field) {
                var fieldName = $(field).attr('name');

                if ($(field).attr('type') == 'date') {

                    var value;
                    if (editData[fieldName]) value = editData[fieldName].replace(/(\d{4})-(\d{2})-(\d{2})\S+/, '$1-$2-$3');
                    $(field).val(value);
                    debugger
                }
                else {
                    var value = editData[fieldName];
                    if ($(field).attr('type') == 'radio') {
                        if ($(field).val() == value) {
                            $(field).prop('checked', true);
                        }
                    } else {
                        $(field).val(value);
                    }

                }



        })
        

        })
        

        // lưu lại dữ liệu được sửa.
        // lấy id của bản ghi đang sửa tương ứng trong data
        //TODO: data đang config(sửa)
        this.idEditData = idData;
       /* this.indexEditData = data.findIndex(obj => obj[$(".content-table table").attr('fieldId')] == idData);*/
        // lấy id của bản ghi đã sửa để lưu lại ở hàm sự kiện của nút cất
    }

    /**
     * validate các trường cần thiết trong form/dialog
     * author: nvthong ( 30/09/2020)
     * */
    validateDialog() {
        // chọn trường cần validate
        var inputRequireds = $('input[required]');
        // mặc định validate sẽ là true
        var validate = true;
        $.each(inputRequireds, function (index, input) {
            // validate cho từng trường
            validate = validateForm.inputRequired(input) && validate;

        })
        // trả về giá trị validate
        return validate;
    }
    /**
     * select các trường đê lấy dữ liệu trong dialog
     * author: nvthong ( 30/09/2020)
     * */
    selectDialog() {

    }


    //#endregion

    //#region điều khiển cửa sổ dialog

    /**
     * hiển thị dialog thông tin chi tiết khách hàng
     * author: nvthong ( 24/05/2020)
     */
    showBaseModal() {
        $("#base-of-customer").show();
        $('.base-modal input')[0].focus();
    }
    /**
     * ẩn dialog thông tin chi tiết khách hàng
     * author: nvthong ( 24/09/2020)
     * edit: NVThong (02\10\2020) - xóa mọi dữ liệu trên form khi đóng dialog
     */
    hideBaseModal() {
        //lấy các input
        var inputs = $('#base-of-customer input,select,textarea');

        $.each(inputs, function (index, input) {
            // nếu là radio thì bỏ check
            if ($(input).attr('type') == 'radio') {
                $(input).prop('checked', false);
            }
            else {
                // không phải radio thì reset giá trị về 0
                $(input).val('');
            }

        })
        // ẩn đi dialog
        $("#base-of-customer").hide();
    }

    /**
     * hiển thị dialog thông báo 
     * author: NVThong ( 04/10/2020)
     * @param {string} message thông báo cần hiển thị
     */
    showMessageError(message) {
        
        
        $('#err-cancel').hide();
        $('#err-label').text(message);
        $('#message-err').show();
       
    }
    /**
     * hiển thị dialog xác nhận
     * author: NVThong ( 04/10/2020)
     * 
     */
    //TODO:  xác nhận qua hộp thoại confirm
    showMessageConfirm() {
        
        $('#message-err').show();
    }
    /**
     * Ẩn dialog thông báo 
     * author: NVThong ( 04/10/2020)
     * 
     */
    hideMessageError() {
        $('#message-err').hide();
    }
    /**
     * hàm xử lý sự kiện cho nút OK trong cửa sổ thông báo
     * author: NVThong ( 04/10/2020)
     */

    btnOkErrorOnClick() {
        this.hideMessageError();
        return true;
    }
    /**
    * hàm xử lý sự kiện cho nút OK trong cửa sổ thông báo
    * author: NVThong ( 04/10/2020)
    */

    btnCancelErrorOnClick() {
        this.hideMessageError();
        return false;
    }
    //#endregion
    /**
     * hàm xử lý sự kiện cho nút nhân bản
     * author: nvthong (21/10/2020)
     * */
    btnCopyOnClick() {
        this.btnEditOnclick();
        this.state = 'add';
    }
    /**
     * hàm validate cho các ô nhập liệu có thuộc tính required khi blur
     * author : nvthong ( 25/09/2020)
     * 
     */
    validate() {
        validateForm.inputRequired(this);
    }

    /**
     * hàm tự động điền thêm dấu / cho input nhập ngày 
     * author: NVThong (12/10/2020)
     * */
    autoCompleteDate() {
        var value = $(this).val();
        
        if (value.length == 2 || value.length == 5) value = value + `/`;
        $(this).val(value);

    }

    
}
