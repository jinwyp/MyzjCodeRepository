<form id="frmLogin" action="">
  <div data-role="fieldcontain">
    <fieldset data-role="controlgroup" id="test">
      <label for="login_email"> 邮箱地址</label>
      <input type="text" id="login_email" name="login_email" class="required email" placeholder="请填写Email地址"
                    value="" />
      <span id="login_ErroMesg" class="error" generated="true" style="display: none"></span>
    </fieldset>
  </div>
  <div data-role="fieldcontain">
    <fieldset data-role="controlgroup">
      <label for="login_password"> 密码</label>
      <input type="password" id="login_password" name="login_password" class="required" placeholder="请输入密码"
                    value="" />
    </fieldset>
  </div>
  <input data-theme="b" value="登录" id="btnLoginSubmit" type="button" />
  <div class="ui-grid-a p10">
    <div class="ui-block-a"> <a href="<%= MobileSite.BaseLib.WebUrls.forgetpassword() %>" >忘记密码</a> </div>
    <div class="ui-block-b"> <a href="<%= MobileSite.BaseLib.WebUrls.registration() %>" >立即注册</a> </div>
  </div>
  <!--<ul data-role="listview" data-divider-theme="c" data-inset="true">
        <li data-role="list-divider" role="heading">使用其他合作网站登录</li>
        <li><a href="#page1">
            <img src="/images/16-sinaweibo.png" class="ui-li-icon" />新浪微博登录</a> </li>
        <li><a href="#page1" style="padding-left: 60px;">
            <img src="/images/alipay.png" class="ui-li-icon" />支付宝登录</a> </li>
        <li><a href="#page1">
            <img src="/images/qq.png" class="ui-li-icon" />QQ登录</a> </li>
    </ul>-->
</form>
