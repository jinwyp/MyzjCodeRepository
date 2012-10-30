define(function(require, exports, module) {

	exports.initalize = function() {

		var indexView = require("../view/index");

		var mainView = new indexView({
			el : $("#Index_Content")
		});

	};

});
