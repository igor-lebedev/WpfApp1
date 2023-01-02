namespace WpfApp1.Common.PointsProvider
{
    public class FirstFunction : ILineFunction
    {
        public FirstFunction() { }

        public string Name => "first(x)";

        double ILineFunction.Calculate(double x)
        {
            return x > 2 ? x / 2 : x * 2;
        }

        double ILineFunction.ReverseCalculate(double x)
        {
            return x > 2 ? x * 2 : x / 2;
        }
    }
}
