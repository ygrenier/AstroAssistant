using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Astro.Library.Tests
{
    public class GeoPositionTest
    {

        [Fact]
        public void TestCreateEmpty()
        {
            GeoPosition pos = new GeoPosition();
            Assert.Equal(0, pos.Longitude.Value);
            Assert.Equal(0, pos.Latitude.Value);
            Assert.Equal(0, pos.Altitude);
        }

        [Fact]
        public void TestCreate()
        {
            GeoPosition pos = new GeoPosition(46.72, -5.23, 12);
            Assert.Equal(46.72, pos.Longitude.Value);
            Assert.Equal(-5.23, pos.Latitude.Value);
            Assert.Equal(12, pos.Altitude);
        }

        [Fact]
        public void TestToString()
        {
            GeoPosition pos = new GeoPosition(46.72, -5.23, 12);
            Assert.Equal("46E43'12\", 5S13'48\", 12 m", pos.ToString());
        }

    }
}
