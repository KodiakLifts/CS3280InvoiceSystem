using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
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
        /// <summary>
        /// If the edit invoice button is clicked
        /// </summary>
        bool bIsEditInvoice = false;
        #endregion


        #region METHODS
        /// <summary>
        /// Loads up the main Window
        /// </summary>
        public wndMain()
        {
            try
            {
                InitializeComponent();

                //allow application to close
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                //Instantiate Window Objects
                wndItemsForm = new wndItems();
                wndSearchForm = new wndSearch();
                oMainLogic = new clsMainLogic();
            }
            catch (Exception ex)
            {
                HandleError.handleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Opens up the Window Items Form
        /// Look at items.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miDefTable_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
                wndItemsForm.ShowDialog();
                this.Show();
            }
            catch (Exception ex)
            {
                HandleError.handleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
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
            try
            {
                this.Hide();
                wndSearchForm.updateSearchOptions();
                wndSearchForm.ShowDialog();
                //Populate the invoice with oldInvoice
                if (wndSearchForm.getSelectedInvoiceId() != -1)
                {
                    bIsNewInvoice = false;
                    updateUI();
                }
                disableUI();

                //Finished retreive
                this.Show();
            }
            catch (Exception ex)
            {
                HandleError.handleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        #region ITEMS
        /// <summary>
        /// Displays the cost of the item selected in the combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                txtCost.Text = ((clsItem)cbItems.SelectedItem).ICost.ToString();
            }
            catch (Exception ex)
            {
                HandleError.handleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Add the item selected in the combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //add the item from the combobox to the invoice
                if (cbItems.SelectedItem != null)
                {
                    clsItem selectedItem = new clsItem((clsItem)cbItems.SelectedItem);
                    oMainLogic.addItem(selectedItem);
                    resetUI();
                }
            }
            catch (Exception ex)
            {
                HandleError.handleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Delete the item selected in the datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                HandleError.handleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        #endregion


        #region INVOICE
        /// <summary>
        /// Creates a new Invoice. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bIsEditInvoice = false;
                bIsNewInvoice = true;
                updateUI();
            }
            catch (Exception ex)
            {
                HandleError.handleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Edit the invoice Information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                enableUI();
                bIsEditInvoice = true;
            }
            catch (Exception ex)
            {
                HandleError.handleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Saves User input Into Invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dateInvoiceDate.SelectedDate != null && txtTotalCost.Text != "0")
                {
                    MessageBox.Show("Invoice Created");
                    //Get date Value and update invoice date
                    DateTime InvoiceDate = dateInvoiceDate.SelectedDate.Value.Date;
                    oMainLogic.OInvoice.DateInvoiceDate = InvoiceDate;

                    if (bIsEditInvoice)
                    {
                        //update invoice in db
                        oMainLogic.updateInvoiceFromDB();
                    }
                    else
                    {
                        //Add invoice to Database
                        oMainLogic.addInvoiceToDB();
                        //update the invoice number
                        txtInvoiceNumber.Text = oMainLogic.OInvoice.IInvoiceNumber.ToString();
                    }

                    disableUI();
                }
                else
                {
                    MessageBox.Show("Missing Invoice Info");
                }
            }
            catch (Exception ex)
            {
                HandleError.handleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// .Delete The current invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ////delete object from database
                oMainLogic.delInvoiceFromDB();
                MessageBox.Show("Invoice Deleted!");

                //reset ui
                dateInvoiceDate.SelectedDate = null;
                bIsNewInvoice = true;
                updateUI();
                btnEditInvoice.IsEnabled = false;
                btnDeleteInvoice.IsEnabled = false;
                btnCreateInvoice.IsEnabled = true;
                txtInvoiceNumber.Text = "";
            }
            catch (Exception ex)
            {
                HandleError.handleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        #endregion


        #region SUPPORTFUNCTIONS
        /// <summary>
        /// Updates the UI and Invoice Information for new and old invoices.
        /// </summary>
        /// <param name="pInvoiceNumber"></param>
        /// <param name="pInvoiceDate"></param>
        /// <param name="pInvoiceTotal"></param>
        /// <param name="pItemCode"></param>
        void updateUI()
        {
            try
            {
                //Enable Editing for Invoice
                enableUI();

                //Populate the item combo box with item descriptions
                cbItems.ItemsSource = oMainLogic.LItems;

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
            catch (System.Exception ex)
            {
                //Just throw the exception
                throw new System.Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Resets the info in the UI
        /// </summary>
        void resetUI()
        {
            try
            {
                //Set or reset invoice date picker
                if (!bIsNewInvoice)
                {
                    //Set the invoice textbox to the current invoice number
                    txtInvoiceNumber.Text = oMainLogic.OInvoice.IInvoiceNumber.ToString();
                    //set the selected date
                    dateInvoiceDate.SelectedDate = oMainLogic.OInvoice.DateInvoiceDate;
                }
                else
                {
                    txtInvoiceNumber.Text = "TBD";
                }

                //update total cost text box
                txtTotalCost.Text = oMainLogic.OInvoice.ITotalCost.ToString();
                //Populate datagrid with items and prices
                dgItemList.ItemsSource = oMainLogic.OInvoice.LItems;
            }
            catch (System.Exception ex)
            {
                //Just throw the exception
                throw new System.Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Enables the controls in the UI
        /// </summary>
        void enableUI()
        {
            try
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
            catch (System.Exception ex)
            {
                //Just throw the exception
                throw new System.Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Disables the controls in the UI
        /// </summary>
        void disableUI()
        {
            try
            {
                btnSaveInvoice.IsEnabled = false;
                cbItems.IsEnabled = false;
                dateInvoiceDate.IsEnabled = false;
                btnAddItem.IsEnabled = false;
                btnDeleteItem.IsEnabled = false;
                btnCreateInvoice.IsEnabled = true;
                cbItems.IsEnabled = false;

                btnEditInvoice.IsEnabled = true;
                btnDeleteInvoice.IsEnabled = true;
            }
            catch (System.Exception ex)
            {
                //Just throw the exception
                throw new System.Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        #endregion

        private void dgItemList_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}
