# CS3280InvoiceSystem

To choose which window launches when running app, change the StartupUri in App.xaml.cs to the relevant path.

**Example:**<br/>
`StartupUri="./Search/wndSearch.xaml;"`

To show the Search window, instantiate the window and call "Show". To retrieve the Invoice ID selected by the search window call "getSelectedInvoiceId". The Search window will hide itself when the Select Invoice button is pressed.

**Example:**<br/>
`wndSearch SearchWindow = new wndSearch();
int searchId = SearchWindow.getSelectedInvoiceId();`

