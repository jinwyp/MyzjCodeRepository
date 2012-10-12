define(function(require, exports, module) {

	exports.initalize = function() {

		var returnurl = window.context.urlparams[0] || "";

		var $ = require("jquery");
		var jQuery = $;
		var basejs = require("basejs");
		var Backbone = require("backbone");
		var Handlebars = require("handlebars");
		var _ = require("underscore");

		var app = {
			view : {},
			model : {}
		};

		app.model = require("./model/login").Model;

		var m = new app.model();

		m.on("change:email", function() {
			console.log("您修改了模型");
		});
		m.set({
			email : "abc"
		});
		console.dir(m, "abc");

		app.view = Backbone.View.extend({
			initialize : function() {
				this.render();
				basejs.RefreshPage();
			},
			render : function() {
				var template = Handlebars.compile(require("/templates/login.tpl"));
				basejs.PageChange(this.el, template);
				Backbone.Validation.bind(this, {
					valid : function(view, attr, selector) {
						alert(1);
					},
					invalid : function(view, attr, error, selector) {
						alert(-1);
					}
				});
				Backbone.ModelBinder.bind(this, $('#frmLogin'));
				return this;
			},
			close : function() {
				this.remove();
				this.unbind();
				Backbone.ModelBinder.unbind(this);
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
						var wUr = GetUrlParam("returnurl") || window.WebRoot + "Member/myaccount.aspx";
						//if (wUr === "undefined") wUr = window.WebRoot + "Member/myaccount.aspx";
						LS.clear();
						window.location.href = $("a[data-rel='back']").attr("href") || wUr;
						//Change_Url(wUr);
					} else
						jQuery("#login_ErroMesg").css("display", "block").text(json.msg);
				}, true);
			}
		});

		var uView = new app.view({
			el : $("#Index_Content"),
			model : app.model,
		});

	};

});
