//#region
var intervalID_mesage = 0;
function setMesage(mesag, product_id) {
    hideMesage();
    $("#error_" + product_id).text(mesag);
    intervalID_mesage = setTimeout(function () { intervalID_mesage = 0; $("#error_" + product_id).text(""); }, 3000);
}
function hideMesage() {
    if (intervalID_mesage != 0) {
        clearTimeout(intervalID_mesage);
        intervalID_mesage = 0;
    }
}
//#endregion

//#region 购物车

function shoppingcart_Fun() {
    //jQuery.mobile.changePage(window.WebRoot + "CheckOut/shoppingcart.aspx");
    //alert($("#Good_Total_Count").text());
    //alert(goodsTotal);
    var goodsTotal = $("#Hid_Good_Total_price").val() || 0;
    $("#goodsTotal").html(parseFloat(goodsTotal).toFixed(2) + "元");

    //#region 绑定购物车列表
    var BindDate_Shopcart = function () {
        var shoppingCart_list = $('#shoppingCart_list');
        GetWcf({
            _api: "Order.get_shoppingcartgoods_list"
        }, function (jsonString) {
            if (jsonString.status == 1 && typeof (jsonString.info) == "object" && jsonString.info.shoppingcart_list != null && jsonString.info.shoppingcart_list.length > 0) {
                for (var i = 0; i < jsonString.info.shoppingcart_list.length; i++) {
                    jsonString.info.shoppingcart_list[i].vchPicURL = jsonString.info.shoppingcart_list[i].vchPicURL.toString().replace("{0}", "normal");
                }
                $("#good_totals").html($("#Good_Total_Count").text());
                var goodsTotal = $("#Hid_Good_Total_price").val() || 0;

                $("#goodsTotal").html(parseFloat(goodsTotal).toFixed(2) + "元");

                shoppingCart_list.setTemplate($('#shoppingCart_list_template').html());
                shoppingCart_list.processTemplate(jsonString.info.shoppingcart_list, null, { append: false });
                shoppingCart_list.trigger('create').listview("refresh");
            } else {
                $("#good_totals").html("0");
                $("#goodsTotal").html("0.00元");
                shoppingCart_list.setTemplate("<li style='text-align:center;'>暂无商品信息！</li>");
                shoppingCart_list.processTemplate(jsonString.info.shoppingcart_list, null, { append: false });
                shoppingCart_list.listview("refresh");
            }
        }, true, {
            "ref_loading": shoppingCart_list, "ref_loading_text": '<li style="text-align:center; background:url(../images/loading.gif) no-repeat center center; height:80px;"></li>'
        });
    };
    BindDate_Shopcart();
    //#endregion

    //#region 设置购物车数商品量
    var Set_shoppingcartgoodsnum = function (shoppingcarid, gid, num, onum, obj) {
        GetWcf({
            _api: "Order.set_shoppingcartgoodsnum",
            _url: "/" + shoppingcarid + "/" + gid + "/" + num
        }, function (jsonString) {
            if (jsonString.status == 1) {
                $("#error_" + product_id).text("");
                Get_shoppingcartgoodsnum_Fun();
                $("#good_totals").html($("#Good_Total_Count").text());
                var goodsTotal = $("#Hid_Good_Total_price").val() || 0;
                $("#goodsTotal").html(parseFloat(goodsTotal).toFixed(2) + "元");
                if (onum != num) {
                    $(obj).attr("onum", num);
                }

            } else {
                setMesage(jsonString.msg, product_id);
                //$("#error_" + product_id).text(jsonString.msg);
                //alert(onum);
                $(obj).val(onum);
                obj.val(onum).slider("refresh");
                //$("#error_" + product_id).text("");
            }
        }, true);
    }
    //#endregion

    //#region 改变值调用方式
    var product_id = 0; //商品ID
    var p_num = 0; //当前的数量
    var onum = 0; //原来的数量
    var ShopCartID = 0; //购物车ID
    var Change_Project_object = function (obj, product_id, p_num, ShopCartID) {
        //alert(onum+":"+p_num);
        if (onum != p_num) {
            //$(obj).attr("onum", p_num);
            Set_shoppingcartgoodsnum(ShopCartID, product_id, p_num, onum, obj);
        }
    }
    //#endregion

    //#region 改变数量框里的值
    var changeNum = function () {
        $("#shoppingCart_list div[role=application] a").live("touchend mouseup", function () {
            var Find_Number_object = $(this).parent().parent().find("input[type=number]");
            ShopCartID = Find_Number_object.attr("ShopCartID")
            product_id = Find_Number_object.attr("product_id");
            onum = Find_Number_object.attr("onum") || 0;
            p_num = Find_Number_object.val();
            //Log("slider商品ID：" + product_id + "现数量:" + p_num + "原数量:" + onum);
            Change_Project_object(Find_Number_object, product_id, p_num, ShopCartID);
        });

        $("#shoppingCart_list input[type=number]").live("blur", function () {
            ShopCartID = $(this).attr("ShopCartID");
            product_id = $(this).attr("product_id");
            onum = $(this).attr("onum") || 0;
            p_num = $(this).val();

            //Log("input商品ID：" + product_id + "现数量:" + p_num + "原数量:" + onum);
            Change_Project_object($(this), product_id, p_num, ShopCartID);

        });
    } ();
    //#endregion

    //#region 删除购物车
    var del_ShoppingCart = function () {
        $("#shoppingCart_list a.Del_id").live("click", function () {
            var intShopCartID = $(this).attr("delete_id");
            product_id = $(this).attr("product_id");
            GetWcf({
                _api: "Order.del_shoppingcart",
                _url: "/" + intShopCartID
            }, function (jsonString) {
                if (jsonString.status == 1) {
                    Get_shoppingcartgoodsnum_Fun();
                    BindDate_Shopcart();
                } else {
                    $("#error_" + product_id).text(jsonString.msg);
                }
            }, true);
        });
    } ();
    //#endregion

    //#region 提交订单
    $("#ShoppingCart_btn").click(function (e) {
        if (parseInt($("#good_totals").text()) > 0) {
            $('html, body').animate({
                scrollTop: $(document).height()
            },
        1500);

            window.location.href = window.WebRoot + "CheckOut/orderconfirm.aspx";
        } else {
            $("#tis_Tip").append('<li class="error-text">您还没有购买商品！</li>');
        }
    });
    //#endregion
}
//#endregion

