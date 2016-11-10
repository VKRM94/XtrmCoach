(function () {
	'use strict';
	angular
		.module('app')
		.controller('playerController', playerController);

	function playerController($scope, $location, $rootScope, $cookieStore) {
		$rootScope.bodyLayout = 'dashboard-body';
		$rootScope.masterHeaderTitle = 'Players';

		
	}
})();