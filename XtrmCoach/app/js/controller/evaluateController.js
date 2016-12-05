(function () {
	'use strict';
	angular
		.module('app')
		.controller('evaluateController', evaluateController);

	function evaluateController($scope, $rootScope, sportService, playerService, evaluateService) {
		$rootScope.bodyLayout = 'dashboard-body';
		$rootScope.masterHeaderTitle = 'Evaluate Player';

		$scope.sports = [];
		$scope.players = [];
		$scope.playerEvaluations = [];

		$scope.selectedSportId = 0;
		$scope.selectedPlayerId = 0;
		$scope.selectedEvaluationParameter = null;

		sportService.getSports(function (sports, status) {
			if (status == 200) {
				$scope.sports = sports;

				playerService.getPlayers($rootScope.user.id, function (players, status) {
					$scope.players = players;
					$rootScope.showDashboardLoader = false;
				});
			}
		});

		$scope.updateSelectedSport = function (sportId) {
			$scope.selectedSportId = sportId;
			$scope.selectedPlayerId = 0;
			$scope.playerEvaluations = [];
			$scope.selectedEvaluationParameter = null;
		};

		var getPlayerEvaluations = function () {
			evaluateService.getPlayerEvaluations($scope.selectedSportId, $scope.selectedPlayerId, function (evaluations, status) {
				if (status == 200) {
					$scope.playerEvaluations = evaluations;
				}

				$rootScope.showDashboardLoader = false;
			});
		}

		$scope.updateSelectedPlayer = function (playerId) {
			$scope.selectedPlayerId = playerId;
			$scope.selectedEvaluationParameter = null;

			$rootScope.showDashboardLoader = true;
			getPlayerEvaluations();
		};

		$scope.updateSelectedEvaluationParameter = function (evaluationParameter) {
			$scope.selectedEvaluationParameter = angular.copy(evaluationParameter);
		};

		$scope.evaluate = function () {
			if ($scope.selectedEvaluationParameter.perfParaTypes.length == 1) {
				$scope.selectedEvaluationParameter.selectedType = $scope.selectedEvaluationParameter.perfParaTypes[0];
			}

			if ($scope.selectedEvaluationParameter.selectedType.id != null) {
				$rootScope.showDashboardLoader = true;
				evaluateService.addPlayerEvaluation($scope.selectedEvaluationParameter, function (status) {
					if (status == 200) {
						$rootScope.showDashboardLoader = true;
						$scope.selectedEvaluationParameter = null;
						getPlayerEvaluations();
					}

					$rootScope.showDashboardLoader = false;
				});
			}
		};
	}
})();