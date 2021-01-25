using System;
using Geodesics.Domain.Enum;
using Geodesics.Domain.GeoDistance;
using Geodesics.Domain.Util;

namespace Geodesics.Service
{
    public class GeoDistanceService: IGeoDistanceService
    {
        public double CalculateDistance(DistanceMethod distanceMethod, DistancePoint point1, DistancePoint point2, MeasureUnit measureUnit)
        {
            switch (distanceMethod)
            {
                case DistanceMethod.GeodesicCurve:
                    return CalculateGeodesicCurve(point1, point2, measureUnit);
                case DistanceMethod.Pythagoras:
                    return CalculatePythagoras(point1, point2, measureUnit);
                default:
                    throw new ArgumentOutOfRangeException(nameof(distanceMethod));
            }
        }

        public double CalculateGeodesicCurve(DistancePoint point1, DistancePoint point2, MeasureUnit measureUnit)
        {
            var a = GeoUtils.PointOriginDegrees - point2.Latitude;
            var b = GeoUtils.PointOriginDegrees - point1.Latitude;
            var fi = point1.Longitude - point2.Longitude;
            var cosP = Math.Cos(GeoUtils.DegreesToRadians(a)) * Math.Cos(GeoUtils.DegreesToRadians(b)) +
                       Math.Sin(GeoUtils.DegreesToRadians(a)) * Math.Sin(GeoUtils.DegreesToRadians(b)) * Math.Cos(GeoUtils.DegreesToRadians(fi));
            var n = GeoUtils.RadiansToDegrees(Math.Acos(cosP));
            var d = Math.PI * n * GeoUtils.GetEarthRadius(measureUnit) / 180;
            return d;
        }

        public double CalculatePythagoras(DistancePoint point1, DistancePoint point2, MeasureUnit measureUnit)
        {
            var x = (GeoUtils.DegreesToRadians(point2.Longitude) - GeoUtils.DegreesToRadians(point1.Longitude)) *
                    Math.Cos((GeoUtils.DegreesToRadians(point1.Latitude) + GeoUtils.DegreesToRadians(point2.Latitude)) / 2);
            var y = GeoUtils.DegreesToRadians(point2.Latitude) - GeoUtils.DegreesToRadians(point1.Latitude);
            var d = Math.Sqrt(x * x + y * y) * GeoUtils.GetEarthRadius(measureUnit);
            return d;
        }
    }
}
