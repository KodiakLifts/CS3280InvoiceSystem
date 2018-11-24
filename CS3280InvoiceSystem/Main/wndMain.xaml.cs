using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
        /// <summary>
        /// Tells whether it is a new invoice being created or an old one being selected
        /// </summary>
        bool bIsNewInvoice;

        ObservableCollection<clsItem> lItems = new ObservableCollection<clsItem>();
        #endregion

        #region Methods
        /// <summary>
        /// Loads up the main Window
        /// </summary>
        public wndMain()
        {
            InitializeComponent();

            //allow application to close
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            //Instantiate Window Objects
            wndItemsForm = new wndItems();
            wndSearchForm = new wndSearch();
            clsSql = new clsMainSQL();
            lItems = clsSql.getItems();
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

        /// <summary>
        /// Creates a new Invoice. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateInvoice_Click(object sender, RoutedEventArgs e)
        {
            //Setting currentinvoice to -1 signifies that a new invoice is being created instead of selected form the search form.
            bIsNewInvoice = true;
            populateInvoiceInfo();
        }

        /// <summary>
        /// Saves User input Into Invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (dateInvoiceDate.SelectedDate != null && txtTotalCost.Text != "")
            {
                MessageBox.Show("good");
                //    DateTime InvoiceDate = dateInvoiceDate.SelectedDate.Value.Date;
                //    int iTotalCost = -1;Int32.Parse(txtTotalCost.Text);
                //    ObservableCollection<clsItem> lItems = new ObservableCollection<clsItem>();
                //    //Get List of items in invoice
                //    foreach (var item in dgItemList.Items)
                //    {
                //        lItems.Add((clsItem)item);
                //    }
                //    //Create Invoice Object with new Information
                //    oInvoice = new clsInvoice(iCurrentInvoice, InvoiceDate, iTotalCost, lItems);

                //    //Update Database
                //    clsSql.updateDataBase(oInvoice);
            }
            else
            {
                MessageBox.Show("bad");
            }
        }


        #region helperFunctions
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

            //Create lists to populate item combo box
            ObservableCollection<clsItem> lItems = new ObservableCollection<clsItem>();
            lItems = clsSql.getItems();
            ObservableCollection<string> lItemDesc = new ObservableCollection<string>();
            foreach (var item in lItems)
            {
                lItemDesc.Add(item.SItemDesc);
            }
            cbItems.ItemsSource = lItemDesc;

            //Set text box values for invoice if it not a New Invoice being created
            if (!bIsNewInvoice)
            {
                oInvoice = clsSql.getInvoiceInfo(iCurrentInvoice);
                txtInvoiceNumber.Text = oInvoice.IInvoiceNumber.ToString();
                dateInvoiceDate.SelectedDate = oInvoice.DateInvoiceDate;
                txtTotalCost.Text = oInvoice.ITotalCost.ToString();
                //Populate datagrid with items and prices
                dgItemList.ItemsSource = oInvoice.LItems;
            }
            else
            {
                clearSelection();
                iCurrentInvoice = clsSql.getMaxInvoice();
                txtInvoiceNumber.Text = iCurrentInvoice.ToString();
            }
        }

        /// <summary>
        /// Clears the UI
        /// </summary>
        void clearSelection()
        {
            iCurrentInvoice = -1;
            txtInvoiceNumber.Text = "";
            dateInvoiceDate.SelectedDate = null;
            txtTotalCost.Text = "";
            //Populate datagrid with items and prices
            dgItemList.ItemsSource = null;
        }
        #endregion

        #endregion

        /// <summary>
        /// Displays the cost of the item selected in the combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in lItems)
            {
                if (cbItems.SelectedItem.ToString() == item.SItemDesc)
                {
                    txtCost.Text = item.ICost.ToString();
                }
            } 
        }
    }
}
