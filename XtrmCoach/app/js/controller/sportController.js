(function () {
	'use strict';
	angular
		.module('app')
		.controller('sportController', sportController);

	function sportController($scope, $location, $rootScope, $cookieStore, $timeout, sportService) {
		$rootScope.bodyLayout = 'dashboard-body';
		$rootScope.masterHeaderTitle = 'Sports';

		$scope.sports = [];
		var uneditedSport = {};
		$scope.showParameters = false;
		$scope.perfParameters = [];
		$scope.perfParaNames = [];
		$scope.perfParaTypeGroups = [];
		var uneditedPerfParameter = {};
		$scope.currentSportId = 0;
		$scope.isPerfParaEditOrAddInProgress = false;

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

		sportService.getPerfParaTypeGroups(5, function (perfParaTypeGroups, status) {
			if (status == 200) {
				$scope.perfParaTypeGroups = perfParaTypeGroups;
			}
		});

		$scope.fetchPerfParaTypeGroups = function (perfParaNameId, perfParaId) {
			if (perfParaNameId != 0) {
				sportService.getPerfParaTypeGroups(perfParaNameId, function (perfParaTypeGroups, status) {
					if (status == 200) {
						$scope.perfParaTypeGroups = perfParaTypeGroups;

						if (perfParaId != null) {
							for (var perfParaIndex = 0; perfParaIndex < $scope.perfParameters.length; perfParaIndex++) {
								if (perfParaId == $scope.perfParameters[perfParaIndex].id) {
									for (var perfParaTypeGroupIndex = 0; perfParaTypeGroupIndex < $scope.perfParaTypeGroups.length; perfParaTypeGroupIndex++) {
										if ($scope.perfParameters[perfParaIndex].perfParaTypeGroup.id == $scope.perfParaTypeGroups[perfParaTypeGroupIndex].id) {
											$scope.perfParameters[perfParaIndex].perfParaTypeGroup = $scope.perfParaTypeGroups[perfParaTypeGroupIndex];
											break;
										}
									}

									for (var perfParaNameIndex = 0; perfParaNameIndex < $scope.perfParaTypeGroups.length; perfParaNameIndex++) {
										if ($scope.perfParameters[perfParaIndex].perfParaName.id == $scope.perfParaNames[perfParaNameIndex].id) {
											$scope.perfParameters[perfParaIndex].perfParaName = $scope.perfParaNames[perfParaNameIndex];
											break;
										}
									}
								}
							}
						}
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
			$scope.isPerfParaEditOrAddInProgress = true;
			$scope.perfParameters.push({
				id: 0,
				sportId: $scope.currentSportId,
				perfParaName: {
					id: 0,
					name: ''
				},
				customName: '',
				perfParaTypeGroup: {
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
						$scope.isPerfParaEditOrAddInProgress = false;
					}
				});
			} else {
				sportService.updatePerfParameter(perfParameter, function (perfParameters, status) {
					if (status != false) {
						$scope.perfParameters = perfParameters;
						$scope.isPerfParaEditOrAddInProgress = false;
					}
				});
			}
		};

		$scope.cancelAddOrUpdateParameter = function (perfParameterId) {
			var perfParameter = getPerfParameter(perfParameterId);
			if (perfParameterId == 0) {
				$scope.perfParameters.pop(perfParameter);
			} else {
				perfParameter.perfParaName = uneditedPerfParameter.perfParaName;
				perfParameter.perfParaTypeGroup = uneditedPerfParameter.perfParaTypeGroup;
				perfParameter.isEdit = false;
			}

			$scope.isPerfParaEditOrAddInProgress = false;
		};

		$scope.editPerfParameter = function (perfParameterId) {
			var perfParameter = getPerfParameter(perfParameterId);
			uneditedPerfParameter = angular.copy(perfParameter);
			perfParameter.isEdit = true;
			$scope.isPerfParaEditOrAddInProgress = true;
			$scope.fetchPerfParaTypeGroups(perfParameter.perfParaName.id, perfParameterId);
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