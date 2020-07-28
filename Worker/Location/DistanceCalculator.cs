using System;

namespace Worker.Location
{
    public class DistanceCalculator
    {
        private const double RadiusOfEarthInKm = 6371.0;
        public double CalculateInKm(MapPoint src, MapPoint dst)
        {
            var dLat = DegreesToRadians(dst.Latitude - src.Latitude);
            var dLng = DegreesToRadians(dst.Longitude - src.Longitude);
            var a =
                Haversine(dLat) +
                Math.Cos(DegreesToRadians(src.Latitude)) *
                Math.Cos(DegreesToRadians(dst.Latitude)) *
                Haversine(dLng);
            var partOfCircle = Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distanceInKm = 2 * RadiusOfEarthInKm * partOfCircle;
            return distanceInKm;
        }

        private double DegreesToRadians(double angleInDegrees)
        {
            return 2 * angleInDegrees * (Math.PI / 360);
        }

        private double Haversine(double angleInRadians)
        {
            return Math.Sin(angleInRadians / 2) * Math.Sin(angleInRadians / 2);
        }
    }
}