//#region 默认收货人信息存储
var De_Con_Object = "";
var Default_Consignee_information_Fun = function (De_object_Info) {
    De_Con_Object = De_object_Info;
    LS.set("county_id", De_object_Info.county_id); //区域ID
    LS.set("current_addressid", De_object_Info.id); //收货人信息ID
}
//#endregion

//#region  支付方式信息存储
var Payment_info_Object = function (payment_text_name, payment_Id) {
    LS.set("payment_text_name", payment_text_name); //付款名称 payment_text_name  如在线支付
    LS.set("payment_Id", payment_Id);
}
//#endregion

//#region  删除支付方式信息存储
var Delete_Payment_info_Object = function (payment_text_name, payment_Id) {
    LS.remove(payment_text_name); //付款名称 payment_text_name  如在线支付
    LS.remove(payment_Id);
}
//#endregion

//#region  配送方式信息存储
var Delivery_info_Object = function (delivery_array) {
    for (var i = 0; i < delivery_array.slides.length; i++) {
        //alert("aa:" + delivery_array.slides[i] + delivery_array.values[i]);
        LS.set(delivery_array.slides[i], delivery_array.values[i]);
    }
}
//#endregion

//#region  配送方式信息删除
var Delelt_Delivery_info_Object = function (delivery_array) {
    for (var i = 0; i < delivery_array.slides.length; i++) {
        LS.remove(delivery_array.slides[i]);
    }
}
//#endregion

//#region 计算总金额
var Final_Price = function () {
    var goodsTotal = $("#Hid_Good_Total_price").val() || 0;
    $("#Total_Price").text(parseFloat(goodsTotal).toFixed(2));
    var _Final_price = parseFloat(goodsTotal) + parseFloat($("#Y_Price").text());
    $("#Final_Price").text(parseFloat(_Final_price).toFixed(2) + "元");
}
//#endregion

//#region 订单实体类
var orderEntity = {
    createNew: function () {
        var order = {};
        order.buyer_uid = $.cookie("m_uid"); //买家字符串ID
        order.addressid = LS.get("current_addressid") || "no_c"; //收货地址ID
        order.payid = LS.get("payment_Id") || "no_c"; //支付方式id
        order.logisticsid = LS.get("delivery_Id") || "no_c"; //配送方式id

        var isorInvice = LS.get("isorInvice") || "0"; //发票抬头类型
        var invoice_Type = LS.get("invoice_Type"); //发票分类
        var FaTHeader = LS.get("FaTHeader"); //发票抬头
        var remark = $("#remark").text(); //订单备注
        var posttimetype = LS.get("delivery_sh_time_id"); //配送时间类型
        //alert(typeof (posttimetype));
        //alert(typeof(isorInvice) + ":" + typeof(invoice_Type) + ":" + typeof(FaTHeader) + ":" + typeof(remark));
        //alert(isorInvice + ":" + invoice_Type + ":" + FaTHeader + ":" + remark);


        order.titletype = isorInvice;

        if (invoice_Type != null) {
            order.invoicecategory = invoice_Type;
        }
        if (FaTHeader != null) {
            order.invoicetitle = FaTHeader;
        }
        if (remark != "") {
            order.remark = remark;
        }
        if (posttimetype != "null") {
            order.posttimetype = posttimetype;
        }

        return order;
    },
    init_Judge: function (orderEntity_c) {
        if (orderEntity_c.addressid === "no_c") {
            $("#Y_Price").text("0");
            return false;
        } else if (orderEntity_c.payid === "no_c") {
            $("#Y_Price").text("0");
            return false;
        } else if (orderEntity_c.logisticsid === "no_c") {
            $("#Y_Price").text("0");
            return false;
        } else {
            return true;
        }
    }
}
//#endregion

