using Geodesics.Domain.Enum;
using Geodesics.Domain.GeoDistance;

namespace Geodesics.Service
{
    public interface IGeoDistanceService
    {
        /// <summary>
        /// Calculates the distance between two points using the DistanceMethod informed
        /// </summary>
        /// <param name="distanceMethod"></param>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <param name="measureUnit"></param>
        /// <returns>distance in kilometers or miles</returns>
        double CalculateDistance(DistanceMethod distanceMethod, DistancePoint point1, DistancePoint point2, MeasureUnit measureUnit);

        /// <summary>
        /// Calculates the distance between two points considering the geodesic curve
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <param name="measureUnit"></param>
        /// <returns>distance in kilometers or miles</returns>
        double CalculateGeodesicCurve(DistancePoint point1, DistancePoint point2, MeasureUnit measureUnit);

        /// <summary>
        /// Calculates the distance between two points considering Pythagoras
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <param name="measureUnit"></param>
        /// <returns>distance in kilometers or miles</returns>
        double CalculatePythagoras(DistancePoint point1, DistancePoint point2, MeasureUnit measureUnit);
    }
}
