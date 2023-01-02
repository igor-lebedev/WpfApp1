using System.IO;
using System.Text.Json;

namespace WpfApp1.Common.Database
{
    public class DataRepository : IDataRepository
    {
        private const string FileName = "points";

        void IDataRepository.SavePoints(double[] points)
        {
            var json = JsonSerializer.Serialize(points);
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }
            File.WriteAllText(FileName, json);
        }

        double[] IDataRepository.GetPoints()
        {
            if (File.Exists(FileName))
            {
                var json = File.ReadAllText(FileName);
                return JsonSerializer.Deserialize<double[]>(json);
            }

            return new double[] { };
        }
    }
}
