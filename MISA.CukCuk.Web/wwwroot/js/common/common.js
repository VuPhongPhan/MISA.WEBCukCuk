


//#region Hàm định dạng ngày/tháng/năm
/**----------------------------------
 *Định dạng ngày/tháng/năm
 *CteatedBy: PVPhong (04/01/2021)
 *@param {any} date
 */
function formatDate1(date) {
    var date = new Date(date);
    if (Number.isNaN(date.getTime()) == true) {
        return "";
    } else {
        var day = date.getDate(),
            month = date.getMonth() + 1,
            year = date.getFullYear();
        day = day < 10 ? '0' + day : day;
        month = month < 10 ? '0' + month : month;
        return year + '-' + month + '-' + day;
        // return day + '/' + month + '/' + year;
    }

}

function formatDate(date) {
    if (date == null) {
        return "";
    }
    var date = new Date(date);
    if (Number.isNaN(date.getTime()) == true) {
        return "";
    } else {
        var day = date.getDate(),
            month = date.getMonth() + 1,
            year = date.getFullYear();
        day = day < 10 ? '0' + day : day;
        month = month < 10 ? '0' + month : month;
        return day + '/' + month + '/' + year;
    }

}



//#endregion

//#region Hàm định dạng tiền
/**---------------------------------
 * Hàm định dạng tiền 
 * CreatedBy: PVPhong(04/01/2021)
 * @param {any} money
 */
function formatMoney(money) {
    if (money == null) {
        return "";
    }
    else {
        money = money.toFixed(0).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1.");
        return money;
    }

}
//#endregion


//#region Hàm lấy giá trị combobox Gender
/** ----------------------------------------
 * Hàm lấy name Gender
 * */
function getNameGender(gender) {
    if (gender == 0) {
        gender = "Nữ";
    }
    else if (gender == 1) {
        gender = "Nam"
    } else {
        gender = "Khác";
    }
    return gender;
}
//#endregion


//#region Hàm lấy giá trị combobox WorkStatus
/** ----------------------------------------
 * Hàm lấy name WorkStatus
 * */
function getNameWorkStatus(workStatus) {
    if (workStatus == 1) {
        workStatus = "Đang làm việc";
    }
    else if (workStatus == 2) {
        workStatus = "Nghỉ phép"
    } else {
        workStatus = "Đã nghỉ việc";
    }
    return workStatus;
}


/**
 * format money
 * @param {any} n
 */
function formatNumber(n) {
    return n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ".")
}

/**
 * keyup money
 * @param {any} input
 * @param {any} blur
 */
function formatCurrency(input, blur) {
  
    var input_val = input.val();

    if (input_val === "") { return; }

    var original_len = input_val.length;

    var caret_pos = input.prop("selectionStart");

    if (input_val.indexOf(",") >= 0) {

     
        var decimal_pos = input_val.indexOf(",");

        var left_side = input_val.substring(0, decimal_pos);
        var right_side = input_val.substring(decimal_pos);

        left_side = formatNumber(left_side);

        right_side = formatNumber(right_side);

        if (blur === "blur") {
            right_side += "00";
        }

        right_side = right_side.substring(0, 2);

        input_val = left_side + "." + right_side;

    } else {
      
        input_val = formatNumber(input_val);
        input_val = input_val;
    }

    input.val(input_val);

    var updated_len = input_val.length;
    caret_pos = updated_len - original_len + caret_pos;
    input[0].setSelectionRange(caret_pos, caret_pos);
}

/**
 * Hàm convert money -> string
 * @param {any} money
 * CreatedBy: PVPhong (25/01/2021)
 */
function formatStringMoney(money) {
    var formatMoney = "";
    var string = money.split(".");
    $.each(string, function (index, m) {
         formatMoney += m;
    })
    return parseFloat(formatMoney);
}

/**
 * Hàm lấy code tiếp theo
 * @param {any} code
 * CreatedBy: PVPhong (25/01/2021)
 */
function getNextCode(code) {
    var max = parseInt(code.substring(2));
    code = "NV00" + (max + 1)
    return code;
}

/**
 * Hàm cắt chuối messager dulicate
 * @param {any} msg
 * CreatedBy: PVPhong (25/01/2021)
 */
function getMes(msg) {
    var mes = "";
    $.each(msg, function (index, item) {
        mes += `</br> <i class="fa fa-exclamation-triangle"></i>` + item; 
    })
    return mes;
}







