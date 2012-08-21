using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF.Model.Entity
{
    public class OrderDeliver
    {
        private int orderDeliverID;
        private string orderCode;
        private int addressID;
        private string consignee;
        private string phone;
        private string mobile;
        private int stateID;
        private string stateName;
        private int cityID;
        private string cityName;
        private int countyID;
        private string countyName;
        private string detailAddr;
        private string postCode;
        private string roadName;
        private string hausnummer;
        private string userMemo;
        private string inMemo;
        /// <summary>
        /// 内部备注（ERP上限制100个汉字，200个字符）
        /// </summary>
        public string InMemo
        {
            get { return inMemo; }
            set { inMemo = value; }
        }
        /// <summary>
        /// 用户备注（ERP上限制250个汉字，500个字符）
        /// </summary>
        public string UserMemo
        {
            get { return userMemo; }
            set { userMemo = value; }
        }
        /// <summary>
        /// 门牌号
        /// </summary>
        public string Hausnummer
        {
            get { return hausnummer; }
            set { hausnummer = value; }
        }
        /// <summary>
        /// 路名
        /// </summary>
        public string RoadName
        {
            get { return roadName; }
            set { roadName = value; }
        }
        /// <summary>
        /// 邮编
        /// </summary>
        public string PostCode
        {
            get { return postCode; }
            set { postCode = value; }
        }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string DetailAddr
        {
            get { return detailAddr; }
            set { detailAddr = value; }
        }
        /// <summary>
        /// 区县名称
        /// </summary>
        public string CountyName
        {
            get { return countyName; }
            set { countyName = value; }
        }
        /// <summary>
        /// 区县标识
        /// </summary>
        public int CountyID
        {
            get { return countyID; }
            set { countyID = value; }
        }
        /// <summary>
        /// 市名称
        /// </summary>
        public string CityName
        {
            get { return cityName; }
            set { cityName = value; }
        }
        /// <summary>
        /// 市标识
        /// </summary>
        public int CityID
        {
            get { return cityID; }
            set { cityID = value; }
        }
        /// <summary>
        /// 省名称
        /// </summary>
        public string StateName
        {
            get { return stateName; }
            set { stateName = value; }
        }
        /// <summary>
        /// 省标识
        /// </summary>
        public int StateID
        {
            get { return stateID; }
            set { stateID = value; }
        }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        /// <summary>
        /// 收货人名称
        /// </summary>
        public string Consignee
        {
            get { return consignee; }
            set { consignee = value; }
        }
        /// <summary>
        /// 收货人地址标识
        /// </summary>
        public int AddressID
        {
            get { return addressID; }
            set { addressID = value; }
        }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode
        {
            get { return orderCode; }
            set { orderCode = value; }
        }
        /// <summary>
        /// 订单配送信息标识
        /// </summary>
        public int OrderDeliverID
        {
            get { return orderDeliverID; }
            set { orderDeliverID = value; }
        }
    }
}
