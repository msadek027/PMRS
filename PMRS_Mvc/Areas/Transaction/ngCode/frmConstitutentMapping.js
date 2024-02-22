app.controller("myCtrl", function ($scope, $http) {
    $scope.EventPerm(10);
    $scope.btnSaveValue = "Save";
    $scope.uiSelectDisable = false;
    $scope.EmployeeStatus = $scope.ActiveSts;

    $http({
        method: "GET",
        url: MyApp.rootPath + "ConstitutentInfo/GetConstitutentInfoList"
    }).then(function (response) {
        $scope.Constitutents = response.data;
    }, function (response) {
        toastr.warning("Error Occurred!");
    });

 
    $http({
        method: "GET",
        url: MyApp.rootPath + "EmployeeInfo/GetActiveMPList"
    }).then(function (response) {
        if (response.data !== "") {
            $scope.Employees = response.data;
        } else {
            toastr.warning("No Data Found!");
        }
    }, function (response) {
        toastr.warning("Error Occurred!");
    });

    $scope.GetMapList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "ConstitutentMapping/GetConstitutentMappingList"
        }).then(function (response) {
            if (response.data.length > 0) {
                $('#EmployeeInfoModal').modal('toggle');
                $scope.gridEmployeeInfoOptions.data = response.data;
            } else {
                toastr.warning("No Data Found!");
            }
        }, function (response) {
            toastr.warning("Error Occurred!");
        });
    };


    var columnEmployeeList = [
        { name: 'ConstitutentID', displayName: "Constitutent ID", visible: false },
        { name: 'ParliamentNo', displayName: "Parliament No", width: 180 },
        { name: 'UserID', displayName: "UserID", width: 350, visible: false  },
        { name: 'UserName', displayName: "MP Name", width: 340},
        { name: 'ConstitutentArea', displayName: "Consitutent", width: 340 },
        { name: 'ConstitutentNumber', displayName: "Con No", visible: false },
        { name: 'UserMappingID', displayName: "MapID", visible: false },
    ];

    $scope.gridEmployeeInfoOptions = {
        enableFiltering: true,
        enableSorting: true,
        enableColumnResizing: true,
        paginationPageSizes: [8, 16, 24],
        paginationPageSize: 8,
        columnDefs: columnEmployeeList,
        rowTemplate: rowTemplate(),
        onRegisterApi: function (gridApi) {
            $scope.gridEmployeeOptions = gridApi;
        }
    };

    function rowTemplate() {
        return '<div ng-dblclick="grid.appScope.rowDblClickComp(row)" >' +
            '  <div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div></div>';
    }

    $scope.rowDblClickComp = function (row) {
        $scope.Reset();
        $scope.uiID = row.entity.UserMappingID;
        $scope.ParliamentNo = row.entity.ParliamentNo;
        $scope.frmConstitutentMapping.Employee = row.entity.UserID;
        $scope.frmConstitutentMapping.ConstitutentID = row.entity.ConstitutentID;

        $scope.btnSaveValue = "Update";
        $('#EmployeeInfoModal').modal('hide');
    };

    $scope.SaveData = function () {
        $scope.SaveDb = {};
        $scope.SaveDb.ParliamentNo = $scope.ParliamentNo;
        $scope.SaveDb.UserID = $scope.frmConstitutentMapping.Employee;
        $scope.SaveDb.ConstitutentID = $scope.frmConstitutentMapping.Constitutent;

        if ($scope.uiID === '' || typeof $scope.uiID === 'undefined') {
            $http({
                method: "post",
                url: MyApp.rootPath + "ConstitutentMapping/InsertConstitutentMapping",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                if (response.data.Status === "Yes") {
                    OperationMsg(response.data.Mode);
                    if (response.data.Mode !== "Unique") {     
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
                url: MyApp.rootPath + "ConstitutentMapping/UpdateConstitutentMapping",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                if (response.data.Status === "Yes") {
                    OperationMsg(response.data.Mode);
                    if (response.data.Mode !== "Unique") {
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
        $scope.uiID = "";
        $scope.ParliamentNo = "";
        $scope.frmConstitutentMapping.Employee = undefined;
        $scope.frmConstitutentMapping.Constitutent = undefined;
        $scope.btnSaveValue = "Save";
        $scope.uiSelectDisable = false;
    };
});