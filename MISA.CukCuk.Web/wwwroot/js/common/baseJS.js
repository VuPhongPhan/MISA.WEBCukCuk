

class BaseJS {
    constructor() {
        this.host = "https://localhost:44358"
        this.apiRouter = null;
        this.setApiRouter();

        this.Id = null;
        this.setId();

        this.filter = null;
        this.setFilter();

        this.loadData();
        this.initEvents();

    }

    setFilter() {

    }
    setApiRouter() {

    }
    setId() {
         
    }
    /**
     * Hàm get sự kiện
     * CreatedBy: PVPhong (25/01/2021)
     * */
    initEvents() {
        var me = this;
        me.hideToastError();
        me.hideToastSuccess();
        // Biến lấy CustomerId
        var id = null;
        // sự kiện click khi nhấn thêm mới:
        $('#btnAdd').click(me.Add.bind(me));

        // Load lại dữ liệu khi nhấn button nạp:
        $('#btnRefresh').click(function () {
            me.loadData();
        })

        // Ản form chi tiết khi nhấn hủy:
        $('#btnCancel').click(me.close);

        // Close form chi tiết
        $('#btnClose').click(me.close);


        // Thực hiện lưu dữ liệu khi nhấn button [Lưu] trên form chi tiết:
        $('#btnSave').click(me.Update.bind(me));


        $('#btnExit').click(me.exitPopup);
        // Thực hiện xóa dữ liệu
        $('#btnDelete').click(function () {
            // Lấy CustomerId
            var recordId = id;
            $.ajax({
                url: me.host + me.apiRouter + `/${recordId}`,
                method: "GET"
            }).done(function (res) {
                var code = res.EmployeeCode;
            }).fail(function (res) {

            })
            me.setContentPopupDelete("Bạn có chắc chắn muốn xóa nhân viên có mã này không?");
            me.showPopupDelete();

            $('#deleteEmployee').click(function () {
                $.ajax({
                    url: me.host + me.apiRouter + `/${recordId}`,
                    method: "DELETE"
                }).done(function (res) {
                    me.loadData();
                    me.addContentToastSuccess("Đã xóa thành công!");
                    me.showToastSuccess();
                    me.hidePopupDelete();
                    $('#btnDelete').attr("disabled", true);
                }).fail(function (res) {
                    $('#btnDelete').attr("disabled", true);
                })
            })
            $('#cancel').click(function () {
                me.hidePopupDelete();
            })
            $('#exit').click(function () {
                me.hidePopupDelete();
            })
        })

        // Định dạng money khi nhập vào input
        $("input[data-type='salary']").on({
            keyup: function () {
                formatCurrency($(this));
            },
            blur: function () {
                formatCurrency($(this), this.blur);
            }
        });

        // click chuột vào bản ghi 

        $('table tbody').on('click', 'tr', function () {
            $('tr').find('td').removeClass('row-selected');
            $(this).find('td').addClass('row-selected');
            $('#btnDelete').removeAttr("disabled");
            var recordId = $(this).data('recordId');
            id = recordId;
        })


        // Hiển thị thông tin chi tiết khi nhấn dúp chuột trên 1 bản ghi dữ liệu
        $('table tbody').on('dblclick', 'tr', function () {
            $(this).find('td').addClass('row-selected');
            $('.loading').show();
            // Lấy dữ liệu combobox
            me.loadCbx();

            me.FormMode = "Edit";
            // Lấy khóa chính của bản ghi: 
            var recordId = $(this).data('recordId');
            me.recordId = recordId;
            // Gọi service lấy thông tin chi tiết qua Id:
            $.ajax({
                url: me.host + me.apiRouter + `/${recordId}`,
                method: "GET"
            }).done(function (res) {
                // Bindding dữ liệu lên form dialog: 
                var inputs = $('input[fieldName], select[fieldName]');

                $.each(inputs, function (index, input) {
                    var propertyName = $(this).attr('fieldName');
                    var value = res[propertyName];
                    var type = $(this).attr('type');
                    switch (type) {
                        // binding dữ liệu -> Combobox
                        case "cbxLoad":
                            var propertyValue = $(this).attr('fieldValue');
                            value = res[propertyValue];
                            $(this).val(value);
                            break;

                        // binding dữ liệu -> date
                        case "date":
                            if (value == null) {
                            } else {
                                value = formatDate1(value);
                            }
                            $(this).val(value);
                            break;
                        default: {
                            // binding dữ liệu -> text
                            if (propertyName == "Salary") {
                                value = formatMoney(value);
                            }
                            $(this).val(value);
                            break;
                        }

                    }
                })
                $('.loading').hide();
            }).fail(function (res) {
                $('.loading').hide();
            })
            $('#dialog').show();
        })


        /** ---------
         * Validate bắt buộc nhập
         * CreateBy: PVPhong (15/01/2021)
         */
        $('input[required]').blur(function () {
            // Kiểm tra dữ liệu đã nhập, nếu trống thì cảnh báo
            var value = $(this).val();
            if (!value) {
                $(this).addClass('border-red');
                $(this).attr('title', 'Trường này không được phép để trống')
                $(this).attr("validate", false);
            }
            else {
                $(this).removeClass('border-red');
                $(this).attr("validate", true);
            }
        })

        /** ---------
        * Validate Email đúng định dạng
        * CreateBy: PVPhong (15/01/2021)
        */
        $('input[type="email"]').blur(function () {
            var value = $(this).val();
            var testEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;
            if (!testEmail.test(value)) {
                $(this).addClass('border-red');
                $(this).attr('title', 'Email không đúng định dạng.');
                $(this).attr("validate", false);
            } else {
                $(this).removeClass('border-red');
                $(this).attr("validate", true);
            }
        })
    }

