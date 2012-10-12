define(function(require, exports, module) {

	exports.initalize = function() {

		var returnurl = window.context.urlparams[0] || "";

		var $ = require("jquery");
		var jQuery = $;
		var basejs = require("basejs");
		var Backbone = require("backbone");
		var Handlebars = require("handlebars");
		var _ = require("underscore");
		var BackboneValidation=require("backbonevalidation");

		var contentView = Backbone.View.extend({
			initialize : function() {
				
				this.render();
				basejs.RefreshPage();
				var object = {};
				_.extend(object, Backbone.Events);
				object.bind("alert", function(eventName) {
					console.log(1);
				});
			},
			render : function() {
				var template = Handlebars.compile(require("/templates/login.tpl"))
				var loginModel = require("./model/login");
				
				basejs.PageChange(this.el, template);

				Backbone.Validation.bind(this, {
					valid : function(view, attr, selector) {

						console.log(loginModel);
						return;
						loginModel.Model.set({
							email : $("#login_email").val(),
							password : $("#login_password").val()
						});

						PostWcf({
							_api : "Member.Login",
							_data : loginModel.Model.toJSON()
						}, function(json) {
							if (json.status == 1 && typeof (json.data) == "string" && json.data.length > 0) {
								UpdateLoginCookie(json.info, email, json.data);
								var wUr = GetUrlParam("returnurl") || window.WebRoot + "Member/myaccount.aspx";
								//if (wUr === "undefined") wUr = window.WebRoot + "Member/myaccount.aspx";
								LS.clear();
								window.location.href = $("a[data-rel='back']").attr("href") || wUr;
								//Change_Url(wUr);
							} else
								jQuery("#login_ErroMesg").css("display", "block").text(json.msg);
						}, true);

					},
					invalid : function(view, attr, error, selector) {
						alert(-1);
					}
				});
				
				return this;
			},
			events : {
				//"click #btnLoginSubmit" : "submigLogin"
			},
			submigLogin : function() {

			}
		});

		var uView = new contentView({
			el : $("#Index_Content")
		});

	};

});
