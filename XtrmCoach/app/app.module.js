(function () {
	'use strict';
	var app = angular.module('app', ['ngRoute', 'ngCookies']);

	app.config(function ($routeProvider, $httpProvider) {
		$routeProvider
		.when('/', {
			controller: 'homeController',
			templateUrl: 'app/views/home.html'
		})
		.when('/dashboard', {
			resolve: {
				"check": function ($location, $rootScope) {
					if (!$rootScope.isLoggedIn) {
						$location.path('/');
					}
				}
			},
			controller: 'dashboardController',
			templateUrl: 'app/views/dashboard.html'
		})
		.when('/signup', {
			controller: 'signupController',
			templateUrl: 'app/views/signup.html'
		})
		.otherwise({
			redirectTo: 'app/views/home.html'
		});

		$httpProvider.defaults.headers.common = {};
		$httpProvider.defaults.headers.post = {};
		$httpProvider.defaults.headers.put = {};
		$httpProvider.defaults.headers.patch = {};
	});

	app.run(['$rootScope', '$cookieStore',
	function ($rootScope, $cookieStore) {
		$rootScope.isLoggedIn = $cookieStore.get('isLoggedIn') || false;
		$rootScope.user = $cookieStore.get('user') || {};
	}]);

})();