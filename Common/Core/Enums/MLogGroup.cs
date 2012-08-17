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
            登陆 = 101,
            读取用户信息 = 102,
            修改用户基本信息 = 103,
            修改用户收货地址 = 104
        }
        /// <summary>
        /// 商品相关 取值 200-399
        /// </summary>
        public enum Goods
        {
            读取商品数据 = 200
        }
        /// <summary>
        /// 订单相关 取值 400-599
        /// </summary>
        public enum Order
        {

        }
        /// <summary>
        /// 面向切面通知 取值 700-899
        /// </summary>
        public enum AopAdvice
        {
            方法拦截 = 700,
            方法执行前 = 701,
            方法执行后 = 702,
            方法错误 = 703
        }
        /// <summary>
        /// wcf 服务 记录类型 取值 900-999
        /// </summary>
        public enum WcfService
        {
            构造函数,
            执行操作
        }
        /// <summary>
        /// 其他 取值 1000-1199
        /// </summary>
        public enum Other
        {
            获取缓存对象 = 1000,
            配置文件操作 = 1001,

            Redis缓存 = 1010,
            Memcached缓存 = 1011,

            MongoDb = 1500,

            数据测试 = 1190,
            代码测试 = 1191
        }
    }
}
