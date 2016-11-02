﻿(function () {
	'use strict';
	angular
		.module('app')
		.controller('homeController', ['$scope', '$location', '$rootScope', '$cookieStore', 'loginService', function ($scope, $location, $rootScope, $cookieStore, loginService) {
			$rootScope.bodyLayout = 'login-signup-body';

			$scope.user = {
				username: '',
				password: ''
			};

			$scope.isInValidCredentials = false;

			$scope.isSignUpSuccess = false;
			if ($rootScope.isSignUpSuccess == true) {
				$scope.isSignUpSuccess = true;
				$rootScope.isSignUpSuccess = false;
			}

			$scope.login = function () {
				var username = $scope.user.username;
				var password = $scope.user.password;

				loginService.authenticateCredentials(username, password, function (response, statusCode) {
					if (statusCode == 200) {
						$rootScope.user = response;
						$cookieStore.put('user', $rootScope.user);
						$rootScope.isLoggedIn = true;
						$cookieStore.put('isLoggedIn', $rootScope.isLoggedIn);
						$location.path('/dashboard');
					} else {
						$scope.error = response;
						$scope.isInValidCredentials = true;
					}
				});
			};

			$scope.signup = function () {
				$location.path('/signup');
			};
		}
	]);
})();