app.controller("myCtrl", function ($scope, $http) {
    $scope.btnSaveValue = "Save";
    $scope.EventPerm(3);

    $scope.GetHeadMenuList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "MH/GetHeadMenuList"
        }).then(function (response) {
            $scope.MhListCombo = response.data;
        }, function () {
            alert("Error Loading Category");
        });
    };

    $scope.GetHeadMenuList();

    $scope.GetSubMenuList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "SM/GetSubMenuList"
        }).then(function (response) {
            $scope.gridSMOptions.data = response.data;
            $('#test').modal('show');
        }, function () {
            alert("Error Loading Category");
        });
    };

    var columnSMList = [
        { name: 'ID', displayName: "ID", visible: false },
        { name: 'MH_ID', displayName: "MH_ID", visible: false },
        { name: 'Nm', displayName: "Menu Head" },
        { name: 'Subname', displayName: "Sub Menu Name" },
        { name: 'Seq', displayName: "Sequence" },
        { name: 'Url', displayName: "Url" },
        { name: 'CssClass', displayName: "Department", visible: false },
        //{ name: 'btnUpdate', enableFiltering: false, enableSorting: false, displayName: "Update", cellTemplate: '<div><button type="button" ng-click="grid.appScope.UpdateDept(row.entity)">Update</button></div>' },
    ];

    $scope.gridSMOptions = {
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnSMList,
        rowTemplate: rowTemplate(),
        onRegisterApi: function (gridApi) {
            $scope.gridSMOptions = gridApi;
        }
    };

    function rowTemplate() {
        return '<div ng-dblclick="grid.appScope.rowDblClickComp(row)" >' +
            '  <div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div></div>';
    }

    $scope.rowDblClickComp = function (row) {
        $scope.MH_ID = row.entity.MH_ID;
        $scope.Name = row.entity.Subname;
        $scope.CssClass = row.entity.CssClass;
        $scope.Sequence = row.entity.Seq;
        $scope.Url = row.entity.Url;
        $scope.uiID = row.entity.ID;

        $('#test').modal('hide');
    };

    $scope.SaveData = function () {


        if ($scope.MH_ID === '' || $scope.MH_ID === undefined) {
            toastr.warning("Please select Menu Head first");
            return false;
        }

        if ($scope.Name === '' || $scope.Name === undefined) {
            toastr.warning("Please provide Menu Head name");
            return false;
        }

        if ($scope.Sequence === '' || $scope.Sequence === undefined) {
            toastr.warning("Please provide Menu Sequence");
            return false;
        }

        if ($scope.Url === '' || $scope.Url === undefined) {
            toastr.warning("Please provide URL");
            return false;
        }


        $scope.SaveDb = {};

        $scope.SaveDb.Nm = $scope.Name;
        $scope.SaveDb.Seq = $scope.Sequence;
        $scope.SaveDb.CssClass = $scope.CssClass;
        $scope.SaveDb.ID = $scope.uiID;
        $scope.SaveDb.Url = $scope.Url;
        $scope.SaveDb.MH_ID = $scope.MH_ID;

        if ($scope.uiID === '' || typeof $scope.uiID === 'undefined') {
            $http({
                method: "post",
                url: MyApp.rootPath + "SM/InsertSm",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                OperationMsg("I");
            });
        } else {
            $http({
                method: "post",
                url: MyApp.rootPath + "SM/UpdateSm",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                OperationMsg("U");
            });
        }
    }

    $scope.Reset = function () {
        $scope.Name = "";
        $scope.Sequence = "";
        $scope.CssClass = "";
        $scope.uiID = "";
        $scope.Url = "";
    };
});