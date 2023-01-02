using System;
using System.Collections.Generic;

namespace WpfApp1.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class PointSet
    {
        public string Id { get; set; }
        public DateTime DateTime { get; set; }
        public List<Point> XPoints { get; set; } = new List<Point>();
    }
}
