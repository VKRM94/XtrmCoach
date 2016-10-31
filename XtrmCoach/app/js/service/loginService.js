(function () {
	'use strict';
	angular.module('app')
	.service('loginService', ['$http', function ($http) {
		this.authenticateCredentials = function (username, password, callback) {
			var user = {
				'emailId': username,
				'password': password
			};

			var config = {
				headers: {
					'Content-Type': 'application/json'
				}
			};

			$http.post('http://localhost:65335/api/Login/', user, config)
			.success(function (data, status) {
				callback(data, status);
			})
			.error(function (data, status) {
				callback(data, status);
			});
		};
	}]);
})();