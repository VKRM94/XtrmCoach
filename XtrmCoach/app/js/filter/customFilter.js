(function () {
	'use strict';
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

	angular
		.module('app')
		.filter('showAssociatedPlayerForEval', function () {
			return function (items, sportId) {
				if (sportId != 0) {
					var filtered = [];
					angular.forEach(items, function (item) {
						for (var i = 0; i < item.sports.length; i++) {
							if (item.sports[i].id == sportId) {
								filtered.push(item);
							}
						}
					});
					return filtered;
				}
			};
		});
})();