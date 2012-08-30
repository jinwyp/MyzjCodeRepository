using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Reflection;
using Core.FactoryUtility;
using EF.DAL;
using Spring.Aop.Framework;
using Spring.Context;
using Spring.Context.Support;
using Spring.Objects.Factory;

namespace Factory
{
    /// <summary>
    ///  数据层 工厂
    /// </summary>
    public static class DALFactory
    {
        /// <summary>
        /// 系统管理 数据
        /// </summary>
        /// <returns></returns>
        public static Manage Manage()
        {
            return new EF.DAL.Manage();
        }

        /// <summary>
        /// 会员 数据
        /// </summary>
        /// <returns></returns>
        public static Member Member()
        {
            return new EF.DAL.Member();
            //return new MobileWcf.DAL.Member();
            //return MFactoryManager.GetFactoryAssembly<IMember>("Member", "Factory/DALContent", true);
            //return MIocUtility.GetObject<IMember>("Member");
        }

        /// <summary>
        /// 商品数据
        /// </summary>
        /// <returns></returns>
        public static Goods Goods()
        {
            return new EF.DAL.Goods();
            //return MFactoryManager.GetFactoryAssembly<IGoods>("Goods", "Factory/DALContent", true);
            //var goods = MIocUtility.GetObject<IGoods>("Goods");
            //return goods;
            //var proxyFactory = new ProxyFactory(goods);
            //proxyFactory.AddAdvice(new MethodInterceptor());
            //proxyFactory.AddAdvice(new AfterReturningAdvice());
            //proxyFactory.AddAdvice(new MethodBeforeAdvice());
            //return (IGoods)proxyFactory.GetProxy();
        }

        /// <summary>
        /// 订单数据
        /// </summary>
        /// <returns></returns>
        public static Order Order()
        {
            return new EF.DAL.Order();
            //return MFactoryManager.GetFactoryAssembly<IOrder>("Order", "Factory/DALContent", true);
            //return MIocUtility.GetObject<IOrder>("Order");
        }

        /// <summary>
        /// 基础数据
        /// </summary>
        /// <returns></returns>
        public static BaseData BaseData()
        {
            return new EF.DAL.BaseData();
            //return MFactoryManager.GetFactoryAssembly<IBaseData>("BaseData", "Factory/DALContent", true);
            //return MIocUtility.GetObject<IBaseData>("BaseData");
        }

        /// <summary>
        /// 购物车数据
        /// </summary>
        /// <returns></returns>
        public static ShoppingCartDal ShoppingCartDal()
        {
            return new EF.DAL.ShoppingCartDal();
            //return MIocUtility.GetObject<IShoppingCartDal>("ShoppingCartDal");
        }
    }
}
