var addCompanyUrl = "/InsertData/AddCompany";
var editCompanyUrl = "/EditData/EditCompany";
var addBranchUrl = "/InsertData/AddBranch";
var editBranchUrl = "/EditData/EditBranch";
var addAdminUrl = "/InsertData/AddAdmin";
var editAdminUrl = "/EditData/EditAdmin";
var addRoleUrl = "/InsertData/AddRole";
var editRoleUrl = "/EditData/EditRole";
var addEmployeeUrl = "/InsertData/AddEmployee";
var editEmployeeUrl = "/EditData/EditEmployee";
var addClientUrl = "/InsertData/AddClient";
var editClientUrl = "/EditData/EditClient";
var addDocumetUrl = "/InsertData/AddDocument";
var editDocumentUrl = "/EditData/EditDocument";
var addJobTypeUrl = "/InsertData/AddJobType";
var editJobTypeUrl = "/EditData/EditJobType";
var createJobUrl = "/InsertData/AddJob";
var JobStatusUpdateUrl = "/EditData/JobStatusUpdate";

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

    if (action === "AddCompany") {
        JsonPOST(addCompanyUrl, data);
        LoadData();
    }
    else if (action === "EditCompany") {
        JsonPOST(editCompanyUrl, data);
        LoadData();
    }
    if (action === "AddBranch") {
        JsonPOST(addBranchUrl, data);
        LoadData();
    }
    else if (action === "EditBranch") {
        JsonPOST(editBranchUrl, data);
        LoadData();
    }
    if (action === "AddAdmin") {
        JsonPOST(addAdminUrl, data);
        LoadData();
    }
    else if (action === "EditAdmin") {
        JsonPOST(editAdminUrl, data);
        LoadData();
    }
    if (action === "AddRole") {
        JsonPOST(addRoleUrl, data);
        LoadData();
    }
    else if (action === "EditRole") {
        JsonPOST(editRoleUrl, data);
        LoadData();
    }
    if (action === "AddEmployee") {
        JsonPOST(addEmployeeUrl, data);
        LoadData();
    }
    else if (action === "EditEmployee") {
        JsonPOST(editEmployeeUrl, data);
        LoadData();
    }
    if (action === "AddClient") {
        JsonPOST(addClientUrl, data);
        LoadData();
    }
    else if (action === "EditClient") {
        JsonPOST(editClientUrl, data);
        LoadData();
    }
    if (action === "AddDocument") {
        JsonPOST(addDocumetUrl, data);
        LoadData();
    }
    else if (action === "EditDocument") {
        JsonPOST(editDocumentUrl, data);
        LoadData();
    }
    if (action === "AddJobType") {
        JsonPOST(addJobTypeUrl, data);
        LoadData();
    }
    else if (action === "EditJobType") {
        JsonPOST(editJobTypeUrl, data);
        LoadData();
    }
    else if (action === "CreateJob") {
        JsonPOST(createJobUrl, data);
        LoadData();
    }
    else if (action === "ChangeJobStatus") {
        JsonPOST(JobStatusUpdateUrl, data);
        LoadData();
    }
  

}

