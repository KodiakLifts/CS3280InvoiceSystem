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
        /// <summary>
        /// A form to view and edit the Items.
        /// </summary>
        wndItems wndItemsForm;
        /// <summary>
        /// A form to search for invoices.
        /// </summary>
        wndSearch wndSearchForm;
        /// <summary>
        /// The current invoice being edited
        /// </summary>
        int iCurrentInvoice;
        #endregion

        public wndMain()
        {
            InitializeComponent();

            //allow application to close
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            //Instantiate Window Objects
            wndItemsForm = new wndItems();
            wndSearchForm = new wndSearch();
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
            //iCurrentInvoice = wndSearchForm.iSelectedInvoice;
            //txtInvoiceNumber.Text = iCurrentInvoice.ToString();
            this.Show();
        }

        private void btnCreateInvoice_Click(object sender, RoutedEventArgs e)
        {
            txtInvoiceNumber.Text = "TBD";
        }

        private void btnSaveInvoice_Click(object sender, RoutedEventArgs e)
        {
            int iInvoiceNumber; //will equal max invoice number.
            string sInvoiceDate = dateInvoiceDate.SelectedDate.Value.Date.ToShortDateString();


        }
    }
}
