(function () {
	'use strict';
	angular
		.module('app')
		.controller('dashboardController', dashboardController);

	function dashboardController($scope, $location, $rootScope, $cookieStore) {
		$scope.logout = function () {
			$rootScope.isLoggedIn = false;
			$cookieStore.remove('isLoggedIn');
			$location.path('/');
		};
	}
})();