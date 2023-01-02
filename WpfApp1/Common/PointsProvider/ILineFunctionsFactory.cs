namespace WpfApp1.Common.PointsProvider
{
    public interface ILineFunctionsFactory
    {
        ILineFunction CreateFirstFunction();
        ILineFunction CreateSecondFunction();
    }
}
