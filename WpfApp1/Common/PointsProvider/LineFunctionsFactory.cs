namespace WpfApp1.Common.PointsProvider
{
    public class LineFunctionsFactory : ILineFunctionsFactory
    {
        ILineFunction ILineFunctionsFactory.CreateFirstFunction()
        {
            return new FirstFunction();
        }

        ILineFunction ILineFunctionsFactory.CreateSecondFunction()
        {
            return new SecondFunction();
        }
    }
}
