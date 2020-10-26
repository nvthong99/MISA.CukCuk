/**
* format tiền theo mã tiền của khu vực
* author: nvthong (25/09/2020)
* edit: chuyển từ class customerJS qua file chung (28/09/2020)
* @param {string} curencyCode mã tiền cần định dạng, VNĐ -> đ, USA->$
*/

String.prototype.formatMoney = function (currencyCode) {
    return new Intl.NumberFormat('vi', { style: 'currency', currency: currencyCode || 'VND' }).format(this);
}

Number.prototype.formatMoney = function (currencyCode) {
    return new Intl.NumberFormat('vi', { style: 'currency', currency: currencyCode || 'VND' }).format(this);
}
/**
 * chuyển đổi định dạng ngày/tháng/năm cho kiểu date
 * author: NVthong (25/09/2020)
 * edit: chuyển từ class customerJS qua file chung (28/09/2020)
 */
Date.prototype.formatDate = function() {
    var day = this.getDay();
    var month = this.getMonth() + 1;
    if (month < 10) month = '0' + month;
    if (day < 10) day = '0' + day;
    var years = this.getFullYear();
    return `${years}/${month}/${day}`
}

/**
 * chuyển đổi định dạng ngày/tháng/năm cho kiểu string
 * author: NVthong (25/09/2020)
 * edit: chuyển từ class customerJS qua file chung (28/09/2020)
 * 
 */
String.prototype.formatDate = function () {
    return this.replace(/(\d{4})-(\d{2})-(\d{2})\S+/, '$3/$2/$1');
}
/**
 * chuyển đổi định dạng ngày/tháng/năm thành năm-tháng-ngày để dùng cho input type date
 * author: NVthong (02/10/2020)
 * edit: chuyển từ class customerJS qua file chung (28/09/2020)
 */
Date.prototype.formatInputTypeDate = function () {
    var day = this.getDay();
    var month = this.getMonth() + 1;
    if (month < 10) month = '0' + month;
    if (day < 10) day = '0' + day;
    var years = this.getFullYear();
    return `${years}-${month}-${day}`
}
/**
 * chuyển đổi định dạng ngày/tháng/năm thành năm-tháng-ngày để dùng cho input type date
 * cho kiểu string
 * author: NVthong (07/10/2020)
 * 
 */
String.prototype.formatInputTypeDate = function () {
    return this.replace(/(\d{4})-(\d{2})-(\d{2})\S+/, '$1-$2-$3');
}
/**
 * định dạng số điện thoại cho kiểu string
 * author: NVthong (25/09/2020)
 * edit: chuyển từ class customerJS qua file chung (28/09/2020)
 */
String.prototype.formatPhoneNumber = function () {
    return this.replace(/(\d{3})(\d{3})(\d{3})/, '$1-$2-$3');// \d ~ các chữ số 0-9, {3} -> xuất hiện 3 lần
}
/**
 * định dạng số điện thoại cho kiểu number
 * author: NVthong (25/09/2020)
 * edit: chuyển từ class customerJS qua file chung (28/09/2020)
 */
Number.prototype.formatPhoneNumber = function () {
    return this.replace(/(\d{3})(\d{3})(\d{3})/, '$1-$2-$3');// \d ~ các chữ số 0-9, {3} -> xuất hiện 3 lần
}

/**
 * format dữ liệu.
 * author: NVThong (24/10/2020)
 * */
var format = {
    currency: function (value) {
        value = value.toString().replace(/^0+/, '');
        return value.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1.');
    }
}