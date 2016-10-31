(function () {
	'use strict';
	angular
		.module('app')
		.controller('signupController', ['$scope', '$location', '$rootScope', '$cookieStore', 'signupService', function signupController($scope, $location, $rootScope, $cookieStore, signupService) {
			$rootScope.bodyLayout = 'login-signup-body';

			$scope.user = {
				firstName: '',
				lastName: '',
				emailId: '',
				password: '',
				rePassword: ''
			};

			$scope.isUserAlreadyPresent = false;

			$scope.signup = function () {
				signupService.signup($scope.user, function (userCreated) {
					if (userCreated) {
						$location.path('/');
						$rootScope.isSignUpSuccess = true;
					} else {
						$scope.isUserAlreadyPresent = true;
					}
				});
			};
		}]);
})();