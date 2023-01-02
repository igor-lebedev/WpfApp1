using System;
using System.Windows;
using WpfApp1.Common.Navigation;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    public partial class MainWindow : Window
    {
        private readonly IViewNavigation _viewNavigation;
        private readonly IDataStateHolder _dataStateHolder;

        public MainWindow(MainViewModel viewModel, IViewNavigation viewNavigation, IDataStateHolder dataStateHolder)
        {
            InitializeComponent();
            _viewNavigation = viewNavigation;
            _dataStateHolder = dataStateHolder;
            DataContext = viewModel;
            viewNavigation.Setup(_mainFrame.NavigationService);
            viewNavigation.OpenDataPage();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _viewNavigation.PostAppClose();
            if (_dataStateHolder.IsDataChanged)
            {
                e.Cancel = true;
            }
        }
    }
}
