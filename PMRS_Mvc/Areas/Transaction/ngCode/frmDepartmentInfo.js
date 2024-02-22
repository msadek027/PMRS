app.controller("myCtrl", function ($scope, $http) {
    $scope.EventPerm(1);
    $scope.btnSaveValue = "Save";
    $scope.Status = $scope.ActiveSts;

    $scope.GetDepartmentInfoList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "DepartmentInfo/GetDepartmentInfoList"
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
        { name: 'DepartmentID', displayName: "ID", visible: false },
        { name: 'DepartmentCode', displayName: "Department Code", width: 200 },
        { name: 'DepartmentName', displayName: "Department Name", width: 450 },
        { name: 'Status', displayName: "Status", width: 220 }
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
        $scope.uiID = row.entity.DepartmentID;
        $scope.uiCode = row.entity.DepartmentCode;
        $scope.DepartmentName = row.entity.DepartmentName;
        $scope.Status = row.entity.Status;
        $scope.btnSaveValue = "Update";
        $('#DepartmentModal').modal('hide');
    };

    $scope.SaveData = function () {

        if ($scope.DepartmentName === '' || $scope.DepartmentName === undefined) {
            toastr.warning("ডিপার্ট্মেন্টের নাম দিন");
            return false;
        }

        $scope.SaveDb = {};
        $scope.SaveDb.DepartmentID = $scope.uiID;
        $scope.SaveDb.DepartmentCode = $scope.uiCode;
        $scope.SaveDb.DepartmentName = $scope.DepartmentName;
        $scope.SaveDb.Status = $scope.Status;

        if ($scope.uiCode == '' || typeof $scope.uiCode == 'undefined') {
            $http({
                method: "post",
                url: MyApp.rootPath + "DepartmentInfo/InsertDepartment",
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
                url: MyApp.rootPath + "DepartmentInfo/UpdateDepartment",
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
        $scope.DepartmentName = "";
        $scope.Status = $scope.ActiveSts;
        $scope.btnSaveValue = "Save";
    };
});