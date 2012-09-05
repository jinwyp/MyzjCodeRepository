using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Xml.Linq;

namespace Core.FileUtility
{
    /// <summary>
    /// Xml 文件工具类
    /// </summary>
    public class XmlFileUtility
    {
        private XElement _xel;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fileName"></param>
        public XmlFileUtility(string fileName)
        {
            var filePath = HttpContext.Current.Server.MapPath(fileName);
            if (File.Exists(filePath))
            {
                _xel = XElement.Load(filePath);
            }
        }

        /// <summary>
        /// 获取 xml 节点
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public IEnumerable<XElement> GetNodeList(XName nodeName)
        {
            if (_xel != null)
            {
                return _xel.Elements(nodeName);
            }
            return null;
        }

    }
}
