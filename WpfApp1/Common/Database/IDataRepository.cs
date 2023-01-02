namespace WpfApp1.Common.Database
{
    public interface IDataRepository
    {
        void SavePoints(double[] points);
        double[] GetPoints();
    }
}
