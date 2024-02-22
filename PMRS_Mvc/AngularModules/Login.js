var applg = angular.module("myApp", []);
applg.directive('loading', ['$http', function ($http) {
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
                        $(".account2").addClass('blur');
                    } else {
                        element.addClass('ng-hide');
                        $(".account2").removeClass('blur');
                    }
                });
        }
    };
}]);
applg.directive('autocomplete', function () {

    return {

        restrict: 'A',
        link: function ($scope, el, attr) {

            el.bind('change',
                function (e) {
                    e.preventDefault();
                });
        }
    }

});
applg.controller("myCtrl", function ($scope, $http, $window) {

    $scope.LoginSystem = function () {

        if (($scope.Username === "" || $scope.Username == undefined) || ($scope.Password == undefined || $scope.Password === "")) {
            toastr.error("Invalid Credential!", '');
            return;
        } else {
            $scope.SaveDb = {};

            $scope.SaveDb.UserLoginName = $scope.Username;
            $scope.SaveDb.Password = $scope.Password;

            $http({
                method: "post",
                url: MyApp.rootPath + "Home/SingleSignOn",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                if (response.data == "true") {
                    $window.location.href = '/Home/Index';
                } else {
                    toastr.error("Invalid Credential!", '');
                }
            });
        };
    }


    $scope.SingleSignOn = function () {
 
        $http({
            method: "get",
            url: MyApp.rootPath + "Home/SingleSignOn?userName=" + $scope.Username+"&password="+$scope.Password,
          
            datatype: "json",
         
        }).then(function (response) {
            if (response.data == "true") {
                $window.location.href = '/Home/Index';
            } else {
                toastr.error("Invalid Credential!", '');
            }
        });
    };
   
});