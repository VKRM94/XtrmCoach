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
		$scope.currentSportId = 0;

		sportService.getSports(function (sports, status) {
			$scope.sports = sports;
			$rootScope.showDashboardLoader = false;
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

		var getSport = function (sportId) {
			return $scope.sports.filter(function (sport) { return sport.id == sportId; })[0];
		}

		sportService.getPerfParaNames(function (perfParaNames, status) {
			$scope.perfParaNames = perfParaNames;
		});

		$scope.fetchPerfParaTypes = function (perfParaNameId) {
			if (perfParaNameId != 0) {
				sportService.getPerfParaTypes(perfParaNameId, function (perfParaTypes, status) {
					if (status == 200) {
						$scope.perfParaTypes = perfParaTypes;
					}
				});
			}
		};

		$scope.getParameters = function (sportId) {
			$scope.showParameters = true;
			$scope.currentSportId = sportId;

			sportService.getPerfParameters(sportId, function (perfParameters, status) {
				if (status == 200) {
					$scope.perfParameters = perfParameters;
				}
			});
		};

		$scope.addNewPerfParameter = function () {
			$scope.perfParameters.push({
				id: 0,
				sportId: $scope.currentSportId,
				perfParaName: {
					id: 0,
					name: ''
				},
				customName: '',
				perfParaName: {
					id: 0,
					name: ''
				},
				isEdit: true
			});
		};

		$scope.addOrUpdatePerfParameter = function (perfParameterId) {
			var perfParameter = getPerfParameter(perfParameterId);
			if (perfParameterId == 0) {
				sportService.addNewPerfParameter(perfParameter, function (perfParameters, status) {
					if (status != false) {
						$scope.perfParameters = perfParameters;
					}
				});
			} else {
				sportService.updatePerfParameter(perfParameter, function (perfParameters, status) {
					if (status != false) {
						$scope.perfParameters = perfParameters;
					}
				});
			}
		};

		$scope.cancelAddOrUpdateParameter = function (perfParameterId) {
			var perfParameter = getPerfParameter(perfParameterId);
			if (perfParameterId == 0) {
				$scope.perfParameters.pop(perfParameter);
			} else {
				perfParameter.name = uneditedPerfParameter.name;
				perfParameter.isEdit = false;
			}
		};

		$scope.editPerfParameter = function (perfParameterId) {
			var perfParameter = getPerfParameter(perfParameterId);
			uneditedSport = angular.copy(perfParameter);
			perfParameter.isEdit = true;
		};

		$scope.deletePerfParameter = function (perfParameterId) {
			sportService.deletePerfParameter(perfParameterId, function (response, status) {
				if (status == 200) {
					sportService.getPerfParameters($scope.currentSportId, function (perfParameters, status) {
						if (status == 200) {
							$scope.perfParameters = perfParameters;
						}
					});
				} else {
					$scope.error = response;
				}
			});
		};

		var getPerfParameter = function (perfParameterId) {
			return $scope.perfParameters.filter(function (perfParameter) {
				return perfParameter.id == perfParameterId;
			})[0];
		}
	}
})();