//#region 计算运费
var calculation_shipping_costs_Fun = function () {
    var orderEntity_c = orderEntity.createNew();
    //alert(orderEntity.init_Judge(orderEntity_c));
    if (orderEntity.init_Judge(orderEntity_c)) {
        PostWcf({
            _api: "Order.get_temporder_info",
            _data: JSON.stringify(orderEntity_c)
        }, function (json) {
            $("#Y_Price").text(parseFloat(json.info.total_freight).toFixed(2));
            $("#Total_Price").text(parseFloat(json.info.total_goods_fee).toFixed(2));
            $("#Final_Price").text(parseFloat(json.info.total_order_fee).toFixed(2) + "元");
            //Final_Price();
        }, true);
    }
}
//#endregion

//#region 去结算页面
function orderconfirm_Fun() {
    //#region  收货人信息显示
    GetWcf({
        _api: "Member.get_defaultaddress_info"
    }, function (jsonString) {
        var address_default = $('#address_default');
        var address_message = $('#address_message');
        if (jsonString.status == 1 && typeof (jsonString.info) == "object" && jsonString.info.contact_name != null) {
            Default_Consignee_information_Fun(jsonString.info);
            address_default.setTemplate($('#address_default_template').html());
            address_default.processTemplate(jsonString.info, null, { append: false });
            address_message.listview('refresh');
        } else {
            address_default.setTemplate("<p>暂无收货信息，请添加！</p>");

            address_default.processTemplate(jsonString.info, null, { append: false });
            address_message.listview('refresh');
        }

        var paymentlist_p = $("#paymentlist_p");
        var delivery_text_name_p = $("#delivery_text_name_p");
        var delivery_sh_time_text_p = $("#delivery_sh_time_text_p");
        var delivery_fk_time_text_p = $("#delivery_fk_time_text_p");
        var regionid = LS.get("county_id");
        var paygroupid = LS.get("payment_Id");

        //#region 选支付方式时判断
        $("#paymentlist_a").click(function () {
            if (regionid === null) {
                alert("请选择收货人信息！");
                return;
            } else {
                window.location.href = window.WebRoot + "CheckOut/paymentlist.aspx";
            }
        });
        //#endregion

        //#region  支付方式显示
        if (LS.get("payment_text_name") == null) {
            paymentlist_p.html("请选择支付方式");
        } else {
            paymentlist_p.html(LS.get("payment_text_name"));
        }
        //#endregion

        //#region 配送时判断
        $("#deliverylist_a").click(function () {
            if (paygroupid === null) {
                alert("请选择支付方式！");
                return;
            } else {
                window.location.href = window.WebRoot + "CheckOut/deliverylist.aspx";
            }
        });
        //#endregion

        //#region  配送方式显示
        if (LS.get("delivery_text_name") == null) {
            delivery_text_name_p.html("请选择配送方式");
        } else {
            $("#delivery_text_name_p").html(LS.get("delivery_text_name"));
            //alert(LS.get("delivery_fk_time_text") +":"+ (LS.get("delivery_fk_time_text") === "null"));
            if (LS.get("delivery_sh_time_text") === "null") {
                delivery_sh_time_text_p.html("");
            } else {
                delivery_sh_time_text_p.html("送货日期：" + LS.get("delivery_sh_time_text"));
            }
            if (LS.get("delivery_fk_time_text") != null) {
                delivery_fk_time_text_p.html("付款方式：" + LS.get("delivery_fk_time_text"));
            } else {
                delivery_fk_time_text_p.html("");
            }
        }
        //#endregion
    }, true);
    //#endregion

    //#region  发票信息显示
    //alert(typeof (LS.get("isorInvice")) + LS.get("isorInvice") + ":" + typeof (LS.get("FaTHeader")) + LS.get("FaTHeader"));
    if (LS.get("isorInvice") == null || LS.get("isorInvice") == "0") {
        $("#invoice_Type_Text").html("不需要发票");
    } else {
        $("#invoice_Type_Text").html("发票类型：" + LS.get("invoice_Type_Text"));
    }
    if (LS.get("FaTHeader") != null) {
        if (LS.get("FaTHeader") != "-1") {
            $("#invoice_Theader_Text").html("发票抬头：" + LS.get("FaTHeader"));
        }
    }
    //#endregion

    Final_Price();

    //#region 提交订单

    $("#orderConfirm_btn").click(function () {
        var orderEntity_c = orderEntity.createNew();
        if (orderEntity.init_Judge(orderEntity_c)) {
            PostWcf({
                _api: "Order.add_order_info",
                _data: JSON.stringify(orderEntity_c)
            }, function (json) {
                if (json.status === 1 && typeof (json.info) == "object" && json.info != null) {
                    LS.clear(); //先清再存
                    var delivery_array = {
                        slides: [
                                "make_oid", //订单号
                                "make_total_order", //应付金额
                                "make_paytype", //支付方式
                                "make_logisticstype", //配送方式
                                "make_posttimetype" //送货时间
                            ],
                        values: [
                                json.info.oid,
                                json.info.total_order,
                                json.info.paytype,
                                json.info.logisticstype,
                                json.info.posttimetype
                            ]
                    };
                    Delivery_info_Object(delivery_array);
                    window.location.href = window.WebRoot + "CheckOut/makeorder.aspx";
                }

            }, true);
        }
        else {
            alert("信息还不完全！");
        }
    });

    //#endregion
}
//#endregion

