(function () {
	'use strict';
	angular
		.module('app')
		.controller('dashboardController', dashboardController);

	function dashboardController($scope, $location, $rootScope, $cookieStore, $state, $stateParams) {
		$rootScope.bodyLayout = 'dashboard-body';
		$rootScope.masterHeaderTitle = 'Dashboard';

		if ($state.current.name == 'dashboard') {
			$scope.selectedOption = 'dashboard';
		} else if ($state.current.name == 'dashboard.sports') {
			$scope.selectedOption = 'sport';
		} else if ($state.current.name == 'dashboard.players') {
			$scope.selectedOption = 'player';
		}

		$scope.goToDashboard = function () {
			$rootScope.showDashboardLoader = false;
			$rootScope.masterHeaderTitle = 'Dashboard';
			$state.go('dashboard');
		};

		$scope.goToSport = function () {
			$rootScope.showDashboardLoader = true;
			if ($state.current.name == 'dashboard.sports') {
				$state.reload();
			} else {
				$state.go('dashboard.sports');
			}
		};

		$scope.goToPlayer = function () {
			$rootScope.showDashboardLoader = false;
			$state.go('dashboard.players');
		};

		$scope.logout = function () {
			$rootScope.isLoggedIn = false;
			$rootScope.user = {};
			$cookieStore.remove('isLoggedIn');
			$cookieStore.remove('user');

			$state.go('home');
		};
	}
})();