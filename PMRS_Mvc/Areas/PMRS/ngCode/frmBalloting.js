app.controller("myCtrl", function ($scope, $http, $filter) {
    $scope.EventPerm(14);
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
            url: MyApp.rootPath + "BallotInfo/GetResolutionForBalloting",
            data: { session: $scope.frmBallotInfo.ParliamentSession }
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
        { name: 'MemberResolutionDate', displayName: "প্রস্তাবের তারিখ", cellFilter: "FullDateWithTime", width: 150 },
        { name: 'html', displayName: "সিদ্ধান্ত প্রস্তাব", width: 330, cellTemplate: '<div ng-bind-html="COL_FIELD"></div>' },
        { name: 'SpeakerApproveDetail', displayName: "Original", visible: false },
        { name: 'MemberResolutionFIleURL', displayName: "URL", visible: false },
        { name: 'ConstitutentBangla', displayName: "নির্বাচনী এলাকা", width: 170 },
        { name: 'ParlSessID', displayName: "Session ID", visible: false },
        { name: 'UserID', displayName: "EMP ID", visible: false },
        { name: 'BanglaName', displayName: "সদস্যের নাম", width: 220 },
        { name: 'RDNo', displayName: "আর ডি নং", width: 120 },
        { name: 'Status', displayName: "Status", visible: false },
        { name: 'MemberResPriority', displayName: "প্রায়োরিটি", width: 150, visible: false },
        {
            name: 'Action ', displayName: "ব্যালোটিং", enableFiltering: false, enableSorting: false, width: "100",
            cellTemplate: '<div style="text-align:center;position: relative;width:100%;padding:2px 2px 2px 6px;"><button  class="btn btn-sm btn-success " ng-click="grid.appScope.editGridReferenceProductOptionsRow(row)"><i class="fa fa-edit"></i>&nbspব্যালোটিং</button></div>'
        },
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

    $scope.editGridReferenceProductOptionsRow = function (row) {
        $('#ResolutionModal').modal('show');

        $scope.ResolutionApproveID = row.entity.ResolutionApproveID;
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

        if ($scope.RaisedStatus === '' || $scope.RaisedStatus === undefined) {
            toastr.warning("ব্যালটের স্ট্যাটাস প্রদান করুন");
            return false;
        }

        if ($scope.BallotDate === '' || $scope.BallotDate === undefined) {
            toastr.warning("ব্যালটের তারিখ প্রদান করুন");
            return false;
        }


        $scope.SaveDb = {};

        $scope.SaveDb.BallotID = $scope.uiID;
        $scope.SaveDb.BallotDate = $scope.BallotDate;
        $scope.SaveDb.ResolutionApproveID = $scope.ResolutionApproveID;
        $scope.SaveDb.ParlSessID = $scope.frmBallotInfo.ParliamentSession;
        $scope.SaveDb.RaisedStatus = $scope.RaisedStatus;

        $http({
            method: "post",
            url: MyApp.rootPath + "BallotInfo/InsertBallot",
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

    $scope.BallotProcess = function () {
        if ($scope.gridResolutionOptions.data === undefined || $scope.gridResolutionOptions.data.length === 0) {
            alert("যথাযথ ডাটার অভাব রয়েছে");
        }
        else {
            alert($scope.gridResolutionOptions.data.length);
            $scope.RandomNumber = Math.floor((Math.random() * $scope.gridResolutionOptions.data.length) + 1);
            alert($scope.RandomNumber);  
        }
    };

    $scope.Reset = function () {
        $scope.uiID = "";
        $scope.RaisedStatus = "";
        $scope.gridResolutionOptions.data = [];
        $scope.BallotDate = "";
        $scope.frmBallotInfo.ParliamentSession = undefined;
        $scope.btnSaveValue = "Save";
    };
});