//#region 收货人列表信息页面
function addresslist_Fun() {

    GetWcf({
        _api: "Member.get_address_list"
    }, function (jsonString) {
        var addresslist_cont = $('#addresslist_cont');
        if (jsonString.status == 1 && typeof (jsonString.list) == "object" && jsonString.list != null && jsonString.list.length > 0) {
            var selected_addressId = LS.get("current_addressid");

            for (var i = 0; i < jsonString.list.length; i++) {
                //alert((jsonString.list[i].id.toString() === selected_addressId.toString()) + ":" + jsonString.list[i].id + ":" + selected_addressId);
                if (jsonString.list[i].id.toString() === selected_addressId.toString()) {
                    jsonString.list[i].get_def = true;
                } else {
                    jsonString.list[i].get_def = false;
                }
            }
            addresslist_cont.setTemplate($('#addresslist_cont_template').html());
            addresslist_cont.processTemplate(jsonString.list, null, { append: false });
            addresslist_cont.trigger('create');
        } else {
            //alert(jsonString.msg);
            addresslist_cont.setTemplate("<p>暂无收货信息！</p>");
            addresslist_cont.processTemplate(jsonString.list, null, { append: false });
            addresslist_cont.trigger('create');
        }

    }, true, {
        "ref_loading": $('#addresslist_cont'), "ref_loading_text": '<div style="text-align:center; background:#ffffff url(../images/loading.gif) no-repeat center center; height:60px;"></div>'
    });

    //#region 点击给radio赋值
    $("[name = radio_choice_add]:radio").live('change', function () {
        $("[name = radio_choice_add]:radio").attr("checked", false).checkboxradio("refresh");
        $(this).attr("checked", true).checkboxradio("refresh");   // 绑定事件及时更新checkbox的checked值
        //alert($("[name = radio_choice_add]:radio:checked").attr("value"));

    });
    //#endregion

    //#region 设置默认收货地址
    $("#addresslist_btn").live("click", function () {
        $("#frmAddress_list").validate({
            errorElement: "span",
            rules: {
                radio_choice_add: {
                    required: true
                }
            },
            messages: {
                radio_choice_add: {
                    required: "请选择收货地址"
                }
            },
            submitHandler: function (form) {
                //alert($("[name = radio_choice_add]:radio:checked").attr("value"));
                var address_id = $("[name = radio_choice_add]:radio:checked").attr("value") || 0;
                //alert(address_id);
                $("#Add_mesage").css("display", "none").text("");
                if (address_id == 0) {
                    $("#Add_mesage").css("display", "block").text("请选择收货地址");
                    return;
                }
                GetWcf({
                    _api: "Member.set_defaultaddress",
                    _url: "/" + address_id
                }, function (jsonString) {
                    if (jsonString.status == 1) {
                        Delete_Payment_info_Object("payment_text_name", "payment_Id");
                        var delivery_array = {
                            slides: [
                                "delivery_text_name", //配送方式名称
                                "delivery_Id", //配送方式ID
                                "delivery_sh_time_text", //送货时间的名称
                                "delivery_sh_time_id", //送货时间的值
                                "delivery_fk_text", //付款类型名称  如现金
                                "delivery_fk_id"//付款类型值
                            ]
                        };
                        Delelt_Delivery_info_Object(delivery_array);
                        LS.set("current_addressid", address_id); //设置默认收货地址
                        window.location.href = window.WebRoot + "CheckOut/orderconfirm.aspx";
                    } else {
                        alert(jsonString.msg);
                    }
                }, true);
            }
        });
    });
    //#endregion

}
//#endregion

