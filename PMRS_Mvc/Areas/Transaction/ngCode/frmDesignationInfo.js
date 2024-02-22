app.controller("myCtrl", function ($scope, $http) {
    $scope.EventPerm(9);
    $scope.btnSaveValue = "Save";
    $scope.Status = $scope.ActiveSts;

    $scope.GetDesignationInfoList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "DesignationInfo/GetDesignationInfoList"
        }).then(function (response) {
            if (response.data.length > 0) {
                $('#DesignationModal').modal('toggle');
                $scope.gridDesignationOptions.data = response.data;
            }
            else {
                toastr.warning("No Data Found!");
            }
        }, function () {
            toastr.warning("No Data Found!");
        });
    };

    var columnDesignationList = [
        { name: 'DesignationID', displayName: "ID", visible: false },
        { name: 'DesignationCode', displayName: "Designation Code", width: 190 },
        { name: 'DesignationName', displayName: "Designation Name", width: 500 },
        { name: 'Status', displayName: "Status", width: 175 }
    ];

    $scope.gridDesignationOptions = {
        enableFiltering: true,
        enableSorting: true,
        //enableHorizontalScrollbar: true,
        //enableVerticalScrollbar: true,
        enableColumnResizing: true,
        paginationPageSizes: [8, 16, 24],
        paginationPageSize: 8,
        columnDefs: columnDesignationList,
        rowTemplate: rowTemplate(),
        onRegisterApi: function (gridApi) {
            $scope.gridDesignationOptions = gridApi;
        }
    };

    function rowTemplate() {
        return '<div ng-dblclick="grid.appScope.rowDblClickComp(row)" >' +
            '  <div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div></div>';
    }

    $scope.rowDblClickComp = function (row) {
        $scope.Reset();
        $scope.uiID = row.entity.DesignationID;
        $scope.uiCode = row.entity.DesignationCode;
        $scope.DesignationName = row.entity.DesignationName;
        $scope.Status = row.entity.Status;
        $scope.btnSaveValue = "Update";
        $('#DesignationModal').modal('hide');
    };

    $scope.SaveData = function () {

        if ($scope.DesignationName === '' || $scope.DesignationName === undefined) {
            toastr.warning("পদবী উল্লেখ করুন");
            return false;
        }

        $scope.SaveDb = {};
        $scope.SaveDb.DesignationID = $scope.uiID;
        $scope.SaveDb.DesignationCode = $scope.uiCode;
        $scope.SaveDb.DesignationName = $scope.DesignationName;
        $scope.SaveDb.Status = $scope.Status;

        if ($scope.uiCode == '' || typeof $scope.uiCode == 'undefined') {
            $http({
                method: "post",
                url: MyApp.rootPath + "DesignationInfo/InsertDesignation",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                if (response.data.Status == "Yes") {
                    OperationMsg(response.data.Mode);
                    if (response.data.Mode != "Unique") {
                        $scope.uiID = response.data.ID;
                        $scope.uiCode = response.data.Code;
                        $scope.btnSaveValue = "Update";
                    }
                } else {
                    toastr.error("Failed!");
                }
            });
        } else {
            $http({
                method: "post",
                url: MyApp.rootPath + "DesignationInfo/UpdateDesignation",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                if (response.data.Status == "Yes") {
                    OperationMsg(response.data.Mode);
                    if (response.data.Mode != "Unique") {
                        $scope.uiID = response.data.ID;
                        $scope.uiCode = response.data.Code;
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
        $scope.DesignationName = "";
        $scope.Status = $scope.ActiveSts;
        $scope.btnSaveValue = "Save";
    };
});