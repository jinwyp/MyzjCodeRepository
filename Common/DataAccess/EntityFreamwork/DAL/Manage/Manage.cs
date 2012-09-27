using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EF.Model.DataContext;

namespace EF.DAL
{
    public class Manage
    {
        /// <summary>
        /// 检查 数据是否存在
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int CheckIsExistsSystemPermission(Model.DataContext.System_Permission item)
        {
            using (var db = new bbHomeEntities())
            {
                var queryTxt = from a in db.System_Permission
                               where a.MethodName == item.MethodName
                               select a.Id;
                return queryTxt.FirstOrDefault();
            }
        }

        /// <summary>
        /// 更新系统权限
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool UpdateSystemPermission(Model.DataContext.System_Permission item)
        {
            using (var db = new bbHomeEntities())
            {
                var queryTxt = from a in db.System_Permission
                               where a.Id == item.Id
                               select a;

                var systemPermission = queryTxt.FirstOrDefault();

                systemPermission.RefreshTime = item.RefreshTime;
                systemPermission.RequestType = item.RequestType;
                systemPermission.RequestUri = item.RequestUri;
                systemPermission.ReturnParameters = item.ReturnParameters;
                systemPermission.AfferentParameters = item.AfferentParameters;
                systemPermission.MethodAttrs = item.MethodAttrs;
                if (systemPermission.Id > 0)
                    db.AddToSystem_Permission(systemPermission);

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 新增系统权限
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool AddSystemPermission(Model.DataContext.System_Permission item)
        {
            using (var db = new bbHomeEntities())
            {
                db.System_Permission.AddObject(item);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 获取系统权限
        /// </summary>
        /// <returns></returns>
        public List<System_Permission> GetSystemPermissionList()
        {
            using (var db = new bbHomeEntities())
            {
                var queryTxt = from a in db.System_Permission
                               select a;
                return queryTxt.ToList();
            }
        }

    }
}
