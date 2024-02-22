app.controller("employeeInfoCtrl", function ($scope, $http) {
    $scope.EventPerm(8);
    $scope.btnSaveValue = "Save";
    $scope.uiSelectDisable = false;
    $scope.EmployeeStatus = $scope.ActiveSts;

    $http({
        method: "GET",
        url: MyApp.rootPath + "DepartmentInfo/GetActiveDepartmentInfoList"
    }).then(function (response) {
        $scope.Departments = response.data;
    }, function (response) {
        toastr.warning("Error Occurred!");
    });

    $http({
        method: "GET",
        url: MyApp.rootPath + "DesignationInfo/GetActiveDesignationInfoList"
    }).then(function (response) {
        if (response.data !== "") {
            $scope.Designations = response.data;
        } else {
            toastr.warning("No Data Found!");
        }
    }, function (response) {
        toastr.warning("Error Occurred!");
    });

    $http({
        method: "GET",
        url: MyApp.rootPath + "EmployeeInfo/GetActiveEmployeeInfoList"
    }).then(function (response) {
        if (response.data !== "") {
            $scope.EmpList = response.data;
        } else {
            toastr.warning("No Data Found!");
        }
    }, function (response) {
        toastr.warning("Error Occurred!");
    });

    $scope.GetEmployeeList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "EmployeeInfo/GetEmployeeInfoList"
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
    $scope.GetAllMpInfo = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "EmployeeInfo/GetAllMpInfo"
        }).then(function (response) {
            toastr.success("MP has been updated!");
        }, function (response) {
            toastr.warning("Error Occurred!");
        });
    };
    $scope.GetAllEmpInfo = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "EmployeeInfo/GetAllEmpInfo"
        }).then(function (response) {
            toastr.success("Employee has been updated!");
        }, function (response) {
            toastr.warning("Error Occurred!");
        });
    };
    $scope.GetAllSignatures = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "EmployeeInfo/GetAllSignatures"
        }).then(function (response) {
            toastr.success("Employee has been updated!");
        }, function (response) {
            toastr.warning("Error Occurred!");
        });
    };
    var columnEmployeeList = [
        { name: 'UserID', displayName: "Emp ID", visible: false },
        { name: 'EmployeeCode', displayName: "Employee Code", width: 150 },
        { name: 'UserName', displayName: "Employee Name", width: 300 },
        { name: 'BanglaName', displayName: "Name in Bangla", visible: false },
        { name: 'FatherName', displayName: "Father Name", visible: false },
        { name: 'Address', displayName: "Address", visible: false },
        { name: 'UserType', displayName: "UserType", visible: false },
        { name: 'NationalID', displayName: "NID", visible: false },
        { name: 'Email', displayName: "Email", visible: false },
        { name: 'PhoneNumber', displayName: "Phone No", visible: false },
        { name: 'PhotoURL', displayName: "PhotoURL", visible: false },
        { name: 'DesignationID', displayName: " DesignationID", visible: false },
        { name: 'DepartmentID', displayName: "DepartmentID", visible: false },
        //{ name: 'DepartmentID', displayName: "DepartmentID", visible: false },
        { name: 'DesignationName', displayName: " Designation", width: 190 },
        { name: 'DepartmentName', displayName: "Department", width: 140 },
        { name: 'Status', displayName: "Status", width: 100 }
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
        $scope.UserName = row.entity.UserName;
        $scope.BanglaName = row.entity.BanglaName;
        $scope.Email = row.entity.Email;
        $scope.PhoneNumber = row.entity.PhoneNumber;
        $scope.EmployeeCode = row.entity.EmployeeCode;

        $scope.FatherName = row.entity.FatherName;
        $scope.Address = row.entity.Address;
        $scope.UserType = row.entity.UserType;        
        $scope.Nation = row.entity.NationalID;

        $scope.PhotoURL = row.entity.FatherName;
        $scope.EmployeeStatus = row.entity.Status;
        $scope.EmployeeID = row.entity.UserID;
        $scope.frmEmployeeInfo.EmployeeDesignation = row.entity.DesignationID;
        $scope.frmEmployeeInfo.EmployeeDepartment = row.entity.DepartmentID;

        $http({
            method: "POST",
            url: MyApp.rootPath + "EmployeeInfo/IsLogInInfoExist",
            data: { employeeId: row.entity.UserID }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.uiSelectDisable = true;
            } else {
                $scope.uiSelectDisable = false;
            }
        }, function () {
            toastr.warning("Error Occurred!");
        });
        $scope.btnSaveValue = "Update";
        $('#EmployeeInfoModal').modal('hide');
    };

    $scope.SaveData = function () {


        if ($scope.UserName === '' || $scope.UserName === undefined) {
            toastr.warning("ইংরেজি নাম দিন");
            return false;
        }

        if ($scope.BanglaName === '' || $scope.BanglaName === undefined) {
            toastr.warning("বাংলা নাম দিন");
            return false;
        }


        if ($scope.FatherName === '' || $scope.FatherName === undefined) {
            toastr.warning("বাবার নাম দিন");
            return false;
        }


        if ($scope.Nation === '' || $scope.Nation === undefined) {
            toastr.warning("এন আই ডি দিন");
            return false;
        }


        if ($scope.Email === '' || $scope.Email === undefined) {
            toastr.warning("ইমেইল এড্রেস দিন");
            return false;
        }


        $scope.SaveDb = {};
        $scope.SaveDb.UserName = $scope.UserName;
        $scope.SaveDb.BanglaName=  $scope.BanglaName ;
        $scope.SaveDb.Email = $scope.Email;
        $scope.SaveDb.PhoneNumber = $scope.PhoneNumber;
        $scope.SaveDb.EmployeeCode = $scope.EmployeeCode;

        $scope.SaveDb.FatherName = $scope.FatherName;
        $scope.SaveDb.Address = $scope.Address;
        $scope.SaveDb.UserType = $scope.UserType;
        $scope.SaveDb.NationalID = $scope.Nation;
        $scope.SaveDb.PhotoURL = $scope.PhotoURL;

        $scope.SaveDb.Status = $scope.EmployeeStatus;
        $scope.SaveDb.UserID = $scope.EmployeeID;
        $scope.SaveDb.DesignationID = $scope.frmEmployeeInfo.EmployeeDesignation;
        $scope.SaveDb.DepartmentID = $scope.frmEmployeeInfo.EmployeeDepartment;

        if ($scope.EmployeeID === '' || typeof $scope.EmployeeID === 'undefined') {
            $http({
                method: "post",
                url: MyApp.rootPath + "EmployeeInfo/InsertEmployeeInfo",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                if (response.data.Status === "Yes") {
                    OperationMsg(response.data.Mode);
                    if (response.data.Mode !== "Unique") {
                        $scope.EmployeeCode = response.data.Code;
                        $scope.EmployeeID = response.data.ID;
                        $scope.btnSaveValue = "Update";
                    }
                } else {
                    toastr.error("Failed!");
                }
            });
        } else {
            $http({
                method: "post",
                url: MyApp.rootPath + "EmployeeInfo/UpdateEmployeeInfo",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                if (response.data.Status === "Yes") {
                    OperationMsg(response.data.Mode);
                    if (response.data.Mode !== "Unique") {
                        $scope.EmployeeCode = response.data.Code;
                        $scope.EmployeeID = response.data.ID;
                        $scope.btnSaveValue = "Update";
                    }
                } else {
                    toastr.error("Failed!");
                }
            });
        }
    };

    $scope.Reset = function () {
        $scope.UserName = "";
        $scope.BanglaName = "";
        $scope.Email = "";
        $scope.PhoneNumber = "";
        $scope.EmployeeCode = "";
        $scope.FatherName = "";
        $scope.Address = "";
        $scope.UserType = "";
        $scope.Nation = "";
        $scope.PhotoURL = "";
        $scope.EmployeeStatus = "";
        $scope.EmployeeID = "";
        $scope.frmEmployeeInfo.EmployeeDesignation = undefined;
        $scope.frmEmployeeInfo.EmployeeDepartment = undefined;
        $scope.btnSaveValue = "Save";
        $scope.uiSelectDisable = false;
    };
});