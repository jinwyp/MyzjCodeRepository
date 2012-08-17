

require.config({
    baseUrl: "/js/",
    //paths 文件别名配置
    paths: {},
    //依赖加载 'jquerymobile': ['jquery','live'] 加载 jquerymobile 模块需要首先加载 'jquery','live'
    shim: {
        'jquery.mobile-1.1.0.min': ['jquery-1.7.2.min'],
        'jquery-jtemplates': ['jquery-1.7.2.min'],
        'jquery.cookie': ['jquery-1.7.2.min'],
        'mobile.common': ['jquery.cookie', 'json2', 'md5']
    }
});

require([
        'jquery-1.7.2.min',
        'jquery.mobile-1.1.0.min',
        'jquery-jtemplates',
        'jquery.cookie',
        'json2',
        'md5',
        'mobile.common',
        'live'], function () {

        /*
            $('.gotop').tap(function () {
                $.mobile.silentScroll(10);
            });

            if (typeof (PageInit) == "function") {
                PageInit();
            } else {
                alert("请指定 PageInit 函数！");
            }
        */
            
        });

