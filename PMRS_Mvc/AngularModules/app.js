var app = angular.module("myApp", ['ngCkeditor', 'ngTouch', 'ui.grid.selection', 'ui.grid.autoResize', 'ui.grid', 'ui.grid.cellNav', 'ui.grid.edit', 'ui.grid.rowEdit', 'ui.grid.grouping', 'ui.grid.pinning', 'ui.grid.exporter', 'ui.grid.resizeColumns', 'ui.grid.exporter', 'ui.grid.pagination', 'ui.grid.moveColumns', 'nvd3', 'ngSanitize', 'ui.select', 'ui.grid.pagination', '720kb.datepicker']);

app.directive('ckEditor', function () {
    return {
        require: '?ngModel',
        link: function (scope, elm, attr, ngModel) {
            var ck = CKEDITOR.replace(elm[0]);
            if (!ngModel) return;
            ck.on('instanceReady', function () {
                ck.setData(ngModel.$viewValue);
            });
            function updateModel() {
                scope.$apply(function () {
                    ngModel.$setViewValue(ck.getData());
                });
            }
            ck.on('change', updateModel);
            ck.on('key', updateModel);
            ck.on('dataReady', updateModel);

            ngModel.$render = function (value) {
                ck.setData(ngModel.$viewValue);
            };
        }
    };
});



