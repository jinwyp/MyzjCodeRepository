using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Enums;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Core.DataType
{
    /// <summary>
    /// 返回结果
    /// </summary>
    [Serializable]
    [DataContract]
    public class MResult
    {
        /// <summary>
        /// 返回文字信息
        /// </summary>
        [DataMember]
        public string msg { get; set; }

        /// <summary>
        /// 返回状态
        /// </summary>
        [DataMember]
        public MResultStatus status { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        [DataMember]
        public string data { get; set; }
    }

    /// <summary>
    /// 返回结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    [DataContract]
    public class MResultList<T> : MResult where T : new()
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MResultList()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="isInit">是否初始化数据实例</param>
        public MResultList(bool isInit)
        {
            if (isInit)
            {
                this.list = new List<T>();
            }
        }

        /// <summary>
        /// 列表数据
        /// </summary>
        [DataMember]
        public List<T> list { get; set; }

        /// <summary>
        /// 列表总数
        /// </summary>
        [DataMember]
        public long total { get; set; }

        /// <summary>
        /// 列表页数
        /// </summary>
        [DataMember]
        public int page { get; set; }

        /// <summary>
        /// 每页大小
        /// </summary>
        [DataMember]
        public int size { get; set; }
    }
    /// <summary>
    /// 返回结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    [DataContract]
    public class MResult<T> : MResult
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MResult()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="isInit">是否初始化数据实例</param>
        public MResult(bool isInit)
        {
            if (isInit)
            {
                this.info = System.Activator.CreateInstance<T>();
            }
        }

        /// <summary>
        /// 数据
        /// </summary>
        [DataMember]
        public T info { get; set; }
    }
}