//#region 支付方式
function paymentlist_Fun() {
    //#region 绑定支付方式信息
    var regionid = LS.get("county_id");
    if (regionid == null) {
        $('#field_pay_list').setTemplate("<p>暂无支付方式信息！</p>");
        $('#field_pay_list').processTemplate(jsonString.list, null, { append: false });
        $('#field_pay_list').trigger('create');
    } else {
        GetWcf({
            _api: "Order.get_payment_list",
            _url: "/" + regionid
        }, function (jsonString) {
            if (jsonString.status == 1 && typeof (jsonString.list) == "object" && jsonString.list.length > 0) {
                $('#field_pay_list').setTemplate($('#jTemplate').html());
                $('#field_pay_list').processTemplate(jsonString.list, null, { append: false });
                $('#field_pay_list').trigger('create');
                $("[name = radio-pay-1]:radio").attr("checked", false).checkboxradio("refresh");
                $("[name = radio-pay-1]:radio:first").attr("checked", true).checkboxradio("refresh");
            } else {
                $('#field_pay_list').setTemplate("<p>暂无支付方式信息！</p>");
                $('#field_pay_list').processTemplate(jsonString.list, null, { append: false });
                $('#field_pay_list').trigger('create');
            }
        }, true);
    }
    //#endregion

    //#region 点击给radio赋值
    $("[name = radio-pay-1]:radio").live('change', function () {
        $("[name = radio-pay-1]:radio").attr("checked", false).checkboxradio("refresh");
        $(this).attr("checked", true).checkboxradio("refresh");   // 绑定事件及时更新checkbox的checked值
    });
    //#endregion

    //#region 提交支付方式
    $("#paymentlist_sub_btn").live('click', function () {
        Payment_info_Object($("[name = radio-pay-1]:radio:checked").parent().find("label").attr("text"), $("[name = radio-pay-1]:radio:checked").attr("value"));
        var delivery_array = {
            slides: [
                "delivery_text_name", //配送方式名称
                "delivery_Id", //配送方式ID
                "delivery_sh_time_text", //送货时间的名称
                "delivery_sh_time_id", //送货时间的值
                "delivery_fk_text", //付款类型名称  如现金
                "delivery_fk_id"//付款类型值
                    ]
        };
        Delelt_Delivery_info_Object(delivery_array);
        window.location.href = window.WebRoot + "CheckOut/orderconfirm.aspx";
    });
    //#endregion
}
//#endregion

//#region 配送方式
function deliverylist_Fun() {
    //#region  控制送货时间
    var sh_type_radio_checked = function () {
        var sh_type_radio_value = $("[name = radio-type-1]:radio:checked").attr("value");
        if (sh_type_radio_value === "1") {
            $("#sh_time").css("display", "block");
            $("[name = radio-date-1]:radio").attr("checked", false).checkboxradio("refresh");
            $("[name = radio-date-1]:radio:first").attr("checked", true).checkboxradio("refresh");
        } else {
            $("#sh_time").css("display", "none");
            $("[name = radio-date-1]:radio").attr("checked", false).checkboxradio("refresh");
        }
    };
    //#endregion

    //#region  控制货到付款就有付款方式
    var payment_Id = LS.get("payment_Id");
    if (payment_Id == "0") {
        $("#fk_type").css("display", "block");
        $("[name = radio-pay-1]:radio").attr("checked", false).checkboxradio("refresh");
        $("[name = radio-pay-1]:radio:first").attr("checked", true).checkboxradio("refresh");
    } else {
        $("#fk_type").css("display", "none");
        $("[name = radio-pay-1]:radio").attr("checked", false).checkboxradio("refresh");
    }
    //#endregion

    //#region 绑定配送方式信息
    var regionid = LS.get("county_id");
    var paygroupid = LS.get("payment_Id");
    GetWcf({
        _api: "Order.get_logistics_list",
        _url: "/" + regionid + "/" + paygroupid
    }, function (jsonString) {
        if (jsonString.status == 1 && typeof (jsonString.list) == "object" && jsonString.list.length > 0) {
            $('#deliverylist_cont').setTemplate($('#deliverylist_cont_template').html());
            $('#deliverylist_cont').processTemplate(jsonString.list, null, { append: false });
            $('#deliverylist_cont').trigger('create');
            //没有任何默认值时指定
            $("[name = radio-type-1]:radio").attr("checked", false).checkboxradio("refresh");
            $("[name = radio-type-1]:radio:first").attr("checked", true).checkboxradio("refresh");   // 绑定事件及时更新checkbox的checked值
            sh_type_radio_checked();
        } else {
            $('#deliverylist_cont').setTemplate("<p>暂无配送方式信息！</p>");
            $('#deliverylist_cont').processTemplate(jsonString.list, null, { append: false });
            $('#deliverylist_cont').trigger('create');
        }

    }, true);
    //#endregion

    //#region 点击给radio赋值
    $("[name = radio-type-1]:radio").live('change', function () {
        $("[name = radio-type-1]:radio").attr("checked", false).checkboxradio("refresh");
        $(this).attr("checked", true).checkboxradio("refresh");   // 绑定事件及时更新checkbox的checked值

        sh_type_radio_checked();
    });
    $("[name = radio-date-1]:radio").live('change', function () {
        $("[name = radio-date-1]:radio").attr("checked", false).checkboxradio("refresh");
        $(this).attr("checked", true).checkboxradio("refresh");   // 绑定事件及时更新checkbox的checked值
    });
    $("[name = radio-pay-1]:radio").live('change', function () {
        $("[name = radio-pay-1]:radio").attr("checked", false).checkboxradio("refresh");
        $(this).attr("checked", true).checkboxradio("refresh");   // 绑定事件及时更新checkbox的checked值
    });
    //#endregion

    //#region 提交支付方式
    $("#deliverylist_sub_btn").live('click', function () {
        var delivery_array = {
            slides: [
                "delivery_text_name", //配送方式名称
                "delivery_Id", //配送方式ID
                "delivery_sh_time_text", //送货时间的名称
                "delivery_sh_time_id", //送货时间的值
                "delivery_fk_text", //付款类型名称  如现金
                "delivery_fk_id"//付款类型值
                    ],
            values: [
                $("[name = radio-type-1]:radio:checked").parent().find("span.ui-btn-text").text() || "null",
                $("[name = radio-type-1]:radio:checked").attr("value") || "null",
                $("[name = radio-date-1]:radio:checked").parent().find("span.ui-btn-text").text() || "null",
                $("[name = radio-date-1]:radio:checked").attr("value") || "null",
                $("[name = radio-pay-1]:radio:checked").parent().find("span.ui-btn-text").text() || "null",
                $("[name = radio-pay-1]:radio:checked").attr("value") || "null"
            ]
        };
        Delivery_info_Object(delivery_array);
        window.location.href = window.WebRoot + "CheckOut/orderconfirm.aspx";
    });
    //#endregion
}
//#endregion

