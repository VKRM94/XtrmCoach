(function () {
	'use strict';
	angular
		.module('app')
		.controller('playerController', playerController);

	function playerController($scope, $location, $rootScope, $cookieStore, $state, sportService, playerService, imageUploadService) {
		$rootScope.bodyLayout = 'dashboard-body';
		$rootScope.masterHeaderTitle = 'Players';
		$scope.currentPlayerImageSource = '';

		$scope.sports = [];
		$scope.players = [];

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

		sportService.getSports(function (sports, status) {
			$scope.sports = sports;
		});

		var getPlayers = function () {
			playerService.getPlayers($rootScope.user.id, function (players, status) {
				$scope.players = players;
				$rootScope.showDashboardLoader = false;
			});
		};
		getPlayers();

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
	}
})();