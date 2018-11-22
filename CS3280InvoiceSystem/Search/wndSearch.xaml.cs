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
        /// <summary>
        /// Used for accessing business logic.
        /// </summary>
        clsSearchLogic logic;

        /// <summary>
        /// Currently displayed invoice in the data grid.
        /// </summary>
        int selectedInvoiceId;

        /// <summary>
        /// Constructor
        /// </summary>
        public wndSearch()
        {
            InitializeComponent();
            logic = new clsSearchLogic();
            selectedInvoiceId = -1;
        }

        /// <summary>
        /// Retrieve the ID of the most recently searched invoice.
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
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

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
                dgridInvoiceList.DataContext = logic.searchInvoices(number, date, total);
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
            dgridInvoiceList.DataContext = null;
            selectedInvoiceId = -1;
        }

        /// <summary>
        /// Updates data grid based on invoice number selection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbInvoiceNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbInvoiceNumber.SelectedIndex != -1)
            {
                btnSelectInvoice.IsEnabled = true;
            } else
            {
                btnSelectInvoice.IsEnabled = false;
            }
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
    }
}