//#region 发票信息
function invoice_Fun() {
    var FaTtype_Status = function () {
        $("[name = FaTtype]:radio").attr("checked", false).checkboxradio("refresh");
        $("[name = FaTtype]:radio:first").attr("checked", true).checkboxradio("refresh");
    }

    //#region 给默认赋值
    var invoice_default_status = function () {
        $("[name = IsNoInvoice]:radio").attr("checked", false).checkboxradio("refresh");
        $("[name = IsNoInvoice]:radio:first").attr("checked", true).checkboxradio("refresh");

        $("#FaTHeader,#invoice_Type").css("display", "none");
        $("[name = FaTtype]:radio").attr("checked", false).checkboxradio("refresh");
        $("#FaTHeader").val("");
        $("#Cah_Mesag").css("display", "none").text("");
    };
    //#endregion

    //#region 否
    invoice_default_status();
    //#endregion

    //#region 个人
    var invoice_Personal_Status = function () {
        $("#FaTHeader").css("display", "none").val("");
        $("#Cah_Mesag").css("display", "none").text("");
        FaTtype_Status();
        $("#invoice_Type").css("display", "block");
    }
    //#endregion

    //#region 公司
    var invoice_company_Status = function () {
        FaTtype_Status();
        $("#FaTHeader,#invoice_Type").css("display", "block");
    }
    //#endregion

    $("[name = IsNoInvoice]:radio").click(function () {
        $("[name = IsNoInvoice]:radio").attr("checked", false).checkboxradio("refresh");
        $(this).attr("checked", true).checkboxradio("refresh");
        var current_Status = $(this).attr("value");
        if (current_Status === "0") {
            invoice_default_status();
        } else if (current_Status === "1") {
            invoice_Personal_Status();
        } else if (current_Status === "2") {
            invoice_company_Status();
        }
    });

    //#region  提交发票信息
    $("#invoice_btn").click(function () {
        var isorInvice, FaTHeader, invoice_Type; //是否要发票、发票抬头、发票类型
        isorInvice = $("[name = IsNoInvoice]:radio:checked").attr("value") || "-1";
        FaTHeader = $("#FaTHeader").val() || "-1";
        invoice_Type = $("[name = FaTtype]:radio:checked").attr("value") || "-1";
        var di_no = $("#FaTHeader").attr("style").toString();
        if (di_no.indexOf("block") != "-1") {
            if ($("#FaTHeader").val() == "") {
                $("#Cah_Mesag").css("display", "block").text("请填写发票抬头");
                return;
            } else {
                $("#Cah_Mesag").css("display", "none").text("");
            }
        }
        //alert($("[name = FaTtype]:radio:checked").text());
        var delivery_array = {
            slides: [
                "isorInvice", //是否要发票
                "FaTHeader", //发票抬头
                "invoice_Type", //发票类型
                "invoice_Type_Text"
                    ],
            values: [
                isorInvice,
                FaTHeader,
                invoice_Type,
                $("[name = FaTtype]:radio:checked").parent().find("span.ui-btn-text").text()
            ]
        };
        Delivery_info_Object(delivery_array); //发票信息存储
        window.location.href = window.WebRoot + "CheckOut/orderconfirm.aspx";
    });
    //#endregion
}
//#endregion

