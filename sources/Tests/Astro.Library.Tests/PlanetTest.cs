using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Astro.Library.Tests
{
    public class PlanetTest
    {
        [Fact]
        public void TestCreate()
        {
            var planet = new Planet();
            Assert.Equal(0, planet.Id);
            planet = new Planet(1234);
            Assert.Equal(1234, planet.Id);
        }

        [Fact]
        public void TestIntCast()
        {
            Planet planet = 1234;
            Assert.Equal(1234, planet.Id);

            int i = planet;
            Assert.Equal(1234, i);
        }

        [Fact]
        public void TestPlanetType()
        {
            Planet planet = 0;
            Assert.Equal(false, planet.IsAsteroid);
            Assert.Equal(false, planet.IsComet);
            Assert.Equal(false, planet.IsFictitious);
            Assert.Equal(true, planet.IsPlanet);

            planet = -100;
            Assert.Equal(false, planet.IsAsteroid);
            Assert.Equal(false, planet.IsComet);
            Assert.Equal(false, planet.IsFictitious);
            Assert.Equal(false, planet.IsPlanet);

            planet = 10;
            Assert.Equal(false, planet.IsAsteroid);
            Assert.Equal(false, planet.IsComet);
            Assert.Equal(false, planet.IsFictitious);
            Assert.Equal(true, planet.IsPlanet);

            planet = 25;
            Assert.Equal(false, planet.IsAsteroid);
            Assert.Equal(false, planet.IsComet);
            Assert.Equal(false, planet.IsFictitious);
            Assert.Equal(false, planet.IsPlanet);

            planet = 50;
            Assert.Equal(false, planet.IsAsteroid);
            Assert.Equal(false, planet.IsComet);
            Assert.Equal(true, planet.IsFictitious);
            Assert.Equal(false, planet.IsPlanet);

            planet = 50;
            Assert.Equal(false, planet.IsAsteroid);
            Assert.Equal(false, planet.IsComet);
            Assert.Equal(true, planet.IsFictitious);
            Assert.Equal(false, planet.IsPlanet);

            planet = 1000;
            Assert.Equal(false, planet.IsAsteroid);
            Assert.Equal(true, planet.IsComet);
            Assert.Equal(false, planet.IsFictitious);
            Assert.Equal(false, planet.IsPlanet);

            planet = 2000;
            Assert.Equal(false, planet.IsAsteroid);
            Assert.Equal(true, planet.IsComet);
            Assert.Equal(false, planet.IsFictitious);
            Assert.Equal(false, planet.IsPlanet);

            planet = 10000;
            Assert.Equal(true, planet.IsAsteroid);
            Assert.Equal(false, planet.IsComet);
            Assert.Equal(false, planet.IsFictitious);
            Assert.Equal(false, planet.IsPlanet);

            planet = 12000;
            Assert.Equal(true, planet.IsAsteroid);
            Assert.Equal(false, planet.IsComet);
            Assert.Equal(false, planet.IsFictitious);
            Assert.Equal(false, planet.IsPlanet);
        }

        [Fact]
        public void TestAsAsteroid()
        {
            Planet planet = Planet.AsAsteroid(12);
            Assert.Equal(Planet.FirstAsteroid + 12, planet.Id);
        }

        [Fact]
        public void TestToString()
        {
            Planet planet = Planet.Venus;
            Assert.Equal("3", planet.ToString());
        }
    }
}
