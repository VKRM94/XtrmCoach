(function () {
	'use strict';
	angular
		.module('app')
		.controller('analysisController', dashboardController);

	function dashboardController($scope, $rootScope, $state, sportService, playerService, evaluateService, analysisService) {
		$rootScope.bodyLayout = 'dashboard-body';
		$rootScope.masterHeaderTitle = 'Analysis';

		if ($state.current.name == 'dashboard.analysis') {
			$scope.selectedOption = 'analysis';
		} else if ($state.current.name == 'dashboard.sports') {
			$scope.selectedOption = 'sport';
		} else if ($state.current.name == 'dashboard.players') {
			$scope.selectedOption = 'player';
		}

		$scope.sports = [];
		$scope.players = [];
		$scope.perfParameters = [];

		$scope.selectedSport = null;
		$scope.selectedEvaluationParameter = null;
		$scope.isGraphMode = false;

		sportService.getSports(function (sports, status) {
			if (status == 200) {
				$scope.sports = sports;

				playerService.getPlayers($rootScope.user.id, function (players, status) {
					$scope.players = players;
					$rootScope.showDashboardLoader = false;
				});
			}
		});

		$scope.updateSelectedSport = function (sport) {
			$scope.selectedSport = sport;
			$scope.selectedEvaluationParameter = null;

			for (var playerIndex = 0; playerIndex < $scope.players.length; playerIndex++) {
				if ($scope.players[playerIndex].isSelected) {
					$scope.players[playerIndex].isSelected = false;
				}
			}

			$rootScope.showDashboardLoader = true;
			sportService.getPerfParameters(sport.id, function (perfParameters, status) {
				$rootScope.showDashboardLoader = false;
				if (status == 200) {
					$scope.perfParameters = perfParameters;
				}
			});
		};

		$scope.updateSelectedEvaluationParameter = function (evaluationParameter) {
			$scope.selectedEvaluationParameter = evaluationParameter;
		};

		var playerAnalysis = {};
		$scope.analyze = function () {
			playerAnalysis = {
				sport: $scope.selectedSport,
				players: [],
				perfPara: $scope.selectedEvaluationParameter,
				timeRange: 'LAST1WEEK'
			};

			var isAtleastOnePlayerSelected = false;
			for (var i = 0; i < $scope.players.length; i++) {
				if ($scope.players[i].isSelected) {
					playerAnalysis.players.push($scope.players[i]);
					isAtleastOnePlayerSelected = true;
				}
			}

			if (isAtleastOnePlayerSelected == true) {
				$scope.isGraphMode = true;
				$rootScope.showDashboardLoader = true;

				analysisService.getPlayerAnalysis(playerAnalysis, function (response, status) {
					$rootScope.showDashboardLoader = false;

					if (status == 200) {
						for (var i = 0; i < response.series.length; i++) {
							var data = response.series[i].data;
							var intData = [];

							for (var j = 0; j < data.length; j++) {
								if (data[j] != '' && data[j] != null) {
									intData.push(parseInt(data[j]));
								} else {
									intData.push(null);
								}
							}

							response.series[i].data = intData;
						}

						Highcharts.chart('analysis-high-chart-container', response);
					}
				});
			} else {
				// display toast
			}
		};

		$scope.updateTimeRange = function (timeRange) {
			if (playerAnalysis != null) {
				playerAnalysis.timeRange = timeRange;
				$rootScope.showDashboardLoader = true;

				analysisService.getPlayerAnalysis(playerAnalysis, function (response, status) {
					$rootScope.showDashboardLoader = false;

					if (status == 200) {
						for (var i = 0; i < response.series.length; i++) {
							var data = response.series[i].data;
							var intData = [];

							for (var j = 0; j < data.length; j++) {
								if (data[j] != '' && data[j] != null) {
									intData.push(parseInt(data[j]));
								} else {
									intData.push(null);
								}
							}

							response.series[i].data = intData;
						}

						Highcharts.chart('analysis-high-chart-container', response);
					}
				});
			}
		};
	}
})();