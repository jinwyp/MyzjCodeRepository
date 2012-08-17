using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using Core.Enums;
using Core.LogUtility;
using Core.ConfigUtility;

namespace Core.DataBase
{
    public class MMongoDbManager
    {
        #region 内部字段
        private static readonly object LockObj = new object();
        private static MMongoDbManager _obj;
        private static MongoServer _server;
        private static MongoDatabase _database;
        #endregion

        public static MMongoDbManager GetInstance()
        {
            if (_obj == null)
                lock (LockObj)
                    if (_obj == null)
                        _obj = new MMongoDbManager();
            return _obj;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        private MMongoDbManager()
        {
            try
            {
                var host = MConfigManager.GetAppSettingsValue<string>(MConfigManager.FormatKey("MongoDbServer", MConfigs.ConfigsCategory.Cache));
                var port = MConfigManager.GetAppSettingsValue<int>(MConfigManager.FormatKey("MongoDbPort", MConfigs.ConfigsCategory.Cache));
                if (!string.IsNullOrEmpty(host) && port > 0)
                {
                    var serverCon = string.Format("mongodb://{0}:{1}", host.Trim(), port);
                    _server = MongoServer.Create(serverCon);
                    if (_server == null)
                        MLogManager.Error(MLogGroup.Other.MongoDb, null, "初始化 失败！");
                }
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Other.MongoDb, null, "初始化 失败！", ex);
            }
        }


    }
}
