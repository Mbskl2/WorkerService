using Worker;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Location;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using NUnit.Framework;
using Worker.DAL.Models;

namespace WorkerTests
{
    [TestFixture]
    public class DistanceCalculatorTests
    {
        [Test]
        public void CalculateTest() // TODO: zrobić testy
        {
            //var translator = new AddressToCoordinatesTranslator(new GeocodingService(new HttpClient(), new Config()));
            //var address = new Address() {Country = "PL", City = "Wrocław", Street = "Chrzanowskiego", HouseNumber = "23"};
            //var coordinates = ""; //translator.Translate(address);
            //Console.WriteLine(coordinates);
        }
    }
}