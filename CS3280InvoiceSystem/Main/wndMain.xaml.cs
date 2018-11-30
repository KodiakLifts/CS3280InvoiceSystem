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
        /// <summary>
        /// A form to view and edit the Items.
        /// </summary>
        wndItems wndItemsForm;
        /// <summary>
        /// A form to search for invoices.
        /// </summary>
        wndSearch wndSearchForm;
        /// <summary>
        /// Contains the logic behind the UI
        /// </summary>
        clsMainLogic oMainLogic;
        /// <summary>
        /// Tells whether it is a new invoice being created or an old one being selected
        /// </summary>
        bool bIsNewInvoice;
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
            oMainLogic = new clsMainLogic();
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
            //Populate the invoice with oldInvoice
            if(wndSearchForm.getSelectedInvoiceId() != -1)
            {
                bIsNewInvoice = false;
                updateUI();
            }
            
            //Finished retreive
            this.Show();
        }


        /// <summary>
        /// Displays the cost of the item selected in the combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in oMainLogic.LItems)
            {
                if (cbItems.SelectedItem.ToString() == item.SItemDesc)
                {
                    txtCost.Text = item.ICost.ToString();
                }
            }
        }

        /// <summary>
        /// Add the item selected in the combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            //add the item from the combobox to the invoice
            if (cbItems.SelectedItem != null)
            {
                clsItem selectedItem = (clsItem)cbItems.SelectedItem;
                oMainLogic.addItem(selectedItem);
                resetUI();
            }
        }
     
        /// <summary>
        /// Delete the item selected in the datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            //Delete the item from the combobox and the invoice
            if (dgItemList.SelectedCells != null && dgItemList.SelectedIndex != -1)
            {
                var currentRowIndex = dgItemList.SelectedIndex;
                clsItem selectedItem = ((clsItem)dgItemList.SelectedCells[currentRowIndex].Item);
                oMainLogic.deleteItem(selectedItem);
                resetUI();
            }
        }


        /// <summary>
        /// Creates a new Invoice. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateInvoice_Click(object sender, RoutedEventArgs e)
        {
            bIsNewInvoice = true;
            updateUI();
        }

        /// <summary>
        /// Edit the invoice Information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditInvoice_Click(object sender, RoutedEventArgs e)
        {
            enableUI();
        }

        /// <summary>
        /// Saves User input Into Invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (dateInvoiceDate.SelectedDate != null && txtTotalCost.Text != "0")
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
                //    oMainLogic.OInvoice = new clsInvoice(iCurrentInvoice, InvoiceDate, iTotalCost, lItems);

                //    //Update Database
                //    oSQL.updateDataBase(oInvoice);
                disableUI();
            }
            else
            {
                MessageBox.Show("bad");
            }
        }

        /// <summary>
        /// .Delete The current invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteInvoice_Click(object sender, RoutedEventArgs e)
        {
            ////delete object from database
            //oSQL.deleteInvoice(oMainLogic.OInvoice);
            
            //reset ui
            bIsNewInvoice = true;
            updateUI();
            btnEditInvoice.IsEnabled = false;
            btnDeleteInvoice.IsEnabled = false;
            btnCreateInvoice.IsEnabled = true;
            txtInvoiceNumber.Text = "";
        }


        #region helperFunctions
        /// <summary>
        /// Updates the UI and Invoice Information for new and old invoices.
        /// </summary>
        /// <param name="pInvoiceNumber"></param>
        /// <param name="pInvoiceDate"></param>
        /// <param name="pInvoiceTotal"></param>
        /// <param name="pItemCode"></param>
        void updateUI()
        {
            //Enable Editing for Invoice
            enableUI();

            //Populate the item combo box with item descriptions
            cbItems.ItemsSource =  oMainLogic.LItems;
            //Populate the delete items combo box from the database
            //TODO populate based off of clsInvoice.
            if(wndSearchForm.getSelectedInvoiceId() != -1)
            {
                cbDeleteItems.ItemsSource = oMainLogic.fillInvoiceItems(wndSearchForm.getSelectedInvoiceId());
            }
            

            //Set text box values for invoice if it not a New Invoice being created
            if (!bIsNewInvoice)
            {
                oMainLogic.updateOldInvoice(wndSearchForm.getSelectedInvoiceId());
            }
            else
            {
                oMainLogic.updateNewInvoice();
            }

            //Fill UI
            resetUI();
        }

        /// <summary>
        /// Resets the info in the UI
        /// </summary>
        void resetUI()
        {
            txtInvoiceNumber.Text = oMainLogic.OInvoice.IInvoiceNumber.ToString();
            if (oMainLogic.OInvoice.DateInvoiceDate.ToShortDateString() == "1/1/0001")
            {
                dateInvoiceDate.SelectedDate = null;
            }
            else
            {
                dateInvoiceDate.SelectedDate = oMainLogic.OInvoice.DateInvoiceDate;
            }
                txtTotalCost.Text = oMainLogic.OInvoice.ITotalCost.ToString();
            //Populate datagrid with items and prices
            dgItemList.ItemsSource = oMainLogic.OInvoice.LItems;
        }

        /// <summary>
        /// Enables the controls in the UI
        /// </summary>
        void enableUI()
        {
            btnSaveInvoice.IsEnabled = true;
            cbItems.IsEnabled = true;
            dateInvoiceDate.IsEnabled = true;
            btnAddItem.IsEnabled = true;
            btnDeleteItem.IsEnabled = true;
            cbItems.IsEnabled = true;

            btnEditInvoice.IsEnabled = false;
            btnDeleteInvoice.IsEnabled = false;
        }

        /// <summary>
        /// Enables the controls in the UI
        /// </summary>
        void disableUI()
        {
            btnSaveInvoice.IsEnabled = false;
            cbItems.IsEnabled = false;
            dateInvoiceDate.IsEnabled = false;
            btnAddItem.IsEnabled = false;
            btnDeleteItem.IsEnabled = false;
            btnCreateInvoice.IsEnabled = false;
            cbItems.IsEnabled = false;

            btnEditInvoice.IsEnabled = true;
            btnDeleteInvoice.IsEnabled = true;
        }
        #endregion
        #endregion
    }
}
