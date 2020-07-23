namespace Worker.Location
{
    public class MapPoint
    {
        public double Latitude { get; }
        public double Longitude { get; }

        public MapPoint(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}