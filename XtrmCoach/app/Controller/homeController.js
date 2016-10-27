(function () {
    'use strict';
    angular
        .module('app')
        .controller('homeController', homeController);

    function homeController($scope, $http) {
        $http.get('http://localhost:65335/api/User').then(function (response) {
            $scope.users = response.data;
        });
    }
})();