using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3280InvoiceSystem.Main
{
    class clsMainLogic
    {
        #region Class Variables
        /// <summary>
        /// Executes sql statements
        /// </summary>
        private clsMainSQL oSQL = new clsMainSQL();
        /// <summary>
        /// A list of the current available items.
        /// </summary>
        private ObservableCollection<clsItem> lItems = new ObservableCollection<clsItem>();
        /// <summary>
        /// A list of the item descriptions on current invoice
        /// </summary>
        ObservableCollection<string> lInvoiceItems = new ObservableCollection<string>();
        /// <summary>
        /// The current invoice being edited
        /// </summary>
        int iCurrentInvoice;
        /// <summary>
        /// The Invoice currently being edited
        /// </summary>
        clsInvoice oInvoice;
        /// <summary>
        /// The current item line number
        /// </summary>
        int iLineItemNumber = 0;
        #endregion

        #region Methods
        /// <summary>
        /// Constructor to initialize all the lists
        /// </summary>
        public clsMainLogic()
        {
            LItems = oSQL.getItems();
        }

        public ObservableCollection<string> fillInvoiceItems(int invoiceId)
        {
            foreach (var item in oSQL.getInvoiceItems(invoiceId))
            {
                LInvoiceItems.Add(item.SItemDesc);
            }
            return LInvoiceItems;
        }

        /// <summary>
        /// Property to get and set the list of items
        /// </summary>
        public ObservableCollection<clsItem> LItems { get => lItems; set => lItems = value; }
        /// <summary>
        /// property to get and set the list of item descriptions on current invoice
        /// </summary>
        public ObservableCollection<string> LInvoiceItems { get => lInvoiceItems; set => lInvoiceItems = value; }
        /// <summary>
        /// property to get and set the current invoice number
        /// </summary>
        public int ICurrentInvoice { get => iCurrentInvoice; set => iCurrentInvoice = value; }
        /// <summary>
        /// property to get and set the current invoice
        /// </summary>
        public clsInvoice OInvoice { get => oInvoice; set => oInvoice = value; }


        /// <summary>
        /// Updates the invoice with all the old invoice information
        /// </summary>
        public void updateOldInvoice(int pCurrentInvoice)
        {
            iCurrentInvoice = pCurrentInvoice;
            oInvoice = oSQL.getInvoiceInfo(iCurrentInvoice);
            iLineItemNumber = oInvoice.LItems.Count();
        }
        /// <summary>
        /// Updates the invoice with all the new invoice information
        /// </summary>
        public void updateNewInvoice()
        {
            iCurrentInvoice = oSQL.getMaxInvoice();
            oInvoice = new clsInvoice(ICurrentInvoice);
            iLineItemNumber = 0;
        }

        public void addItem(clsItem oSelectedItem)
        {
                //incrimient and set the line item number
                iLineItemNumber++;


                //This is changing every item in the invoices item list to ilineitemnumber? why?
                oSelectedItem.ILineItemNum = iLineItemNumber;


                //add the item to the invoice
                oInvoice.LItems.Add(oSelectedItem);
                //set the total cost to the calculated total cost
                oInvoice.ITotalCost += oSelectedItem.ICost;
                //set the items lineitemnumber to the current lineitemnumber
                oInvoice.LItems[oInvoice.LItems.Count - 1].ILineItemNum = iLineItemNumber;
        }

        public void deleteItem(clsItem selectedItem)
        {
                    //set the total cost to the calculated total cost
                    oInvoice.ITotalCost -= selectedItem.ICost;
                    //Delete item from invoice
                    oInvoice.LItems.Remove(selectedItem);
                    return;
        }

        #endregion
    }
}
