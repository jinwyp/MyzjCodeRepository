define(function (require, exports, module) {

    exports.initialize = function () {

        var productlistView = require("../view/productlist");

        var mainView = new productlistView({
            el: $("#Index_Content")
        });

    };


});