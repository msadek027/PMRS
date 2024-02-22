app.controller("myCtrl", function ($scope, $http) {
    $scope.EventPerm(6);
    $scope.btnSaveValue = "Save";

    $scope.GetRoleList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "RL/GetRoleList"
        }).then(function (response) {
            $scope.RLListCombo = response.data;
        }, function () {
            alert("Error Loading Category");
        });
    };

    $scope.GetRoleList();

    $scope.GetEmployeeList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "EmployeeInfo/GetActiveEmployeeInfoList"
        }).then(function (response) {
            if (response.data != "") {
                $scope.Employees = response.data;
            } else {
                toastr.warning("No Data Found!");
            }
        }, function () {
            toastr.warning("Error Occurred!");
        });
    };

    $scope.GetEmployeeList();

    $scope.GetEmployeeByRoleList = function () {
        $http({
            method: "POST",
            url: MyApp.rootPath + "EmployeeInfo/GetEmployeeByRoleList",
            data: { roleId: $scope.RL_ID },
        }).then(function (response) {
            $scope.gridRLConfOptions.data = response.data;
        }, function () {
            toastr.warning("Error Occurred!");
        });
    };

    var columnRLConfList = [
        { name: 'EmployeeID', displayName: "ID", visible: false },
        { name: 'EmployeeCode', displayName: "Employee Code" },
        { name: 'UserName', displayName: "Employee Name" },
        { name: 'DesignationName', displayName: " Designation" },
        { name: 'DesignationCode', displayName: " DesignationCode", visible: false },
        { name: 'DepartmentCode', displayName: "DepartmentCode", visible: false },
        { name: 'DepartmentName', displayName: "Department" },
        { name: 'Status', displayName: "Status" }
    ];

    $scope.gridRLConfOptions = {
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnRLConfList,
        rowTemplate: rowTemplate(),
        onRegisterApi: function (gridApi) {
            $scope.gridRLConfOptions = gridApi;
        }
    };

    function rowTemplate() {
        return '<div ng-dblclick="grid.appScope.rowDblClickComp(row)" >' +
            '  <div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div></div>';
    }

    $scope.rowDblClickComp = function (row) {
        $scope.frmROLConf.Emp_ID = row.entity.UserID;
        $scope.btnSaveValue = "Update";
    };

    $scope.SaveData = function () {


        if ($scope.RL_ID === '' || $scope.RL_ID === undefined) {
            toastr.warning("Please select User Role");
            return false;
        }

        if ($scope.frmROLConf.Emp_ID === '' || $scope.frmROLConf.Emp_ID === undefined) {
            toastr.warning("Please select Employee");
            return false;
        }

        $scope.SaveDb = {};
        $scope.SaveDb.RL_ID = $scope.RL_ID;
        $scope.SaveDb.Emp_ID = $scope.frmROLConf.Emp_ID;

        $http({
            method: "post",
            url: MyApp.rootPath + "ROLConf/SaveRLConf",
            datatype: "json",
            data: JSON.stringify($scope.SaveDb)
        }).then(function (response) {
            if (response.data.Status == "Yes") {
                OperationMsg(response.data.Mode);
                $scope.GetEmployeeByRoleList();
                //$scope.uiCode = response.data.Code;
            } else {
                toastr.error("Failed!");
            }
        });
    };

    $scope.Reset = function () {
        $scope.frmROLConf.Emp_ID = "";
        $scope.RL_ID = "";
        $scope.gridRLConfOptions.data = [];
        $scope.btnSaveValue = "Save";
    };
});