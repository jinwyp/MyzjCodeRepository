

/// <reference path="../seajs/sea.js" />

define(function () {

    seajs.config({
        base: '/scripts/',
        charset: 'utf-8',
        debug: true,
        alias: {
            'jquery': 'jquery/jquery-debug',
            'jqm':'jquerymobile/jquery.mobile-1.1.0.min',
            'webconfig':'configs/web.config',
            'pagejs':'pagejs/mobile.page',
            'basejs':'pagejs/mobile.common',
            'cookie':'jquery-plugin/jquery.cookie',
            'jquery.validate':'jquery-plugin/jquery.validate.min',
            'jquery.templates':'jquery-plugin/jquery-jtemplates_uncompressed',
            'md5':'plugin/md5',
            'json':'plugin/json2'
        },
        preload: ['seajs/plugin-json', 'seajs/plugin-text']
    });

    seajs.use("configs/seajs.web.init");

});