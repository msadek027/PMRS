app.controller("myCtrl", function ($scope, $http) {
    $scope.EventPerm(5);
    $scope.btnSaveValue = "Save";

    $scope.GetRoleInfoList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "RL/GetRoleList"
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridRoleOptions.data = response.data;
                $('#RoleModal').modal('show');
            } else {
                toastr.warning("No Data Found!");
            }
        }, function () {
            toastr.warning("No Data Found!");
        });
    };

    var columnRoleList = [
        { name: 'ID', displayName: "Role Code" },
        { name: 'Nm', displayName: "Role Code" },
        { name: 'Priority', visible: false }
    ];

    $scope.gridRoleOptions = {
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnRoleList,
        rowTemplate: rowTemplate(),
        onRegisterApi: function (gridApi) {
            $scope.gridRoleOptions = gridApi;
        }
    };

    function rowTemplate() {
        return '<div ng-dblclick="grid.appScope.rowDblClickComp(row)" >' +
            '  <div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div></div>';
    }

    $scope.rowDblClickComp = function (row) {
        $scope.uiID = row.entity.ID;
        $scope.uiCode = row.entity.ID;
        $scope.RoleName = row.entity.Nm;
        $scope.btnSaveValue = "Update";
        $('#RoleModal').modal('hide');
    };

    $scope.SaveData = function () {

        if ($scope.RoleName === '' || $scope.RoleName === undefined) {
            toastr.warning("Please provide Role name");
            return false;
        }

        $scope.SaveDb = {};
        $scope.SaveDb.ID = $scope.uiID;
        $scope.SaveDb.Nm = $scope.RoleName;

        if ($scope.uiCode === '' || typeof $scope.uiCode == 'undefined') {
            $http({
                method: "post",
                url: MyApp.rootPath + "RL/InsertRL",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                if (response.data.Status === "Yes") {
                    OperationMsg(response.data.Mode);
                    $scope.uiCode = response.data.Code;
                } else {
                    toastr.error("Failed!");
                }
            });
        } else {
            $http({
                method: "post",
                url: MyApp.rootPath + "RL/UpdateRL",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                if (response.data.Status === "Yes") {
                    OperationMsg(response.data.Mode);
                    $scope.uiCode = response.data.Code;
                } else {
                    toastr.error("Failed!");
                }
            });
        }
    };

    $scope.Reset = function () {
        $scope.uiCode = "";
        $scope.uiID = "";
        $scope.RoleName = "";
        $scope.btnSaveValue = "Save";
    };
});