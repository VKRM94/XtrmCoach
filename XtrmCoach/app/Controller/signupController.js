(function () {
	'use strict';
	angular
		.module('app')
		.controller('signupController', signupController);

	function signupController($scope, $location, $rootScope, $cookieStore) {
		$scope.signup = function () {
			// If Signup successful
			$location.path('/');
		};
	}
})();