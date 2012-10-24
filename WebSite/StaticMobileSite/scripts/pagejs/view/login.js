define(function(require, exports, module) {

	var basejs = require("basejs");
	var Backbone = require("backbone");

	var view = Backbone.View.extend({
		initialize : function() {
			this.render();
			basejs.RefreshPage();
		},
		render : function() {
			var template = Handlebars.compile(require("/templates/login.tpl"));
			basejs.PageChange(this.el, template);

			Backbone.Validation.bind(this, {
				valid : function(view, attr, selector) {
					view.$el.find("#" + attr + "Tip").empty().show();
				},
				invalid : function(view, attr, error, selector) {
					view.$el.find("#" + attr + "Tip").text(error).show();
				}
			});

			var modelBinder = new Backbone.ModelBinder();
			modelBinder.bind(this.model, this.el);
			return this;
		},
		close : function() {
			this.remove();
			this.unbind();
			Backbone.ModelBinder.unbind();
		},
		events : {
			"click #btnLoginSubmit" : "submigLogin"
		},
		submigLogin : function(e) {
			e.preventDefault();
			console.log(this.model.isValid(true));
			return;
			PostWcf({
				_api : "Member.Login",
				_data : loginModel.toJSON()
			}, function(json) {
				if (json.status == 1 && typeof (json.data) == "string" && json.data.length > 0) {
					UpdateLoginCookie(json.info, email, json.data);
					var returnurl = window.context.urlparams[0] || "";
					LS.clear();
					window.location.hash = returnurl;
				} else
					jQuery("#login_ErroMesg").css("display", "block").text(json.msg);
			}, true);
		}
	});

	return view;

});
