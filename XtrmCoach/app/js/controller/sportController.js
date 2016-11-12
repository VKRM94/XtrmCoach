(function () {
	'use strict';
	angular
		.module('app')
		.controller('sportController', sportController);

	function sportController($scope, $location, $rootScope, $cookieStore, sportService) {
		$rootScope.bodyLayout = 'dashboard-body';
		$rootScope.masterHeaderTitle = 'Sports';

		$scope.sports = [];
		var uneditedSport = {};
		$scope.showParameters = false;
		$scope.perfParameters = [];
		$scope.perfParaNames = [];
		$scope.perfParaTypes = [];
		var uneditedPerfParameter = {};

		sportService.getSports(function (sports, status) {
			$scope.sports = sports;
		});

		$scope.addNewSport = function () {
			$scope.sports.push({
				id: 0,
				name: '',
				isEdit: true,
				userId: $rootScope.user.id
			});
		};

		$scope.addOrUpdateSport = function (sportId) {
			var sport = getSport(sportId);
			if (sportId == 0) {
				sportService.addNewSport(sport, function (sports, status) {
					if (status != false) {
						$scope.sports = sports;
					}
				});
			} else {
				sportService.updateSport(sport, function (sports, status) {
					if (status != false) {
						$scope.sports = sports;
					}
				});
			}
		};

		$scope.cancelAddOrUpdate = function (sportId) {
			var sport = getSport(sportId);
			if (sportId == 0) {
				$scope.sports.pop(sport);
			} else {
				sport.name = uneditedSport.name;
				sport.isEdit = false;
			}
		};

		$scope.editSport = function (sportId) {
			var sport = getSport(sportId);
			uneditedSport = angular.copy(sport);
			sport.isEdit = true;
		};

		$scope.deleteSport = function (sportId) {
			sportService.deleteSport(sportId, function (sports, status) {
				if (status != false) {
					$scope.sports = sports;
				}
			});
		};

		var getSport = function(sportId) {
			return $scope.sports.filter(function (sport) { return sport.id == sportId })[0];
		}

		sportService.getPerfParaNames(function (perfParaNames, status) {
			$scope.perfParaNames = perfParaNames;
		});

		$scope.getParameters = function (sportId) {
			$scope.showParameters = true;

			sportService.getPerfParameters(sportId, function (perfParameters, status) {
				if (status == 200) {
					$scope.perfParameters = perfParameters;
				}
			});
		}
	}
})();