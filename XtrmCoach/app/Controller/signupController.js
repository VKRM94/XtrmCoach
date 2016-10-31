(function () {
	'use strict';
	angular
		.module('app')
		.controller('signupController', ['$scope', '$location', '$rootScope', '$cookieStore', 'signupService', function signupController($scope, $location, $rootScope, $cookieStore, signupService) {
			$scope.user = {
				firstName: '',
				lastName: '',
				emailId: '',
				password: '',
				rePassword: ''
			};

			$scope.signup = function () {
				signupService.signup($scope.user, function (userCreated) {
					if (userCreated) {
						$location.path('/');
					} else {
						$scope.error = "User Already Present. Enter new email id.";
					}
				});
			};
		}]);
})();