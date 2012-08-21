using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF.Model.Entity
{
    public class OrderInvoice
    {
        private int invoicID;
        private string orderCode;
        private int userID;
        private int invoiceType;
        private int invoiceKind;
        private string account;
        private decimal amount;
        private string address;
        private string invoicTitile;
        private DateTime? billingTime;
        private int createrID;
        private string invoiceID;
        private string phone;
        private string memo;
        private int isBilling;
        private int isDetail;
        private DateTime? createDate;

        #region 属性
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
        /// <summary>
        /// 是否显示明细（0 不显示，1显示）
        /// </summary>
        public int IsDetail
        {
            get { return isDetail; }
            set { isDetail = value; }
        }
        /// <summary>
        /// 是否开发票（0 未开票，1已经开票）
        /// </summary>
        public int IsBilling
        {
            get { return isBilling; }
            set { isBilling = value; }
        }
        /// <summary>
        /// 发票备注
        /// </summary>
        public string Memo
        {
            get { return memo; }
            set { memo = value; }
        }
        /// <summary>
        /// 开票电话
        /// </summary>
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        /// <summary>
        /// 开票编号
        /// </summary>
        public string InvoiceID
        {
            get { return invoiceID; }
            set { invoiceID = value; }
        }
        /// <summary>
        /// 开票人
        /// </summary>
        public int CreaterID
        {
            get { return createrID; }
            set { createrID = value; }
        }
        /// <summary>
        /// 开票时间
        /// </summary>
        public DateTime? BillingTime
        {
            get { return billingTime; }
            set { billingTime = value; }
        }
        /// <summary>
        /// 发票抬头
        /// </summary>
        public string InvoicTitile
        {
            get { return invoicTitile; }
            set { invoicTitile = value; }
        }
        /// <summary>
        /// 开票地址
        /// </summary>
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        /// <summary>
        /// 发票金额
        /// </summary>
        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        /// <summary>
        /// 开票帐号
        /// </summary>
        public string Account
        {
            get { return account; }
            set { account = value; }
        }
        /// <summary>
        /// 发票类别(1普票2增票)
        /// </summary>
        public int InvoiceKind
        {
            get { return invoiceKind; }
            set { invoiceKind = value; }
        }
        /// <summary>
        /// 发票类型(1用品2食品)
        /// </summary>
        public int InvoiceType
        {
            get { return invoiceType; }
            set { invoiceType = value; }
        }
        /// <summary>
        /// 会员编号
        /// </summary>
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
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
        /// 发票标识
        /// </summary>
        public int InvoicID
        {
            get { return invoicID; }
            set { invoicID = value; }
        }

        public string CreateBy
        {
            get;
            set;
        }
        #endregion
    }
}
