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
using System.Reflection;

namespace CS3280InvoiceSystem.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        #region Class Members
        /// <summary>
        /// Used for accessing business logic.
        /// </summary>
        private clsSearchLogic logic;

        /// <summary>
        /// Currently displayed invoice in the data grid.
        /// </summary>
        private int selectedInvoiceId;
        #endregion

        #region Public Methods
        /// <summary>
        /// Constructor
        /// </summary>
        public wndSearch()
        {
            InitializeComponent();
            logic = new clsSearchLogic();
            selectedInvoiceId = -1;
            initializeDataGrid();
        }

        /// <summary>
        /// Retrieve the ID of the most recently searched invoice.
        /// Used by Main window to retrieve invoice information for the selected ID.
        /// </summary>
        /// <returns></returns>
        public int getSelectedInvoiceId()
        {
            return selectedInvoiceId;
        }

        /// <summary>
        /// Refresh the data shown in each search combo box.
        /// </summary>
        public void updateSearchOptions()
        {
            try
            {
                fill_cbInvoiceNumber(logic.getInvoiceNumbers());
                fill_cbInvoiceDate(logic.getInvoiceDates());
                fill_cbInvoiceTotalCharge(logic.getInvoiceTotalCharges());
                initializeDataGrid();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Fill boxes when window is shown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_ContentRendered(object sender, EventArgs e)
        {
            try
            {
                updateSearchOptions();
                initializeDataGrid();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Populate cbInvoiceNumber items.
        /// </summary>
        private void fill_cbInvoiceNumber(List<int> invoiceNumbers)
        {
            try
            {
                cbInvoiceNumber.Items.Clear();
                foreach (int n in invoiceNumbers)
                {
                    cbInvoiceNumber.Items.Add(n);
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Populate cbInvoiceDate items.
        /// </summary>
        /// <param name="invoiceDates"></param>
        private void fill_cbInvoiceDate(List<string> invoiceDates)
        {
            try
            {
                cbInvoiceDate.Items.Clear();
                foreach (string d in invoiceDates)
                {
                    cbInvoiceDate.Items.Add(d);
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Populate cbInvoiceTotalCharge items.
        /// </summary>
        /// <param name="invoiceTotals"></param>
        private void fill_cbInvoiceTotalCharge(List<int> invoiceTotals)
        {
            try
            {
                cbInvoiceTotalCharge.Items.Clear();
                foreach (int t in invoiceTotals)
                {
                    cbInvoiceTotalCharge.Items.Add(t);
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Closes window and sets the invoice ID field to the selected invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Initializes datagrid with all invoices.
        /// </summary>
        private void initializeDataGrid()
        {
            try
            {
                dgridInvoiceList.DataContext = logic.getAllInvoices();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Updates the data grid based on current search parameters.
        /// </summary>
        private void updateDataGrid()
        {
            try
            {
                int number = -1;
                string date = null;
                int total = -1;
                if (cbInvoiceNumber.SelectedIndex != -1)
                {
                    Int32.TryParse(cbInvoiceNumber.SelectedItem.ToString(), out number);
                }
                if (cbInvoiceDate.SelectedIndex != -1)
                {
                    date = cbInvoiceDate.SelectedItem.ToString();
                }
                if (cbInvoiceTotalCharge.SelectedIndex != -1)
                {
                    Int32.TryParse(cbInvoiceTotalCharge.SelectedItem.ToString(), out total);
                }
                selectedInvoiceId = number;
                if(number != -1 || date != null || total != -1)
                {
                    dgridInvoiceList.DataContext = logic.searchInvoices(number, date, total);
                }
                
                if (logic.getInvoiceFound())
                {
                    btnSelectInvoice.IsEnabled = true;
                    selectedInvoiceId = logic.getSelectedInvoiceId();
                } else
                {
                    btnSelectInvoice.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Clear search parameters and data grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearSelection_Click(object sender, RoutedEventArgs e)
        {
            cbInvoiceNumber.SelectedIndex = -1;
            cbInvoiceDate.SelectedIndex = -1;
            cbInvoiceTotalCharge.SelectedIndex = -1;
            initializeDataGrid();
            selectedInvoiceId = -1;
            btnSelectInvoice.IsEnabled = false;
        }

        /// <summary>
        /// Updates data grid based on invoice number selection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbInvoiceNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateDataGrid();
        }

        /// <summary>
        /// Updates data grid based on invoice date selection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbInvoiceDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateDataGrid();
        }

        /// <summary>
        /// Updates data grid based on invoice total charge selection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbInvoiceTotalCharge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateDataGrid();
        }

        /// <summary>
        /// Cancels search and hides window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            cbInvoiceNumber.SelectedIndex = -1;
            cbInvoiceDate.SelectedIndex = -1;
            cbInvoiceTotalCharge.SelectedIndex = -1;
            initializeDataGrid();
            selectedInvoiceId = -1;
            this.Hide();
        }

        /// <summary>
        /// Closing window does not destroy object instance.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            cbInvoiceNumber.SelectedIndex = -1;
            cbInvoiceDate.SelectedIndex = -1;
            cbInvoiceTotalCharge.SelectedIndex = -1;
            initializeDataGrid();
            selectedInvoiceId = -1;
            this.Hide();
        }

        /// <summary>
        /// Handles errors.
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }
        #endregion
    }
}
