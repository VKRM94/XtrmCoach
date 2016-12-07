(function () {
	'use strict';
	angular
		.module('app')
		.controller('homeController', ['$scope', '$location', '$rootScope', '$cookieStore', '$state', 'loginService', function ($scope, $location, $rootScope, $cookieStore, $state, loginService) {
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
				$rootScope.showLoader = true;
				var username = $scope.user.username;
				var password = $scope.user.password;

				loginService.authenticateCredentials(username, password, function (response, statusCode) {
					if (statusCode == 200) {
						$rootScope.user = response;
						$cookieStore.put('user', $rootScope.user);
						$rootScope.isLoggedIn = true;
						$cookieStore.put('isLoggedIn', $rootScope.isLoggedIn);

						$state.go('dashboard');
					} else {
						$scope.error = response;
						$scope.isInValidCredentials = true;
					}

					$rootScope.showLoader = false;
				});
			};

			$scope.signup = function () {
				$state.go('signup');
			};
		}
	]);
})();