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

        //returns a table with the items and their prices for a specific invoice
        public DataTable getItemsAndPrices(int pInvoiceNumber)
        {
            //Local Variables
            DataSet ds;
            DataTable dt = new DataTable();
            iRet = 0;
            sSqlStatement = @"SELECT ItemDesc.ItemDesc, Cost
                FROM ItemDesc
                INNER JOIN LineItems ON LineItems.ItemCode = ItemDesc.ItemCode
                WHERE LineItems.InvoiceNum = " + pInvoiceNumber;


            //query the database for the items and prices
            ds = db.ExecuteSQLStatement(sSqlStatement, ref iRet);
            return ds.Tables[0];
        }

        //returns a table with all the invoice information
        public clsInvoice getInvoiceInfo(int pInvoiceNumber)
        {
            //Local Variables
            DataSet ds;
            iRet = 0;
            sSqlStatement = @"SELECT InvoiceNum, InvoiceDate, TotalCost
            FROM Invoices
            WHERE InvoiceNum = 5000";

            //query the database for the items and prices
            ds = db.ExecuteSQLStatement(sSqlStatement, ref iRet);
            oInvoice = new clsInvoice(Int32.Parse(ds.Tables[0].Rows[0][0].ToString()), DateTime.Parse(ds.Tables[0].Rows[0][1].ToString()), Int32.Parse(ds.Tables[0].Rows[0][2].ToString()));
            return oInvoice;
        }
    }
}
