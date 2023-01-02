using System;

namespace WpfApp1.Common.PointsProvider
{
    public class SecondFunction : ILineFunction
    {
        public SecondFunction() { }

        public string Name => "second(x)";

        double ILineFunction.Calculate(double x)
        {
            return x > 2 ? Math.Sin(x) : Math.Cos(x);
        }

        double ILineFunction.ReverseCalculate(double x)
        {
            return x > 2 ? Math.Cos(x) : Math.Sin(x);
        }
    }
}
