(function () {
	'use strict';
	angular
		.module('app')
		.controller('dashboardController', dashboardController);

	function dashboardController($scope, $rootScope, $cookieStore, $state) {
		$rootScope.bodyLayout = 'dashboard-body';
		$rootScope.masterHeaderTitle = 'Dashboard';

		if ($state.current.name == 'dashboard.analysis') {
			$scope.selectedOption = 'analysis';
		} else if ($state.current.name == 'dashboard.sports') {
			$scope.selectedOption = 'sport';
		} else if ($state.current.name == 'dashboard.players') {
			$scope.selectedOption = 'player';
		}

		$scope.goToDashboardAnalysis = function () {
			$rootScope.showDashboardLoader = true;
			$rootScope.masterHeaderTitle = 'Dashboard';

			if ($state.current.name == 'dashboard.analysis') {
				$state.reload();
			} else {
				$state.go('dashboard.analysis');
			}
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
			$rootScope.showDashboardLoader = true;
			$state.go('dashboard.players');
		};

		$scope.goToEvaluate = function () {
			$rootScope.showDashboardLoader = true;

			if ($state.current.name == 'dashboard.evaluate') {
				$state.reload();
			} else {
				$state.go('dashboard.evaluate');
			}
		};

		$scope.logout = function () {
			$rootScope.isLoggedIn = false;
			$rootScope.user = {};
			$cookieStore.remove('isLoggedIn');
			$cookieStore.remove('user');

			$state.go('home');
		};

		if ($state.current.name == 'dashboard') {
			$scope.goToEvaluate();
		}
	}
})();