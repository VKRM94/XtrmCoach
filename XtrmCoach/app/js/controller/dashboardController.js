(function () {
	'use strict';
	angular
		.module('app')
		.controller('dashboardController', dashboardController);

	function dashboardController($scope, $location, $rootScope, $cookieStore, $state) {
		$rootScope.bodyLayout = 'dashboard-body';
		$rootScope.masterHeaderTitle = 'Dashboard';
		$scope.selectedOption = 'dashboard';

		$scope.goToDashboard = function () {
			$rootScope.masterHeaderTitle = 'Dashboard';
			$state.go('dashboard');
		}

		$scope.goToSport = function () {
			$state.go('dashboard.sports');
		}

		$scope.goToPlayer = function () {
			$state.go('dashboard.players');
		}

		$scope.logout = function () {
			$rootScope.isLoggedIn = false;
			$rootScope.user = {};
			$cookieStore.remove('isLoggedIn');
			$cookieStore.remove('user');

			$state.go('home');
		};
	}
})();