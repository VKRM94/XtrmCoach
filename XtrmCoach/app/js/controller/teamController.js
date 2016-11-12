(function () {
	'use strict';
	angular
		.module('app')
		.controller('teamController', teamController);

	function teamController($scope, $location, $rootScope, $cookieStore) {
		$rootScope.bodyLayout = 'dashboard-body';
		$rootScope.masterHeaderTitle = 'Teams';

		
	}
})();