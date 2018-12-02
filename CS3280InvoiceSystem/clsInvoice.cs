using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CS3280InvoiceSystem.Main;

namespace CS3280InvoiceSystem
{
    public class clsInvoice
    {
        /// <summary>
        /// 
        /// </summary>
        private int iInvoiceNumber;
        /// <summary>
        /// The date the invoice was created
        /// </summary>
        private DateTime dateInvoiceDate;
        /// <summary>
        /// the total cost of the invoice
        /// </summary>
        private int iTotalCost;
        /// <summary>
        /// the items contained in the invoice
        /// </summary>
        private ObservableCollection<clsItem> lItems = new ObservableCollection<clsItem>();
        

        /// <summary>
        /// Constructor for an invoice without a list of items
        /// </summary>
        /// <param name="iInvoiceNumber"></param>
        /// <param name="dateInvoiceDate"></param>
        /// <param name="iTotalCost"></param>
        public clsInvoice(int iInvoiceNumber, DateTime dateInvoiceDate, int iTotalCost)
        {
            try
            {
                this.iInvoiceNumber = iInvoiceNumber;
                this.dateInvoiceDate = dateInvoiceDate;
                this.iTotalCost = iTotalCost;
            }
            catch (System.Exception ex)
            {
                //Just throw the exception
                throw new System.Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Constructor for an invoice number
        /// </summary>
        /// <param name="iInvoiceNumber"></param>
        public clsInvoice(int iInvoiceNumber)
        {
            try
            {
                this.iInvoiceNumber = iInvoiceNumber;
                this.iTotalCost = 0;
                this.lItems = new ObservableCollection<clsItem>();
            }
            catch (System.Exception ex)
            {
                //Just throw the exception
                throw new System.Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// property for invoice number
        /// </summary>
        public int IInvoiceNumber { get => iInvoiceNumber; set => iInvoiceNumber = value; }
        /// <summary>
        /// property for invoice date
        /// </summary>
        public DateTime DateInvoiceDate { get => dateInvoiceDate; set => dateInvoiceDate = value; }
        /// <summary>
        /// property for totalcost
        /// </summary>
        public int ITotalCost { get => iTotalCost; set => iTotalCost = value; }
        /// <summary>
        /// property for invoice item list
        /// </summary>
        internal ObservableCollection<clsItem> LItems { get => lItems; set => lItems = value; }
    }
}
