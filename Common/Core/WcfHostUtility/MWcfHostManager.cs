using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.WcfHostUtility
{
    /// <summary>
    ///ServiceHost 的摘要说明
    /// </summary>
    public class MWcfHostManager<T>
    {
        public MWcfHostManager()
        {
            WcfServiceInit();
        }

        #region 属性
        private System.ServiceModel.ServiceHost _host;
        /// <summary>
        /// Wcf服务对象
        /// </summary>
        public System.ServiceModel.ServiceHost Host
        {
            get { WcfServiceInit(); return _host; }
        }
        #endregion

        /// <summary>
        /// 初始化Wcf服务
        /// </summary>
        public void WcfServiceInit()
        {
            if (_host == null)
            {
                _host = new System.ServiceModel.ServiceHost(typeof(T));

                _host.Opened += delegate
                {
                    WriteLog("Wcf服务启动成功！");
                };
                _host.Closed += delegate
                {
                    WriteLog("Wcf服务停止成功！");
                };
            }
        }

        public void Open()
        {
            if (_host != null && _host.State != System.ServiceModel.CommunicationState.Opened)
            {
                _host.Open();
            }
            else
                WriteLog("Wcf服务已启动！");
        }

        public void Close()
        {
            if (_host != null && _host.State == System.ServiceModel.CommunicationState.Opened)
            {
                _host.Close();
            }
            else
                WriteLog("Wcf服务未启动！");
        }

        public void WriteLog(string format, params object[] arg)
        {

        }

    }
}