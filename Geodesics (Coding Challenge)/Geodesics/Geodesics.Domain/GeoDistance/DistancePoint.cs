using System;

namespace Geodesics.Domain.GeoDistance
{
    public class DistancePoint
    {
        public DistancePoint(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public void ValidatePoint()
        {
            if (Latitude < -90 || Latitude > 90)
            {
                throw new ArgumentOutOfRangeException(nameof(Latitude), "Must be in [-90, 90] interval");
            }
            if (Longitude < -180 || Longitude > 180)
            {
                throw new ArgumentOutOfRangeException(nameof(Longitude), "Must be in [-180, 180] interval");
            }
        }
    }
}
