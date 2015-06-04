using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Astro.Library.Tests
{
    public class LatitudeTest
    {

        [Fact]
        public void TestBase()
        {
            Latitude l = new Latitude();
            Assert.Equal(0, l.Degrees);
            Assert.Equal(0, l.Minutes);
            Assert.Equal(0, l.Seconds);
            Assert.Equal(LatitudePolarity.North, l.Polarity);
            Assert.Equal(0.0, l.Value, 11);
            Assert.Equal("0N00'00\"", l.ToString());
        }

        [Fact]
        public void TestFromValue()
        {
            Double value = 278.123456789;
            Latitude l = new Latitude(value);
            Assert.Equal(98, l.Degrees);
            Assert.Equal(7, l.Minutes);
            Assert.Equal(24, l.Seconds);
            Assert.Equal(LatitudePolarity.North, l.Polarity);
            Assert.Equal(98.1233333333333, l.Value, 11);
            Assert.Equal("98N07'24\"", l.ToString());

            value = -98.123456789;
            l = new Latitude(value);
            Assert.Equal(98, l.Degrees);
            Assert.Equal(7, l.Minutes);
            Assert.Equal(24, l.Seconds);
            Assert.Equal(LatitudePolarity.South, l.Polarity);
            Assert.Equal(-98.1233333333333, l.Value, 11);
            Assert.Equal("98S07'24\"", l.ToString());
        }

        [Fact]
        public void TestFromComponent1()
        {
            Latitude l = new Latitude(98, 7, 24);
            Assert.Equal(98, l.Degrees);
            Assert.Equal(7, l.Minutes);
            Assert.Equal(24, l.Seconds);
            Assert.Equal(LatitudePolarity.North, l.Polarity);
            Assert.Equal(98.1233333333333, l.Value, 11);
            Assert.Equal("98N07'24\"", l.ToString());

            l = new Latitude(-98, 7, 24);
            Assert.Equal(98, l.Degrees);
            Assert.Equal(7, l.Minutes);
            Assert.Equal(24, l.Seconds);
            Assert.Equal(LatitudePolarity.South, l.Polarity);
            Assert.Equal(-98.1233333333333, l.Value, 11);
            Assert.Equal("98S07'24\"", l.ToString());

            Assert.Throws<ArgumentOutOfRangeException>(() => new Latitude(198, 7, 24));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Latitude(98, 77, 24));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Latitude(98, 7, -24));
        }

        [Fact]
        public void TestFromComponent2()
        {
            Latitude l = new Latitude(98, 7, 24, LatitudePolarity.North);
            Assert.Equal(98, l.Degrees);
            Assert.Equal(7, l.Minutes);
            Assert.Equal(24, l.Seconds);
            Assert.Equal(LatitudePolarity.North, l.Polarity);
            Assert.Equal(98.1233333333333, l.Value, 11);
            Assert.Equal("98N07'24\"", l.ToString());

            l = new Latitude(98, 7, 24, LatitudePolarity.South);
            Assert.Equal(98, l.Degrees);
            Assert.Equal(7, l.Minutes);
            Assert.Equal(24, l.Seconds);
            Assert.Equal(LatitudePolarity.South, l.Polarity);
            Assert.Equal(-98.1233333333333, l.Value, 11);
            Assert.Equal("98S07'24\"", l.ToString());

            Assert.Throws<ArgumentOutOfRangeException>(() => new Latitude(198, 7, 24, LatitudePolarity.North));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Latitude(98, 77, 24, LatitudePolarity.North));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Latitude(98, 7, -24, LatitudePolarity.North));
        }

        [Fact]
        public void TestExplicitCast()
        {
            Double value = 278.123456789;

            Latitude l = value;

            Assert.Equal(98, l.Degrees);
            Assert.Equal(7, l.Minutes);
            Assert.Equal(24, l.Seconds);
            Assert.Equal(LatitudePolarity.North, l.Polarity);
            Assert.Equal(98.1233333333333, l.Value, 11);
            Assert.Equal("98N07'24\"", l.ToString());

            value = l;
            Assert.Equal(98.1233333333333, value, 11);

        }

    }
}
