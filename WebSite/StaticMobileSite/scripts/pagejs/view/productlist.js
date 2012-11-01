define(function (require, exports, module) {

    require("jquery.templates")($);
    var plistSeachModel = require("../model/productlistseach");

    var view = Backbone.View.extend({
        model: new plistSeachModel(),
        initialize: function () {
            this.render();

            var list = require("../collection/datalist");
            var urlArgument = window.context.urlparams || [];

            //#region 绑定商品列表
            (function () {

                var productList = new list();

                var loging = false;
                this.model.on("change:page , change:sort , change:cid", function (newModel) {

                    if (loging) return;

                    var getSeachParam = function () {
                        var seachArray = [];
                        _.each(newModel.attributes, function (val, key) {
                            seachArray.push(val);
                        });
                        return seachArray.join('/');
                    };

                    console.log(getSeachParam());

                    loging = true;
                    productList.fetch({
                        useCache: false,
                        url: 'http://Goods.goodList->/' + getSeachParam(),
                        success: function (collection, result) {

                            var data = { list: [] };

                            collection.forEach(function (model, i) {
                                data.list.push({
                                    gid: model.get("gid"),
                                    title: model.get("title"),
                                    price: model.get("price"),
                                    pic_url: (model.get("pic_url") || "").replace("{0}", "normal")
                                });
                            });

                            $('#productlistContent').setTemplate($('#productItemTemplate').html());

                            $('#productlistContent').processTemplate(data, null, { append: true });
                            $("#productlistContent").listview("refresh");

                            loging = false;
                        }

                    });

                });

            }).apply(this);
            //#endregion

            if (urlArgument.length == 8) {
                this.model.set({
                    key: urlArgument[0], bid: urlArgument[1], cid: urlArgument[2], age: urlArgument[3],
                    price: urlArgument[4], sort: urlArgument[5], page: urlArgument[6], size: urlArgument[7]
                });
            }
            else {
                this.model.set({ page: 1 });
            }

        },
        render: function () {
            var template = Handlebars.compile(require("/templates/productlist.tpl"));
            Core.PageChange(this.el, template);
            Core.RefreshPage();
            return this;
        }
    });

    return view;

});