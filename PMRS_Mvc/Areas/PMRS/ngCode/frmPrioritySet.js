app.controller("myCtrl", function ($scope, $http, $filter) {
    $scope.EventPerm(13);
    $scope.btnSaveValue = "Save";
    //$scope.Status = $scope.ActiveSts;

    $http({
        method: "GET",
        url: MyApp.rootPath + "ParliamentSessionInfo/GetActiveSession"
    }).then(function (response) {
        $scope.Sessions = response.data;
    }, function (response) {
        toastr.warning("Error Occurred!");
    });

    $scope.GetResolutionBySession = function () {
        $http({
            method: "POST",
            url: MyApp.rootPath + "PrioritySet/GetResolutionBySession",
            data: { session: $scope.frmPrioritySet.ParliamentSession }
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
    $scope.GetSentResolutionBySession = function () {
        $http({
            method: "POST",
            url: MyApp.rootPath + "Priority/GetSentResolutionBySession",
            data: { session: $scope.frmPrioritySet.ParliamentSession }
        }).then(function (response) {
            if (response.data.length > 0) {
                $('#DepartmentModal1').modal('toggle');
                $scope.gridResolutionOptions1.data = response.data;
            }
            else {
                toastr.warning("No Data Found!");
            }
        }, function () {
            toastr.warning("No Data Found!");
        });
    };
    var columnResolutionList = [
        { name: 'ResolutionApproveID', displayName: "ID", visible: false },
        { name: 'MemberResolutionID', displayName: "ID", visible: false },
        { name: 'MemberResolutionDate', displayName: "প্রস্তাবের তারিখ", cellFilter: "FullDateWithTime", width: 150 },
        { name: 'html', displayName: "সিদ্ধান্ত প্রস্তাব", width: 300, cellTemplate: '<div ng-bind-html="COL_FIELD"></div>' },
        { name: 'SpeakerApproveDetail', displayName: "Original", visible: false },
        { name: 'MemberResolutionFIleURL', displayName: "URL", visible: false },
        { name: 'ConstitutentBangla', displayName: "নির্বাচনী এলাকা", width: 180 },
        { name: 'ParlSessID', displayName: "Session ID", visible: false },
        { name: 'UserID', displayName: "EMP ID", visible: false },
        { name: 'BanglaName', displayName: "সদস্যের নাম", width: 260 },
        { name: 'RDNo', displayName: "আর ডি নং", width: 120 },
        { name: 'Status', displayName: "Status", visible: false },
        { name: 'MemberResPriority', displayName: "প্রায়োরিটি", width: 150, visible: false },
        { name: 'DeputySecSignature', displayName: "সহকারী সচিব", width: 120, cellTemplate: '<img src="{{row.entity.DeputySecSignature}}" alt="Not Signed" width="200">' },
        { name: 'JointSecSignature', displayName: "যুগ্ম সচিব", width: 120, cellTemplate: '<img src="{{row.entity.JointSecSignature}}" alt="Not Signed" width="150">' },
        { name: 'SecSignature', displayName: "অতিরিক্ত সচিব", width: 120, cellTemplate: '<img src="{{row.entity.SecSignature}}" alt="Not Signed" width="150">' },
        { name: 'AddSecSignature', displayName: " সচিব", width: 120, cellTemplate: '<img src="{{row.entity.AddSecSignature}}" alt="Not Signed" width="150">' },
        { name: 'SpeakerSignature', displayName: "স্পিকার", width: 120, cellTemplate: '<img src="{{row.entity.SpeakerSignature}}" alt="Not Signed" width="150">' },
        {
            name: 'Action ', displayName: "প্রায়োরিটি", enableFiltering: false, enableSorting: false, width: "100",
            cellTemplate: '<div style="text-align:center;position: relative;width:100%;padding:2px 2px 2px 6px;"><button  class="btn btn-sm btn-success " ng-click="grid.appScope.editGridReferenceProductOptionsRow(row)"><i class="fa fa-edit"></i>&nbspEdit</button></div>'
        },
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
        { name: 'DeputySecSignature', displayName: "সহকারী সচিব", width: 120, cellTemplate: '<img src="{{row.entity.DeputySecSignature}}" alt="Not Signed" width="200">' },
        { name: 'JointSecSignature', displayName: "যুগ্ম সচিব", width: 120, cellTemplate: '<img src="{{row.entity.JointSecSignature}}" alt="Not Signed" width="150">' },
        { name: 'SecSignature', displayName: "অতিরিক্ত সচিব", width: 120, cellTemplate: '<img src="{{row.entity.SecSignature}}" alt="Not Signed" width="150">' },
        { name: 'AddSecSignature', displayName: "সচিব", width: 120, cellTemplate: '<img src="{{row.entity.AddSecSignature}}" alt="Not Signed" width="150">' },
        { name: 'SpeakerSignature', displayName: "স্পিকার", width: 120, cellTemplate: '<img src="{{row.entity.SpeakerSignature}}" alt="Not Signed" width="150">' },
        { name: 'Status', displayName: "Status", width: 150, visible: false }
    ];
    $scope.gridResolutionOptions = {
        enableFiltering: true,
        enableSorting: true,
        enableColumnResizing: true,
        paginationPageSizes: [10, 20, 50, 100],
        paginationPageSize: 10,
        columnDefs: columnResolutionList,
        onRegisterApi: function (gridApi) {
            $scope.gridResolutionOptions = gridApi;
        }
    };
    $scope.gridResolutionOptions1 = {
        enableFiltering: true,
        enableSorting: true,
        //enableHorizontalScrollbar: true,
        // enableVerticalScrollbar: true,
        enableColumnResizing: true,
        paginationPageSizes: [10, 20, 50, 100],
        paginationPageSize: 10,
        columnDefs: columnDepartmentList1,
        onRegisterApi: function (gridApi) {
            $scope.gridResolutionOptions1 = gridApi;
        }
    };
    $scope.editGridReferenceProductOptionsRow = function (row) {
        $('#ResolutionModal').modal('show');

        $scope.uiID = row.entity.ResolutionApproveID;
        $scope.uiCode = row.entity.ResolutionApproveID;
        $scope.UserName = row.entity.BanglaName + ',' + row.entity.ConstitutentBangla;
        //$scope.ConstitutentBangla = row.entity.ConstitutentBangla;
        $scope.MemberResolutionID = row.entity.MemberResolutionID;
        $scope.MemberResolutionDetail = row.entity.SpeakerApproveDetail;
        $scope.MemberResolutionDate = $filter('FullDateWithTime')(row.entity.MemberResolutionDate);
        $scope.MemberResolutionFIleURL = row.entity.MemberResolutionFIleURL;
        $scope.RDNo = row.entity.RDNo;
        $scope.MemberResPriority = row.entity.MemberResPriority;
        index = $scope.gridReferenceProductOptions.data.indexOf(row.entity);
    };

    $scope.SaveData = function () {

        if ($scope.MemberResPriority === '' || $scope.MemberResPriority === undefined) {
            toastr.warning("প্রায়োরিটি প্রদান করুন");
            return false;
        }

        $scope.SaveDb = {};

        $scope.SaveDb.ResolutionApproveID = $scope.uiID;
        $scope.SaveDb.MemberResPriority = $scope.MemberResPriority;

        $http({
            method: "post",
            url: MyApp.rootPath + "PrioritySet/UpdatePriority",
            datatype: "json",
            data: JSON.stringify($scope.SaveDb)
        }).then(function (response) {
            if (response.data.Status == "Yes") {
                OperationMsg(response.data.Mode);
                if (response.data.Mode != "Unique") {
                    $scope.GetResolutionBySession();
                    $('#ResolutionModal').modal('hide');
                }
            } else {
                toastr.error("Failed!");
            }
        });
    };

    $scope.Reset = function () {
        $scope.uiID = "";
        $scope.uiCode = "";
        $scope.gridResolutionOptions.data = [];
        $scope.frmPrioritySet.ParliamentSession = undefined;
        $scope.btnSaveValue = "Save";
    };
});