//#region 获取省市区数据
var proviceA = "", cityA = "", townA = "";
function GetPrCiTownAjaxDate() {
    GetWcf_F({
        _api: "Order.get_allregion_list"
    }, function (jsonString) {
        if (jsonString.status == 1 && typeof (jsonString.info) == "object" && jsonString.info.length > 0) {
            proviceA = jsonString.info[0];
            cityA = jsonString.info[1];
            townA = jsonString.info[2];
        } else {
            alert(jsonString.msg);
        }
    }, true, true);
}
//#endregion

//#region 获取省
function ProViceCityTown_Function() {
    var s1P = [];
    var s1 = jQuery("#PCR select#s1s");
    s1.empty();
    for (var i = 0, len = proviceA.length; i < len; i++) {
        s1P[i] = "<option value='" + proviceA[i].id + "'>" + proviceA[i].name + "</option>";
    }
    $(s1P.join('')).appendTo(s1);
    var myselect = $("#PCR select#s1s");
    myselect[0].selectedIndex = 0;
    myselect.selectmenu("refresh");
    //jQuery("#PCR select#s2s").empty();
    //jQuery("#PCR select#s3s").empty();
    getCity();
}
//#endregion

//#region 获取市
function getCity() {
    var s1 = jQuery("#PCR select#s1s").val();
    var s2 = jQuery("#PCR select#s2s");
    var s2C = [];
    s2.empty();
    for (var i = 0, len = cityA.length; i < len; i++) {
        if (s1 == cityA[i].pid) {
            s2C[i] = "<option value='" + cityA[i].id + "'>" + cityA[i].name + "</option>";
        }
    }
    $(s2C.join('')).appendTo(s2);
    $("#PCR select#s2s").selectmenu('refresh');
    var myselect = $("#PCR select#s2s");
    myselect[0].selectedIndex = 0;
    myselect.selectmenu("refresh");
    getTown();
}
//#endregion

//#region 获取区
function getTown() {
    var s2 = jQuery("#PCR select#s2s").val();
    var s3 = jQuery("#PCR select#s3s");
    var s3C = [];
    s3.empty();
    for (var i = 0, len = townA.length; i < len; i++) {
        if (s2 == townA[i].pid) {
            s3C[i] = "<option value='" + townA[i].id + "'>" + townA[i].name + "</option>";
        }
    }
    $(s3C.join('')).appendTo(s3);
    $("#PCR select#s3s").selectmenu('refresh');
    var myselect = $("#PCR select#s3s");
    myselect[0].selectedIndex = 0;
    myselect.selectmenu("refresh");
}
//#endregion

//#region 给省市区绑定事件
function BindClick_Pro_City_Town_Event_Fun() {
    GetPrCiTownAjaxDate();
    ProViceCityTown_Function();
    $("#s1s").live("change", function () {
        getCity();
    });
    $("#s2s").live("change", function () {
        getTown();
    });
}
//#endregion

var address_add_update_Object = {};

//#region 点击给radio赋值
address_add_update_Object.addressRadioChange = function () {
    $("[name = radio-choice-1]:radio").live('change', function () {
        $("[name = radio-choice-1]:radio").attr("checked", false).checkboxradio("refresh");
        $(this).attr("checked", true).checkboxradio("refresh");   // 绑定事件及时更新checkbox的checked值
    });
};
//#endregion

//#region 验证
address_add_update_Object.address_valite_c = function (id_obj) {
    $("#frmAddress_Add").validate({
        errorElement: "span",
        rules: {
            email: {
                required: true
            },
            addr: {
                required: true
            },
            zipCode: {
                required: true,
                number: true,
                rangelength: [6, 6]
            },
            mobilePhone: {
                required: true,
                number: true
            }
        },

        messages: {
            email: {
                required: "请填写收货人姓名"
            },
            addr: {
                required: "请填写详细的的收货地址"
            },
            zipCode: {
                required: "请填写邮政编码",
                number: "请填写正确的邮政编码",
                rangelength: "邮政编码只能是{0}位"
            },
            mobilePhone: {
                required: "请填写手机号码",
                number: "请填写正确的手机号码"
            }
        },
        submitHandler: function (form) {
            var email = $("#email").val();
            var s1s = $("#s1s option:selected").val(); //省
            var s1s_Text = $("#s1s option:selected").text();
            var s2s = $("#s2s option:selected").val(); //市
            var s2s_Text = $("#s2s option:selected").text();
            var s3s = $("#s3s option:selected").val(); //区
            var s3s_Text = $("#s3s option:selected").text();
            var addr = $("#addr").val();
            var zipCode = $("#zipCode").val();
            var type_add = $("[name = radio-choice-1]:radio:checked").attr("value");
            var mobilePhone = $("#mobilePhone").val();
            //alert(s1s+s1s_Text+s2s+s2s_Text+s3s+s3s_Text+addr+zipCode+type_add+mobilePhone);

            var jobj = { "addr": addr, "city": s2s_Text, "city_id": s2s, "contact_name": email, "county": s3s_Text, "county_id": s3s, "id": id_obj, "mobile": mobilePhone, "phone": "-", "province": s1s_Text, "province_id": s1s, "type": type_add, "zip": zipCode };


            PostWcf({
                _api: "Member.set_address_info",
                _data: JSON.stringify(jobj)
            }, function (json) {
                if (json.status == 1) {
                    if (id_obj > 0) { LS.set("current_addressid", id_obj); } else {
                        LS.set("current_addressid", json.info);
                    }
                    window.location.href = window.WebRoot + "CheckOut/addresslist.aspx";
                } else
                    $("#Cah_Mesag").css("display", "block").text(json.msg);
            }, true);
        }
    });
}
//#endregion

