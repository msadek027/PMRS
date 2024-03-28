app.controller("myCtrl", function ($scope, $http, $filter, $timeout) {
    $scope.EventPerm(11);
    $scope.btnSaveValue = "Posting";
    $scope.Status = $scope.ActiveSts;
    $scope.SignTo = "";
    $scope.MemberResolutionDetail = "সংসদের অভিমত এই যে, ";
    $scope.isVisible = "false";


    $http({
        method: "GET",
        url: MyApp.rootPath + "ResolutionInfo/GetWorkflow"
    }).then(function (response) {
        $scope.WorkflowList = response.data;
    }, function (response) {
        toastr.warning("Error Occurred!");
    });
    $scope.frmResolutionInfo = {
        SignTo: 1 // Set your default value here
    };

    $http({
        method: "GET",
        url: MyApp.rootPath + "ParliamentSessionInfo/GetActiveSession"
    }).then(function (response) {
        $scope.Sessions = response.data;
    }, function (response) {
        toastr.warning("Error Occurred!");
    });

    $scope.GetActiveMPListByParliament = function () {
        debugger;
        $http({
            method: "POST",
            url: MyApp.rootPath + "EmployeeInfo/GetActiveMPListByParliament",
            data: { parliamentNo: $scope.frmResolutionInfo.ParliamentSession }
        }).then(function (response) {
            $scope.Employees = response.data;
        }, function (response) {
            toastr.warning("Error Occurred!");
        });
    };

    $scope.GetResolutionList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "ResolutionInfo/GetResolutionList"
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
    $scope.GetDraftResolutionList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "ResolutionInfo/GetDraftResolutionList"
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

    $scope.GetPostedHistoryList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "ResolutionInfo/GetPostedHistoryList"
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
        { name: 'ParliamentNo', displayName: "সংসদ নং", cellFilter: "banglaNumber", width: 100 },
        { name: 'SessionNo', displayName: "অধিবেশন নং", cellFilter: "banglaNumber", width: 100 },
        { name: 'RDNo', displayName: "আর ডি নং", width: 100, height: 200 },
        { name: 'UserName', displayName: "প্রস্তাবনা", width: 150, visible: false },
        { name: 'BanglaName', displayName: "প্রস্তাবনা", width: 100 },
        { name: 'html', displayName: "সিদ্ধান্ত প্রস্তাব", width: 530, cellTemplate: '<div ng-bind-html="COL_FIELD"></div>' },
        { name: 'MemberResolutionDetail', displayName: "Original", visible: false },
        { name: 'MemberResolutionDate', displayName: "প্রস্তাবের তারিখ", cellFilter: "FullDateWithTime", width: 150 },
        { name: 'AcceptanceComment', displayName: "গ্রহনযোগ্যতা", width: 130 },
        { name: 'MemberResolutionFIleURL', displayName: "URL", visible: false },
        { name: 'ParlSessID', displayName: "Session ID", visible: false },
        { name: 'AcceptStatus', displayName: "AcceptStatus", visible: false },
        { name: 'EntryType', displayName: "EntryType", visible: false },
        { name: 'UserID', displayName: "EMP ID", visible: false },
        { name: 'Status', displayName: "Status", width: 150, visible: false }
    ];

    var columnDepartmentList1 = [
        { name: 'MemberResolutionID', displayName: "ID", visible: false },

        { name: 'MemberResolutionDetail', displayName: "সিদ্ধান্ত প্রস্তাব", width: 380, cellTemplate: '<div ng-bind-html="COL_FIELD"></div>' },
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
    $scope.gridDepartmentOptions = {
        enableFiltering: true,
        enableSorting: true,
        //enableHorizontalScrollbar: true,
        // enableVerticalScrollbar: true,
        enableColumnResizing: true,
        paginationPageSizes: [10, 20, 50, 100],
        paginationPageSize: 10,
        columnDefs: columnDepartmentList,
        rowTemplate: rowTemplate(),

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
        rowTemplate: rowTemplate(),
        onRegisterApi: function (gridApi) {
            $scope.gridDepartmentOptions = gridApi;
        }
    };

    function rowTemplate() {

        return ' <div style="border-bottom:1px solid #D4D4D4;" ng-dblclick="grid.appScope.rowDblClickComp(row)"  ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div>';
    }


    $scope.rowDblClickComp = function (row) {
        $scope.Reset();

        $scope.dtConvert = ($filter('FullDateWithTime')(row.entity.MemberResolutionDate));


        //alert(dt);

        $scope.isVisible = "true";
        $scope.uiID = row.entity.MemberResolutionID;
        $scope.uiCode = row.entity.MemberResolutionID;
        $scope.MemberResolutionDetail = row.entity.MemberResolutionDetail;
        //$scope.MemberResolutionDate = $filter('FullDateWithTime') (row.entity.MemberResolutionDate);
        $scope.MemberResolutionDate = $scope.dtConvert.substr(0, 10);
        $scope.Hour = $scope.dtConvert.substr(11, 2);
        $scope.Minute = $scope.dtConvert.substr(14, 2);
        $scope.MemberResolutionFIleURL = row.entity.MemberResolutionFIleURL;
        $scope.RDNo = row.entity.RDNo;
        $scope.AcceptanceComment = row.entity.AcceptanceComment;
        $scope.frmResolutionInfo.ParliamentSession = row.entity.ParlSessID;
        $scope.GetActiveMPListByParliament();

        $scope.frmResolutionInfo.Employee = row.entity.UserID;
        $scope.Status = row.entity.Status;

        $scope.btnSaveValue = "Update";
        $('#DepartmentModal').modal('hide');
    };


    var columnResolutionList = [
        /*        { name: 'MemberResolutionID', displayName: "ID", visible: false },*/
        { name: 'RDNo', displayName: "আর ডি নং", },
        { name: 'Html', displayName: "সিদ্ধান্ত প্রস্তাব", width: 330, cellTemplate: '<div ng-bind-html="COL_FIELD"></div>' },
        { name: 'MemberResolutionDetail', displayName: "Original", visible: false },
        { name: 'AcceptanceComment', displayName: "গ্রহনযোগ্যতা" },
        { name: 'AcceptStatus', displayName: "AcceptStatus", visible: false },
        {
            name: 'UpdateAction', displayName: "সম্পাদনা", enableFiltering: false, enableSorting: false, width: 200,
            cellTemplate: '<div style="text-align:center;position: relative;width:100%;padding:2px 2px 2px 6px;"><button  class="btn btn-info btn-sm" ng-click="grid.appScope.modifyGrid(row)"><i class="fa fa-remove"></i>&nbspসম্পাদনা</button></div>'
        },
        {
            name: 'Action', displayName: "ডিলিট", enableFiltering: false, enableSorting: false, width: 200,
            cellTemplate: '<div style="text-align:center;position: relative;width:100%;padding:2px 2px 2px 6px;"><button  class="btn btn-danger btn-sm" ng-click="grid.appScope.RemovefromGrid(row)"><i class="fa fa-remove"></i>&nbspডিলিট</button></div>'
        },
    ];

    $scope.modifyGrid = function (row) {
        $scope.RDNo = row.entity.RDNo;
        $scope.MemberResolutionDetail = row.entity.MemberResolutionDetail;
        $scope.AcceptanceComment = row.entity.AcceptanceComment;

        var index = $scope.gridMemberResolutionsOptions.data.indexOf(row.entity);
        $scope.gridMemberResolutionsOptions.data.splice(index, 1);
    };

    $scope.RemovefromGrid = function (row) {
        var index = $scope.gridMemberResolutionsOptions.data.indexOf(row.entity);
        $scope.gridMemberResolutionsOptions.data.splice(index, 1);
    };

    $scope.gridMemberResolutionsOptions = {
        enableCellEdit: false,
        enableFiltering: true,
        enableSorting: true,
        paginationPageSizes: [10, 20, 50, 100],
        paginationPageSize: 10,
        columnDefs: columnResolutionList,
        enableColumnResizing: true,
        //onRegisterApi: function (gridApi) {
        //    $scope.gridMemberResolutionsOptions = gridApi;
        //}
    };


    $scope.addgridMemberResolutionsData = function () {

        if ($scope.frmResolutionInfo.ParliamentSession === '' || $scope.frmResolutionInfo.ParliamentSession === undefined) {
            toastr.warning("সংসদের অধিবেশন নির্বাচন করুন");
            return false;
        }

        if ($scope.frmResolutionInfo.Employee === '' || $scope.frmResolutionInfo.Employee === undefined) {
            toastr.warning("মাননীয় সংসদ সদস্য নির্বাচন করুন");
            return false;
        }

        if ($scope.RDNo === '' || $scope.RDNo === undefined) {
            toastr.warning("আর ডি নম্বর দিন");
            return false;
        }

        if ($scope.MemberResolutionDetail === '<p>সংসদের অভিমত এই যে,</p>') {
            toastr.warning("অনুপযুক্ত সিদ্ধান্ত প্রস্তাব");
            return false;
        }

        if ($scope.AcceptanceComment === '' || $scope.AcceptanceComment === undefined) {
            toastr.warning("গ্রহনযোগ্যতার মতামত দিন");
            return false;
        }


        var RDCheck = $scope.gridMemberResolutionsOptions.data;
        var count = 0;
        for (var i = 0; i < RDCheck.length; i++) {
            if ($scope.RDNo === RDCheck[i].RDNo) {
                count++;
            }
        }

        if (count > 0) {
            toastr.warning("এই আর ডি নম্বর ইতিমধ্যে ব্যবহৃত হয়েছে");
            return false;
        }


        $scope.gridMemberResolutionsOptions.data.push({
            RDNo: $scope.RDNo,
            MemberResolutionDetail: $scope.MemberResolutionDetail,
            Html: $scope.MemberResolutionDetail,
            AcceptanceComment: $scope.AcceptanceComment,
            AcceptStatus: $scope.AcceptStatus
        });

        toastr.success("প্রস্তাবটি যোগ করা হয়েছে");
        $scope.RDNo = "";
    };

    $scope.SaveData = function () {

        debugger;

        var resolutionList = $scope.gridMemberResolutionsOptions.data;

        $scope.SaveDb = {};

        if ($scope.frmResolutionInfo.ParliamentSession === '') {
            toastr.error("অধিবেশন নং নির্বাচন করুন");
            return false;
        }

        if ($scope.frmResolutionInfo.Employee === '') {
            toastr.error("মাননীয় সংসদ সদস্য নির্বাচন করুন");
            return false;
        }

        if ($scope.MemberResolutionDate === '') {
            toastr.error("সিদ্ধান্ত-প্রস্তাব প্রাপ্তির তারিখ দিন");
            return false;
        }

        if ($scope.Hour === '' || $scope.Minute === '') {
            toastr.error("সিদ্ধান্ত-প্রস্তাব প্রাপ্তির সময় উল্লেখ করুন");
            return false;
        }

        if (resolutionList.length === 0 && ($scope.uiID === '' || typeof $scope.uiID === 'undefined')) {
            toastr.error("কোন সিদ্ধান্ত প্রস্তাব পাওয়া যায়নি");
            return false;
        }

        else if (resolutionList.length === 0 && $scope.uiID !== '') {
            debugger;
            $scope.SaveDb.MemberResolutionID = $scope.uiID;
            $scope.SaveDb.MemberResolutionDetail = $scope.MemberResolutionDetail;
            $scope.SaveDb.MemberResolutionDate = $scope.MemberResolutionDate + " " + $scope.Hour + ":" + $scope.Minute;
            $scope.SaveDb.MemberResolutionFIleURL = $scope.MemberResolutionFIleURL;
            $scope.SaveDb.RDNo = $scope.RDNo;
            $scope.SaveDb.AcceptanceComment = $scope.AcceptanceComment;
            $scope.SaveDb.ParlSessID = $scope.frmResolutionInfo.ParliamentSession;
            $scope.SaveDb.UserID = $scope.frmResolutionInfo.Employee;
            $scope.SaveDb.Status = $scope.Status;
            $scope.SaveDb.SendTo = $scope.frmResolutionInfo.SignTo

            $http({
                method: "post",
                url: MyApp.rootPath + "ResolutionInfo/UpdateResolution",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                if (response.data.Status === "Yes") {
                    OperationMsg(response.data.Mode);
                    if (response.data.Mode !== "Unique") {
                        $scope.uiCode = response.data.ID;
                        $scope.uiID = response.data.ID;
                        $scope.btnSaveValue = "Update";
                    }
                } else {
                    toastr.error("Failed!");
                }
            });
        }
        else {


            for (var i = resolutionList.length - 1; i >= 0; i--) {

                $scope.SaveDb.MemberResolutionID = $scope.uiID;
                $scope.SaveDb.MemberResolutionDetail = resolutionList[i].MemberResolutionDetail;
                $scope.SaveDb.MemberResolutionDateStr = $scope.MemberResolutionDate + " " + $scope.Hour + ":" + $scope.Minute;
                //$scope.SaveDb.MemberResolutionFIleURL = $scope.MemberResolutionFIleURL;
                $scope.SaveDb.RDNo = resolutionList[i].RDNo;
                $scope.SaveDb.AcceptanceComment = resolutionList[i].AcceptanceComment;
                $scope.SaveDb.ParlSessID = $scope.frmResolutionInfo.ParliamentSession;
                $scope.SaveDb.UserID = $scope.frmResolutionInfo.Employee;
                $scope.SaveDb.Status = $scope.Status;


                $http({
                    method: "post",
                    url: MyApp.rootPath + "ResolutionInfo/InsertResolution",
                    datatype: "json",
                    data: JSON.stringify($scope.SaveDb)
                }).then(function (response) {
                    if (response.data.Status === "Yes") {
                        OperationMsg(response.data.Mode);
                        $scope.Reset();
                        if (response.data.Mode !== "Unique") {
                        }
                        else if (response.data.Mode === "Count Exceed") {
                            toastr.error("সংসদ সদস্যের প্রস্তাবনার লিমিট শেষ");
                            return;
                        }
                    } else {
                        toastr.error("Failed!");
                    }
                });
            }
        }
        $scope.Reset();
    };

    $scope.SaveDraft = function () {

        debugger;
        var resolutionList = $scope.gridMemberResolutionsOptions.data;

        $scope.SaveDb = {};

        if ($scope.frmResolutionInfo.ParliamentSession === '') {
            toastr.error("অধিবেশন নং নির্বাচন করুন");
            return false;
        }

        if ($scope.frmResolutionInfo.Employee === '') {
            toastr.error("মাননীয় সংসদ সদস্য নির্বাচন করুন");
            return false;
        }

        if ($scope.MemberResolutionDate === '') {
            toastr.error("সিদ্ধান্ত-প্রস্তাব প্রাপ্তির তারিখ দিন");
            return false;
        }

        if ($scope.Hour === '' || $scope.Minute === '') {
            toastr.error("সিদ্ধান্ত-প্রস্তাব প্রাপ্তির সময় উল্লেখ করুন");
            return false;
        }

        if (resolutionList.length === 0 && ($scope.uiID === '' || typeof $scope.uiID === 'undefined')) {
            toastr.error("কোন সিদ্ধান্ত প্রস্তাব পাওয়া যায়নি");
            return false;
        }

        else if (resolutionList.length === 0 && $scope.uiID !== '') {
            debugger;
            $scope.SaveDb.MemberResolutionID = $scope.uiID;
            $scope.SaveDb.MemberResolutionDetail = $scope.MemberResolutionDetail;
            $scope.SaveDb.MemberResolutionDate = $scope.MemberResolutionDate + " " + $scope.Hour + ":" + $scope.Minute;
            $scope.SaveDb.MemberResolutionFIleURL = $scope.MemberResolutionFIleURL;
            $scope.SaveDb.RDNo = $scope.RDNo;
            $scope.SaveDb.AcceptanceComment = $scope.AcceptanceComment;
            $scope.SaveDb.ParlSessID = $scope.frmResolutionInfo.ParliamentSession;
            $scope.SaveDb.UserID = $scope.frmResolutionInfo.Employee;
            $scope.SaveDb.Status = $scope.Status;
            $scope.SaveDb.SendTo = $scope.frmResolutionInfo.SignTo

            $http({
                method: "post",
                url: MyApp.rootPath + "ResolutionInfo/DraftResolution",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                if (response.data.Status === "Yes") {
                    OperationMsg(response.data.Mode);
                    if (response.data.Mode !== "Unique") {
                        $scope.uiCode = response.data.ID;
                        $scope.uiID = response.data.ID;
                        $scope.btnSaveValue = "Update";

                        location.reload();
                    }
                } else {
                    toastr.error("Failed!");
                }
            });
        }
        $scope.Reset();

    };
    $scope.Reset = function () {
    
        $scope.uiID = "";
        $scope.uiCode = "";
        $scope.gridMemberResolutionsOptions.data = [];
        $scope.MemberResolutionDetail = "সংসদের অভিমত এই যে, ";
        $scope.MemberResolutionDate = "";
        $scope.MemberResolutionFIleURL = "";
        $scope.RDNo = "";
        $scope.AcceptanceComment = "";
        $scope.Hour = "";
        $scope.Minute = "";
        $scope.frmResolutionInfo.ParliamentSession = undefined;
        $scope.frmResolutionInfo.Employee = undefined;

        $scope.Status = $scope.ActiveSts;

        $scope.btnSaveValue = "Save";
        $scope.isVisible = "false";
    };
});