app.controller("myCtrl", function ($scope, $http, $filter) {
    $scope.EventPerm(19);
    $scope.btnSaveValue = "Posting";
    $scope.DataMode = "Backward";
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
        url: MyApp.rootPath + "ResolutionInfo/GetWorkflow"
    }).then(function (response) {
        $scope.WorkflowList = response.data;
    }, function (response) {
        toastr.warning("Error Occurred!");
    });
    $scope.frmSrAssistantSecretaryApproval = {
        SignTo: 1 // Set your default value here
    };

    $http({
        method: "GET",
        url: MyApp.rootPath + "ResolutionApproval/GetApprovalStatus"
    }).then(function (response) {
        $scope.AprSts = response.data;
    }, function (response) {
        toastr.warning("Error Occurred!");
    });


    $scope.GetWaitingListForSpeaker = function () {
        $scope.gridResolutionOptions.data = [];
        $http({
            method: "POST",
            url: MyApp.rootPath + "ResolutionApproval/GetWaitingListForSpeaker",
            data: { session: $scope.frmSpeakerApproval.ParliamentSession }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridResolutionOptions.data = response.data;
            }
            else {
                toastr.warning("No Data Found!");
            }
        }, function () {
            toastr.warning("No Data Found!");
        });
    };
   
    var columnResolutionList = [
        {
            field: 'selectData',
            displayName: "",
            width: '45',
            cellTemplate: '<div class="checkbox checkbox-primary"> <input type="checkbox" ng-click="grid.appScope.SelectRow(row)" class="ng-scope" value="true" ng-model="row.entity.selectData" /> </div>',
            enableFiltering: false, enableSorting: false
        },
        { name: 'ResolutionApproveID', displayName: "ID", visible: false },
        { name: 'MemberResolutionID', displayName: "ID", visible: false },
        { name: 'MemberResolutionDate', displayName: "প্রস্তাবের তারিখ", cellFilter: "FullDateWithTime", width: 150 },
        { name: 'html', displayName: "সিদ্ধান্ত প্রস্তাব", width: 320, cellTemplate: '<div ng-bind-html="COL_FIELD"></div>' },
        { name: 'MemberResolutionDetail', displayName: "মূল প্রস্তাব", width: 150, cellTemplate: '<div ng-bind-html="COL_FIELD"></div>', visible: true },
        { name: 'MemberResolutionFIleURL', displayName: "URL", visible: false },
        { name: 'ConstitutentBangla', displayName: "নির্বাচনী এলাকা", width: 150 },
        { name: 'ParlSessID', displayName: "Session ID", visible: false },
        { name: 'AcceptanceComment', displayName: "গ্রহনযোগ্যতা", visible: false },
        { name: 'UserID', displayName: "EMP ID", visible: false },
        { name: 'BanglaName', displayName: "সদস্যের নাম", width: 240 },
        { name: 'RDNo', displayName: "আর ডি নং", width: 100 },
        { name: 'Status', displayName: "Status", visible: false },
        { name: 'AdministrativeOfcSignature', displayName: "প্রশাসনিক কর্মকর্তা", width: 120, cellTemplate: '<img src="{{row.entity.AdministrativeOfcSignature}}" alt="Not Signed" width="200">' },
        { name: 'AssitantSccSignature', displayName: "সহকারী সচিব", width: 120, cellTemplate: '<img src="{{row.entity.AssitantSccSignature}}" alt="Not Signed" width="200">' },
        { name: 'SrAssitantSccSignature', displayName: "সিনিয়র সহকারী সচিব", width: 120, cellTemplate: '<img src="{{row.entity.SrAssitantSccSignature}}" alt="Not Signed" width="200">' },
        { name: 'DeputySecSignature', displayName: "যুগ্ম সচিব", width: 120, cellTemplate: '<img src="{{row.entity.DeputySecSignature}}" alt="Not Signed" width="150">' },
        { name: 'AddSecSignature', displayName: "অতিরিক্ত সচিব", width: 120, cellTemplate: '<img src="{{row.entity.AddSecSignature}}" alt="Not Signed" width="150">' },
        { name: 'SecSignature', displayName: " সচিব", width: 120, cellTemplate: '<img src="{{row.entity.SecSignature}}" alt="Not Signed" width="150">' },
      
        {
            name: 'Action ', displayName: "অনুমোদন", enableFiltering: false, enableSorting: false, width: "300",
            cellTemplate: '<div style="padding:2px 2px 2px 2px;"><button style="margin-right:5px;"  class="btn-success" ng-click="grid.appScope.DirectSave(row)"><i class="fa fa-save">গ্রহনযোগ্য</i></button><button  class="btn-success" ng-click="grid.appScope.DirectRejectSave(row)"><i class="btn-danger ">প্রত্যাখ্যান</i></button> <button  class="btn-danger " ng-click="grid.appScope.rowDblClickCompCons(row)"><i class="fa fa-edit"></i></button></div>'
        }
    ];
    var columnDepartmentList1 = [
        { name: 'MemberResolutionID', displayName: "ID", visible: false },

        { name: 'html', displayName: "সিদ্ধান্ত প্রস্তাব", width: 330, cellTemplate: '<div ng-bind-html="COL_FIELD"></div>' },
        { name: 'MemberResolutionDetail', displayName: "মূল প্রস্তাব", visible: true, width: 150, cellTemplate: '<div ng-bind-html="COL_FIELD"></div>' },
        { name: 'MemberResolutionFIleURL', displayName: "URL", visible: false },
        { name: 'BanglaName', displayName: "প্রস্তাবনা", width: 200 },
        { name: 'ParliamentNo', displayName: "সংসদ নং", width: 150 },
        { name: 'SessionNo', displayName: "অধিবেশন নং", width: 150 },
        { name: 'AcceptanceComment', displayName: "Acceptance Comment", visible: false },
        { name: 'UserName', displayName: "প্রস্তাবনা", width: 250, visible: false },
        { name: 'RDNo', displayName: "আর ডি নং", width: 100, visible: false },
        { name: 'MemberResPriority', displayName: "প্রায়োরিটি", width: 150, visible: false },
        { name: 'Status', displayName: "Status", width: 150, visible: false },
        { name: 'AdministrativeOfcSignature', displayName: "প্রশাসনিক কর্মকর্তা", width: 120, cellTemplate: '<img src="{{row.entity.AdministrativeOfcSignature}}" alt="Not Signed" width="200">' },
        { name: 'AssitantSccSignature', displayName: "সহকারী সচিব", width: 120, cellTemplate: '<img src="{{row.entity.AssitantSccSignature}}" alt="Not Signed" width="200">' },
        { name: 'SrAssitantSccSignature', displayName: "সিনিয়র সহকারী সচিব", width: 120, cellTemplate: '<img src="{{row.entity.SrAssitantSccSignature}}" alt="Not Signed" width="200">' },
        { name: 'DeputySecSignature', displayName: "যুগ্ম সচিব", width: 120, cellTemplate: '<img src="{{row.entity.DeputySecSignature}}" alt="Not Signed" width="150">' },
        { name: 'AddSecSignature', displayName: "অতিরিক্ত সচিব", width: 120, cellTemplate: '<img src="{{row.entity.AddSecSignature}}" alt="Not Signed" width="150">' },
        { name: 'SecSignature', displayName: " সচিব", width: 120, cellTemplate: '<img src="{{row.entity.SecSignature}}" alt="Not Signed" width="150">' },
        { name: 'SpeakerSignature', displayName: "স্পিকার", width: 120, cellTemplate: '<img src="{{row.entity.SpeakerSignature}}" alt="Not Signed" width="150">' },

    ];
    $scope.gridResolutionOptions = {
        enableFiltering: true,
        enableSorting: true,
        enableColumnResizing: true,
        paginationPageSizes: [10, 20, 50, 100],
        paginationPageSize: 10,
        columnDefs: columnResolutionList,
        rowTemplate: rowTemplate(),
        onRegisterApi: function (gridApi) {
            $scope.gridResolutionOptions = gridApi;
        }
    };
    $scope.gridDepartmentOptions1 = {
        enableFiltering: true,
        enableSorting: true,
        //enableHorizontalScrollbar: true,
        // enableVerticalScrollbar: true,
        enableColumnResizing: true,
        paginationPageSizes: [10, 20, 50, 100],
        paginationPageSize: 10,
        columnDefs: columnDepartmentList1,
        onRegisterApi: function (gridApi) {
            $scope.gridDepartmentOptions = gridApi;
        }
    };
    function rowTemplate() {
        return '<div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div></div>';
    }

    $scope.SelectRow = function (row) {
        row.entity.Selected = true;
    };

    $scope.rowDblClickCompCons = function (row) {
        $('#ResolutionModal').modal('show');

        $scope.uiID = row.entity.ResolutionApproveID;
        $scope.uiCode = row.entity.ResolutionApproveID;

        $scope.UserName = row.entity.BanglaName + ',' + row.entity.ConstitutentBangla;
        //$scope.ConstitutentBangla = row.entity.ConstitutentBangla;
        $scope.MemberResolutionID = row.entity.MemberResolutionID;
        $scope.AcceptanceComment = row.entity.AcceptanceComment;
        $scope.MemberResolutionDetail = row.entity.MemberResolutionDetail;
        $scope.MemberResolutionDate = $filter('FullDateWithTime')(row.entity.MemberResolutionDate);
        $scope.MemberResolutionFIleURL = row.entity.MemberResolutionFIleURL;
        $scope.RDNo = row.entity.RDNo;
        $scope.ApproveDetail = (row.entity.html == '' || row.entity.html == null || row.entity.html == "null") ? row.entity.MemberResolutionDetail : row.entity.html;
        $scope.GetResolutionLog(row.entity.MemberResolutionID);
    };

    $scope.GetSearchForEditSpeaker = function () {

        $scope.gridDepartmentOptions.data = [];

        $http({
            method: "GET",
            url: MyApp.rootPath + "ResolutionApproval/GetSearchForEditSpeaker"
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
    $scope.GetPostedHistoryListBySpeaker = function () {

        $scope.gridDepartmentOptions.data = [];

        $http({
            method: "GET",
            url: MyApp.rootPath + "ResolutionApproval/GetPostedHistoryListBySpeaker"
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
    $scope.GetDraft = function () {

        $scope.gridDepartmentOptions.data = [];

        $http({
            method: "GET",
            url: MyApp.rootPath + "ResolutionApproval/GetSpeakerDraft"
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

    var columnDepartmentList = [
        { name: 'ResolutionApproveID', displayName: "ID", visible: false },
        { name: 'MemberResolutionID', displayName: "ID", visible: false },
        { name: 'AprID', displayName: "AprID", visible: false },
        { name: 'ParliamentNo', displayName: "সংসদ নং", cellFilter: "banglaNumber", width: 100 },
        { name: 'SessionNo', displayName: "অধিবেশন নং", cellFilter: "banglaNumber", width: 100 },
        { name: 'MemberResolutionDate', displayName: "প্রস্তাবের তারিখ", cellFilter: "FullDateWithTime", width: 150 },
        { name: 'html', displayName: "সিদ্ধান্ত প্রস্তাব", width: 360, visible: true, cellTemplate: '<div ng-bind-html="COL_FIELD"></div>' },
        { name: 'MemberResolutionDetail', displayName: "Original", visible: false },
        { name: 'MemberResolutionFIleURL', displayName: "URL", visible: false },
        { name: 'ConstitutentBangla', displayName: "নির্বাচনী এলাকা", width: 150 },
        { name: 'ParlSessID', displayName: "Session ID", visible: false },
        { name: 'UserID', displayName: "EMP ID", visible: false },
        { name: 'BanglaName', displayName: "সদস্যের নাম", width: 260 },
        { name: 'AcceptanceComment', displayName: "গ্রহনযোগ্যতা", visible: false },
        { name: 'RDNo', displayName: "আর ডি নং", width: 100 },
        { name: 'SpeakerApproveStatus', displayName: "Status", width: 120 },
        { name: 'SpeakerApproveDetail', displayName: "Status", visible: false },
        { name: 'SpeakerApproveDate', displayName: "Status", visible: false },
        { name: 'AdministrativeOfcSignature', displayName: "প্রশাসনিক কর্মকর্তা", width: 120, cellTemplate: '<img src="{{row.entity.AdministrativeOfcSignature}}" alt="Not Signed" width="200">' },
        { name: 'AssitantSccSignature', displayName: "সহকারী সচিব", width: 120, cellTemplate: '<img src="{{row.entity.AssitantSccSignature}}" alt="Not Signed" width="200">' },
        { name: 'SrAssitantSccSignature', displayName: "সিনিয়র সহকারী সচিব", width: 120, cellTemplate: '<img src="{{row.entity.SrAssitantSccSignature}}" alt="Not Signed" width="200">' },
        { name: 'DeputySecSignature', displayName: "যুগ্ম সচিব", width: 120, cellTemplate: '<img src="{{row.entity.DeputySecSignature}}" alt="Not Signed" width="150">' },
        { name: 'AddSecSignature', displayName: "অতিরিক্ত সচিব", width: 120, cellTemplate: '<img src="{{row.entity.AddSecSignature}}" alt="Not Signed" width="150">' },
        { name: 'SecSignature', displayName: " সচিব", width: 120, cellTemplate: '<img src="{{row.entity.SecSignature}}" alt="Not Signed" width="150">' },
       
    ];

    $scope.gridDepartmentOptions = {
        enableFiltering: true,
        enableSorting: true,
        enableColumnResizing: true,
        paginationPageSizes: [10, 20, 50, 100],
        paginationPageSize: 10,
        columnDefs: columnDepartmentList,
        rowTemplate: rowTemplateApproval(),
        onRegisterApi: function (gridApi) {
            $scope.gridDepartmentOptions = gridApi;
        }
    };

    function rowTemplateApproval() {
        return '<div ng-dblclick="grid.appScope.rowDblClickCompApproval(row)" >' +
            '  <div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div></div>';
    }


    var columnLogList = [
        { name: 'MemberResolutionID', displayName: "ID", visible: false },
        { name: 'MemberResolutionDate', displayName: "প্রস্তাবের তারিখ", cellFilter: "FullDateWithTime", width: 150 },
        { name: 'html', displayName: "সিদ্ধান্ত প্রস্তাব", width: 300, visible: true, cellTemplate: '<div ng-bind-html="COL_FIELD"></div>' },
        { name: 'MemberResolutionDetail', displayName: "Original", visible: false },
        { name: 'UserName', width: 150, displayName: "নাম" },
        { name: 'DesignationName', displayName: "পদবী", width: 130 },
        { name: 'Comments', displayName: "মতামত", width: 120}
    ];

    $scope.gridMemberResolutionsOptions = {
        enableFiltering: true,
        enableSorting: true,
        enableColumnResizing: true,
        paginationPageSizes: [10, 20, 50, 100],
        paginationPageSize: 10,
        columnDefs:columnLogList,
        rowTemplate: rowTemplateApproval(),
        onRegisterApi: function (gridApi) {
            $scope.gridDepartmentOptions = gridApi;
        }
    };

    $scope.GetResolutionLog = function (memberResolutionID) {
        $scope.gridMemberResolutionsOptions.data = [];
        $http({
            method: "POST",
            url: MyApp.rootPath + "ResolutionApproval/GetResolutionLog",
            data: { ResolutionID: memberResolutionID }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridMemberResolutionsOptions.data = response.data;
            }
            else {
                toastr.warning("No Data Found!");
            }
        }, function () {
            toastr.warning("No Data Found!");
        });
    };

    $scope.rowDblClickCompApproval = function (row) {
        $scope.Reset();

        $scope.uiID = row.entity.ResolutionApproveID;
        $scope.uiCode = row.entity.ResolutionApproveID;

        $scope.UserName = row.entity.BanglaName + ',' + row.entity.ConstitutentBangla;
        //$scope.ConstitutentBangla = row.entity.ConstitutentBangla;
        $scope.MemberResolutionID = row.entity.MemberResolutionID;
        $scope.MemberResolutionDetail = row.entity.MemberResolutionDetail;
        $scope.MemberResolutionDate = $filter('FullDateWithTime')(row.entity.MemberResolutionDate);
        $scope.MemberResolutionFIleURL = row.entity.MemberResolutionFIleURL;
        $scope.RDNo = row.entity.RDNo;
        $scope.AcceptanceComment = row.entity.AcceptanceComment;
        $scope.frmSpeakerApproval.ParliamentSession = row.entity.ParlSessID;

        $scope.ApproveDetail = (row.entity.html == '' || row.entity.html == null || row.entity.html == "null") ? row.entity.MemberResolutionDetail : row.entity.html;
        $scope.ApproveDate = $filter('FullDateTime')(row.entity.SpeakerApproveDate);
        $scope.frmSpeakerApproval.AppStatus = row.entity.AprID;
        $scope.GetResolutionLog(row.entity.MemberResolutionID);
        $scope.btnSaveValue = "Update";
        $('#DepartmentModal').modal('hide');
        $('#ResolutionModal').modal('show');
    };


    $scope.DirectSave = function (row) {

        var dt = new Date();

        $scope.SaveDb = {};

        $scope.SaveDb.ResolutionApproveID = row.entity.ResolutionApproveID;
        $scope.SaveDb.MemberResolutionID = row.entity.MemberResolutionID;
        $scope.SaveDb.RDNo = row.entity.RDNo;
        $scope.SaveDb.ParlSessID = row.entity.ParlSessID;

        $scope.SaveDb.SpeakerApproveDetail = (row.entity.html == '' || row.entity.html == null || row.entity.html == "null") ? row.entity.MemberResolutionDetail : row.entity.html;
        $scope.SaveDb.SpeakerApproveDate = (dt.getFullYear() + '-' + dt.getMonth() + '-' + dt.getDate());
        $scope.SaveDb.SpeakerApproveStatus = "1";

        $scope.SaveDb.SendTo = $scope.frmSpeakerApproval.SignTo;

        if ($scope.uiID === '' || typeof $scope.uiID === 'undefined') {
            $http({
                method: "post",
                url: MyApp.rootPath + "ResolutionApproval/UpdateSpeakerApproval",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                if (response.data.Status === "Yes") {
                    OperationMsg(response.data.Mode);
                    if (response.data.Mode !== "Unique") {
                        $scope.GetWaitingListForSpeaker();
                    }
                } else {
                    toastr.error("Failed!");
                }
            });
        }
        $scope.Reset();
    };
    $scope.DirectRejectSave = function (row) {
     
        var dt = new Date();

        $scope.SaveDb = {};

        $scope.SaveDb.ResolutionApproveID = row.entity.ResolutionApproveID;
        $scope.SaveDb.MemberResolutionID = row.entity.MemberResolutionID;
        $scope.SaveDb.RDNo = row.entity.RDNo;
        $scope.SaveDb.ParlSessID = row.entity.ParlSessID;

        $scope.SaveDb.SpeakerApproveDetail = (row.entity.html == '' || row.entity.html == null || row.entity.html == "null") ? row.entity.MemberResolutionDetail : row.entity.html;
        $scope.SaveDb.SpeakerApproveDate = (dt.getFullYear() + '-' + dt.getMonth() + '-' + dt.getDate());
        $scope.SaveDb.SpeakerApproveStatus = "2";

        $scope.SaveDb.SendTo = $scope.frmSpeakerApproval.SignTo;

        if ($scope.uiID === '' || typeof $scope.uiID === 'undefined') {
            $http({
                method: "post",
                url: MyApp.rootPath + "ResolutionApproval/UpdateSpeakerApproval",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                if (response.data.Status === "Yes") {
                    OperationMsg(response.data.Mode);
                    if (response.data.Mode !== "Unique") {
                        $scope.GetWaitingListForSpeaker();
                    }
                } else {
                    toastr.error("Failed!");
                }
            });
        }
        $scope.Reset();
    };
    $scope.MultipleSaveData = function () {

        var resolutionList = $scope.gridResolutionOptions.data;

        for (var i = resolutionList.length - 1; i >= 0; i--) {

            if (resolutionList[i].Selected === true) {

                var dt = new Date();

                $scope.SaveDb = {};

                $scope.SaveDb.ResolutionApproveID = resolutionList[i].ResolutionApproveID;
                $scope.SaveDb.MemberResolutionID = resolutionList[i].MemberResolutionID;
                $scope.SaveDb.RDNo = resolutionList[i].RDNo;
                $scope.SaveDb.ParlSessID = resolutionList[i].ParlSessID;

                $scope.SaveDb.SpeakerApproveDetail = (resolutionList[i].html == '' || resolutionList[i].html == null || resolutionList[i].html == "null") ? resolutionList[i].MemberResolutionDetail : resolutionList[i].html;
                $scope.SaveDb.SpeakerApproveDate = (dt.getFullYear() + '-' + dt.getMonth() + '-' + dt.getDate());
                $scope.SaveDb.SpeakerApproveStatus = "1";
                $scope.SaveDb.SendTo = $scope.frmSpeakerApproval.SignTo;
                if ($scope.uiID === '' || typeof $scope.uiID === 'undefined') {
                    $http({
                        method: "post",
                        url: MyApp.rootPath + "ResolutionApproval/UpdateSpeakerApproval",
                        datatype: "json",
                        data: JSON.stringify($scope.SaveDb)
                    }).then(function (response) {
                        if (response.data.Status === "Yes") {
                            OperationMsg(response.data.Mode);
                            if (response.data.Mode !== "Unique") {
                                $scope.GetWaitingListForSpeaker();
                            }
                        } else {
                            toastr.error("Failed!");
                        }
                    });
                }
            }
        }
        $scope.gridResolutionOptions.data = [];
        $scope.GetWaitingListForSpeaker();
        $scope.Reset();
    };


    $scope.SaveData = function () {

       
        if ($scope.frmSpeakerApproval.SignTo === '' || $scope.frmSpeakerApproval.SignTo === undefined) {
            toastr.warning("প্রাপোক নির্বাচন করুন");
            return false;
        }
        $scope.SaveDb = {};

        $scope.SaveDb.ResolutionApproveID = $scope.uiID;
        $scope.SaveDb.MemberResolutionID = $scope.MemberResolutionID;
        $scope.SaveDb.RDNo = $scope.RDNo;
        $scope.SaveDb.ParlSessID = $scope.frmSpeakerApproval.ParliamentSession;

        $scope.SaveDb.SpeakerApproveDetail = $scope.ApproveDetail;
        $scope.SaveDb.SpeakerApproveDate = $scope.ApproveDate;
        $scope.SaveDb.SpeakerApproveStatus = "1";// $scope.frmSpeakerApproval.AppStatus;
        $scope.SaveDb.SendTo = $scope.frmSpeakerApproval.SignTo;
        $http({
            method: "post",
            url: MyApp.rootPath + "ResolutionApproval/UpdateSpeakerApproval",
            datatype: "json",
            data: JSON.stringify($scope.SaveDb)
        }).then(function (response) {
            if (response.data.Status === "Yes") {
                OperationMsg(response.data.Mode);
                if (response.data.Mode !== "Unique") {
                    $scope.uiCode = response.data.ID;
                    $scope.uiID = response.data.ID;
                    $scope.btnSaveValue = "Update";
                    $scope.ModalReset();
                    $scope.GetWaitingListForSpeaker();
                    $('#ResolutionModal').modal('hide');
                }
            } else {
                toastr.error("Failed!");
            }
        });
        $scope.Reset();
    };
    $scope.RejectData = function () {

        if ($scope.frmSpeakerApproval.SignTo === '' || $scope.frmSpeakerApproval.SignTo === undefined) {
            toastr.warning("প্রাপোক নির্বাচন করুন");
            return false;
        }

        $scope.SaveDb = {};

        $scope.SaveDb.ResolutionApproveID = $scope.uiID;
        $scope.SaveDb.MemberResolutionID = $scope.MemberResolutionID;
        $scope.SaveDb.RDNo = $scope.RDNo;
        $scope.SaveDb.ParlSessID = $scope.frmSpeakerApproval.ParliamentSession;

        $scope.SaveDb.SpeakerApproveDetail = $scope.ApproveDetail;
        $scope.SaveDb.SpeakerApproveDate = $scope.ApproveDate;
        $scope.SaveDb.SpeakerApproveStatus = "2";// $scope.frmSpeakerApproval.AppStatus;
        $scope.SaveDb.SendTo = $scope.frmSpeakerApproval.SignTo;
        $http({
            method: "post",
            url: MyApp.rootPath + "ResolutionApproval/UpdateSpeakerApproval",
            datatype: "json",
            data: JSON.stringify($scope.SaveDb)
        }).then(function (response) {
            if (response.data.Status === "Yes") {
                OperationMsg(response.data.Mode);
                if (response.data.Mode !== "Unique") {
                    $scope.uiCode = response.data.ID;
                    $scope.uiID = response.data.ID;
                    $scope.btnSaveValue = "Update";
                    $scope.ModalReset();
                    $scope.GetWaitingListForSpeaker();
                    $('#ResolutionModal').modal('hide');
                }
            } else {
                toastr.error("Failed!");
            }
        });
        $scope.Reset();
    };

    $scope.SaveDataDraft = function () {

        $scope.SaveDb = {};

        $scope.SaveDb.ResolutionApproveID = $scope.uiID;
        $scope.SaveDb.MemberResolutionID = $scope.MemberResolutionID;
        $scope.SaveDb.RDNo = $scope.RDNo;

        $scope.SaveDb.SpeakerApproveDetail = $scope.ApproveDetail;
        $scope.SaveDb.SendTo = $scope.frmSpeakerApproval.SignTo;
        $http({
            method: "post",
            url: MyApp.rootPath + "ResolutionApproval/SaveSpeakerDraft",
            datatype: "json",
            data: JSON.stringify($scope.SaveDb)
        }).then(function (response) {
            if (response.data.Status === "Yes") {
                OperationMsg(response.data.Mode);
                if (response.data.Mode !== "Unique") {
                    $scope.uiCode = response.data.ID;
                    $scope.uiID = response.data.ID;
                    $scope.btnSaveValue = "Update";
                    $scope.ModalReset();
                    $scope.GetWaitingListForSpeaker();

                    $('#ResolutionModal').modal('hide');
                }
            } else {
                toastr.error("Failed!");
            }
        });
        
    };

    $scope.Reset = function () {
        $scope.gridResolutionOptions.data = [];
        $scope.frmSpeakerApproval.ParliamentSession = "";
        $scope.uiID = "";
        $scope.uiCode = "";
        $scope.MemberResolutionDetail = "";
        $scope.MemberResolutionDate = "";
        $scope.MemberResolutionFIleURL = "";
        $scope.RDNo = "";

        $scope.UserName = "";
        $scope.ConstitutentBangla = "";
        $scope.MemberResolutionID = "";
        $scope.AcceptanceComment = "";

        $scope.ApproveDetail = "";
        $scope.ApproveDate = "";
        $scope.btnSaveValue = "Save";
    };

    $scope.ModalReset = function () {
        $scope.uiID = "";
        $scope.uiCode = "";
        $scope.MemberResolutionDetail = "";
        $scope.MemberResolutionDate = "";
        $scope.MemberResolutionFIleURL = "";
        $scope.RDNo = "";

        $scope.UserName = "";
        $scope.ConstitutentBangla = "";
        $scope.MemberResolutionID = "";
        $scope.ApproveDetail = "";
        $scope.ApproveDate = "";
        $scope.AcceptanceComment = "";

        $scope.btnSaveValue = "Save";
    };
});