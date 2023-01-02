using GalaSoft.MvvmLight;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WpfApp1.Common.Database;
using WpfApp1.Common.Navigation;
using WpfApp1.Common.PointsProvider;
using WpfApp1.ViewModels;
using WpfApp1.Views;

namespace WpfApp1
{

    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App() : base()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            _serviceProvider.GetService<MainWindow>()?.Show();
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            RegisterServices(services);
            RegisterPages(services);
        }

        private static void RegisterPages(ServiceCollection services)
        {
            RegisterPage<DataView, DataViewModel>(services);
            RegisterPage<MainWindow, MainViewModel>(services);
        }

        private static void RegisterServices(ServiceCollection services)
        {
            services.AddSingleton<IViewNavigation, ViewNavigation>();
            services.AddTransient<IDataRepository, DataRepository>();
            services.AddSingleton<IViewProvider>(_ => new ViewProvider(_));
            services.AddTransient<ILineFunctionsFactory, LineFunctionsFactory>();
            services.AddSingleton<IDataStateHolder, DataStateHolder>();
        }
        
        private static void RegisterPage<TView, TViewModel>(ServiceCollection services)
            where TView : FrameworkElement
            where TViewModel : ViewModelBase
        {
            services.AddSingleton<TView>();
            services.AddSingleton<TViewModel>();
        }

        private void App_SessionEnding(object sender, SessionEndingCancelEventArgs e)
        {

        }

        private void App_Exit(object sender, ExitEventArgs e)
        {

        }
    }
}
