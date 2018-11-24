using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS3280InvoiceSystem.Main;

namespace CS3280InvoiceSystem
{
    public class clsInvoice
    {
        clsMainSQL oMainSql = new clsMainSQL();
        private int iInvoiceNumber;
        private DateTime dateInvoiceDate;
        private int iTotalCost;
        private ObservableCollection<clsItem> lItems = new ObservableCollection<clsItem>();
        


        public clsInvoice(int iInvoiceNumber, DateTime dateInvoiceDate, int iTotalCost)
        {
            this.iInvoiceNumber = iInvoiceNumber;
            this.dateInvoiceDate = dateInvoiceDate;
            this.iTotalCost = iTotalCost;

            clsItem oItem;
            DataTable dt = oMainSql.getItemsAndPrices(iInvoiceNumber);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                oItem = new clsItem(Int32.Parse(dt.Rows[i][0].ToString()), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), Int32.Parse(dt.Rows[i][3].ToString()));
                lItems.Add(oItem);
            }
        }

        public clsInvoice(DateTime dateInvoiceDate, int iTotalCost, ObservableCollection<clsItem> lItems)
        {
            this.dateInvoiceDate = dateInvoiceDate;
            this.iTotalCost = iTotalCost;
            this.lItems = lItems;
        }

        public clsInvoice(int iInvoiceNumber, DateTime dateInvoiceDate, int iTotalCost, ObservableCollection<clsItem> lItems)
        {
            this.iInvoiceNumber = iInvoiceNumber;
            this.dateInvoiceDate = dateInvoiceDate;
            this.iTotalCost = iTotalCost;
            this.lItems = lItems;
        }

        public clsInvoice(int iInvoiceNumber)
        {
            this.iInvoiceNumber = iInvoiceNumber;
            this.iTotalCost = 0;
            this.lItems = new ObservableCollection<clsItem>();
        }

        public int IInvoiceNumber { get => iInvoiceNumber; set => iInvoiceNumber = value; }
        public DateTime DateInvoiceDate { get => dateInvoiceDate; set => dateInvoiceDate = value; }
        public int ITotalCost { get => iTotalCost; set => iTotalCost = value; }
        internal ObservableCollection<clsItem> LItems { get => lItems; set => lItems = value; }
    }
}