    /**--------------------------------
    *Load dữ liệu
    *CreateBy: PVPhong (04/01/2021)
    */
    loadData() {
        var me = this;
        $('.loading').show();
        $('#btnDelete').attr("disabled", true);
        $('table tbody').empty();
        try {
            var columns = $('table thead th');

            var departmentId = "";
            var positionId = "";
            // lấy value từ input search
            var keySearch = $('#txtSearchEmployee').val();

            var filter = "/filter?keySearch=" + keySearch + "&departmentId=" + departmentId + "&positionId=" + positionId;
            // Lấy thông tin các cột dữ liệu:
            var url = me.host + me.apiRouter + filter;
            //     me.loadCbxFilter();
            // Lấy dữ liệu
            $.ajax({
                url: url,
                method: "GET",
                async: true

            }).done(function (res) {
                $.each(res, function (index, obj) {
                    var tr = $(`<tr></tr>`);
                    $(tr).data("recordId", obj.EmployeeId);
                    $.each(columns, function (index, th) {
                        var td = $(`<td><div><span></span></div></td>`);
                        var fieldName = $(th).attr('fieldName');
                        var value = obj[fieldName];
                        var formatType = $(th).attr('formatType');
                        switch (formatType) {
                            case "ddmmyyyy":
                                value = formatDate(value);
                                td.addClass("align-text-center");
                                break;
                            case "Money":
                                value = formatMoney(value);
                                td.addClass("align-text-right");
                                break;
                            case "Gender":
                                value = getNameGender(value);
                                break;
                            case "WorkStatus":
                                value = getNameWorkStatus(value);
                                break;
                            default:
                                break;
                        }

                        td.append(value);
                        $(tr).append(td);
                    })
                    $('table tbody').append(tr);
                    $('.loading').hide();
                })

            }).fail(function (res) {
                $('.loading').hide();
            })
        } catch (e) {
            // Ghi log lỗi:
            console.log(e);
        }
    }




    /** ------------------------------------------
     * Thêm dữ liệu
     * CreateBy: PVPhong (14/01/2021)_
     * */
    Add() {
        var me = this;
        $('.loading').show();
        // reset lại dialog
        $('input').val('');
        me.FormMode = "Add";
        // Hiển thị dialog thông tin chi tiết:
        $('#dialog').show();
        // Mặc định focus vào input Mã nhân viên
        $('#txtEmployeeCode').focus();
        // LẤy dữ liệu combobox
        me.loadCbx();
        $.ajax({
            url: me.host + me.apiRouter + "/maxcode",
            method: "GET"
        }).done(function (res) {
            var code = getNextCode(res.EmployeeCode);
            $('#txtEmployeeCode').val(code);
            $('.loading').hide();
        }).fail(function (res) {
            $('.loading').hide();
        })
       
    }

