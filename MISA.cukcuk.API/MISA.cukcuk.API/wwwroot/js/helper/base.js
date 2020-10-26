

$(document).ready(() => {

})
/**
 * Class quản lý các function chung
 * author: nvthong ( 27/09/2020)
 */
class baseJS {


    //#region Hàm Khởi Tạo
    /**
     * hàm khởi tạo
     * author: NVTHong
     * */
    constructor() {
        try {
            this.pagingData(1);
            this.initEvents();
        }
        catch (err) {
            console.log(err);
        }

    }
    //#endregion

    //#region Khởi tạo sự kiện
    /**
     * Khởi tạo sự kiện cho trang 
     * author: nvThong ( 27/09/2020)
     */
    initEvents() {
        // sự kiện click nút thêm trên toolbar
        $("#add-btn").click(this.btnAddOnclick.bind(this));
        //sự kiện cho nút sửa trên toolbar
        $('#edit-btn').click(this.btnEditOnclick.bind(this));
        //sự kiện cho nút xóa trên toolbar
        $('#delete-btn').click(this.btnDeleteOnclick.bind(this));
        // sự kiện cho nút nhân bản trên toolbar
        $('#copy-btn').click(this.btnCopyOnClick.bind(this));
        // sự kiện click nút nạp trên toolbar
        $('#reload-table').click(this.btnReloadOnclick.bind(this));
        // sự kiện click nút exit trên dialog
        $(".body-btn-exit").click(this.btnExitOnclick.bind(this));
        // sự kiện click nút hủy bỏ trên dialog
        $("#cancel").click(this.btnExitOnclick.bind(this));
        // sự kiện khi click nút cất trên dialog
        $('#dialog-btn-save').click(this.bttSaveOnClick.bind(this));

        // sự kiện click nút cất và thêm trên dialog
        $('#save-and-add').click(this.bttSaveOnClick.bind(this))
        // sự kiện blur để validate cho các input có thuộc tính required
        $('input[required]').blur(this.validate);
        // sự kiện khi click vào hàng để sửa và xóa
        $('#tbListData tbody').on('click', 'tr', function (e) {
            if ($(this).hasClass('tr-selected')) {
                $(this).removeClass('tr-selected');
            }
            else {
                $(this).addClass('tr-selected')
            }
            if (!e.ctrlKey) $(this).siblings().removeClass('tr-selected');
        })



        // xử lý phím tắt cho dialog
        $('.base-modal').keydown(function (e) {

            if (e.ctrlKey && e.shiftKey && e.which == 83) $('#save-and-add').click();
            if (e.ctrlKey && ( e.which == 83 || e.which == 115) ) {
                
                e.preventDefault();
                $('#dialog-btn-save').click();
                }
            if (!e.ctrlKey && e.which == 27) $(".body-btn-exit").click();
            if (e.ctrlKey && e.which == 81) $(".body-btn-exit").click();


        });
        $('#err-ok').click(this.hideMessageError);

        $('input[format = "currency"]').on('input change', this.autoFormatInputMoney);

        $('#page-first').click(this.btnPageFirstOnClick.bind(this));
        $('#page-prev').click(this.btnPagePrevOnclick.bind(this));

        $('#page-next').click(this.btnPageNextOnclick.bind(this));

        $('#page-last').click(this.btnPageLastOnClick.bind(this));

        $('#page-refresh').click(this.btnPageRefreshOnClick.bind(this));
        $('#page-now').keydown(function (e) {
            if (e.which == 13) {
                $('#page-refresh').click();
            }
        })
        $('#row-count').keydown(function (e) {
            if (e.which == 13) {
                $('#page-refresh').click();
            }
        })



    }
    //#endregion

    //#region Lấy url api để gửi request
    /**
     * hàm lấy url  data
     * author: nvthong ( 28/09/2020)
     * */

    getUrlData() {
        return;
    }
    //#endregion

    //#region Load dữ liệu lên trang web

