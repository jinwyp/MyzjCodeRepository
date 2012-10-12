define(function(require, exports, module) {

	var Backbone = require("backbone");

	var Model = Backbone.Model.extend({
		defaults : {
			email : '',
			password : ''
		},
		initialize : function() {

		},
		validation : {
			email : [{
				require : true,
				msg : '请输入Email地址'
			}, {
				rpattern : 'email',
				msg : '请输入正确的email地址'
			}],
			password : [{
				require : true,
				msg : '请输入密码'
			}, {
				rangeLength : [6, 20],
				msg : '密码长度在 6-20 之间'
			}]
		}
	});

	exports.Model = Model;

});
