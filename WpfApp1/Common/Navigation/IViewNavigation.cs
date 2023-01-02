using System;
using System.Windows.Navigation;

namespace WpfApp1.Common.Navigation
{
    public interface IViewNavigation
    {
        event Action OnClosingApp;

        void Setup(NavigationService navigationService);
        void OpenDataPage();
        void PostAppClose();
        void CloseApp();
    }
}
