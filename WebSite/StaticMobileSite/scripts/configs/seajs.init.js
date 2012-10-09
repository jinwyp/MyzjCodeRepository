

/// <reference path="../seajs/sea.js" />

define(function () {

    seajs.config({
        base: '/scripts/',
        charset: 'utf-8',
        debug: true,
        alias: {
            'jquery': 'seajs/modules/jquery/1.7.2/jquery',
            'cookie':'jquery-plugin/jquery.cookie',
            'webconfig':'configs/web.config',
            'pagejs':'pagejs/mobile.page',
            'basejs':'pagejs/mobile.common'
        },
        preload: ['seajs/plugin-json', 'seajs/plugin-text']
    });

    seajs.use("configs/seajs.web.init");

});