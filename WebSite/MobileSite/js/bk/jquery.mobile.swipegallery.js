//$('#imggallery').swipeleft(function(e){

//    var imglist = $(this).find('ul').children();
//    var imgnum = imglist.length;
//    var currentnum = imglist.children().index(e.target);
//    var showcurrentnum = currentnum + 2;
//    var shownum = showcurrentnum.toString() + "/" + imgnum.toString();

//    console.log(currentnum, imgnum,e.type);

//    $(imglist[currentnum+1]).addClass("imggallerynext"); // 首先把下一张图片显示出来

//    // 然后2张图滑动
//    if(currentnum < imgnum - 1 ){
//        $(imglist[currentnum]).animate({
//                left: '-300px',top:'0px'},
//            500,"swing", function(){
//                // 然后把第一张消失
//                $(imglist[currentnum]).removeClass("imggalleryshow");
//            });

//        $(imglist[currentnum + 1]).animate({
//                left: '0px', top:'0px'},
//            500,"swing",function(){
//                // 然后把第二张图失变成第一张图的样式
//                $(imglist[currentnum + 1]).removeClass('imggallerynext').addClass("imggalleryshow");
//                $('#imggallerynum').html(shownum.toString());
//            });
//    }else{
//        //处理到结尾的滑动滑动效果
//        $(imglist[currentnum]).animate({
//                left: '-50px',top:'0px'},
//            300,"swing", function(){
//            });
//        $(imglist[currentnum]).animate({
//                left: '0px',top:'0px'},
//            300,"swing", function(){
//            });
//    }

//});


//$('#imggallery').swiperight(function(e){

//    var imglist = $(this).find('ul').children();
//    var imgnum = imglist.length;
//    var currentnum = imglist.children().index(e.target);
//    var showcurrentnum = currentnum ;
//    var shownum = showcurrentnum.toString() + "/" + imgnum.toString();

//    console.log(currentnum, imgnum,e.type);

//    $(imglist[currentnum - 1]).addClass("imggallerynext"); // 首先把下一张图片显示出来

//    // 然后2张图滑动
//    if(currentnum > 0  ){
//        $(imglist[currentnum]).animate({
//                left: '300px',top:'0px'},
//            500,"swing", function(){
//                // 然后把第一张消失
//                $(imglist[currentnum]).removeClass("imggalleryshow");
//            });

//        $(imglist[currentnum - 1]).animate({
//                left: '0px', top:'0px'},
//            500,"swing",function(){
//                // 然后把第二张图失变成第一张图的样式
//                $(imglist[currentnum - 1]).removeClass('imggallerynext').addClass("imggalleryshow");
//                $('#imggallerynum').html(shownum.toString());
//            });
//    }else{
//        $(imglist[currentnum]).animate({
//                left: '50px',top:'0px'},
//            300,"swing", function(){
//            });
//        $(imglist[currentnum]).animate({
//                left: '0px',top:'0px'},
//            300,"swing", function(){

//            });

//    }

//    });
/*
//$(piclist).filter(":visible").fadeOut(500).animate({left:'0px',top:'0px'},500,"swing");
//$(piclist).eq(i).fadeIn(1000).animate({ left: '300px', top: '0px' }, 500, "swing");
*/

var ImgSwipegall = function () {
    var n = 0;
    var num = $(".slides li").size();
    if (num > 1) {
        $("#imggallerynum").text((n + 1) + "/" + num);
        $("#imgNex_Pre").css("display", "block");

        var item_width = $(".slides li").outerWidth();
        var left_value = item_width * (-1);

        $(".slides li:first").before($(".slides li:last"));
        if (num == 2) {
            left_value = 0;
        }
        $(".slides ul").css({ 'left': left_value });
        
        $("#imggalleryprev").click(function () { prev_Fun(); });
        $("#imggallerynext").click(function () { next_Fun(); });
        $('#imggallery').swipeleft(function () {
            next_Fun();
        });
        $('#imggallery').swiperight(function () {
            prev_Fun();
        });
    } else {
        $("#imgNex_Pre").css("display", "none");
    }
    //上一张图片 
    function prev_Fun() {
        n = n == 0 ? (num - 1) : (n - 1);
        $("#imggallerynum").text((n + 1) + "/" + num);
        if (num == 2) {
            left_value = -300;
            $(".slides ul").css({ 'left': left_value });
        }
        //alert(parseInt($(".slides ul").css('left')));
        var left_indent = parseInt($(".slides ul").css('left')) + item_width;
        $(".slides ul:not(:animated)").animate({ 'left': left_indent }, 300, function () {
            $(".slides li:first").before($(".slides li:last"));

            $('.slides ul').css({ 'left': left_value });
        });
        return false;

    }

    //下一张图片 
    function next_Fun() {
        n = n >= (num - 1) ? 0 : n + 1;
        $("#imggallerynum").text((n + 1) + "/" + num);
        if (num == 2) {
            left_value = 0;
            $(".slides ul").css({ 'left': left_value });
        }
        //alert(parseInt($(".slides ul").css('left')));
        var left_indent = parseInt($(".slides ul").css('left')) - item_width;
        $(".slides ul:not(:animated)").animate({ 'left': left_indent }, 300, function () {
            $(".slides li:last").after($(".slides li:first"));
            //alert($(".slides li:last").index());
            $('.slides ul').css({ 'left': left_value });
        });
        return false;

    }

}