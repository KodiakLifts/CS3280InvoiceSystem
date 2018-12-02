using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CS3280InvoiceSystem
{
    public class clsItem
    {
        #region Class Variables
        /// <summary>
        /// The items line number, a primary key
        /// </summary>
        private int iLineItemNum;
        /// <summary>
        /// The item Code, a primary key
        /// </summary>
        private string sItemCode;
        /// <summary>
        /// Item description
        /// </summary>
        private string sItemDesc;
        /// <summary>
        /// item cost
        /// </summary>
        private int iCost;
        #endregion

        #region Methods
        /// <summary>
        /// Item Constructor
        /// </summary>
        /// <param name="iLineItemNum"></param>
        /// <param name="sItemCode"></param>
        /// <param name="sItemDesc"></param>
        /// <param name="iCost"></param>
        public clsItem(int iLineItemNum, string sItemCode, string sItemDesc, int iCost)
        {
            try
            {
                this.ILineItemNum = iLineItemNum;
                this.SItemCode = sItemCode;
                this.SItemDesc = sItemDesc;
                this.ICost = iCost;
            }
            catch (System.Exception ex)
            {
                //Just throw the exception
                throw new System.Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// copies and creas a new item
        /// </summary>
        /// <param name="itemToCopy"></param>
        public clsItem(clsItem itemToCopy)
        {
            try
            {
                this.ILineItemNum = itemToCopy.iLineItemNum;
                this.SItemCode = itemToCopy.sItemCode;
                this.SItemDesc = itemToCopy.sItemDesc;
                this.ICost = itemToCopy.iCost;
            }
            catch (System.Exception ex)
            {
                //Just throw the exception
                throw new System.Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns the item description as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            try
            {
                return sItemDesc.ToString();
            }
            catch (System.Exception ex)
            {
                //Just throw the exception
                throw new System.Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// property for item line number
        /// </summary>
        public int ILineItemNum { get => iLineItemNum; set => iLineItemNum = value; }
        /// <summary>
        /// property for item code
        /// </summary>
        public string SItemCode { get => sItemCode; set => sItemCode = value; }
        /// <summary>
        /// property for item description
        /// </summary>
        public string SItemDesc { get => sItemDesc; set => sItemDesc = value; }
        /// <summary>
        /// property for item cost
        /// </summary>
        public int ICost { get => iCost; set => iCost = value; }
        #endregion
    }
}
