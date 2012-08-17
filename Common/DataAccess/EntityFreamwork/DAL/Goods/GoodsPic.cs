using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EF.Model.DataContext;

namespace EF.DAL
{
    public partial class Goods
    {
        /// <summary>
        /// 获取商品图片列表
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public List<Pdt_Pic> GetGoodsPicList(int gid)
        {
            var holycaDb = new HolycaEntities();

            var queryTxt = from c in holycaDb.Pdt_Pic
                           where c.intProductID == gid && c.intIsEnable == 1
                           orderby c.vchPicType ascending
                           select c;

            return queryTxt.ToList();
        }
    }
}
