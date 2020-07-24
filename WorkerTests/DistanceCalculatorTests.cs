using Worker;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Location;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using NUnit.Framework;
using Worker.Location;

namespace WorkerTests
{
    [TestFixture]
    public class DistanceCalculatorTests
    {
        private DistanceCalculator calculator;

        [SetUp]
        public void SetUp()
        {
            calculator = new DistanceCalculator();
        }

        [Test]
        [TestCase(8.4669, -17.0366, 8.4669, -17.0366, 0.0)]
        [TestCase(8.4669, -17.0366, 47.2680, 53.9789, 7888.0)]
        [TestCase(-21.2269, 53.9789, 17.0366, -34.1234, 10495.0)]
        public void Test(double srcLat, double srcLng, double dstLat, double dstLng, double expectedResult)
        {
            var src = new MapPoint(srcLat, srcLng);
            var dst = new MapPoint(dstLat, dstLng);
            Assert.AreEqual(expectedResult, calculator.CalculateInKm(src, dst), expectedResult * 0.01);
        }
    }
}