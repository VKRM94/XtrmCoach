(function () {
	'use strict';
	angular
		.module('app')
		.directive('fileModel', fileModel);

	function fileModel($parse) {
		return {
			restrict: 'A',
			link: function (scope, element, attrs) {
				var model = $parse(attrs.fileModel);
				var modelSetter = model.assign;

				var reader = new FileReader();
				reader.onload = function (event) {
					scope.currentPlayerImageSource = event.target.result;
					scope.$apply();
				}

				element.bind('change', function () {
					scope.$apply(function () {
						modelSetter(scope, element[0].files[0]);

						if (element[0].files.length != 0) {
							reader.readAsDataURL(element[0].files[0]);
						}
					});
				});
			}
		};
	}
})();