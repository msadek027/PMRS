app.controller("myCtrl", function ($scope, $http) {
    $scope.btnSaveValue = "Save";
    $scope.EventPerm(2);

    $scope.GetHeadMenuList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "MH/GetHeadMenuList"
        }).then(function (response) {
            $scope.gridMHOptions.data = response.data;
            $('#test').modal('show');
        }, function () {
            alert("Error Loading Category");
        });
    };

    //$scope.GetHeadMenuList();

    var columnMHList = [
        { name: 'ID', displayName: "ID", visible: false },
        { name: 'Nm', displayName: "Name" },
        { name: 'Seq', displayName: "Sequence" },
        { name: 'CssClass', displayName: "Department", visible: false }
    ];

    $scope.gridMHOptions = {
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnMHList,
        rowTemplate: rowTemplate(),
        onRegisterApi: function (gridApi) {
            $scope.gridMHOptions = gridApi;
        }
    };

    function rowTemplate() {
        return '<div ng-dblclick="grid.appScope.rowDblClickComp(row)" >' +
            '  <div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div></div>';
    }

    $scope.rowDblClickComp = function (row) {
        $scope.Name = row.entity.Nm;
        $scope.Sequence = row.entity.Seq;
        $scope.CssClass = row.entity.CssClass;
        $scope.uiID = row.entity.ID;

        $('#test').modal('hide');
    };

    $scope.SaveData = function () {

        if ($scope.Name === '' || $scope.Name === undefined) {
            toastr.warning("Please provide Menu Head name");
            return false;
        }

        if ($scope.Sequence === '' || $scope.Sequence === undefined) {
            toastr.warning("Please provide Menu Sequence");
            return false;
        }

        $scope.SaveDb = {};

        $scope.SaveDb.Nm = $scope.Name;
        $scope.SaveDb.Seq = $scope.Sequence;
        $scope.SaveDb.CssClass = $scope.CssClass;
        $scope.SaveDb.ID = $scope.uiID;

        if ($scope.uiID === '' || typeof $scope.uiID === 'undefined') {
            $http({
                method: "post",
                url: MyApp.rootPath + "MH/InsertMh",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                OperationMsg("I");
            });
        } else {
            $http({
                method: "post",
                url: MyApp.rootPath + "MH/UpdateMh",
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
    };
});