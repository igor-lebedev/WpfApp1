using GalaSoft.MvvmLight;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Windows;
using WpfApp1.Common.Database;
using WpfApp1.Common.Navigation;
using WpfApp1.Common.PointsProvider;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    public sealed class DataViewModel : ViewModelBase
    {
        private ILineFunction _firstLineFunction;
        private ILineFunction _secondLineFunction;
        private readonly IDataRepository _dataRepository;
        private readonly IDataStateHolder _dataStateHolder;
        private readonly IViewNavigation _viewNavigation;

        private double[] _originalPoints;
        private ObservableCollection<PointModel> _points;
        private SeriesCollection _seriesCollection;

        public ObservableCollection<PointModel> Points
        {
            get => _points;
            private set => Set(nameof(Points), ref _points, value);
        }


        public SeriesCollection SeriesCollection
        {
            get => _seriesCollection;
            private set => Set(nameof(SeriesCollection), ref _seriesCollection, value);
        }

        public DataViewModel(
            ILineFunctionsFactory lineFunctionsFactory,
            IDataRepository dataRepository,
            IDataStateHolder dataStateHolder,
            IViewNavigation viewNavigation)
        {
            _firstLineFunction = lineFunctionsFactory.CreateFirstFunction();
            _secondLineFunction = lineFunctionsFactory.CreateSecondFunction();
            _dataRepository = dataRepository;
            _dataStateHolder = dataStateHolder;
            _viewNavigation = viewNavigation;
            UpdateData();
            _viewNavigation.OnClosingApp += OnClosingApp;
            Points.CollectionChanged += OnPointsCollectionChanged;
        }

        public override void Cleanup()
        {
            _viewNavigation.OnClosingApp -= OnClosingApp;
            Points.CollectionChanged -= OnPointsCollectionChanged;
            base.Cleanup();
        }

        private void OnPointsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            Recalculate();
        }

        public void Recalculate()
        {
            for (int i = 0; i < Points.Count; i++)
            {
                var point = Points[i];
                point.Y = _firstLineFunction.Calculate(point.X);
            }

            var points = Points.Select(_ => _.X).ToArray();

            SeriesCollection = new SeriesCollection();
            SeriesCollection.AddRange(GetFunctionSeries(points, _firstLineFunction));
            SeriesCollection.AddRange(GetFunctionSeries(points, _secondLineFunction));

            HandleChanges();
        }

        private static LineSeries[] GetFunctionSeries(double[] xPoints, ILineFunction function)
        {
            var values = new ObservablePoint[xPoints.Length];
            var reverseValues = new ObservablePoint[xPoints.Length];

            for (int i = 0; i < xPoints.Length; i++)
            {
                values[i] = new ObservablePoint(xPoints[i], function.Calculate(xPoints[i]));
                reverseValues[i] = new ObservablePoint(xPoints[i], function.ReverseCalculate(xPoints[i]));
            }

            return new LineSeries[]
            {
                new LineSeries
                {
                    Title = function.Name,
                    Values = new ChartValues<ObservablePoint>(values)
                },
                new LineSeries
                {
                    Title = $"Обратная {function.Name}",
                    Values = new ChartValues<ObservablePoint>(reverseValues)
                }
            };
        }

        private void UpdateData()
        {
            _originalPoints = _dataRepository.GetPoints();
            var points = _originalPoints.Select(_ => new PointModel { X = _ });
            Points = new ObservableCollection<PointModel>(points);
            Recalculate();
        }

        private void HandleChanges()
        {
            if (_originalPoints == null || _originalPoints.Length == 0 && _points.Count > 0)
            {
                _dataStateHolder.IsDataChanged = true;
                return;
            }

            if (_originalPoints != null && _originalPoints.Length == _points.Count)
            {
                for (int i = 0; i < _originalPoints.Length; i++)
                {
                    if (_originalPoints[i] != _points[i].X)
                    {
                        _dataStateHolder.IsDataChanged = true;
                        return;
                    }
                }
            }
            _dataStateHolder.IsDataChanged = false;
        }

        private void OnClosingApp()
        {
            if (_dataStateHolder.IsDataChanged)
            {
                TrySaveChanges();
            }
        }

        private void TrySaveChanges()
        {
            var result = MessageBox.Show("Есть изменения, сохранить?", "Изменения в таблице данных", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                var points = Points.Select(_ => _.X).ToArray();
                _dataRepository.SavePoints(points);
                _dataStateHolder.IsDataChanged = false;
            }

            _viewNavigation.CloseApp();
        }
    }
}
