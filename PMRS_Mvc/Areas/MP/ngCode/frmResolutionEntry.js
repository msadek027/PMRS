app.controller("myCtrl", function ($scope, $http, $filter) {
    $scope.EventPerm(22);
    $scope.btnSaveValue = "Posting";
    $scope.btnDraftValue = "Draft";
  
    $scope.Status = $scope.ActiveSts;
    $scope.MemberResolutionDetail = "সংসদের অভিমত এই যে, ";

    $http({
        method: "GET",
        url: MyApp.rootPath + "ParliamentSessionInfo/GetActiveSession"
    }).then(function (response) {
        $scope.Sessions = response.data;
    }, function (response) {
        toastr.warning("Error Occurred!");
        });


    $http({
        method: "GET",
        url: MyApp.rootPath + "EmployeeInfo/GetIndividualMPInfo"
    }).then(function (response) {
        $scope.BanglaName = response.data[0].BanglaName;
        $scope.ConstitutentBangla = response.data[0].ConstitutentBangla;
        $scope.ConstitutentNumber = response.data[0].ConstitutentNumber;
    }, function (response) {
        toastr.warning("Error Occurred!");
    });


    $scope.GetResolutionList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "Resolution/GetResolutionList"
        }).then(function (response) {
            if (response.data.length > 0) {
                $('#DepartmentModal').modal('toggle');
                $scope.gridDepartmentOptions.data = response.data;
            }
            else {
                toastr.warning("No Data Found!");
            }
        }, function () {
            toastr.warning("No Data Found!");
        });
    };
    $scope.GetSentResolutionList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "Resolution/GetSentResolutionList"
        }).then(function (response) {
            if (response.data.length > 0) {
                $('#DepartmentModal1').modal('toggle');
                $scope.gridDepartmentOptions1.data = response.data;
            }
            else {
                toastr.warning("No Data Found!");
            }
        }, function () {
            toastr.warning("No Data Found!");
        });
    };

    $scope.GetVoice = function () {
        $('#VoiceModal').modal('toggle');
    };

    var columnDepartmentList = [
        { name: 'MemberResolutionID', displayName: "ID", visible: false },

        { name: 'MemberResolutionDetail', displayName: "সিদ্ধান্ত প্রস্তাব", width: 380, cellTemplate: '<div ng-bind-html="COL_FIELD"></div>'  },
        { name: 'MemberResolutionFIleURL', displayName: "URL", visible: false },
        { name: 'BanglaName', displayName: "প্রস্তাবনা", width: 200 },
        { name: 'ParliamentNo', displayName: "সংসদ নং", width: 150 },
        { name: 'SessionNo', displayName: "অধিবেশন নং", width: 150 },
        { name: 'AcceptanceComment', displayName: "Acceptance Comment", visible: false },
        { name: 'UserName', displayName: "প্রস্তাবনা", width: 250, visible: false },
        { name: 'RDNo', displayName: "আর ডি নং", width: 100, visible: false },
        { name: 'MemberResPriority', displayName: "প্রায়োরিটি", width: 150, visible: false },
        { name: 'DeputySecSignature', displayName: "সহকারী সচিব", width: 120, cellTemplate: '<img src="{{row.entity.DeputySecSignature}}" alt="Not Signed" width="200">' },
        { name: 'JointSecSignature', displayName: "যুগ্ম সচিব", width: 120, cellTemplate: '<img src="{{row.entity.JointSecSignature}}" alt="Not Signed" width="150">' },
        { name: 'SecSignature', displayName: "অতিরিক্ত সচিব", width: 120, cellTemplate: '<img src="{{row.entity.SecSignature}}" alt="Not Signed" width="150">' },
        { name: 'AddSecSignature', displayName: "সচিব", width: 120, cellTemplate: '<img src="{{row.entity.AddSecSignature}}" alt="Not Signed" width="150">' },
        { name: 'SpeakerSignature', displayName: "স্পিকার", width: 120, cellTemplate: '<img src="{{row.entity.SpeakerSignature}}" alt="Not Signed" width="150">' },
        { name: 'Status', displayName: "Status", width: 150, visible: false }
    ];


    $scope.gridDepartmentOptions = {
        enableFiltering: true,
        enableSorting: true,
        enableColumnResizing: true,
        paginationPageSizes: [8, 16, 24],
        paginationPageSize: 8,
        columnDefs: columnDepartmentList,
        rowTemplate: rowTemplate(),
        onRegisterApi: function (gridApi) {
            $scope.gridDepartmentOptions = gridApi;
        }
    };

    $scope.gridDepartmentOptions1 = {
        enableFiltering: true,
        enableSorting: true,
        enableColumnResizing: true,
        paginationPageSizes: [8, 16, 24],
        paginationPageSize: 8,
        columnDefs: columnDepartmentList,
        onRegisterApi: function (gridApi) {
            $scope.gridDepartmentOptions1 = gridApi;
        }
    };

    function rowTemplate() {
        return '<div ng-dblclick="grid.appScope.rowDblClickComp(row)" >' +
            '  <div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div></div>';
    }


    $scope.rowDblClickComp = function (row) {
        $scope.Reset();

        $scope.uiID = row.entity.MemberResolutionID;
        $scope.uiCode = row.entity.MemberResolutionID;
        $scope.MemberResolutionDetail = row.entity.MemberResolutionDetail;
        $scope.frmResolutionInfo.ParliamentSession = row.entity.ParlSessID;
        $scope.Status = row.entity.Status;

        $scope.btnSaveValue = "Update";
        $('#DepartmentModal').modal('hide');
    };
    $scope.DraftData = function () {
        $scope.SaveDb = {};

        $scope.SaveDb.MemberResolutionID = $scope.uiID;
        $scope.SaveDb.MemberResolutionDetail = $scope.MemberResolutionDetail;
        $scope.SaveDb.MemberResolutionFIleURL = $scope.MemberResolutionFIleURL;

        $scope.SaveDb.ParlSessID = $scope.frmResolutionInfo.ParliamentSession;
        $scope.SaveDb.Status = 2;// $scope.Status;

        if ($scope.uiID === '' || typeof $scope.uiID === 'undefined') {
            $http({
                method: "post",
                url: MyApp.rootPath + "Resolution/InsertResolution",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                if (response.data.Status == "Yes") {
                    OperationMsg(response.data.Mode);
                    if (response.data.Mode != "Unique") {
                        $scope.uiCode = response.data.ID;
                        $scope.uiID = response.data.ID;
                        $scope.btnSaveValue = "Update";
                    }
                } else {
                    toastr.error("Failed!");
                }
            });
        } else {
            $http({
                method: "post",
                url: MyApp.rootPath + "Resolution/UpdateResolution",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                if (response.data.Status == "Yes") {
                    OperationMsg(response.data.Mode);
                    if (response.data.Mode != "Unique") {
                        $scope.uiCode = response.data.ID;
                        $scope.uiID = response.data.ID;
                        $scope.btnSaveValue = "Update";
                    }
                } else {
                    toastr.error("Failed!");
                }
            });
        }
    };

    $scope.SaveData = function () {
        $scope.SaveDb = {};

        $scope.SaveDb.MemberResolutionID = $scope.uiID;
        $scope.SaveDb.MemberResolutionDetail = $scope.MemberResolutionDetail;
        $scope.SaveDb.MemberResolutionFIleURL = $scope.MemberResolutionFIleURL;

        $scope.SaveDb.ParlSessID = $scope.frmResolutionInfo.ParliamentSession;
        $scope.SaveDb.Status = $scope.Status;

        if ($scope.uiID === '' || typeof $scope.uiID === 'undefined') {
            $http({
                method: "post",
                url: MyApp.rootPath + "Resolution/InsertResolution",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                if (response.data.Status == "Yes") {
                    OperationMsg(response.data.Mode);
                    if (response.data.Mode != "Unique") {
                        $scope.uiCode = response.data.ID;
                        $scope.uiID = response.data.ID;
                        $scope.btnSaveValue = "Update";
                    }
                } else {
                    toastr.error("Failed!");
                }
            });
        } else {
            $http({
                method: "post",
                url: MyApp.rootPath + "Resolution/UpdateResolution",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                if (response.data.Status == "Yes") {
                    OperationMsg(response.data.Mode);
                    if (response.data.Mode != "Unique") {
                        $scope.uiCode = response.data.ID;
                        $scope.uiID = response.data.ID;
                        $scope.btnSaveValue = "Update";
                    }
                } else {
                    toastr.error("Failed!");
                }
            });
        }
    };

    $scope.UploadDocument = function (FolderName, PrNoTxtFldId) {
        var trackNo = '';
        if ($scope.uiID == undefined || $scope.uiID == "") {
            toastr.warning("Upload File: No Saved Data Found!");
            return false;
        } else {
            trackNo = $scope.uiID;
        }
        var prNo = $scope.uiID;
        //var DocName = $("#DocName").val();
        //if (DocName == null || DocName == "") {
        //    toastr.warning("Please Enter File Name");
        //    return false;
        //}

        var fileUpload = $("#DocFile").get(0);
        var files = fileUpload.files;
        var name = files[0].name;

        var regex = new RegExp("(.*?)\.(pdf)$");
        if (!(regex.test(name))) {
            $('#DocFile').val('');
            alert('Please select correct file format');
            return false;
        }

        if (files[0].size > 2097152) // 2 mb for bytes.
        {
            $('#DocFile').val('');
            alert("File size must under 2mb!");
            return false;
        }
        //var myID = 3; //uncomment this to make sure the ajax URL works
        if (files.length > 0) {
            if (window.FormData !== undefined) {
                var data = new FormData();
                for (var x = 0; x < files.length; x++) {
                    data.append("file" + x, files[x]);
                }

                $.ajax({
                    type: "POST",
                    url: MyApp.rootPath + 'Home/UploadManager?FolderName=' + FolderName + '&PrNo=' + prNo + '&DocName=' + '' + '&TrackNo=' + trackNo,
                    contentType: false,
                    processData: false,
                    data: data,
                    success: function (resposnse) {
                        if (resposnse.Message === "done") {
                            $scope.GetDocinfo($scope.uiID);
                        } else {
                            toastr.warning(" Upload Failed!");
                        }
                    },
                    error: function (xhr, status, p3, p4) {
                        var err = "Error " + " " + status + " " + p3 + " " + p4;
                        if (xhr.responseText && xhr.responseText[0] == "{")
                            err = JSON.parse(xhr.responseText).Message;
                        console.log(err);
                    }
                });
            } else {
                alert("This browser doesn't support HTML5 file uploads!");
            }
        }
    };

    $scope.UpdateUpload = function () {
        $scope.SaveDb = {};

        $scope.SaveDb.MemberResolutionID = $scope.uiID;
        $scope.SaveDb.MemberResolutionFileURL = $scope.MemberResolutionFileURL;

        $http({
            method: "post",
            url: MyApp.rootPath + "ResolutionInfo/UpdateUpload",
            datatype: "json",
            data: JSON.stringify($scope.SaveDb)
        }).then(function (response) {
            if (response.data.Status == "Yes") {
                OperationMsg(response.data.Mode);
                if (response.data.Mode != "Unique") {
                }
            } else {
                toastr.error("Failed!");
            }
        });
    };

    $scope.GetDocinfo = function (TrackNo) {
        $http({
            method: "POST",
            url: MyApp.rootPath + "Home/GetDocInfo",
            params: { PrNo: $scope.uiID, FormName: "frmResolutionInfo", TrackNo: TrackNo }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.MemberResolutionFIleURL = response.data.FilePath;
                //$scope.UpdateUpload()
            }
        }, function () {
            toastr.warning("Error Occurred");
        });
    }

    $scope.ViewDocument = function () {
        if ($scope.MemberResolutionFIleURL !== '') {
            var src = $scope.MemberResolutionFIleURL;
            var win = window.open(src, '_blank');
            win.focus();
        }
        else {
            toastr.warning("No Document to View");
        }
    }

    $scope.Reset = function () {
        $scope.uiID = "";
        $scope.uiCode = "";
      
        $scope.MemberResolutionFIleURL = "";
        $scope.MemberResolutionDetail = "সংসদের অভিমত এই যে, ";
        $scope.frmResolutionInfo.ParliamentSession = undefined;

        $scope.Status = $scope.ActiveSts;
        $scope.btnSaveValue = "Save";
    };
});