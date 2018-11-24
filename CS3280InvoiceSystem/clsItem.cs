using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3280InvoiceSystem
{
    public class clsItem
    {
        #region Class Variables
        private int iLineItemNum;
        private string sItemCode;
        private string sItemDesc;
        private int iCost;
        #endregion

        #region Methods
        public clsItem(int iLineItemNum, string sItemCode, string sItemDesc, int iCost)
        {
            this.ILineItemNum = iLineItemNum;
            this.SItemCode = sItemCode;
            this.SItemDesc = sItemDesc;
            this.ICost = iCost;
        }

        public int ILineItemNum { get => iLineItemNum; set => iLineItemNum = value; }
        public string SItemCode { get => sItemCode; set => sItemCode = value; }
        public string SItemDesc { get => sItemDesc; set => sItemDesc = value; }
        public int ICost { get => iCost; set => iCost = value; }
        #endregion
    }
}
