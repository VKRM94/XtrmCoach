(function () {
	'use strict';
	angular
		.module('app')
		.controller('homeController', ['$scope', '$location', '$rootScope', '$cookieStore', 'loginService', function ($scope, $location, $rootScope, $cookieStore, loginService) {
			$scope.user = {
				username: '',
				password: ''
			};

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
					}
				});
			};

			$scope.signup = function () {
				$location.path('/signup');
			};
		}
	]);
})();