using System.ComponentModel.DataAnnotations;

namespace WpfApp1.Models
{
    public class Point
    {
        [Key]
        public string Id { get; set; }
        public double X { get; set; }
    }
}
