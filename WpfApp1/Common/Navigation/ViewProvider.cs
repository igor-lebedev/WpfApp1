using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Controls;
using WpfApp1.Views;

namespace WpfApp1.Common.Navigation
{
    internal sealed class ViewProvider : IViewProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public ViewProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        Page IViewProvider.GetDataPage()
        {
            return GetPage<DataView>();
        }

        private TPage GetPage<TPage>() where TPage : Page
        {
            var page = _serviceProvider.GetService<TPage>();
            if (page == null)
            {
                throw new NotImplementedException();
            }

            return page;
        }
    }
}
