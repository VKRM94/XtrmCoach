(function () {
	'use strict';
	angular.module('app')
	.service('evaluateService', ['$http', function ($http) {
		var EvaluateService = this;
		var userId = 0;
		var config = {
			headers: {
				'Content-Type': 'application/json'
			}
		};

		this.getPlayerEvaluations = function (sportId, playerId, callback) {
			userId = userId;
			$http.get('http://localhost:65335/api/PlayerEvaluation?sportId=' + sportId + '&playerId=' + playerId)
			.success(function (data, status) {
				callback(data, status);
			})
			.error(function (data, status) {
				callback(data, status);
			});
		};

		this.addPlayerEvaluation = function (evaluation, callback) {
			$http.post('http://localhost:65335/api/PlayerEvaluation/', evaluation, config)
			.success(function (response, status) {
				callback(status);
			})
			.error(function (response, status) {
				callback(status);
			});
		};

		this.updatePlayerEvaluation = function (evaluation, callback) {
			$http.put('http://localhost:65335/api/PlayerEvaluation/', evaluation, config)
			.success(function (response, status) {
				callback(status);
			})
			.error(function (status) {
				callback(status);
			});
		};

		this.deletePlayerEvaluation = function (evaluation, callback) {
			$http.delete('http://localhost:65335/api/PlayerEvaluation/' + evaluation.id, config)
			.success(function (response, status) {
				callback(status);
			})
			.error(function (status) {
				callback(status);
			});
		};
	}]);
})();