//#region 添加收货人信息
function address_add_Fun() {
    BindClick_Pro_City_Town_Event_Fun();
    address_add_update_Object.addressRadioChange();
    address_add_update_Object.address_valite_c("0");
}
//#endregion

//#region 修改收货人信息
function address_edit_Fun() {
    BindClick_Pro_City_Town_Event_Fun();
    //alert(window.location.search);
    var address_id = window.location.search.toString().replace("?address_id=", "");
    //#region 绑定收货人详细信息
    GetWcf_F({
        _api: "Member.get_address_info",
        _url: "/" + address_id
    }, function (jsonString) {
        if (jsonString.status == 1 && typeof (jsonString.info) == "object" && jsonString.info.contact_name != null) {
            $("#acc_id").val(jsonString.info.id);
            $("#email").val(jsonString.info.contact_name);
            $("#s1s option[value=" + jsonString.info.province_id + "]").attr("selected", true); //省
            $("#s1s").selectmenu('refresh').trigger("click");
            $("#s2s option[value=" + jsonString.info.city_id + "]").attr("selected", true); //市
            $("#s2s").selectmenu('refresh').trigger("click");
            $("#s3s option[value=" + jsonString.info.county_id + "]").attr("selected", true); //区
            $("#s3s").selectmenu('refresh');

            $("#addr").val(jsonString.info.addr);
            $("#zipCode").val(jsonString.info.zip);
            $("input[type='radio'][name=radio-choice-1][value=" + jsonString.info.type + "]").attr("checked", true).checkboxradio("refresh");
            $("#mobilePhone").val(jsonString.info.mobile);
        } else {
            $("#Cah_Mesag").text(jsonString.msg);
        }
    }, true, true);
    //#endregion
    address_add_update_Object.addressRadioChange();
    //alert($("#acc_id").val());
    address_add_update_Object.address_valite_c($("#acc_id").val());
}
//#endregion

//#region 订单成功页面
var makeorder_Fun = function () {
    var make_oid = LS.get("make_oid") || "no_c";
    var make_total_order = LS.get("make_total_order") || "no_c";
    var make_paytype = LS.get("make_paytype") || "no_c";
    var make_logisticstype = LS.get("make_logisticstype") || "no_c";
    var make_posttimetype = LS.get("make_posttimetype") || "no_c";
    var make_Nub = function (canstring, cansb) {
        if (cansb === "no_c") {
            $("#" + canstring).hide().text("");
            $("#" + canstring).prev().hide();
            $("#" + canstring).parent().hide();
        } else {
            if (canstring === "make_total_order") { $("#" + canstring).show().text(parseFloat(cansb).toFixed(2) + "元"); } else {
                $("#" + canstring).show().text(cansb);
            }
        }
    }

    make_Nub("make_oid", make_oid); //订单号
    make_Nub("make_total_order", make_total_order); //应付金额
    make_Nub("make_paytype", make_paytype); //支付方式
    make_Nub("make_logisticstype", make_logisticstype); //配送方式
    make_Nub("make_posttimetype", make_posttimetype); //送货时间

    if (make_paytype === "货到付款") {
        $("#online_pay").css("display", "none");
    }
}
//#endregion


$(document).ready(function () {
    var url = window.location.href;
    if (/shoppingcart.aspx/i.test(url)) {
        shoppingcart_Fun();
    } else if (/orderconfirm.aspx/i.test(url)) {
        orderconfirm_Fun();
        calculation_shipping_costs_Fun();
    } else if (/addresslist.aspx/i.test(url)) {
        addresslist_Fun();
    } else if (/paymentlist.aspx/i.test(url)) {
        paymentlist_Fun();
    } else if (/deliverylist.aspx/i.test(url)) {
        deliverylist_Fun();
    } else if (/invoice.aspx/i.test(url)) {
        invoice_Fun();
    } else if (/address_add.aspx/i.test(url)) {
        address_add_Fun();
    }
    else if (/address_edit.aspx/i.test(url)) {
        address_edit_Fun();
    } else if (/makeorder.aspx/i.test(url)) {
        makeorder_Fun();
    }
    $('#gotop').tap(function () {
        $.mobile.silentScroll(10);
    });

});

