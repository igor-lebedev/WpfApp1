namespace WpfApp1.Common.PointsProvider
{
    public interface ILineFunction
    {
        string Name { get; }
        double Calculate(double x);
        double ReverseCalculate(double x);
    }
}