    /** ------------------------------------------
    * Sửa dữ liệu
    * CreateBy: PVPhong (14/01/2021)_
    * */
    Update() {
        var me = this;
        // Validate dữ liệu
        var inputValidates = $('input[required], input[type="email"]');
        $.each(inputValidates, function (index, input) {
            $(input).trigger('blur');
        })
        var inputNotValids = $('input[Validate = "false"]');
        if (inputNotValids && inputNotValids.length > 0) {
            me.addContentToastError("Dữ liệu không hợp lệ");
            me.showToastError();
            
            inputNotValids[0].focus();
            return;
        }
        // Thu thập thông tin dữ liệu được nhập -> build thành object
        // Lấy tất cả các control nhập liệu:
        var inputs = $('input[fieldName], select[fieldName]');
        var entity = {};
        $.each(inputs, function (index, input) {
            var propertyName = $(this).attr('fieldName');
            var propertyValue = $(this).attr('fieldValue');
            var value = $(this).children("option:selected").val();
            var name = $(this).val();
            switch (propertyName) {
                case "Salary":
                    name = formatStringMoney(name);
                    break;
                default:
            }

            entity[propertyName] = name;
            entity[propertyValue] = value;
        })

        var method = "POST"
        if (me.FormMode == "Edit") {
            method = "PUT"
            entity.EmployeeId = me.recordId;
        }


        // Gọi Service tương ứng thực hiện lưu dữ liệu: 
        $.ajax({
            url: me.host + me.apiRouter,
            method: method,
            data: JSON.stringify(entity),
            contentType: 'application/json'
        }).done(function (res) {
            // Sau khi lưu thành công -> Đưa ra thông báo, ẩn form , load lại dữ liệu
            if (method == "PUT") {
                me.addContentToastSuccess("Sửa thành công");
                me.showToastSuccess();

            } else {
                me.addContentToastSuccess("Thêm thành công");
                me.showToastSuccess();
            }
            $('#dialog').hide();
            me.loadData();
        }).fail(function (res) {
            var str = getMes(res.responseJSON);
            var msg = str + ".<br/> Vui lòng chỉnh sửa lại!";
            me.setContentPopupNotification(msg);
            me.showPopupNotification();
            me.exitNotification();

        })
    }

    /** ---------------------------------
    * Hàm lấy dữ liệu combobox
    * CreatedBy: PVPhong (24/01/2021)
    * */
    loadCbx() {
        var me = this;
        var selects = $('select[id]');
        selects.empty();
        $.each(selects, function (index, select) {
            var api = $(select).attr('api');
            var fieldName = $(select).attr('fieldName');
            var fieldValue = $(select).attr('fieldValue');
            $('.loading').show();
            // Lấy dữ liệu combobox
            $.ajax({
                url: me.host + api,
                method: "GET",
            }).done(function (res) {
                if (res) {
                    $.each(res, function (index, obj) {
                        var option = $(`<option value="${obj[fieldValue]}">${obj[fieldName]}</option>`);
                        $(`select[fieldName="${fieldName}"]`).append(option);
                    })
                }
                $('.loading').hide();
            }).fail(function (res) {
                $('.loading').hide();
            });
        })
    }

    /** -----------------------------------------
     * Thoát ra form chi tiết dữ liệu
     * CreatedBy: PVPhong (18/01/2021)
     * */
    close() {
        $("#dialog").hide();

        $('table tbody td').removeClass('row-selected');
        $('input[required]').removeClass('border-red');
        $('input[type]').removeClass('border-red');
        /*$.MessageBox({
            buttonFail: {
                cool: {
                    text: "Đóng",
                    customClass: "custom_btnClose",
                }
            },
            buttonDone: {
                cool: {
                    text: "Tiếp tục nhập",
                    customClass: "custom_btnCancel",
                }
            },
            title: "Đóng",
            customClass: "custom_messagebox",
            message: "Bạn có muốn đóng form thông tin   không?",
        }).done(function () {
               
        }).fail(function () {
            $("#dialog").dialog('close');
            $('table tbody td').removeClass('row-selected');
        });*/
    }