app.run(function ($rootScope, $http, $window, $templateCache) {

    // Clearing existing Cache
    $rootScope.$on('$viewContentLoaded', function () {
        $templateCache.removeAll();
    });

    $rootScope.ActiveSts = "1";

    $rootScope.ViewPerm = "";
    $rootScope.SearchPerm = "";
    $rootScope.FormTitle = "";
    $rootScope.MenuName = "";
    $rootScope.NumberPatternNegative = "/^-?[0-9]+(\.[0-9]{1,9})?$/";
    $rootScope.NumberPattern = "/^[0-9]+(\.[0-9]{1,9})?$/";
    $rootScope.NumberPatternWithSign = "/^-?[0-9]+(\.[0-9]{1,2})?$/";
    //$rootScope.NumberPattern = "/^[0-9]*(\.{1})?([0-91-9][1-9])?$/";
    $rootScope.EmailFormat = "/^[a-zA-Z]+[a-z0-9._-]+@[a-z_-]+\.[a-z.]{2,5}$/";

    $rootScope.EventPerm = function (smID) {
        //$http({
        //    method: "Post",
        //    url: MyApp.rootPath + "Home/EventPermission",
        //    datatype: "json",
        //    data: { smID: smID }
        //}).then(function (res) {
        //    if (res.data.length !== 0) {
        //        $rootScope.ViewPerm = (res.data[0].Sv);
        //        $rootScope.SearchPerm = (res.data[0].Dl);
        //        $rootScope.FormTitle = (res.data[0].Nm);
        //        $rootScope.MenuName = (res.data[0].MenuName);
        //    } else {
        //        $window.location.href = '/Home/Index';
        //    }
        //});
    };
    //$rootScope.UploadDocumentApp=function (FolderName,ProposalNoTxtFldId,GridName) {

    //    var propsalNo = $("#" + ProposalNoTxtFldId).val();
    //    var fileUpload = $("#DocFile").get(0);
    //    var files = fileUpload.files;
    //    var name = files[0].name;

    //    var regex = new RegExp("(.*?)\.(xlsx|xls|csv)$");
    //    if (!(regex.test(name))) {
    //        $('#DocFile').val('');
    //        alert('Please select correct file format');
    //        return false;
    //    }

    //    if (files[0].size > 2097152) // 2 mb for bytes.
    //    {
    //        $('#DocFile').val('');
    //        alert("File size must under 2mb!");
    //        return false;
    //    }
    //    //var myID = 3; //uncomment this to make sure the ajax URL works
    //    if (files.length > 0) {
    //        if (window.FormData !== undefined) {
    //            var data = new FormData();
    //            for (var x = 0; x < files.length; x++) {
    //                data.append("file" + x, files[x]);
    //            }
    //            $.ajax({
    //                type: "POST",
    //                url: MyApp.rootPath + 'Home/UploadManager?FolderName=' + FolderName + '&PropsalNo=' + propsalNo,
    //                contentType: false,
    //                processData: false,
    //                data: data,
    //                success: function (result) {
    //                    if (result.Message!="") {
    //                        //var gridDocumentValue = {};
    //                        //gridDocumentValue.DocName = $("#DocName").val(0);
    //                        //gridDocumentValue.FileName = name;
    //                        //gridDocumentValue.Size = files[0].size;
    //                        //gridDocumentValue.FilePath = result.data.FilePath;
    //                        return gridDocumentValue;

    //                    } else {
    //                        return " ";
    //                    }


    //                },
    //                error: function (xhr, status, p3, p4) {
    //                    var err = "Error " + " " + status + " " + p3 + " " + p4;
    //                    if (xhr.responseText && xhr.responseText[0] == "{")
    //                        err = JSON.parse(xhr.responseText).Message;
    //                    console.log(err);
    //                }
    //            });
    //        } else {
    //            alert("This browser doesn't support HTML5 file uploads!");
    //        }
    //    }

    //};
    $rootScope.ResetForm = function () {
        $('input[type="hidden"]').val("");
        $('input[type="text"]').val("");
        $("textarea").val("");
        $('input[type="checkbox"]:checked').prop('checked', false);
        $("select").prop('selectedIndex', 0);
    };
});
app.filter('propsFilter', function () {
    return function (items, props) {
        var out = [];

        if (angular.isArray(items)) {
            items.forEach(function (item) {
                var itemMatches = false;

                var keys = Object.keys(props);
                for (var i = 0; i < keys.length; i++) {
                    var prop = keys[i];
                    var text = props[prop].toLowerCase();
                    if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                        itemMatches = true;
                        break;
                    }
                }
                if (itemMatches) {
                    out.push(item);
                }
            });
        } else {
            // Let the output be the input untouched
            out = items;
        }

        return out;
    };
});
app.filter('FullDateTime', function () {
    return function (value) {
        if (value === "ALL") { return value; }
        if (!value) { return ''; }

        var dt = new Date(parseInt(value.substr(6)));
        var month = ("0" + (dt.getMonth() + 1)).slice(-2);
        var day = ("0" + dt.getDate()).slice(-2);
        var year = dt.getFullYear();
        var hours = dt.getHours();
        var minutes = dt.getMinutes();
        var ampm = hours >= 12 ? 'PM' : 'AM';
        hours = hours % 12;
        hours = hours ? hours : 12; // the hour '0' should be '12'
        minutes = minutes < 10 ? '0' + minutes : minutes;
        var dtpDepEfct = day + '/' + month + '/' + dt.getFullYear();
        return dtpDepEfct;
    };
});


app.filter('FullDateWithTime', function () {
    return function (value) {
        if (value === "ALL") { return value; }
        if (!value) { return ''; }

        var dt = new Date(parseInt(value.substr(6)));
        var month = ("0" + (dt.getMonth() + 1)).slice(-2);
        var day = ("0" + dt.getDate()).slice(-2);
        var year = dt.getFullYear();
        var hours = dt.getHours();
        var minutes = dt.getMinutes();
        var ampm = hours >= 12 ? 'PM' : 'AM';
        hours = hours % 12;
        hours = hours ? hours : 12; // the hour '0' should be '12'
        minutes = minutes < 10 ? '0' + minutes : minutes;
        var dtpDepEfct = day + '/' + month + '/' + dt.getFullYear() + ' ' + hours + ':' + minutes + ' ' + ampm;
        return dtpDepEfct;
    };
});

app.filter('banglaNumber', function () {
    return function (input) {
        if (typeof input === 'number') {
            input = input.toString();
        }

        var banglaDigits = ['০', '১', '২', '৩', '৪', '৫', '৬', '৭', '৮', '৯'];

        var banglaNumber = '';
        for (var i = 0; i < input.length; i++) {
            var digit = parseInt(input[i]);
            banglaNumber += banglaDigits[digit];
        }
        return banglaNumber;
    };
});



