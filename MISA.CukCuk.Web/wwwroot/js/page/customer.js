$(document).ready(function () {
    new CustomerJS();
    dialogDetail = $("#dialog").dialog({
        autoOpen: false,
        fluid: true,
        minWidth: 700,
        resizable: true,
        position: ({ my: "center", at: "center", of: window }),
        modal: true,
    });
    //  $("#dialog").hide()
})


class CustomerJS extends BaseJS {
    constructor() {
        super();
    }

    setApiRouter() {
        this.apiRouter = "/api/customers";
    }
    setId() {
        this.Name = "customerId";
    }
}


