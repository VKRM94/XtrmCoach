(function () {
	'use strict';
	angular
		.module('app')
		.controller('evaluateController', evaluateController);

	function evaluateController($scope, $location, $rootScope, $cookieStore, $state, sportService, playerService, imageUploadService) {
		$rootScope.bodyLayout = 'dashboard-body';
		$rootScope.masterHeaderTitle = 'Evaluate Player';

		// TODO: Uncomment this later
		//$scope.sports = [];
		//$scope.players = [];

		//sportService.getSports(function (sports, status) {
		//	if (status == 200) {
		//		$scope.sports = sports;

		//		playerService.getPlayers($rootScope.user.id, function (players, status) {
		//			$scope.players = players;
		//			$rootScope.showDashboardLoader = false;
		//		});
		//	}
		//});

		// Temp
		$scope.sports = [{
			id: 1,
			name: 'Sport 1',
			userId: 1
		}, {
			id: 2,
			name: 'Sport 2',
			userId: 1
		}];

		$rootScope.showDashboardLoader = false;

		$scope.players = [{
			id: 1,
			firstName: 'Atul',
			lastName: 'Banwar',
			imageId: 'A11428632.jpg',
			sports: [{
				id: 1,
				name: 'sport 1',
				userId: 1
			}, {
				id: 2,
				name: 'sport 2',
				userId: 1
			}]
		}, {
			id: 2,
			firstName: 'Tushar',
			lastName: 'Bhat',
			imageId: 'A22314817.jpg',
			sports: [{
				id: 1,
				name: 'sport 1',
				userId: 1
			}]
		}];

		$scope.selectedSportId = 0;
		$scope.selectedPlayerId = 0;

		$scope.updateSelectedSport = function (sportId) {
			$scope.selectedSportId = sportId;
			$scope.selectedPlayerId = 0;
		};

		$scope.updateSelectedPlayer = function (playerId) {
			$scope.selectedPlayerId = playerId;
		};


		/*
		var getPlayerModel = function () {
			return {
				id: 0,
				firstName: '',
				lastName: '',
				dob: '',
				imageId: '',
				image: {},
				sports: []
			};
		};

		$scope.currentPlayer = getPlayerModel();

		$scope.dropDownSettings = {
			scrollableHeight: '300px',
			scrollable: true,
			displayProp: 'name',
			idProp: 'id',
			buttonDefaultText: 'Select Sports',
			externalIdProp: '',
			smartButtonMaxItems: 3,
			smartButtonTextConverter: function (itemText, originalItem) {
				return itemText;
			}
		};

		$scope.isPlayerAddOrEditInProgress = false;

		

		$scope.addNewPlayer = function () {
			$scope.currentPlayer = getPlayerModel();

			$scope.isPlayerAddOrEditInProgress = true;
			$scope.currentPlayerImageSource = '';
		};

		$scope.addOrUpdatePlayer = function () {
			if ($scope.currentPlayer.id == 0) {
				// add
				var imageId = $scope.currentPlayer.firstName + $scope.currentPlayer.lastName + Math.floor((Math.random() * 1000000) + 1) + $scope.currentPlayer.image.name.substring($scope.currentPlayer.image.name.lastIndexOf("."));
				$scope.currentPlayer.imageId = imageId;

				playerService.addNewPlayer($scope.currentPlayer, function (status) {
					if (status == 200) {
						imageUploadService.upload($scope.currentPlayer.image, imageId, function (status) {
							if (status == 200) {
								getPlayers();
							} else {
								// Error uploading player's image
							}

							$scope.currentPlayer = getPlayerModel();
						});
					} else {
						// Error Adding Player
						$scope.currentPlayer = getPlayerModel();
					}
				});
			} else {
				// update
				playerService.updatePlayer($scope.currentPlayer, function (status) {
					if (status == 200) {
						if ($scope.currentPlayer.image != '') {
							//var imageId = $scope.currentPlayer.firstName + $scope.currentPlayer.lastName + Math.floor((Math.random() * 1000000) + 1) + $scope.currentPlayer.image.name.substring($scope.currentPlayer.image.name.lastIndexOf("."));
							//$scope.currentPlayer.imageId = imageId;

							imageUploadService.upload($scope.currentPlayer.image, $scope.currentPlayer.imageId, function (status) {
								if (status == 200) {
									// UPLOAD DONE, FETCH NEW IMAGE
								} else {
									// Error uploading player's image
								}

								$scope.currentPlayer = getPlayerModel();
								window.location.reload();
							});
						} else {
							$scope.currentPlayer = getPlayerModel();
						}

						getPlayers();
					} else {
						// Error Adding Player
						$scope.currentPlayer = getPlayerModel();
					}
				});
			}

			$scope.isPlayerAddOrEditInProgress = false;
			$scope.currentPlayerImageSource = '';
		};

		$scope.cancelAddOrUpdatePlayer = function () {
			$scope.isPlayerAddOrEditInProgress = false;
			$scope.currentPlayer = getPlayerModel();
			$scope.currentPlayerImageSource = '';
		};

		$scope.editPlayer = function (player) {
			player.image = '';
			$scope.currentPlayer = angular.copy(player);
			$scope.currentPlayerImageSource = 'http://localhost:65335/api/PlayerImage?fileName=' + $scope.currentPlayer.imageId;
			$scope.isPlayerAddOrEditInProgress = true;
		};

		$scope.deletePlayer = function (player) {
			playerService.deletePlayer(player.id, function (status) {
				if (status == 200) {
					getPlayers();
				}
			});
		};

		*/
	}

	/*
	angular
		.module('app')
		.filter('showAssociatedPlayer', function () {
			return function (items, sport) {
				var filtered = [];
				angular.forEach(items, function (item) {
					for (var i = 0; i < item.sports.length; i++) {
						if (item.sports[i].id == sport.id) {
							filtered.push(item);
							sport.hasPlayer = true;
						}
					}
				});
				return filtered;
			};
		});
		*/
})();