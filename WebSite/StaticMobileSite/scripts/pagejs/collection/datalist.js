define(function(require, exports, module) {

	var collection = Backbone.Collection.extend({
		//url : function() {
		//	//return 'http://api.muyingzhijia.me/cms.svc/get_columndata_list/WebSite/token/guid/654/uid/B-A1-A1/1/5';
		//},
		//localStorage: new Backbone.LocalStorage("columnA1Collection"),
		//model : columnModel,
		parse : function(resp, xhr) {
			if (_.isObject(resp)) {
				if (_.isNumber(resp.status) && resp.status == 1) {
					return resp.list || [];
				}
			}
			return resp;
		}
	});
	return collection;

});
