app.controller("myCtrl", function ($scope, $http, $filter) {
    $scope.EventPerm(15);
    $scope.btnSaveValue = "Save";
    $scope.Status = $scope.ActiveSts;

    $scope.GetSession = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "ParliamentSessionInfo/GetSession"
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
        { name: 'ParliamentSessionID', displayName: "ID", visible: false },
        { name: 'ParliamentNo', displayName: "সংসদ নং", width: 250 },
        { name: 'SessionNo', displayName: "অধিবেশন নং", width: 450 },
        { name: 'FromDate', displayName: "From Date", cellFilter: "FullDateTime", visible: false },
        { name: 'ToDate', displayName: "To Date", cellFilter: "FullDateTime", visible: false },
        {
            name: 'Status', displayName: "স্ট্যাটাস", width: 150,

            cellTemplate: '<div ng-show="row.entity.Status == \'1\' "> <p style="color: Green; font-weight: bold; text-align: center;">সচল</p></div> ' +
                ' <div ng-show="row.entity.Status ==  \'0\' "> <p style="color: Red; font-weight: bold; text-align: center;">বাতিল</p></div > ',}
    ];


    $scope.gridDepartmentOptions = {
        enableFiltering: true,
        enableSorting: true,
        //enableHorizontalScrollbar: true,
        // enableVerticalScrollbar: true,
        enableColumnResizing: true,
        paginationPageSizes: [8, 16, 24],
        paginationPageSize: 8,
        columnDefs: columnDepartmentList,
        rowTemplate: rowTemplate(),
        onRegisterApi: function (gridApi) {
            $scope.gridDepartmentOptions = gridApi;
        }
    };

    function rowTemplate() {
        return '<div ng-dblclick="grid.appScope.rowDblClickComp(row)" >' +
            '  <div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div></div>';
    }

    $scope.rowDblClickComp = function (row) {
        $scope.Reset();
        $scope.uiID = row.entity.ParliamentSessionID;
        $scope.uiCode = row.entity.ParliamentSessionID;
        $scope.ParliamentNo = row.entity.ParliamentNo;
        $scope.SessionNo = row.entity.SessionNo;
        $scope.FromDate = $filter('FullDateTime')(row.entity.FromDate);
        $scope.ToDate = $filter('FullDateTime')(row.entity.ToDate);
        $scope.Status = row.entity.Status;
        $scope.btnSaveValue = "Update";
        $('#DepartmentModal').modal('hide');
    };

    $scope.SaveData = function () {


        if ($scope.ParliamentNo === '' || $scope.ParliamentNo === undefined) {
            toastr.warning("সংসদ নম্বর দিন");
            return false;
        }

        if ($scope.SessionNo === '' || $scope.SessionNo === undefined) {
            toastr.warning("অধিবেশন নম্বর দিন");
            return false;
        }

        if ($scope.FromDate === '' || $scope.FromDate === undefined) {
            toastr.warning(" অধিবেশন শুরুর তারিখ দিন");
            return false;
        }

        if ($scope.ToDate === '' || $scope.ToDate === undefined) {
            toastr.warning(" অধিবেশন শেষের তারিখ দিন");
            return false;
        }

        if ($scope.Status === '' || $scope.Status === undefined) {
            toastr.warning("স্ট্যাটাস নির্বাচন করুন");
            return false;
        }

        $scope.SaveDb = {};

        $scope.SaveDb.ParliamentSessionID = $scope.uiCode;
        $scope.SaveDb.ParliamentNo = $scope.ParliamentNo;
        $scope.SaveDb.SessionNo = $scope.SessionNo;
        $scope.SaveDb.FromDate = $scope.FromDate;
        $scope.SaveDb.ToDate = $scope.ToDate;
        $scope.SaveDb.Status = $scope.Status;

        if ($scope.uiCode == '' || typeof $scope.uiCode == 'undefined') {
            $http({
                method: "post",
                url: MyApp.rootPath + "ParliamentSessionInfo/InsertSession",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                if (response.data.Status == "Yes") {
                    OperationMsg(response.data.Mode);
                    if (response.data.Mode != "Unique") {
                        $scope.uiCode = response.data.Code;
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
                url: MyApp.rootPath + "ParliamentSessionInfo/UpdateSession",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                if (response.data.Status == "Yes") {
                    OperationMsg(response.data.Mode);
                    if (response.data.Mode != "Unique") {
                        $scope.uiCode = response.data.Code;
                        $scope.uiID = response.data.ID;
                        $scope.btnSaveValue = "Update";
                    }
                } else {
                    toastr.error("Failed!");
                }
            });
        }
    };

    $scope.Reset = function () {
        $scope.uiCode = "";
        $scope.uiID = "";
        $scope.ParliamentNo = "";
        $scope.SessionNo = "";
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.Status = $scope.ActiveSts;
        $scope.btnSaveValue = "Save";
    };
});