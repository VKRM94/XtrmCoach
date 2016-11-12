(function () {
	'use strict';
	angular
		.module('app')
		.controller('dashboardController', dashboardController);

	function dashboardController($scope, $location, $rootScope, $cookieStore, $state) {
		$rootScope.bodyLayout = 'dashboard-body';
		$rootScope.masterHeaderTitle = 'Dashboard';

		$scope.logout = function () {
			$rootScope.isLoggedIn = false;
			$rootScope.user = {};
			$cookieStore.remove('isLoggedIn');
			$cookieStore.remove('user');

			$state.go('home');
		};
	}
})();