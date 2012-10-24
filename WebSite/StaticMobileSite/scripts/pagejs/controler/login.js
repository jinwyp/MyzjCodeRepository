define(function(require, exports, module) {

	exports.initalize = function() {

		context.model = require("../model/login");

		context.view = require("../view/login");

		uView = new context.view({
			el : $("#Index_Content"),
			model : new context.model()
		});

	};

});
