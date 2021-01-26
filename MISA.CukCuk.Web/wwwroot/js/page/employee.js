$(document).ready(function () {
    new EmployeeJS();
})


class EmployeeJS extends BaseJS {
    constructor() {
        super();
    }

    initEvents() {
        var me = this;
        super.initEvents();
        $('#txtSearchEmployee').blur(function () {
            me.loadData();
        })
       /* $('#cbxPositionFilter').change(function () {
            alert(1);
            me.loadData();
        })
        $('cbxDepartmentFilter').change(function () {
            me.loadData();
        })*/
    }
    setFilter() {
        var departmentId = "";
        var positionId = "";
        // lấy value từ input search
        var keySearch = $('#txtSearchEmployee').val();
        
        this.filter = "/filter?keySearch=" + keySearch + "&departmentId=" + departmentId + "&positionId=" + positionId;
        // Lấy thông tin các cột dữ liệu: 
       
    }

    setApiRouter() {
        this.apiRouter = "/api/employees";
    }

    setId() {
        this.Id = "employeeId";
    }
    setCode() {
        this.Id = "employeeCode";
    }
}

