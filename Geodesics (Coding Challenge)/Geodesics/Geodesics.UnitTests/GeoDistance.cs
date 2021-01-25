using NUnit.Framework;
using System;
using Geodesics.Api.Controllers;
using Geodesics.Domain.Enum;
using Geodesics.Domain.GeoDistance;
using Geodesics.Service;

namespace Geodesics.UnitTests
{
    [TestFixture]
    public class GeoDistance
    {
        private IGeoDistanceService _geoDistanceService;

        [SetUp]
        public void SetUp()
        {
            _geoDistanceService = new GeoDistanceService();
        }

        [Test]
        public void GetDistanceByController()
        {
            var controller = new DistanceController(_geoDistanceService);
            var result = controller.Get(DistanceMethod.GeodesicCurve, MeasureUnit.Km, 53.297975, -6.372663, 41.385101, -81.440440);
            Assert.AreEqual(5536.3386822666853, result.Value.Distance);
        }

        [Test]
        public void ViolateRulesRangeCoordenadas()
        {
            var pointA = new DistancePoint(9999999999999, -6.372663);

            try
            {
                pointA.ValidatePoint();
            }
            catch (System.Exception ex)
            {
                if (ex is ArgumentOutOfRangeException)
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail();
                }
            }   
        }

        [Test]
        public void MetRulesRangeCoordenadas()
        {
            var pointA = new DistancePoint(53.297975, -6.372663);

            try
            {
                pointA.ValidatePoint();
            }
            catch (System.Exception)
            {
                Assert.Fail();
            }

            Assert.IsTrue(true);
        }

        [Test]
        public void CalculateDistanceCurveKm()
        {
            var result = _geoDistanceService.CalculateDistance(Domain.Enum.DistanceMethod.GeodesicCurve,
                new Domain.GeoDistance.DistancePoint(53.297975, -6.372663), new Domain.GeoDistance.DistancePoint(41.385101, -81.440440), Domain.Enum.MeasureUnit.Km);
            Assert.AreEqual(5536.3386822666853, result);
        }

        [Test]
        public void CalculateDistancePythagorasKm()
        {
            var result = _geoDistanceService.CalculateDistance(Domain.Enum.DistanceMethod.Pythagoras,
                new Domain.GeoDistance.DistancePoint(53.297975, -6.372663), new Domain.GeoDistance.DistancePoint(41.385101, -81.440440), Domain.Enum.MeasureUnit.Km);
            Assert.AreEqual(5809.2968123283927, result);
        }

        [Test]
        public void CalculateDistanceCurveMile()
        {
            var result = _geoDistanceService.CalculateDistance(Domain.Enum.DistanceMethod.GeodesicCurve,
                new Domain.GeoDistance.DistancePoint(53.297975, -6.372663), new Domain.GeoDistance.DistancePoint(41.385101, -81.440440), Domain.Enum.MeasureUnit.Mile);
            Assert.AreEqual(3440.333517986785, result);
        }

        [Test]
        public void CalculateDistancePythagorasMile()
        {
            var result = _geoDistanceService.CalculateDistance(Domain.Enum.DistanceMethod.Pythagoras,
                new Domain.GeoDistance.DistancePoint(53.297975, -6.372663), new Domain.GeoDistance.DistancePoint(41.385101, -81.440440), Domain.Enum.MeasureUnit.Mile);
            Assert.AreEqual(3609.9522963440759, result);
        }
    }
}