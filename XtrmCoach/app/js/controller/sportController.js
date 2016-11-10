(function () {
	'use strict';
	angular
		.module('app')
		.controller('sportController', sportController);

	function sportController($scope, $location, $rootScope, $cookieStore) {
		$rootScope.bodyLayout = 'dashboard-body';
		$rootScope.masterHeaderTitle = 'Sports';

		
	}
})();