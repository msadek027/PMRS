app.controller("myCtrl", function ($scope, $http, $filter) {
    $scope.EventPerm(18);
    $scope.btnSaveValue = "Posting";

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
        url: MyApp.rootPath + "ResolutionApproval/GetApprovalStatus"
    }).then(function (response) {
        $scope.AprSts = response.data;
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

    $scope.GetWatingListForSrAssistantSecretary = function () {
        $scope.gridResolutionOptions.data = [];
        $http({
            method: "POST",
            url: MyApp.rootPath + "ResolutionApproval/GetWatingListForSrAssistantSecretary",
            data: { session: $scope.frmSrAssistantSecretaryApproval.ParliamentSession }
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
            enableFiltering: false, enableSorting: false,
        },
        { name: 'ResolutionApproveID', displayName: "ID", visible: false },
        { name: 'MemberResolutionID', displayName: "ID", visible: false },
        { name: 'MemberResolutionDate', displayName: "প্রস্তাবের তারিখ", cellFilter: "FullDateWithTime", width: 150 },
        { name: 'html', displayName: "সিদ্ধান্ত প্রস্তাব", width: 450, cellTemplate: '<div ng-bind-html="COL_FIELD"></div>' },
        { name: 'MemberResolutionDetail', displayName: "মূল প্রস্তাব", width: 350, cellTemplate: '<div ng-bind-html="COL_FIELD"></div>', visible: true },

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

        {
            name: 'Action ', displayName: "অনুমোদন", enableFiltering: false, enableSorting: false, width: "100",
            cellTemplate: '<div style="padding:2px 2px 2px 2px;"><button  class="btn-success" ng-click="grid.appScope.DirectSave(row)"><i class="fa fa-forward"></i></button> <button  class="btn-danger " ng-click="grid.appScope.rowDblClickCompCons(row)"><i class="fa fa-edit"></i></button></div>'
        }
    ];

    $scope.gridResolutionOptions = {
        enableFiltering: true,
        enableSorting: true,
        enableColumnResizing: true,
        paginationPageSizes: [8, 16, 24],
        paginationPageSize: 8,
        columnDefs: columnResolutionList,
        rowTemplate: rowTemplate(),
        onRegisterApi: function (gridApi) {
            $scope.gridResolutionOptions = gridApi;
        }
    };

    function rowTemplate() {
        return '<div style="border-bottom:1px solid #D4D4D4;" ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div></div>';
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
        $scope.MemberResolutionDate = $filter('FullDateTime')(row.entity.MemberResolutionDate);
        $scope.MemberResolutionFIleURL = row.entity.MemberResolutionFIleURL;
        $scope.RDNo = row.entity.RDNo;
        $scope.ApproveDetail = (row.entity.html == '' || row.entity.html == null || row.entity.html == "null") ? row.entity.MemberResolutionDetail : row.entity.html;
        $scope.GetResolutionLog(row.entity.MemberResolutionID);
    };

    $scope.GetSearchForEditSrAssistantSecretary = function () {

        $scope.gridDepartmentOptions.data = [];
        $http({
            method: "GET",
            url: MyApp.rootPath + "ResolutionApproval/GetSearchForEditSrAssistantSecretary"
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
    $scope.GetPostedHistoryListBySrAssistantSecretary = function () {

        $scope.gridDepartmentOptions.data = [];
        $http({
            method: "GET",
            url: MyApp.rootPath + "ResolutionApproval/GetPostedHistoryListBySrAssistantSecretary"
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
            method: "POST",
            url: MyApp.rootPath + "ResolutionApproval/GetWatingListForSrAssistantSecretary",
            data: { session: $scope.frmSrAssistantSecretaryApproval.ParliamentSession, DataMode: "Draft" }
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
        { name: 'html', displayName: "সিদ্ধান্ত প্রস্তাব", width: 450, cellTemplate: '<div ng-bind-html="COL_FIELD"></div>' },
        { name: 'MemberResolutionDetail', displayName: "মূল প্রস্তাব", width: 350, cellTemplate: '<div ng-bind-html="COL_FIELD"></div>', visible: true },
        { name: 'MemberResolutionFIleURL', displayName: "URL", visible: false },
        { name: 'ConstitutentBangla', displayName: "নির্বাচনী এলাকা", width: 120 },
        { name: 'ParlSessID', displayName: "Session ID", visible: false },
        { name: 'UserID', displayName: "EMP ID", visible: false },
        { name: 'BanglaName', displayName: "সদস্যের নাম", width: 260 },
        { name: 'AcceptanceComment', displayName: "গ্রহনযোগ্যতা", visible: false },
        { name: 'RDNo', displayName: "আর ডি নং", width: 100 },
        { name: 'SrAssitantSccApproveStatus', displayName: "Status", width: 120 },
        { name: 'SrAssitantSccDetail', displayName: "Status", visible: false },

        { name: 'AdministrativeOfcSignature', displayName: "প্রশাসনিক কর্মকর্তা", width: 120, cellTemplate: '<img src="{{row.entity.AdministrativeOfcSignature}}" alt="Not Signed" width="200">' },
        { name: 'AssitantSccSignature', displayName: "সহকারী সচিব", width: 120, cellTemplate: '<img src="{{row.entity.AssitantSccSignature}}" alt="Not Signed" width="200">' },
        { name: 'SrAssitantSccSignature', displayName: "সিনিয়র সহকারী সচিব", width: 120, cellTemplate: '<img src="{{row.entity.SrAssitantSccSignature}}" alt="Not Signed" width="200">' },

    ];
    var columnDepartmentList1 = [
        { name: 'MemberResolutionID', displayName: "ID", visible: false },
        { name: 'RDNo', displayName: "আর ডি নং", width: 100 },
        { name: 'html', displayName: "সিদ্ধান্ত প্রস্তাব", width: 450, cellTemplate: '<div ng-bind-html="COL_FIELD"></div>' },
        { name: 'MemberResolutionDetail', displayName: "মূল প্রস্তাব", width: 350, cellTemplate: '<div ng-bind-html="COL_FIELD"></div>', visible: true },
        { name: 'MemberResolutionFIleURL', displayName: "URL", visible: false },
        { name: 'BanglaName', displayName: "প্রস্তাবনা", width: 200 },
        { name: 'ParliamentNo', displayName: "সংসদ নং", width: 150 },
        { name: 'SessionNo', displayName: "অধিবেশন নং", width: 150 },
        { name: 'AcceptanceComment', displayName: "Acceptance Comment", visible: false },
        { name: 'UserName', displayName: "প্রস্তাবনা", width: 250, visible: false },

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
    var columnLogList = [
        { name: 'MemberResolutionID', displayName: "ID", visible: false },
        { name: 'MemberResolutionDate', displayName: "প্রস্তাবের তারিখ", cellFilter: "FullDateWithTime", width: 150 },
        { name: 'html', displayName: "সিদ্ধান্ত প্রস্তাব", width: 300, visible: true, cellTemplate: '<div ng-bind-html="COL_FIELD"></div>' },
        { name: 'MemberResolutionDetail', displayName: "Original", visible: false },
        { name: 'UserName', width: 150, displayName: "নাম" },
        { name: 'DesignationName', displayName: "পদবী", width: 130 },
        { name: 'Comments', displayName: "মতামত", width: 120 }
    ];

    $scope.gridMemberResolutionsOptions = {
        enableFiltering: true,
        enableSorting: true,
        enableColumnResizing: true,
        paginationPageSizes: [10, 20, 50, 100],
        paginationPageSize: 10,
        columnDefs: columnLogList,
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
    function rowTemplateApproval() {
        return ' <div style="border-bottom:1px solid #D4D4D4;" ng-dblclick="grid.appScope.rowDblClickCompApproval(row)"  ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div>';
    }

    $scope.rowDblClickCompApproval = function (row) {
        $scope.Reset();

        $scope.uiID = row.entity.ResolutionApproveID;
        $scope.uiCode = row.entity.ResolutionApproveID;

        $scope.UserName = row.entity.BanglaName + ',' + row.entity.ConstitutentBangla;
        //$scope.ConstitutentBangla = row.entity.ConstitutentBangla;
        $scope.MemberResolutionID = row.entity.MemberResolutionID;
        $scope.MemberResolutionDetail = row.entity.MemberResolutionDetail;
        $scope.MemberResolutionDate = $filter('FullDateTime')(row.entity.MemberResolutionDate);
        $scope.MemberResolutionFIleURL = row.entity.MemberResolutionFIleURL;
        $scope.AcceptanceComment = row.entity.AcceptanceComment;
        $scope.RDNo = row.entity.RDNo;
        $scope.frmSrAssistantSecretaryApproval.ParliamentSession = row.entity.ParlSessID;

        $scope.ApproveDetail = row.entity.html;
        $scope.ApproveDate = $filter('FullDateTime')(row.entity.AssitantSccApproveDate);
        $scope.frmSrAssistantSecretaryApproval.AppStatus = row.entity.AprID;
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

        $scope.SaveDb.SrAssitantSccDetail = (row.entity.html == '' || row.entity.html == null || row.entity.html == "null") ? row.entity.MemberResolutionDetail : row.entity.html;
        $scope.SaveDb.SrAssitantSccApproveDate = (dt.getFullYear() + '-' + dt.getMonth() + '-' + dt.getDate());
        $scope.SaveDb.SrAssitantSccApproveStatus = "1";
        $scope.SaveDb.SendTo = $scope.frmSrAssistantSecretaryApproval.SignTo;
        if ($scope.uiID === '' || typeof $scope.uiID === 'undefined' && $scope.SaveDb.SendTo != '' && $scope.SaveDb.SendTo != 'undefined' && $scope.SaveDb.SendTo != undefined) {
            $http({
                method: "post",
                url: MyApp.rootPath + "ResolutionApproval/UpdateSrAssistantSecApproval",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                if (response.data.Status === "Yes") {
                    OperationMsg(response.data.Mode);
                    if (response.data.Mode !== "Unique") {
                        $scope.GetWatingListForSrAssistantSecretary();
                    }
                } else {
                    toastr.error("Failed!");
                }
            });
        }
        else {
            toastr.warning("প্রাপোক নির্বাচন করুন");
        }
    };


    $scope.MultipleSaveData = function () {

        var resolutionList = $scope.gridResolutionOptions.data;

        for (var i = resolutionList.length - 1; i >= 0; i--) {

            if (resolutionList[i].Selected === true) {

                var dt = new Date();

                $scope.SaveDb = {};
                debugger;
                $scope.SaveDb.ResolutionApproveID = resolutionList[i].ResolutionApproveID;
                $scope.SaveDb.MemberResolutionID = resolutionList[i].MemberResolutionID;
                $scope.SaveDb.RDNo = resolutionList[i].RDNo;
                $scope.SaveDb.ParlSessID = resolutionList[i].ParlSessID;

                $scope.SaveDb.SrAssitantSccDetail = (resolutionList[i].html == '' || resolutionList[i].html == null || resolutionList[i].html == "null") ? resolutionList[i].MemberResolutionDetail : resolutionList[i].html;
                $scope.SaveDb.SrAssitantSccApproveDate = (dt.getFullYear() + '-' + dt.getMonth() + '-' + dt.getDate());
                $scope.SaveDb.SrAssitantSccApproveStatus = "1";
                $scope.SaveDb.SendTo = $scope.frmSrAssistantSecretaryApproval.SignTo;
                if ($scope.uiID === '' || typeof $scope.uiID === 'undefined' && $scope.SaveDb.SendTo != '' && $scope.SaveDb.SendTo != 'undefined' && $scope.SaveDb.SendTo != undefined) {
                    $http({
                        method: "post",
                        url: MyApp.rootPath + "ResolutionApproval/UpdateSrAssistantSecApproval",
                        datatype: "json",
                        data: JSON.stringify($scope.SaveDb)
                    }).then(function (response) {
                        if (response.data.Status === "Yes") {
                            OperationMsg(response.data.Mode);
                            if (response.data.Mode !== "Unique") {
                                $scope.GetWatingListForSrAssistantSecretary();
                            }
                        } else {
                            toastr.error("Failed!");
                        }
                    });
                }
                else {
                    toastr.warning("প্রাপোক নির্বাচন করুন");
                }
            }

        }
        $scope.gridResolutionOptions.data = [];
        $scope.GetWatingListForSrAssistantSecretary();
    }

    $scope.SaveData = function () {
        debugger;
        if ($scope.frmSrAssistantSecretaryApproval.AppStatus === '' || $scope.frmSrAssistantSecretaryApproval.AppStatus === undefined) {
            toastr.warning("সিদ্ধান্ত-প্রস্তাবের মতামত প্রদান করুন");
            return false;
        }
        if ($scope.frmSrAssistantSecretaryApproval.SignTo === '' || $scope.frmSrAssistantSecretaryApproval.SignTo === undefined) {
            toastr.warning("প্রাপোক নির্বাচন করুন");
            return false;
        }
        $scope.SaveDb = {};

        $scope.SaveDb.ResolutionApproveID = $scope.uiID;
        $scope.SaveDb.MemberResolutionID = $scope.MemberResolutionID;
        $scope.SaveDb.RDNo = $scope.RDNo;
        $scope.SaveDb.ParlSessID = $scope.frmSrAssistantSecretaryApproval.ParliamentSession;

        $scope.SaveDb.SrAssitantSccDetail = $scope.ApproveDetail;
        $scope.SaveDb.SrAssitantSccApproveDate = $scope.ApproveDate;
        $scope.SaveDb.SrAssitantSccApproveStatus = $scope.frmSrAssistantSecretaryApproval.AppStatus;

        $scope.SaveDb.SendTo = $scope.frmSrAssistantSecretaryApproval.SignTo;

        $http({
            method: "post",
            url: MyApp.rootPath + "ResolutionApproval/UpdateSrAssistantSecApproval",
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
                    $scope.GetWatingListForSrAssistantSecretary();
                    $('#ResolutionModal').modal('hide');
                }
            } else {
                toastr.error("Failed!");
            }
        });
    };




    $scope.SaveDataDraft = function () {

        $scope.SaveDb = {};
        $scope.SaveDb.ResolutionApproveID = $scope.uiID;
        $scope.SaveDb.MemberResolutionID = $scope.MemberResolutionID;
        $scope.SaveDb.RDNo = $scope.RDNo;
        $scope.SaveDb.ParlSessID = $scope.frmSrAssistantSecretaryApproval.ParliamentSession;

        $scope.SaveDb.SrAssitantSccDetail = $scope.ApproveDetail;

        
        $http({
            method: "post",
            url: MyApp.rootPath + "ResolutionApproval/DraftSrAssistantSecretary",
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
                    $scope.GetWatingListForSrAssistantSecretary();

                    $('#ResolutionModal').modal('hide');
                }
            } else {
                toastr.error("Failed!");
            }
        });

    };

    $scope.Reset = function () {
        $scope.gridResolutionOptions.data = [];
        $scope.uiID = "";
        $scope.uiCode = "";
        $scope.MemberResolutionDetail = "";
        $scope.MemberResolutionDate = "";
        $scope.MemberResolutionFIleURL = "";
        $scope.AcceptanceComment = "";
        $scope.RDNo = "";
        $scope.frmSrAssistantSecretaryApproval.ParliamentSession = "";

        $scope.UserName = "";
        $scope.ConstitutentBangla = "";
        $scope.MemberResolutionID = "";

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
        $scope.AcceptanceComment = "";
        $scope.MemberResolutionID = "";
        $scope.ApproveDetail = "";
        $scope.ApproveDate = "";

        $scope.btnSaveValue = "Save";
    };
});