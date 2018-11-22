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
        /// <summary>
        /// SQL statement for retrieving all Invoice Numbers.
        /// </summary>
        public static string getInvoiceNumbers = "SELECT InvoiceNum FROM Invoices";
        /// <summary>
        /// SQL statement for retrieving all Invoice Dates.
        /// </summary>
        public static string getInvoiceDates = "SELECT InvoiceDate FROM Invoices";
        /// <summary>
        /// SQL statement for retrieving all Invoice Total Costs.
        /// </summary>
        public static string getInvoiceTotals = "SELECT TotalCost FROM Invoices";        
    }
}
