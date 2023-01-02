using System.Windows.Controls;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    public partial class DataView : Page
    {
        private readonly DataViewModel _dataViewModel;

        public DataView(DataViewModel dataViewModel)
        {
            InitializeComponent();
            _dataViewModel = dataViewModel;
            DataContext = _dataViewModel;
        }

        private void DataGrid_CurrentCellChanged(object sender, System.EventArgs e)
        {
            _dataViewModel.Recalculate();
        }

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            _dataViewModel.Recalculate();
        }
    }
}
