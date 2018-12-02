using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CS3280InvoiceSystem.Search
{
    /// <summary>
    /// Class for retrieiving all relevent search data from the database.
    /// </summary>
    class clsSearchSQL
    {
        #region Class Members
        /// <summary>
        /// Used to access database.
        /// </summary>
        clsDataAccess db;
        #endregion

        #region Public Methods
        public clsSearchSQL()
        {
            db = new clsDataAccess();
        }

        /// <summary>
        /// Queries database for all invoices.
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet getAllInvoices()
        {
            try
            {
                DataSet ds;

                int iRet = 0;

                ds = db.ExecuteSQLStatement(
                    "SELECT * FROM Invoices", ref iRet
                );

                return ds;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Queries database for all invoices that match the given InvoiceNumber, InvoiceDate, and TotalCost.
        /// Set number to -1 if no number selected.
        /// Set date to null if no date selected.
        /// Set total to -1 if no total selected.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="date"></param>
        /// <param name="total"></param>
        /// <returns>Data table for DataGrid</returns>
        public DataSet searchInvoices(int number, string date, int total)
        {
            try
            {
                if (number == -1 && date == null && total == -1)
                {
                    return null;
                }

                DataSet ds;

                int iRet = 0;

                string sql = "SELECT * FROM Invoices WHERE ";
                if (number != -1)
                {
                    sql += ("InvoiceNum = " + number);
                }
                if (date != null)
                {
                    if (number != -1)
                    {
                        sql += " AND ";
                    }
                    sql += ("InvoiceDate = #" + date + "#");
                }
                if (total != -1)
                {
                    if (number != -1 || date != null)
                    {
                        sql += " AND ";
                    }
                    sql += ("TotalCost = " + total);
                }

                ds = db.ExecuteSQLStatement(sql, ref iRet);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Queries database for Invoice Numbers sorted numerically.
        /// </summary>
        /// <returns>List of int</returns>
        public DataSet getInvoiceNumbers()
        {
            try
            {
                DataSet ds;

                int iRet = 0;

                ds = db.ExecuteSQLStatement(
                    "SELECT InvoiceNum FROM Invoices", ref iRet
                );

                return ds;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Queries database for all Invoice Dates sorted by date.
        /// </summary>
        /// <returns>List of DateTime</returns>
        public DataSet getInvoiceDates()
        {
            try
            {
                DataSet ds;
                int iRet = 0;

                

                ds = db.ExecuteSQLStatement(
                    "SELECT InvoiceDate FROM Invoices", ref iRet
                );

                return ds;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Queries database for all Invoice Total Charges and sorts by ammount.
        /// </summary>
        /// <returns>List of int</returns>
        public DataSet getInvoiceTotalCharges()
        {
            try
            {
                DataSet ds;
                int iRet = 0;

                ds = db.ExecuteSQLStatement(
                    "SELECT TotalCost FROM Invoices", ref iRet
                );

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
   
    }
    #endregion
}
