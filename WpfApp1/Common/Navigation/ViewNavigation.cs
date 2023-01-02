using System;
using System.Windows;
using System.Windows.Navigation;

namespace WpfApp1.Common.Navigation
{
    internal class ViewNavigation : IViewNavigation
    {
        private NavigationService _navigationService;

        private readonly IViewProvider _viewProvider;

        public event Action OnClosingApp = delegate { };
        public event Action Close = delegate { };

        public ViewNavigation(IViewProvider viewProvider)
        {
            _viewProvider = viewProvider;
        }

        void IViewNavigation.Setup(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        void IViewNavigation.OpenDataPage()
        {
            _navigationService.Navigate(_viewProvider.GetDataPage());
        }

        void IViewNavigation.PostAppClose()
        {
            OnClosingApp();
        }

        void IViewNavigation.CloseApp()
        {
            Application.Current.Shutdown();
        }
    }
}
