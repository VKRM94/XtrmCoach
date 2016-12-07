(function () {
	'use strict';
	angular.module('app')
	.service('playerService', ['$http', function ($http) {
		var playerService = this;
		var userId = 0;
		var config = {
			headers: {
				'Content-Type': 'application/json'
			}
		};

		this.getPlayers = function (userId, callback) {
			userId = userId;
			$http.get('http://localhost:65335/api/Player/' + userId)
			.success(function (data, status) {
				if (data != null) {
					for (var i = 0; i < data.length; i++) {
						data[i].isEdit = false;

						if (data[i].dob != null && data[i].dob != '') {
							data[i].dob = data[i].dob.split('T')[0]
						}
					}
				}

				callback(data, status);
			})
			.error(function (data, status) {
				callback(data, status);
			});
		};

		this.addNewPlayer = function (player, callback) {
			$http.post('http://localhost:65335/api/Player/', player, config)
			.success(function (response, status) {
				callback(status);
			})
			.error(function (response, status) {
				callback(status);
			});
		};

		this.updatePlayer = function (player, callback) {
			$http.put('http://localhost:65335/api/Player/', player, config)
			.success(function (response, status) {
				callback(status);
			})
			.error(function (status) {
				callback(status);
			});
		};

		this.deletePlayer = function (playerId, callback) {
			$http.delete('http://localhost:65335/api/Player/' + playerId, config)
			.success(function (response, status) {
				callback(status);
			})
			.error(function (status) {
				callback(status);
			});
		};
	}]);
})();