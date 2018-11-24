using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3280InvoiceSystem.Main
{
    class clsMainSQL
    {
        clsDataAccess db = new clsDataAccess();
        clsInvoice oInvoice;
        private string sSqlStatement;
        int iRet = 0;

        /// <summary>
        /// returns a table with the items and their prices for a specific invoice
        /// </summary>
        /// <param name="pInvoiceNumber"></param>
        /// <returns></returns>
        public DataTable getItemsAndPrices(int pInvoiceNumber)
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

        /// <summary>
        /// returns a table with all the invoice information
        /// </summary>
        /// <param name="pInvoiceNumber"></param>
        /// <returns></returns>
        public clsInvoice getInvoiceInfo(int pInvoiceNumber)
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

        /// <summary>
        /// Gets the max invoice number
        /// </summary>
        /// <returns></returns>
        public int getMaxInvoice()
        {
            //Local Variables
            DataSet ds;
            iRet = 0;
            sSqlStatement = @"SELECT MAX(InvoiceNum)
                FROM Invoices";

            //query the database for the Max Invoice Number
            ds = db.ExecuteSQLStatement(sSqlStatement, ref iRet);
            return Int32.Parse(ds.Tables[0].Rows[0][0].ToString()) +1;
        }

        /// <summary>
        /// Updates the Database With the new Invoice And Lineitem data
        /// </summary>
        /// <param name="oInvoice"></param>
        public void updateDataBase(clsInvoice oInvoice)
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

            //Add New Invoice
            db.ExecuteNonQuery("INSERT INTO Invoices VALUES ("
                + oInvoice.IInvoiceNumber + ","
                + oInvoice.DateInvoiceDate + ","
                + oInvoice.ITotalCost + ")");
            //Add All New Line Items
            foreach (var item in oInvoice.LItems)
            {
                db.ExecuteNonQuery("INSERT INTO LineItems(InvoiceNum, LineItemNum, ItemCode) VALUES(" +
                    oInvoice.IInvoiceNumber + "," + item.ILineItemNum + "," + item.SItemCode +
                    ")");

            }
        }

        public void deleteInvoice(clsInvoice oInvoice)
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

        /// <summary>
        /// returns a list of all items in the database
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<clsItem> getItems()
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
    }
}
