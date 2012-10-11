define(function(require, exports, module) {

	exports.initalize = function() {

		var returnurl = window.context.urlparams[0] || "";

		var $ = require("jquery");
		var jQuery = $;
		var basejs = require("basejs");
		var Backbone = require("backbone");
		var _ = require("underscore");

		var viewContent = Backbone.View.extend({
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
				basejs.PageChange(this.el, require("/templates/login.tpl"), {});
				return this;
			},
			events : {
				"" : "submigLogin"
			},
			submigLogin : function() {
				Unbind_bind("#login_email", "focus", function() {
					jQuery("#login_ErroMesg").css("display", "none").text("");
				});
				$("#frmLogin").validate({
					rules : {
						login_email : {
							required : true,
							email : true
						},
						login_password : {
							required : true,
							minlength : 6,
							maxlength : 20
						}
					},

					messages : {
						login_email : {
							required : "请输入Email地址",
							email : "请输入正确的email地址"
						},
						login_password : {
							required : "请输入密码",
							minlength : jQuery.validator.format("密码不能小于{0}个字符"),
							maxlength : jQuery.validator.format("密码不能最多超过{0}的字符")
						}
					},
					submitHandler : function(form) {
						var email = $("#login_email").val();
						var password = $("#login_password").val();

						var jobj = {
							uid : email,
							pwd : password
						};

						PostWcf({
							_api : "Member.Login",
							_data : JSON.stringify(jobj)
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
			}
		});

		var uView = new viewContent({
			el : $("#Index_Content")
		});

	};

}); 