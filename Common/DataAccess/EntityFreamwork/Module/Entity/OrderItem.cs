using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF.Model.Entity
{
    public class OrderItem
    {
        private int intDetailId;
        private int intProductId;
        private string vchProductPrinted;
        private string vchProductName;
        private int intQty;
        private decimal numSalePrice;
        private decimal numTotalAmount;
        private int intHerdPriceID;
        private decimal? numStandarPrice;
        private int intRtnQty;
        private decimal? numCost;
        private decimal? numCleanCost;
        private decimal? numMonthCost;
        private decimal? numMonthCleanCost;
        private int intBaseStar;
        private int intScores;
        private DateTime dtOpDate;
        private int? intLogisticsID;
        private int intChannel;
        private int intIsDelete;
        private int intPromID;
        private int intIsInvoice;
        private string productUrl;

        public int IntDetailId
        {
            get { return intDetailId; }
            set { intDetailId = value; }
        }

        public int IntProductId
        {
            get { return intProductId; }
            set { intProductId = value; }
        }


        public string VchProductPrinted
        {
            get { return vchProductPrinted; }
            set { vchProductPrinted = value; }
        }


        public string VchProductName
        {
            get { return vchProductName; }
            set { vchProductName = value; }
        }


        public int IntQty
        {
            get { return intQty; }
            set { intQty = value; }
        }


        public decimal NumSalePrice
        {
            get { return numSalePrice; }
            set { numSalePrice = value; }
        }


        public decimal NumTotalAmount
        {
            get { return numTotalAmount; }
            set { numTotalAmount = value; }
        }

        public int IntHerdPriceID
        {
            get { return intHerdPriceID; }
            set { intHerdPriceID = value; }
        }

        public decimal? NumStandarPrice
        {
            get { return numStandarPrice; }
            set { numStandarPrice = value; }
        }

        public int IntRtnQty
        {
            get { return intRtnQty; }
            set { intRtnQty = value; }
        }

        public decimal? NumCost
        {
            get { return numCost; }
            set { numCost = value; }
        }

        public decimal? NumCleanCost
        {
            get { return numCleanCost; }
            set { numCleanCost = value; }
        }

        public decimal? NumMonthCost
        {
            get { return numMonthCost; }
            set { numMonthCost = value; }
        }

        public decimal? NumMonthCleanCost
        {
            get { return numMonthCleanCost; }
            set { numMonthCleanCost = value; }
        }

        public int IntBaseStar
        {
            get { return intBaseStar; }
            set { intBaseStar = value; }
        }

        public int IntScores
        {
            get { return intScores; }
            set { intScores = value; }
        }

        public int? IntLogisticsID
        {
            get { return intLogisticsID; }
            set { intLogisticsID = value; }
        }

        public System.DateTime DtOpDate
        {
            get { return dtOpDate; }
            set { dtOpDate = value; }
        }

        public int IntChannel
        {
            get { return intChannel; }
            set { intChannel = value; }
        }

        public int IntIsDelete
        {
            get { return intIsDelete; }
            set { intIsDelete = value; }
        }

        public int IntPromID
        {
            get { return intPromID; }
            set { intPromID = value; }
        }

        public int IntIsInvoice
        {
            get { return intIsInvoice; }
            set { intIsInvoice = value; }
        }

        public string ProductUrl
        {
            get { return productUrl; }
            set { productUrl = value; }
        }

    }
}
