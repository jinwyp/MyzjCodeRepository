using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EF.Model.DataContext;

namespace EF.DAL
{
    /// <summary>
    /// 
    /// </summary>
    public class Cms
    {
        /// <summary>
        /// 获取栏位数据列表
        /// </summary>
        /// <param name="columncode"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<Web_ContentData> GetColumnDataList(string columncode, int page, int size, out int total)
        {
            using (var db = new HolycaEntities())
            {
                var queryTxt = from a in db.Web_ContentData
                               where a.Wcd_Code == columncode && a.Wcd_IsEnable == 1
                               orderby a.Wcd_Sort descending
                               select a;
                total = queryTxt.Count();
                return queryTxt.Skip(size * (page - 1)).Take(size).ToList();
            }
        }

        /// <summary>
        /// 获取公告列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<Web_Bulletin> GetNoticeList(int page, int size, out int total)
        {
            using (var db = new HolycaEntities())
            {
                var queryTxt = from a in db.Web_Bulletin
                               where a.intIsEnable == 1
                               orderby a.intSort descending
                               select a;
                total = queryTxt.Count();
                return queryTxt.Skip(size * (page - 1)).Take(size).ToList();
            }
        }

        /// <summary>
        /// 获取公告信息
        /// </summary>
        /// <param name="noticeId"></param>
        /// <returns></returns>
        public Web_Bulletin GetNoticeInfo(int noticeId)
        {
            using (var db = new HolycaEntities())
            {
                var queryTxt = from a in db.Web_Bulletin
                               where a.intIsEnable == 1 && a.intBulletinID == noticeId
                               select a;
                return queryTxt.First();
            }
        }
    }
}
