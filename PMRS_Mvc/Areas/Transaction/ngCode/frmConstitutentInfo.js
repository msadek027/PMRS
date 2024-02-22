app.controller("myCtrl", function ($scope, $http) {
    $scope.EventPerm(7);
    $scope.btnSaveValue = "Save";
    $scope.Status = $scope.ActiveSts;

    $scope.GetConsitutentInfoList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "ConstitutentInfo/GetConstitutentInfoList"
        }).then(function (response) {
            if (response.data.length > 0) {
                $('#ConsitutentModal').modal('toggle');
                $scope.gridConsitutentOptions.data = response.data;
            }
            else {
                toastr.warning("No Data Found!");
            }
        }, function () {
            toastr.warning("No Data Found!");
        });
    };

    var columnConsitutentList = [
        { name: 'ConstitutentID', displayName: "ID", visible: false },
        { name: 'ConstitutentArea', displayName: "Consitutent Name", width: 270 },
        { name: 'ConstitutentBangla', displayName: "Consitutent Name in Bangla", width: 300 },
        { name: 'ConstitutentNumber', displayName: "Consitutent Number", width: 300 },
        { name: 'Status', displayName: "Status", width: 100, visible: false  }
    ];

    $scope.gridConsitutentOptions = {
        enableFiltering: true,
        enableSorting: true,
        //enableHorizontalScrollbar: true,
        // enableVerticalScrollbar: true,
        enableColumnResizing: true,
        paginationPageSizes: [8, 16, 24],
        paginationPageSize: 8,
        columnDefs: columnConsitutentList,
        rowTemplate: rowTemplate(),
        onRegisterApi: function (gridApi) {
            $scope.gridConsitutentOptions = gridApi;
        }
    };

    function rowTemplate() {
        return '<div ng-dblclick="grid.appScope.rowDblClickComp(row)" >' +
            '  <div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div></div>';
    }

    $scope.rowDblClickComp = function (row) {
        $scope.Reset();
        $scope.uiID = row.entity.ConstitutentID;
        $scope.ConstitutentArea = row.entity.ConstitutentArea;
        $scope.ConstitutentBangla = row.entity.ConstitutentBangla;
        $scope.ConstitutentNumber = row.entity.ConstitutentNumber;
        $scope.Status = row.entity.Status;
        $scope.btnSaveValue = "Update";
        $('#ConsitutentModal').modal('hide');
    };

    $scope.SaveData = function () {

        if ($scope.ConstitutentArea === '' || $scope.ConstitutentArea === undefined) {
            toastr.warning("সংসদীয় আসনের ইংরেজি নাম দিন");
            return false;
        }

        if ($scope.ConstitutentBangla === '' || $scope.ConstitutentBangla === undefined) {
            toastr.warning("সংসদীয় আসনের বাংলা নাম দিন");
            return false;
        }

        if ($scope.ConstitutentNumber === '' || $scope.ConstitutentNumber === undefined) {
            toastr.warning("সংসদীয় আসন নম্বর দিন");
            return false;
        }

        $scope.SaveDb = {};
        $scope.SaveDb.ConstitutentID = $scope.uiID;
        $scope.SaveDb.ConstitutentArea = $scope.ConstitutentArea;
        $scope.SaveDb.ConstitutentBangla = $scope.ConstitutentBangla;
        $scope.SaveDb.ConstitutentNumber = $scope.ConstitutentNumber;
        $scope.SaveDb.Status = $scope.Status;

        if ($scope.uiID == '' || typeof $scope.uiID == 'undefined') {
            $http({
                method: "post",
                url: MyApp.rootPath + "ConstitutentInfo/InsertConstitutent",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                if (response.data.Status == "Yes") {
                    OperationMsg(response.data.Mode);
                    if (response.data.Mode != "Unique") {
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
                url: MyApp.rootPath + "ConstitutentInfo/UpdateConstitutent",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                if (response.data.Status == "Yes") {
                    OperationMsg(response.data.Mode);
                    if (response.data.Mode != "Unique") {
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
        $scope.ConstitutentArea = "";
        $scope.ConstitutentBangla = "";
        $scope.ConstitutentNumber = "";
        $scope.Status = $scope.ActiveSts;
        $scope.btnSaveValue = "Save";
    };
});