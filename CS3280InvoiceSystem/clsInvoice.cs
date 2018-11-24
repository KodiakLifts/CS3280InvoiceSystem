using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3280InvoiceSystem
{
    public class clsInvoice
    {
        private int iInvoiceNumber;
        private DateTime dateInvoiceDate;
        private int iTotalCost;

        public clsInvoice(int iInvoiceNumber, DateTime dateInvoiceDate, int iTotalCost)
        {
            this.iInvoiceNumber = iInvoiceNumber;
            this.dateInvoiceDate = dateInvoiceDate;
            this.iTotalCost = iTotalCost;
        }

        public int IInvoiceNumber { get => iInvoiceNumber; set => iInvoiceNumber = value; }
        public DateTime DateInvoiceDate { get => dateInvoiceDate; set => dateInvoiceDate = value; }
        public int ITotalCost { get => iTotalCost; set => iTotalCost = value; }
    }
}
