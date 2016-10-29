(function () {
	'use strict';
	angular.module('app')
	.service('signupService', ['$http', function ($http) {
		this.signup = function (user, callback) {
			var config = {
				headers: {
					'Content-Type': 'application/json'
				}
			};

			$http.post('http://localhost:65335/api/Signup/', user, config)
			.success(function (status) {
				callback(true);
			})
			.error(function (status) {
				callback(false);
			});
		};
	}]);
})();