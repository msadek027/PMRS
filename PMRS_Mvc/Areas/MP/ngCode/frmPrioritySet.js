app.controller("myCtrl", function ($scope, $http, $filter) {
    $scope.EventPerm(23);
    $scope.btnSaveValue = "Save";
    //$scope.Status = $scope.ActiveSts;

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
            url: MyApp.rootPath + "Priority/GetResolutionBySession",
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


    var columnResolutionList = [
        { name: 'ResolutionApproveID', displayName: "ID", visible: false },
        { name: 'MemberResolutionID', displayName: "ID", visible: false },
        { name: 'MemberResolutionDate', displayName: "প্রস্তাবের তারিখ", cellFilter: "FullDateTime", width: 200 },
        { name: 'html', displayName: "সিদ্ধান্ত প্রস্তাব", width: 300, cellTemplate: '<div ng-bind-html="COL_FIELD"></div>' },
        { name: 'SpeakerApproveDetail', displayName: "Original", visible: false },
        { name: 'MemberResolutionFIleURL', displayName: "URL", visible: false },
        { name: 'ConstitutentBangla', displayName: "নির্বাচনী এলাকা", width: 200, visible: false },
        { name: 'ParlSessID', displayName: "Session ID", visible: false },
        { name: 'UserID', displayName: "EMP ID", visible: false },
        { name: 'UserName', displayName: "সদস্যের নাম", width: 260, visible: false },
        { name: 'RDNo', displayName: "আর ডি নং", width: 200 },
        { name: 'Status', displayName: "Status", visible: false },
        { name: 'MemberResPriority', displayName: "প্রায়োরিটি", width: 150, visible: false },
        {
            name: 'Action ', displayName: "প্রায়োরিটি", enableFiltering: false, enableSorting: false, width: "180",
            cellTemplate: '<div style="text-align:center;position: relative;width:100%;padding:2px 2px 2px 6px;"><button  class="btn btn-sm btn-success " ng-click="grid.appScope.editGridReferenceProductOptionsRow(row)"><i class="fa fa-edit"></i>&nbspEdit</button></div>'
        },
    ];
    $scope.gridResolutionOptions = {
        enableFiltering: true,
        enableSorting: true,
        enableColumnResizing: true,
        paginationPageSizes: [8, 16, 24],
        paginationPageSize: 8,
        columnDefs: columnResolutionList,
        onRegisterApi: function (gridApi) {
            $scope.gridResolutionOptions = gridApi;
        }
    };

    $scope.editGridReferenceProductOptionsRow = function (row) {
        $('#ResolutionModal').modal('show');

        $scope.uiID = row.entity.ResolutionApproveID;
        $scope.uiCode = row.entity.ResolutionApproveID;
        $scope.UserName = row.entity.UserName;
        $scope.ConstitutentBangla = row.entity.ConstitutentBangla;
        $scope.MemberResolutionID = row.entity.MemberResolutionID;
        $scope.MemberResolutionDetail = row.entity.SpeakerApproveDetail;
        $scope.MemberResolutionDate = $filter('FullDateTime')(row.entity.MemberResolutionDate);
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
            url: MyApp.rootPath + "Priority/UpdatePriority",
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