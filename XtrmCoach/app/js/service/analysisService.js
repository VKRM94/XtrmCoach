(function () {
	'use strict';
	angular.module('app')
	.service('analysisService', ['$http', function ($http) {
		var EvaluateService = this;
		var userId = 0;
		var config = {
			headers: {
				'Content-Type': 'application/json'
			}
		};

		this.getPlayerAnalysis = function (playerAnalysis, callback) {
			$http.post('http://localhost:65335/api/PlayerAnalysis/', playerAnalysis, config)
			.success(function (response, status) {
				callback(response, status);
			})
			.error(function (response, status) {
				callback(response, status);
			});
		};
	}]);
})();