using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Enums
{
    public class MLogGroup
    {
        /// <summary>
        /// 用户相关 取值 100-199
        /// </summary>
        public enum Member
        {
            注册 = 100,
            登陆,
            读取用户信息,
            修改用户基本信息,
            修改用户收货地址
        }
        /// <summary>
        /// 商品相关 取值 200-299
        /// </summary>
        public enum Goods
        {
            读取商品数据 = 200
        }
        /// <summary>
        /// 订单相关 取值 300-399
        /// </summary>
        public enum Order
        {
            创建订单 = 300,
            获取临时订单信息,
            检查商品销售区域,
            订单汇总信息,
            获取订单信息,
            获取订单列表
        }
        /// <summary>
        /// 支付相关 取值 700-800
        /// </summary>
        public enum Payment
        {
            创建支付对象=700

        }
        /// <summary>
        /// 系统管理 取值 800-999
        /// </summary>
        public enum Manage
        {
            重建接口权限 = 800
        }
        /// <summary>
        /// 面向切面通知 取值 1000-1099
        /// </summary>
        public enum AopAdvice
        {
            方法拦截 = 1000,
            方法执行前,
            方法执行后,
            方法错误
        }
        /// <summary>
        /// wcf 服务 记录类型 取值 1100-1199
        /// </summary>
        public enum WcfService
        {
            构造函数 = 1100,
            执行操作
        }
        /// <summary>
        /// 其他 取值 1200-1299
        /// </summary>
        public enum Other
        {
            获取缓存对象 = 1000,
            配置文件操作,

            Redis缓存,
            Memcached缓存,

            MongoDb,

            数据测试,
            代码测试
        }
    }
}
