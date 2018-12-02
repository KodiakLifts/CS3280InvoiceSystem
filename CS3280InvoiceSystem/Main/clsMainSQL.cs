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
    class clsMainSQL
    {
        /// <summary>
        /// The database object
        /// </summary>
        clsDataAccess db = new clsDataAccess();
        /// <summary>
        /// An invoice to populate with data from DB
        /// </summary>
        clsInvoice oInvoice;
        /// <summary>
        /// the sql statment to run
        /// </summary>
        private string sSqlStatement;
        /// <summary>
        /// returns the ammount of rows
        /// </summary>
        int iRet = 0;


        /// <summary>
        /// returns a table with the items and their prices for a specific invoice
        /// </summary>
        /// <param name="pInvoiceNumber"></param>
        /// <returns></returns>
        public DataTable getItemsAndPrices(int pInvoiceNumber)
        {
            try
            {
                //Local Variables
                DataSet ds;
                DataTable dt = new DataTable();
                iRet = 0;
                sSqlStatement = @"SELECT LineItemNum, LineItems.ItemCode, ItemDesc.ItemDesc, Cost
                FROM ItemDesc
                INNER JOIN LineItems ON LineItems.ItemCode = ItemDesc.ItemCode
                WHERE LineItems.InvoiceNum = " + pInvoiceNumber;


                //query the database for the items and prices
                ds = db.ExecuteSQLStatement(sSqlStatement, ref iRet);
                return ds.Tables[0];
            }
            catch (System.Exception ex)
            {
                //Just throw the exception
                throw new System.Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// returns a table with all the invoice information
        /// </summary>
        /// <param name="pInvoiceNumber"></param>
        /// <returns></returns>
        public clsInvoice getInvoiceInfo(int pInvoiceNumber)
        {
            try
            {
                //Local Variables
                DataSet ds;
                iRet = 0;
                sSqlStatement = @"SELECT InvoiceNum, InvoiceDate, TotalCost
            FROM Invoices
            WHERE InvoiceNum = " + pInvoiceNumber;

                //query the database for the items and prices
                ds = db.ExecuteSQLStatement(sSqlStatement, ref iRet);
                oInvoice = new clsInvoice(Int32.Parse(ds.Tables[0].Rows[0][0].ToString()), DateTime.Parse(ds.Tables[0].Rows[0][1].ToString()), Int32.Parse(ds.Tables[0].Rows[0][2].ToString()));
                return oInvoice;
            }
            catch (System.Exception ex)
            {
                //Just throw the exception
                throw new System.Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets the max invoice number
        /// </summary>
        /// <returns></returns>
        public int getMaxInvoice()
        {
            try
            {
                //Local Variables
                DataSet ds;
                iRet = 0;
                sSqlStatement = @"SELECT MAX(InvoiceNum)
                FROM Invoices";

                //query the database for the Max Invoice Number
                ds = db.ExecuteSQLStatement(sSqlStatement, ref iRet);
                return Int32.Parse(ds.Tables[0].Rows[0][0].ToString()) + 1;
            }
            catch (System.Exception ex)
            {
                //Just throw the exception
                throw new System.Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// returns a list of all items in the database
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<clsItem> getItems()
        {
            try
            {
                ObservableCollection<clsItem> lItems = new ObservableCollection<clsItem>();
                sSqlStatement = "SELECT * FROM ItemDesc";
                iRet = 0;
                DataSet ds;

                ds = db.ExecuteSQLStatement(sSqlStatement, ref iRet);
                clsItem oItem;
                for (int i = 0; i < iRet; i++)
                {
                    oItem = new clsItem(-1, ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), Int32.Parse(ds.Tables[0].Rows[i][2].ToString()));
                    lItems.Add(oItem);
                }
                return lItems;
            }
            catch (System.Exception ex)
            {
                //Just throw the exception
                throw new System.Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets all the invoice items
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public ObservableCollection<clsItem> getInvoiceItems(int invoiceId)
        {
            try
            {
                ObservableCollection<clsItem> lItems = new ObservableCollection<clsItem>();
                sSqlStatement =
                    "SELECT L.LineItemNum, D.ItemCode, D.ItemDesc, D.Cost FROM ItemDesc AS D " +
                    "INNER JOIN LineItems AS L " +
                    "ON L.ItemCode = D.ItemCode " +
                    "WHERE L.InvoiceNum = " + invoiceId;
                iRet = 0;
                DataSet ds;

                ds = db.ExecuteSQLStatement(sSqlStatement, ref iRet);
                clsItem oItem;
                for (int i = 0; i < iRet; i++)
                {
                    oItem = new clsItem(Int32.Parse(ds.Tables[0].Rows[i][0].ToString()), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString(), Int32.Parse(ds.Tables[0].Rows[i][3].ToString()));
                    lItems.Add(oItem);
                }
                return lItems;
            }
            catch (System.Exception ex)
            {
                //Just throw the exception
                throw new System.Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Updates the Database With the new Invoice And Lineitem data
        /// </summary>
        /// <param name="oInvoice"></param>
        public void addNewInvoiceToDB(clsInvoice oInvoice)
        {
            //Add New Invoice
            try
            {
                //Create new invoice
                db.ExecuteNonQuery("INSERT INTO Invoices(InvoiceDate, TotalCost) VALUES (#"
                + oInvoice.DateInvoiceDate.ToShortDateString() + "#, "
                + oInvoice.ITotalCost + ")");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to add new invoice.");
                throw (ex);
            }

            //Add All New Line Items
            try
            {
                //get the invoice number
                int iInvoiceNumber = getMaxInvoice() -1;

                //Add the items for the invoice
                foreach (var item in oInvoice.LItems)
                {
                    db.ExecuteNonQuery("INSERT INTO LineItems(InvoiceNum, LineItemNum, ItemCode) VALUES(" +
                       iInvoiceNumber + ", " + item.ILineItemNum + ", '" + item.SItemCode.ToString() +
                        "')");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to add new line items.");
                throw (ex);
            }
        }

        /// <summary>
        /// deletes the invoice
        /// </summary>
        /// <param name="oInvoice"></param>
        public void deleteInvoice(clsInvoice oInvoice)
        {
            try
            {
                //Delete all Old line items
                foreach (var item in oInvoice.LItems)
                {
                    db.ExecuteNonQuery("DELETE FROM LineItems WHERE InvoiceNum = " + oInvoice.IInvoiceNumber +
                    " AND LineItemNum = " + item.ILineItemNum);
                }
                //Delete Old Invoice
                db.ExecuteNonQuery("DELETE FROM Invoices WHERE InvoiceNum = "
                    + oInvoice.IInvoiceNumber);
            }
            catch (System.Exception ex)
            {
                //Just throw the exception
                throw new System.Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// updates the invoice int he db
        /// </summary>
        /// <param name="oInvoice"></param>
        public void editInvoiceFromDB(clsInvoice oInvoice)
        {
            try
            {
                //Delete all Old line items
                foreach (var item in oInvoice.LItems)
                {
                    db.ExecuteNonQuery("DELETE FROM LineItems WHERE InvoiceNum = " + oInvoice.IInvoiceNumber +
                    " AND LineItemNum = " + item.ILineItemNum);
                }
                //Add new Line Items
                foreach (var item in oInvoice.LItems)
                {
                    db.ExecuteNonQuery("INSERT INTO LineItems(InvoiceNum, LineItemNum, ItemCode) VALUES(" +
                       oInvoice.IInvoiceNumber + ", " + item.ILineItemNum + ", '" + item.SItemCode.ToString() +
                        "')");
                }
                //update Invoice
                db.ExecuteNonQuery("UPDATE Invoices SET InvoiceDate = #" + oInvoice.DateInvoiceDate.ToShortDateString() + "#, TotalCost = " + oInvoice.ITotalCost +
                    " WHERE InvoiceNum = " + oInvoice.IInvoiceNumber);
            }
            catch (System.Exception ex)
            {
                //Just throw the exception
                throw new System.Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
