using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
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
            try
            {
                lItems = oSQL.getItems();
            }
            catch (System.Exception ex)
            {
                //Just throw the exception
                throw new System.Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// Property to get and set the list of items
        /// </summary>
        public ObservableCollection<clsItem> LItems { get => lItems; set => lItems = value; }
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
            try
            {
                iCurrentInvoice = pCurrentInvoice;
                oInvoice = oSQL.getInvoiceInfo(iCurrentInvoice);
                iLineItemNumber = oInvoice.LItems.Count();
            }
            catch (System.Exception ex)
            {
                //Just throw the exception
                throw new System.Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Updates the invoice with all the new invoice information
        /// </summary>
        public void updateNewInvoice()
        {
            try
            {
                iCurrentInvoice = oSQL.getMaxInvoice();
                oInvoice = new clsInvoice(ICurrentInvoice);
                iLineItemNumber = 0;
            }
            catch (System.Exception ex)
            {
                //Just throw the exception
                throw new System.Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Adds an item to the the current invoice
        /// </summary>
        /// <param name="oSelectedItem"></param>
        public void addItem(clsItem oSelectedItem)
        {
            try
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
            catch (System.Exception ex)
            {
                //Just throw the exception
                throw new System.Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// deletes an item from the current invoice
        /// </summary>
        /// <param name="selectedItem"></param>
        public void deleteItem(clsItem selectedItem)
        {
            try
            {
                //set the total cost to the calculated total cost
                oInvoice.ITotalCost -= selectedItem.ICost;
                //Delete item from invoice
                oInvoice.LItems.Remove(selectedItem);
                return;
            }
            catch (System.Exception ex)
            {
                //Just throw the exception
                throw new System.Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// adds the current invoice to the DB
        /// </summary>
        public void addInvoiceToDB()
        {
            try
            {
                oSQL.addNewInvoiceToDB(oInvoice);
            }
            catch (System.Exception ex)
            {
                //Just throw the exception
                throw new System.Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes the current invoice from the DB
        /// </summary>
        public void delInvoiceFromDB()
        {
            try
            {
                oSQL.deleteInvoice(oInvoice);
            }
            catch (System.Exception ex)
            {
                //Just throw the exception
                throw new System.Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Updates the current invoice in the DB
        /// </summary>
        public void updateInvoiceFromDB()
        {
            try
            {
                oSQL.editInvoiceFromDB(oInvoice);
            }
            catch (System.Exception ex)
            {
                //Just throw the exception
                throw new System.Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion
    }
}
