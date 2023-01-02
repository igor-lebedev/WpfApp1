using GalaSoft.MvvmLight;

namespace WpfApp1.Models
{
    public class PointModel : ObservableObject
    {
        private double _x;
        private double _y;

        public double X 
        {
            get => _x;
            set => Set(nameof(X), ref _x, value);
        }

        public double Y
        {
            get => _y;
            set => Set(nameof(Y), ref _y, value);
        }
    }
}
