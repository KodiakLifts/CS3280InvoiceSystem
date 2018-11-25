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
    class clsSearchLogic
    {
        /// <summary>
        /// SQL class for executing queries to database.
        /// </summary>
        clsSearchSQL sql;
        /// <summary>
        /// Tells whether a single invoice is the result of the searchInvoices query.
        /// </summary>
        bool invoiceFound;
        /// <summary>
        /// If single invoice found stored here.
        /// </summary>
        int selectedInvoiceId;

        public clsSearchLogic()
        {
            sql = new clsSearchSQL();
            invoiceFound = false;
            selectedInvoiceId = -1;
        }

        /// <summary>
        /// Returns true or false if a single invoice has been found.
        /// </summary>
        /// <returns>bool</returns>
        public bool getInvoiceFound()
        {
            return invoiceFound;
        }

        /// <summary>
        /// Returns selected invoice ID. -1 if none found.
        /// </summary>
        /// <returns>int</returns>
        public int getSelectedInvoiceId()
        {
            return selectedInvoiceId;
        }

        /// <summary>
        /// Returns a data table containing all invoices that match given parameters.
        /// Set number to -1 if no number selected.
        /// Set date to null if no date selected.
        /// Set total to -1 if no total selected.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="date"></param>
        /// <param name="total"></param>
        /// <returns>Data table for DataGrid</returns>
        public DataTable searchInvoices(int number, string date, int total)
        {
            try
            {
                DataSet ds;

                ds = sql.searchInvoices(number, date, total);

                if(ds.Tables[0].Rows.Count == 1)
                {
                    invoiceFound = true;
                    selectedInvoiceId = ds.Tables[0].Rows[0].Field<int>("InvoiceNum");
                }
                else
                {
                    invoiceFound = false;
                    selectedInvoiceId = -1;
                }
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Returns an ordered list of all invoice numbers.
        /// </summary>
        /// <returns>List of int</returns>
        public List<int> getInvoiceNumbers()
        {
            try
            {
                DataSet ds;

                ds = sql.getInvoiceNumbers();

                List<int> invoiceNumbers = new List<int>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    int n;
                    try
                    {
                        n = Int32.Parse(dr[0].ToString());
                        invoiceNumbers.Add(n);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                invoiceNumbers.Sort();
                return invoiceNumbers;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Returns ordered list of all invoice dates.
        /// </summary>
        /// <returns>List of DateTime</returns>
        public List<string> getInvoiceDates()
        {
            try
            {
                DataSet ds;
               

                List<string> invoiceDates = new List<string>();

                ds = sql.getInvoiceDates();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string d;
                    DateTime dt;
                    try
                    {
                        dt = (DateTime)dr[0];
                        d = dt.ToString("M/dd/yyy");
                        invoiceDates.Add(d);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                invoiceDates.Sort();

                return invoiceDates;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Returns an ordered list of all invoice total charges.
        /// </summary>
        /// <returns>List of int</returns>
        public List<int> getInvoiceTotalCharges()
        {
            try
            {
                DataSet ds;

                List<int> invoiceTotals = new List<int>();

                ds = sql.getInvoiceTotalCharges();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    int t;
                    try
                    {
                        t = Int32.Parse(dr[0].ToString());
                        invoiceTotals.Add(t);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                invoiceTotals.Sort();

                return invoiceTotals;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
