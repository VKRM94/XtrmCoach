(function () {
	'use strict';
	angular
		.module('app')
		.controller('homeController', homeController);

	function homeController($scope, $location, $rootScope, $cookieStore) {
		$scope.login = function () {
			var username = $scope.user.username;
			var password = $scope.user.password;

			if (username == 'a' && password == 'a') {
				$rootScope.isLoggedIn = true;
				$cookieStore.put('isLoggedIn', $rootScope.isLoggedIn);
				$location.path('/dashboard');
			}
		};

		$scope.signup= function () {
			$location.path('/signup');
		};
	}
})();