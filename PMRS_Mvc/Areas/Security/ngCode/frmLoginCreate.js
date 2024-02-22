app.controller("myCtrl", function ($scope, $http) {
    $scope.EventPerm(20);
    $scope.btnSaveValue = "Save";
    $scope.ShowDt = false;

    $scope.GetEmployeeList = function () {
        $http({
            method: "POST",
            url: MyApp.rootPath + "UserLogin/GetRemainingEmployee",
            datatype: "json",
            data: { empType: $scope.EmpType }
        }).then(function (response) {
            if (response.data !== "") {
                $scope.Employees = response.data;
            }
        }, function () {
            toastr.warning("Error Occurred!");
        });
    };

    // $scope.GetEmployeeList();

    $scope.GetUserLoginList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "UserLogin/GetUserLoginList"
        }).then(function (response) {
            if (response.data !== "") {
                $('#UserLoginModal').modal('toggle');
                $scope.gridUserLoginOptions.data = response.data;
            }
        }, function () {
            toastr.warning("No Data Found!");
        });
    };

    var columnUserLoginList = [
        { name: 'UserLoginID', displayName: "ID", visible: false },
        { name: 'UserID', displayName: "ID", visible: false },
        { name: 'UserName', displayName: "Employee" },
        { name: 'UserLoginName', displayName: "User Name" },
        { name: 'Password', visible: false },
        { name: 'Status', displayName: "Status" }
    ];

    $scope.gridUserLoginOptions = {
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnUserLoginList,
        rowTemplate: rowTemplate(),
        onRegisterApi: function (gridApi) {
            $scope.gridUserLoginOptions = gridApi;
        }
    };

    function rowTemplate() {
        return '<div ng-dblclick="grid.appScope.rowDblClickComp(row)" >' +
            '  <div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div></div>';
    }

    $scope.rowDblClickComp = function (row) {
        $scope.ShowDt = true;
        $scope.uiID = row.entity.UserLoginID;
        $scope.frmLoginCreate.EmployeeID = row.entity.UserID;
        $scope.EmpName = row.entity.UserName;
        $scope.Username = row.entity.UserLoginName;
        $scope.Password = row.entity.Password;
        $scope.RePassword = row.entity.Password;
        $scope.Status = row.entity.Status;
        $scope.btnSaveValue = "Update";
        $('#UserLoginModal').modal('hide');
    };

    $scope.SaveData = function () {

        if ($scope.frmLoginCreate.EmployeeID === '' || $scope.frmLoginCreate.EmployeeID === undefined) {
            toastr.warning("Please select Employee");
            return false;
        }

        if ($scope.Username === '' || $scope.Username === undefined) {
            toastr.warning("User Name can not be empty");
            return false;
        }

        if ($scope.Password === '' || $scope.Password === undefined) {
            toastr.warning("Password can not be empty");
            return false;
        }

        $scope.SaveDb = {};
        $scope.SaveDb.UserLoginID = $scope.uiID;
        $scope.SaveDb.UserID = $scope.frmLoginCreate.EmployeeID;
        $scope.SaveDb.UserLoginName = $scope.Username;
        $scope.SaveDb.Password = $scope.Password;
        $scope.SaveDb.Status = $scope.Status;

        if ($scope.uiID === '' || typeof $scope.uiID === 'undefined') {
            $http({
                method: "post",
                url: MyApp.rootPath + "UserLogin/InsertUser",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                if (response.data.Status === "Yes") {
                    OperationMsg(response.data.Mode);
                    $scope.uiID = response.data.Code;
                    $scope.GetEmployeeList();
                } else {
                    toastr.error("Failed!");
                }
            });
        } else {
            $http({
                method: "post",
                url: MyApp.rootPath + "UserLogin/UpdateUser",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                if (response.data.Status === "Yes") {
                    OperationMsg(response.data.Mode);
                    $scope.uiID = response.data.Code;
                    $scope.GetEmployeeList();
                } else {
                    toastr.error("Failed!");
                }
            });
        }
    };

    $scope.Reset = function () {
        $scope.uiID = "";
        $scope.frmLoginCreate.EmployeeID = undefined;
        $scope.Employees = [];
        $scope.Username = "";
        $scope.Password = "";
        $scope.RePassword = "";
        $scope.Status = "";
        $scope.ShowDt = false;
        $scope.btnSaveValue = "Save";
    };
});