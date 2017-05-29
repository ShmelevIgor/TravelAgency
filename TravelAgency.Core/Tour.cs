using System;

namespace TravelAgency.Core
{
    public class Tour
    {
        public int Id { get; set; }
        public string Direction { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime FromDate { get; set; }
        public string Hotel { get; set; }
        public double Rating { get; set; }
        public int Price { get; set; }
    }
}
