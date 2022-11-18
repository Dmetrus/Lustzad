namespace ASPAirport.Models
{
    public class AirRequestModel
    {
        public int numflight { get; set; }
        public Types type { get; set; }
        public DateTime eta { get; set; }
        public int countPas { get; set; }
        public double pricePas { get; set; }
        public int countCrew { get; set; }
        public double priceCrew { get; set; }
        public double procDop { get; set; }
    }
}
