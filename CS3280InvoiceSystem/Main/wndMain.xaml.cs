using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CS3280InvoiceSystem.Items;
using CS3280InvoiceSystem.Search;

namespace CS3280InvoiceSystem.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        #region Global Variables
        clsMainSQL clsSql;
        /// <summary>
        /// A form to view and edit the Items.
        /// </summary>
        wndItems wndItemsForm;
        /// <summary>
        /// A form to search for invoices.
        /// </summary>
        wndSearch wndSearchForm;
        clsInvoice oInvoice;
        /// <summary>
        /// The current invoice being edited
        /// </summary>
        int iCurrentInvoice;
        bool bIsNewInvoice;
        #endregion

        public wndMain()
        {
            InitializeComponent();

            //allow application to close
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            //Instantiate Window Objects
            wndItemsForm = new wndItems();
            wndSearchForm = new wndSearch();
            clsSql = new clsMainSQL();
        }

        /// <summary>
        /// Opens up the Window Items Form
        /// Look at items.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miDefTable_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            wndItemsForm.ShowDialog();
            this.Show();
        }

        /// <summary>
        /// Opens up the Window Search Form
        /// Search for invoices.
        /// Selects
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miSearchInv_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            wndSearchForm.ShowDialog();

            //Retrieve information from search form
            iCurrentInvoice = wndSearchForm.getSelectedInvoiceId();
            bIsNewInvoice = false;
            populateInvoiceInfo();
            //Finished retreive

            this.Show();
        }

        private void btnCreateInvoice_Click(object sender, RoutedEventArgs e)
        {
            //Setting currentinvoice to -1 signifies that a new invoice is being created instead of selected form the search form.
            bIsNewInvoice = true;
            populateInvoiceInfo();
        }

        private void btnSaveInvoice_Click(object sender, RoutedEventArgs e)
        {
            string sInvoiceDate = dateInvoiceDate.SelectedDate.Value.Date.ToShortDateString();


        }

        /// <summary>
        /// Populates the Invoice UI information, Does not error check
        /// </summary>
        /// <param name="pInvoiceNumber"></param>
        /// <param name="pInvoiceDate"></param>
        /// <param name="pInvoiceTotal"></param>
        /// <param name="pItemCode"></param>
        void populateInvoiceInfo()
        {
            //Enable Editing for Invoice
            btnSaveInvoice.IsEnabled = true;
            cbItems.IsEnabled = true;
            dateInvoiceDate.IsEnabled = true;
            btnAddItem.IsEnabled = true;
            btnDeleteItem.IsEnabled = true;

            //Set text box values for invoice if it not a New Invoice being created
            if (!bIsNewInvoice)
            {
                oInvoice = clsSql.getInvoiceInfo(iCurrentInvoice);
                txtInvoiceNumber.Text = oInvoice.IInvoiceNumber.ToString();
                dateInvoiceDate.SelectedDate = oInvoice.DateInvoiceDate;
                txtTotalCost.Text = oInvoice.ITotalCost.ToString();
                //Populate datagrid with items and prices
                dgItemList.ItemsSource = clsSql.getItemsAndPrices(oInvoice.IInvoiceNumber).DefaultView;
            }
            else
            {
                txtInvoiceNumber.Text = "TBD";
            }
        }
    }
}
