using System;
using Microsoft.AspNetCore.Mvc;
using Geodesics.Service;
using Geodesics.Domain.Response;
using Geodesics.Domain.Enum;
using Geodesics.Domain.GeoDistance;

namespace Geodesics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistanceController : ControllerBase
    {
        private readonly IGeoDistanceService _geoDistanceService;

        public DistanceController(IGeoDistanceService geoDistanceService)
        {
            _geoDistanceService = geoDistanceService;
        }

        /// <summary>
        /// Retrieves the distance between the provided points.
        /// </summary>
        /// <response code="200">Distance successfully calculated.</response>
        /// <response code="400">Latitude or longitude of any of the given points is out of range.</response>
        /// <response code="500">Unexpected server exception.</response>
        [HttpGet]
        [Route("{distanceMethod}/{measureUnit}")]
        public ActionResult<DistanceResponse> Get(DistanceMethod distanceMethod, MeasureUnit measureUnit,
            [FromQuery] double point1Latitude, [FromQuery] double point1Longitude,
            [FromQuery] double point2Latitude, [FromQuery] double point2Longitude)
        {
            var point1 = new DistancePoint(point1Latitude, point1Longitude);
            var point2 = new DistancePoint(point2Latitude, point2Longitude);

            try
            {
                point1.ValidatePoint();
                point2.ValidatePoint();

                return new ActionResult<DistanceResponse>(new DistanceResponse
                {
                    Distance = _geoDistanceService.CalculateDistance(distanceMethod, point1, point2, measureUnit)
                });
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
