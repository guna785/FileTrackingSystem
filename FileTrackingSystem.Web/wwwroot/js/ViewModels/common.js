var addHospitalUrl = "/InsertData/InsertHospital";
var addRequestUrl = "/InsertData/InsertBranch";
var addAdminUrl = "/InsertData/InsertAdmin";
var editHospitalUrl = "/EditData/EditHospital";
var editBranchUrl = "/EditData/EditBranch";

function JsonPOST(url, data) {
    var token = $("#myToken").val();
    $.ajax({
        beforeSend: function (request) {
            request.setRequestHeader("X-CSRF-TOKEN-Medical", token)
        },
        url: url,
        dataType: 'json',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(data),
        processData: false,
        async: true,
        success: function (response) {
            sweetAlert('Congratulations!', response.status, 'success');
            $(".modal").modal("hide");
        },
        error: function (e) {
            swal("Oops", e.responseText, "error");
            // $(".modal").modal("hide");
        }
    });
}

function DoAction(action, data) {

    if (action === "AddHospital") {
        JsonPOST(addHospitalUrl, data);
        LoadData();
    }
    else if (action === "EditHospital") {
        JsonPOST(editHospitalUrl, data);
        LoadData();
    }
    else if (action === "AddBranch") {
        JsonPOST(addRequestUrl, data);
        LoadData();
    }
    else if (action === "EditBranch") {
        JsonPOST(editBranchUrl, data);
        LoadData();
    }
    else if (action === "AddAdmin") {
        JsonPOST(addAdminUrl, data);
        LoadData();
    }
   

}

