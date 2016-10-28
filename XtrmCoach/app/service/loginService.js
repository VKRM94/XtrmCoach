(function () {
	'use strict';
	angular
		.module('app')
		.service('loginService', loginService);

	function loginService($scope, $location, $rootScope, $https) {
		$scope.login = function (username, password, callback) {
			/*$http.get('http://localhost:65335/api/User').then(function (response) {
				$scope.users = response.data;
				callback();
			});*/
		};
	}
})();