    exitPopup() {
        dialogPopup.dialog("close");
    }


    /**
     * Khởi tạo các sự kiện cho popup thông báo
     * */
    addContentPopup(content) {
        $('#cl').html(content);
    }

    //#region Toast
    /**
     * Khởi tạo các sự kiện cho các Toast thông báo
     * */
    initToast() {
        var me = this;
        $('.toast-box .toast-cancel').click(function () {
            /* Ẩn Toast thông báo*/
            me.hideToastSuccess();
            me.hideToastError();
        })
    }
    /* Hiển thị Toast Error */
    showToastError() {
        var me = this;
        $('.toast-error').show();
        setTimeout(function () { me.hideToastError(); }, 3000);

    }
    /* Ẩn Toast Error */
    hideToastError() {
        $('.toast-error').hide();
    }
    /* Thêm nội dung cho từng thông báo thất lỗi */
    addContentToastError(content) {
        $('.toast-error .toast-content').html(content);
    }
    /* Hiển thị Toast Success */
    showToastSuccess() {
        var me = this;
        $('.toast-success').show();
        setTimeout(function () { me.hideToastSuccess(); }, 3000);
    }
    /* Ẩn Toast Success */
    hideToastSuccess() {
        $('.toast-success').hide();
    }
    /* Thêm nội dung cho từng thông báo thành công */
    addContentToastSuccess(content) {
        $('.toast-success .toast-content').html(content);
    }
    //#endregion
    
    /* loadCbxFilter() {
         var me = this;
         var selects = $('select[key]');
         selects.empty();
         $.each(selects, function (index, select) {
             var api = $(select).attr('api');
             var fieldName = $(select).attr('fieldName');
             var fieldValue = $(select).attr('fieldValue');
             $('.loading').show();
             // Lấy dữ liệu combobox
             $.ajax({
                 url: me.host + api,
                 method: "GET",
             }).done(function (res) {
                 if (res) {
                     $.each(res, function (index, obj) {
                         var option = $(`<option value="${obj[fieldValue]}">${obj[fieldName]}</option>`);
                         $(`select[fieldName="${fieldName}"]`).append(option);
                     })
                 }
                 $('.loading').hide();
             }).fail(function (res) {
                 $('.loading').hide();
             });
         })
     }*/


    //#region popup

    /**
     * Hàm ẩn popup thông báo
    * CreatedBy:PVPhong (25/01/2021)
    */
    hidePopupNotification() {
        $('.popup-notification').hide();
    }
    /**
     * Hàm hiển thị popup thông báo
     * CreatedBy:PVPhong (25/01/2021)
     * */
    showPopupNotification() {
        $('.popup-notification').show();
    }
    /**
     * Hàm ẩn popup xác nhận xóa bản ghi
     * CreatedBy:PVPhong (25/01/2021)
     * */
    hidePopupDelete() {
        $('.popup-delete').hide();
    }
    /**
     * Hàm hiển thị popup xóa bản ghi
     * CreatedBy:PVPhong (25/01/2021)
     */
    showPopupDelete() {
        $('.popup-delete').show();
    }
    /**
     * Thêm nội dung cho popup xóa dữ liệu bản ghi
     * CreatedBy: PVPhong (25/01/2021)
     * @param {any} content
     */
    setContentPopupNotification(content) {
        $('.popup-notification .popup-row-content').html(content);
    }
    /**
     * Thêm nội dung cho popup xóa dữ liệu bản ghi
     * CreatedBy: PVPhong (25/01/2021)
     * @param {any} content
     */
    setContentPopupDelete(content) {
        $('.popup-delete .popup-row-content').html(content);
    }

    //#endregion

    /** -------------------------------------------
     * Hàm đóng form thông báo
     * CreatedBy: PVPhong (25/01/2021)
     * */
    exitNotification() {
        var me = this;
        // đóng form thông báo
        $('#btn-closePopup').click(function () {
            me.hidePopupNotification();
        })
        // đóng form thông báo
        $('#btn-closePopup-icon').click(function () {
            me.hidePopupNotification();
        })
    }
    
}