    /**
     *  load dữ liệu lên trang web
     * author: nvthong ( 27/09/2020)
     * edit: thêm try..catch  nvthong ( 30/09/2020)
     * @param {int} maxRecord số bản ghi trên 1 trang
     * @param {int} recordBegin vị trí bắt đầu 
     */
    loadData(maxRecord, recordBegin) {
        // làm trống bảng
        $(".content-table table tbody").empty();
        // lấy data từ sevice
        var urlData = this.getUrlData();
        if (maxRecord != 0 && recordBegin >= 0) {
            urlData = `${urlData}/${maxRecord}/${recordBegin}`;
        }
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
                        var td = $(`<td class = "${className}" title = " ` + title + ` " >` + (value || "") + `</td>`);

                        $(tr).append(td);// thêm vào thẻ <tr>

                    })
                    // gán id và data tương ứng cho thẻ <tr>
                    tr.data('id', item[$(".content-table table").attr('fieldId')]);
                    tr.data('data', item);

                    $(".content-table table tbody").append(tr);
                })
                self.loadPagination();
            }
            catch (err) {
                console.log(err);
            }
        })


    }

    /**
     * lấy dữ liệu của từng trang
     * author: NVThong(23/10/2020)
     * @param {int} pageNumber số thứ tự trang
     * 
     */
    pagingData(pageNumber) {
        //tính recordBegin

        var row_count = parseInt($('#row-count').val(), 10);
        var max_row = $('#record-numb').text();
        if (row_count > max_row) {
            row_count = max_row;
            $('#row-count').val(max_row)
        }
        var recordBegin = (pageNumber - 1) * row_count;
        // lấy dữ liệu trang tương ứng
        this.loadData(row_count, recordBegin);
        // load lại thanh panigation
        var maxCell = $('#record-numb').text();

        $("#cell-begin").text(recordBegin + 1);
        var cellEnd = recordBegin + row_count;

        if (cellEnd > maxCell) cellEnd = maxCell;
        $("#cell-end").text(cellEnd);
        $("#page-now").val(pageNumber);
    }
    /**
     * load dữ liệu cho thanh phân trang
     * author: NVthong (23/10/2020)
     * */
    loadPagination() {
        //load tổng số trang và bản ghi
        var row_count = $('#row-count').val();
        var urlData = this.getUrlData() + '/num-paging';
        $.ajax({
            url: urlData,
            method: 'GET'
        }).done(function (res) {
            $('#record-numb').text(res);

            $('#num-page').text(Math.ceil(res / row_count));
        })
    }

    //#endregion

    //#region Format dữ liệu trước khi load

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
                    return format.currency(data);
                    break;
                default:
                    return data;
            }
        } else {
            if (data == 0 && type == 'gender') return 'Nữ';
            else return data;
        }

    }
    /**
   * Tự động format tiền tệ trong ô nhập liệu\
   * author: NVThong (22/10/2020)
   * 
   * */

    autoFormatInputMoney() {
        var value = $(this).val().replaceAll(/\D/g, '');
        value = format.currency(value);
        $(this).val(value);

    }

    //#endregion

    //#region Sự kiện của các button


    //#region các button trên toolbar


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
    /**
  * hàm xử lý sự kiện cho nút nhân bản
  * author: nvthong (21/10/2020)
  * */
    btnCopyOnClick() {
        this.btnEditOnclick();
        this.state = 'add';
    }


    /**
    * sự kiện cho nút xóa - xóa bản ghi đã chọn trên table
    * author: NVThong (03/10/2020)
    * edit: NVThong (23/10/2020) - thêm chức năng xóa nhiều bản ghi cùng lúc
    * */

    btnDeleteOnclick() {
        var self = this;
        //lấy bản ghi cần xóa dữ liệu
        var recordDeletes = $('#tbListData tr[class = tr-selected]');



        var message;
        if (recordDeletes.length == 0) {

            this.showMessageError('Bạn Chưa chọn bản ghi cần xóa');
            return;
        }
        else {
            if (recordDeletes.length == 1) {
                var className = $(tbListData).attr('detectName');
                var name;
                var td = recordDeletes.children();
                $.each(td, function (index, item) {
                    if ($(item).hasClass(className)) {
                        name = $(item).text();
                    }
                })
                message = `Bạn có chắc muốn xóa Nhân viên <<${name}>> không?`
            } else {
                message = 'Bạn có chắc chắn xóa những Nhân viên đã chọn không?'
            }
        }

        // xác nhận lại việc xóa
        this.showMessageConfirm(message, function () {
            $.each(recordDeletes, function (index, record) {
                // lấy id tương ứng với bản ghi
                var idDeleteData = $(record).data('id');
                // gửi id lên sever và xóa dữ liệu
                var url = `${self.getUrlData()}/` + idDeleteData;
                $.ajax({
                    url: url,
                    method: 'DELETE',

                }).done(function (res) {

                    $('#page-refresh').click();
                    self.showSuccessMessage('Đã xóa thành công');

                })
            })

        }, function () { });
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
        //check số lượng bản ghi đã chọn
        
        if (recordEdit.length == 0) {
            this.showMessageError('Bạn Chưa chọn bản ghi!');
            return;
        }
        else if (recordEdit.length > 1) {
            this.showMessageError('Bạn chỉ được chọn 1 bản ghi!');
            return;
        }
        // lấy dữ liệu tương ứng với bản ghi
        var idData = recordEdit.data('id');
        $.ajax(
            {
                url: `${urlData}/${idData}`,
                method: "GET",
                contentType: "application/json"
            }

        ).done(function (response) {

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
        // lấy id của bản ghi đang sửa tương ứng trong data
        this.idEditData = idData;
    }

    //#endregion

    //#region các nút phân trang
    /**
     * sự kiện click của nút first pages (điều hướng đến trang đầu tiên)
     * author: NVThong (23/10/2020)
     * */
    btnPageFirstOnClick() {
        this.pagingData(1);
    }
    /**
     * sự kiện click của nút first pages (điều hướng đến trang đầu tiên)
     * author: NVThong (23/10/2020)
     * */
    btnPagePrevOnclick() {
        var pagePrev = parseInt($("#page-now").val(), 10);
        if (pagePrev <= 1) return;


        this.pagingData(pagePrev - 1);
    }

    /**
     * sự kiện click của nút prev pages (điều hướng đến trang đầu tiên)
     * author: NVThong (23/10/2020)
     * */
    btnPageNextOnclick() {
        var pageNext = parseInt($("#page-now").val(), 10);
        var lastPage = parseInt($('#num-page').text(), 10);
        if (pageNext >= lastPage) return;
        this.pagingData(pageNext + 1);
    }

    /**
     * sự kiện click của nút last pages (điều hướng đến trang đầu tiên)
     * author: NVThong (23/10/2020)
     * */
    btnPageLastOnClick() {
        var lastPage = parseInt($('#num-page').text(), 10);
        this.pagingData(lastPage);
    }

    /**
     * sự kiện click của nút refresh pages (điều hướng đến trang đầu tiên)
     * author: NVThong (23/10/2020)
     * */
    btnPageRefreshOnClick() {
        var page = parseInt($('#page-now').val(), 10);
        var lastPage = parseInt($('#num-page').text(), 10);
        if (page > lastPage) {
            page = lastPage;
            $('#page-now').val(lastPage);
        }
        else if (page < 1) {
            page = 1;
            $('#page-now').val(1);
        }
        this.pagingData(page);
    }

    //#endregion

    //#region Các button trên dialog
    /**
    * sự kiện click cho nút exit của base-modal
    * author: nvthong ( 24/09/2020)
    */
    btnExitOnclick() {
        this.hideBaseModal();
    }


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

                        var dataType = $(field).attr('format');
                        switch (dataType) {
                            case 'number':
                                value = parseInt(value) || 0;
                                break;
                            case 'Date':
                                value = $(field).datepicker('getDate');
                                break;
                            case 'currency':
                                value = value.toString().replaceAll('.', '');
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

                    $('#page-refresh').click();
                    self.hideBaseModal();
                    self.showSuccessMessage(`${notification} thành công!`);

                }).fail(function (err) {
                    
                    var message = err.responseText.replace('System.Exception:', '');
                    var tmp = message.indexOf('\n');
                    message = message.slice(0, tmp);
                    


                    self.showMessageError(message);
                });



                // cất dữ liệu vào database
            }
            else {

                var warningContent = '';
                var labels = $('#dialog-employee label')
                $.each(labels, function (index, label) {
                    var idInput = '#' + label.htmlFor;
                    if ($(idInput).hasClass('required-error')) {
                        warningContent += ', ' + label.title;
                    }
                })
                warningContent = warningContent.replace(/^(, )/, '');
                warningContent += ' không được để trống.'
                self.showMessageError(warningContent);
                
            }

        }
        catch (err) {
            console.log(err);
        }

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
    // #endregion

    //#region Điều khiển cửa sổ dialog
    /**
  * hiển thị thông báo success
  * author: NVThong()26/10/2020
  * @param {string} message nội dung thông báo
  */
    showSuccessMessage(message) {
        
        $('#success-label').text(message);
        $('#message-success').show();
        setTimeout(function () {
            $('#message-success').hide();
        }, 500)
    }


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
        var inputs = $('#base-of-customer input,textarea');

        $.each(inputs, function (index, input) {
            // nếu là radio thì bỏ check
            if ($(input).hasClass('required-error')) {
                $(input).removeClass('required-error')
            }
            if ($(input).attr('type') == 'radio') {
                $(input).prop('checked', false);
            }
            else {
                // không phải radio thì reset giá trị về 0
                $(input).val('');
            }

        })
        // ẩn đi dialog
        $(".base-modal").hide();
    }

    /**
     * hiển thị dialog thông báo 
     * author: NVThong ( 04/10/2020)
     * @param {string} message thông báo cần hiển thị
     */
    showMessageError(message) {


        $('#confirm-cancel').hide();
        $('#confirm-ok').hide();
        $('#err-ok').show();

        $('#message-err .icon-waring').css('background-image', 'url("/content/img/warning-icon.png")');
        $('#err-label').text(message);
        $('#message-err').show();

    }
    /**
     * hiển thị dialog xác nhận
     * author: NVThong ( 04/10/2020)
     * 
     */
    //TODO:  xác nhận qua hộp thoại confirm
    showMessageConfirm(message, yesCallBack, noCallBack) {
        $('#err-label').text(message);
        $('#message-err .icon-waring').css('background-image', 'url("/content/img/help.png")');
        $('#message-err').show();
        $('#confirm-cancel').show();
        $('#confirm-ok').show();
        $('#err-ok').hide();

        $('#confirm-cancel').click(function () {
            $('#message-err').hide();
            noCallBack();
        })
        $('#confirm-ok').click(function () {
            $('#message-err').hide();
            yesCallBack();
        })




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

    /**
    * hàm validate cho các ô nhập liệu có thuộc tính required khi blur
    * author : nvthong ( 25/09/2020)
    * 
    */
    validate() {
        validateForm.inputRequired(this);
    }
    //#endregion

}
