
$(function () {

    //PageInit();

});

/*
http://mobile.51cto.com/others-288631.htm
加载页面局部
$.mobile.loadPage("about/us.html");
如果局部有绑定事件，则 调用 容器的 create 事件
$("#div").appendTo(".ui-page").trigger( "create" );
页面跳转：      
$.mobile.changePage("xx.html",{transition:"slideup"});
$.mobile.changePage("xx.html", {
type: "post", 
data: $("form#search").serialize()
});

*/

//$(document).on("pagecreate", function () {

//    PageInit();

//});

$(document).ready(function () {
    PageInit()
});


function PageInit() {
    $("#btn_alerttest").click(function () {
        $.mobile.showPageLoadingMsg("e", "哈哈哈哈哈俣人", false);

//        $.ajax({
//            url: "/api/WebUtility.ashx",
//            type: "get",
//            dataType: "html",
//            success: function (html) {
//                $.mobile.pageLoading(true);
//            }
//        });
    });
};