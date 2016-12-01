(function () {
	'use strict';
	angular.module('app')
	.service('imageUploadService', ['$http', function ($http) {
		this.upload = function (file, fileName, callback) {
			var formData = new FormData();
			formData.append('image', file, fileName);
			//formData.append('fileName', fileName);

			$http.post('http://localhost:65335/api/PlayerImage/', formData, {
				transformRequest: angular.identity,
				headers: { 'Content-Type': undefined }
			})
			.success(function (response, status) {
				callback(status);
			})
			.error(function (response, status) {
				callback(status);
			});
		};

		this.get = function (fileName, callback) {
			$http.get('http://localhost:65335/api/PlayerImage?fileName=' + fileName)
			.success(function (response, status) {
				callback(response, status);
			})
			.error(function (response, status) {
				callback(response, status);
			});
		}
	}]);
})();