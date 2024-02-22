app.controller("myCtrl", function ($scope, $http, $filter) {
    $scope.labelValue = "Project No";
    $scope.isRequired = '';

    $scope.isVisible = true;
    var date = new Date();
    var firstDay = new Date(new Date().getFullYear(), 0, 1);
    firstDay = ('0' + firstDay.getDate()).slice(-2) + '/' + ('0' + (firstDay.getMonth() + 1)).slice(-2) + '/' + firstDay.getFullYear();
    var toDay = new Date();
    toDay = ('0' + toDay.getDate()).slice(-2) + '/' + ('0' + (toDay.getMonth() + 1)).slice(-2) + '/' + toDay.getFullYear();
    $scope.FromDate = firstDay;
    $scope.ToDate = toDay;
    //$scope.ProposalClick = function () {
    //    $scope.ReportData = "";
    //    $scope.labelValue = "Proposal No";
    //};


    $http({
        method: "GET",
        url: MyApp.rootPath + "ParliamentSessionInfo/GetActiveSession"
    }).then(function (response) {
        $scope.Sessions = response.data;
    }, function (response) {
        toastr.warning("Error Occurred!");
    });


    $scope.GetSession = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "ParliamentSessionInfo/GetSession"
        }).then(function (response) {
            if (response.data.length > 0) {
                $('#SessionModal').modal('toggle');
                $scope.gridSessionOptions.data = response.data;
            }
            else {
                toastr.warning("No Data Found!");
            }
        }, function () {
            toastr.warning("No Data Found!");
        });
    };


    var columnSessionList = [
        { name: 'ParliamentSessionID', displayName: "ID", visible: false },
        { name: 'ParliamentNo', displayName: "সংসদ নং", width: 350 },
        { name: 'SessionNo', displayName: "অধিবেশন নং", width: 500 },
    ];


    $scope.gridSessionOptions = {
        enableFiltering: true,
        enableSorting: true,
        enableColumnResizing: true,
        paginationPageSizes: [8, 16, 24],
        paginationPageSize: 8,
        columnDefs: columnSessionList,
        rowTemplate: rowTemplateSession(),
        onRegisterApi: function (gridApi) {
            $scope.gridSessionOptions = gridApi;
        }
    };

    function rowTemplateSession() {
        return '<div ng-dblclick="grid.appScope.rowDblClickSession(row)" >' +
            '  <div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div></div>';
    }

    $scope.rowDblClickSession = function (row) {
        $scope.ParliamentSessionID = row.entity.ParliamentSessionID;
        $scope.ParliamentNo = "সংসদ নং: " + row.entity.ParliamentNo + ",   অধিবেশন নং: " + row.entity.SessionNo;
        $('#SessionModal').modal('hide');
    };


    $scope.GridReportClick = function (event) {
        $scope.isVisible = false;
        var src = event.defaultValue;
        var win = window.open(src, '_blank');
        win.focus();
    };
    $scope.SetItemTypeCode = function () {
        $scope.ItemTypeCode = $scope.frmCommonRpt.ItemType.selected;
    };

    $scope.GetReportData = function () {
        var fromDate = new Date($scope.FromDate.split("/").reverse().join("-"));
        var rptFromDate = fromDate.getDate() + '/' + (fromDate.getMonth() + 1) + '/' + fromDate.getFullYear();
        var toDate = new Date($scope.ToDate.split("/").reverse().join("-"));
        var rptToDate = toDate.getDate() + '/' + (toDate.getMonth() + 1) + '/' + toDate.getFullYear();

    };


    var columnAllProposalList = [
        { name: 'MfrID', displayName: "MfrID", visible: false },
        { name: 'MfrNo', displayName: "Mfr No", width: "80" },
        { name: 'ProposalNo', displayName: "Proposal No", width: "90" },
        { name: 'MfrStatus', displayName: "Status", visible: false },
        { name: 'GenericStrength', displayName: "Generic & Strength", width: "400" },
        { name: 'DosageName', displayName: "Dosage Form", width: "300" },
        { name: 'PreparedBy', displayName: "Prepared By", visible: false }
    ];

    $scope.gridAllProposalOptions = {
        enableFiltering: true,
        enableSorting: true,
        enableColumnResizing: true,
        paginationPageSizes: [8, 16, 24],
        paginationPageSize: 8,
        columnDefs: columnAllProposalList,
        rowTemplate: rowTemplate()
    };

    function rowTemplate() {
        return '<div  ng-dblclick="grid.appScope.rowDblClickComp(row)" >' +
            '  <div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div></div>';
    }

    $scope.rowDblClickComp = function (row) {
        $scope.ReportData = row.entity.ProposalNo;
        $('#AllProposalModal').modal('hide');
    };

    $scope.Reset = function () {
        $(".btn-reset").each(function (i) {
            this.checked = false;
        });
        $scope.ReportData = "";
        $scope.ParliamentNo = "";
        //$scope.frmCommonRpt.ItemType = undefined;
        $scope.ReportFormat = "PDF";
        $scope.ReportName = "";
        $scope.FromDate = firstDay;
        $scope.ToDate = toDay;
        $scope.isRequired = '';
        $scope.isVisible = true;
    };

    var columnRptlist = [
        { name: 'ID', visible: false },
        { name: 'ReportName', displayName: "Select Report" },
        { name: 'RptCode', visible: false }
    ];

    $scope.gridReportOptions = {
        enableFiltering: true,
        enableColumnResizing: true,
        columnDefs: columnRptlist,
        rowTemplate: rowRptTemplate()
    };

    $scope.rowDblClick = function (row) {
        $scope.ReportName = row.entity.ReportName;
        $scope.ParameterFieldOperation(row.entity.RptCode);
    };

    $scope.HideFieldForGridReport = function () {

        $scope.companyDtls = false;
        $scope.ProjectDataShow = false;
    };

    $scope.ParameterFieldOperation = function (ReportCode) {

    };

    function rowRptTemplate() {
        return '<div ng-dblclick="grid.appScope.rowDblClick(row)" >' +
            '  <div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div>' +
            '</div>';
    }

    $scope.GetReportsByForm = function () {
        $http({
            method: "POST",
            url: MyApp.rootPath + "RPTConf/GetReportByFormRole",
            data: { frmName: "frmPRMSRpt" }
        }).then(function (response) {
            $scope.gridReportOptions.data = response.data;
        },
            function () {
                alert("Error Loading Category");
            });
    };

    $scope.GetReportsByForm();
});