app.factory('Session', function ($http) {
    var Session = {
        data: {},
        saveSession: function () { /* save session data to db */ },
        updateSession: function () {
            /* load data from db */
            $http.get('session.json')
                .then(function (r) { return Session.data = r.data; });
        }
    };
    Session.updateSession();
    return Session;
});
app.directive('loading', ['$http', function ($http) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            scope.isLoading = function () {
                return $http.pendingRequests.length > 0;
            };
            scope.$watch(scope.isLoading,
                function (value) {
                    if (value) {
                        element.removeClass('ng-hide');
                        //element.parent().addClass('blur');
                        $(".form-horizontal").addClass('blur');
                    } else {
                        element.addClass('ng-hide');
                        $(".form-horizontal").removeClass('blur');
                    }
                });
        }
    };
}]);

angular.module('ck', []).directive('ckEditor', function () {
    return {
        require: '?ngModel',
        link: function ($scope, elm, attr, ngModel) {

            var ck = CKEDITOR.replace(elm[0]);

            ck.on('instanceReady', function () {
                ck.setData(ngModel.$viewValue);
            });

            ck.on('pasteState', function () {
                $scope.$apply(function () {
                    ngModel.$setViewValue(ck.getData());
                });
            });

            ngModel.$render = function (value) {
                ck.setData(ngModel.$modelValue);
            };
        }
    };
})

app.directive('tooltip', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            element.hover(function () {
                // on mouseenter
                element.tooltip('show');
            }, function () {
                // on mouseleave
                element.tooltip('hide');
            });
        }
    };
});
function OperationMsg(mode) {

    if (mode === "I") {
        toastr.success("Saved Successfully!", '');
    }
    else if (mode === "U") {
        toastr.success('Updated Successfully!', '');
    }
    else if (mode === "No") {
        toastr.error('Not Saved!', '');
    }
    else if (mode === "D") {
        toastr.success('Deleted Successfully!', '');
    }
    else if (mode === "NoDel") {
        toastr.error('Not Deleted!', '');
    }
    else if (mode === "Unique") {
        toastr.error("Data Exists!", '');

    }
    else if (mode === "C") {
        toastr.success("Checked Successfully!", '');
    }
    else if (mode === "A") {
        toastr.success("Approved Successfully!", '');
    }
}
function CompareDate(fromDate, toDate, checkingMode) {
    var startDate = new Date(fromDate.split("/").reverse().join("-"));
    startDate = new Date(startDate.getFullYear(), startDate.getMonth(), startDate.getDate());

    var sDate = (startDate.getMonth() + 1) + '/' + startDate.getDate() + '/' + startDate.getFullYear();
    var endDate = new Date(toDate.split("/").reverse().join("-"));
    endDate = new Date(endDate.getFullYear(), endDate.getMonth(), endDate.getDate());
    var eDate = (endDate.getMonth() + 1) + '/' + endDate.getDate() + '/' + endDate.getFullYear();
    var todayDate = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());
    var tDate = (todayDate.getMonth() + 1) + '/' + todayDate.getDate() + '/' + todayDate.getFullYear();
    
    if (checkingMode === "greater") {
        if (startDate > endDate) {
            toastr.warning("Start Date Cannot be Greater Than End Date !");
            return false;
        }
        if (endDate > todayDate) {
            toastr.warning("End Date Cannot be Greater Than Present Date !");
            return false;
        }
    }if (checkingMode === "onlygreater") {
        if (startDate > endDate) {
            toastr.warning("Start Date Cannot be Greater Than End Date !");
            return false;
        }
        
    }
    if (checkingMode === "less") {
        if (startDate > endDate) {
            toastr.warning("Start Date Cannot be Greater Than End Date !");
            return false;
        }
        if (endDate > todayDate) {
            toastr.warning("End Date Cannot be Greater Than Present Date !");
            return false;
        }
    }
   

    return true;
}
