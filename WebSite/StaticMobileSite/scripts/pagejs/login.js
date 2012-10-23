define(function(require, exports, module) {

	exports.initalize = function() {

		var $ = require("jquery");
		var jQuery = $;
		var basejs = require("basejs");
		var Backbone = require("backbone");
		var Handlebars = require("handlebars");
		var _ = require("underscore");
		
		context.model = require("./model/login");

		context.view = require("./view/login");

		uView = new context.view({
			el : $("#Index_Content"),
			model : new context.model()
		});

	};

});
