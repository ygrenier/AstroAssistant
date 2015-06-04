using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Astro.Library.Tests
{
    public class LongitudeTest
    {

        [Fact]
        public void TestBase()
        {
            Longitude l = new Longitude();
            Assert.Equal(0, l.Degrees);
            Assert.Equal(0, l.Minutes);
            Assert.Equal(0, l.Seconds);
            Assert.Equal(LongitudePolarity.East, l.Polarity);
            Assert.Equal(0.0, l.Value, 11);
            Assert.Equal("0E00'00\"", l.ToString());
        }

        [Fact]
        public void TestFromValue()
        {
            Double value = 278.123456789;
            Longitude l = new Longitude(value);
            Assert.Equal(98, l.Degrees);
            Assert.Equal(7, l.Minutes);
            Assert.Equal(24, l.Seconds);
            Assert.Equal(LongitudePolarity.East, l.Polarity);
            Assert.Equal(98.1233333333333, l.Value, 11);
            Assert.Equal("98E07'24\"", l.ToString());

            value = -98.123456789;
            l = new Longitude(value);
            Assert.Equal(98, l.Degrees);
            Assert.Equal(7, l.Minutes);
            Assert.Equal(24, l.Seconds);
            Assert.Equal(LongitudePolarity.West, l.Polarity);
            Assert.Equal(-98.1233333333333, l.Value, 11);
            Assert.Equal("98W07'24\"", l.ToString());
        }

        [Fact]
        public void TestFromComponent1()
        {
            Longitude l = new Longitude(98, 7, 24);
            Assert.Equal(98, l.Degrees);
            Assert.Equal(7, l.Minutes);
            Assert.Equal(24, l.Seconds);
            Assert.Equal(LongitudePolarity.East, l.Polarity);
            Assert.Equal(98.1233333333333, l.Value, 11);
            Assert.Equal("98E07'24\"", l.ToString());

            l = new Longitude(-98, 7, 24);
            Assert.Equal(98, l.Degrees);
            Assert.Equal(7, l.Minutes);
            Assert.Equal(24, l.Seconds);
            Assert.Equal(LongitudePolarity.West, l.Polarity);
            Assert.Equal(-98.1233333333333, l.Value, 11);
            Assert.Equal("98W07'24\"", l.ToString());

            Assert.Throws<ArgumentOutOfRangeException>(() => new Longitude(198, 7, 24));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Longitude(98, 77, 24));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Longitude(98, 7, -24));
        }

        [Fact]
        public void TestFromComponent2()
        {
            Longitude l = new Longitude(98, 7, 24, LongitudePolarity.East);
            Assert.Equal(98, l.Degrees);
            Assert.Equal(7, l.Minutes);
            Assert.Equal(24, l.Seconds);
            Assert.Equal(LongitudePolarity.East, l.Polarity);
            Assert.Equal(98.1233333333333, l.Value, 11);
            Assert.Equal("98E07'24\"", l.ToString());

            l = new Longitude(98, 7, 24, LongitudePolarity.West);
            Assert.Equal(98, l.Degrees);
            Assert.Equal(7, l.Minutes);
            Assert.Equal(24, l.Seconds);
            Assert.Equal(LongitudePolarity.West, l.Polarity);
            Assert.Equal(-98.1233333333333, l.Value, 11);
            Assert.Equal("98W07'24\"", l.ToString());

            Assert.Throws<ArgumentOutOfRangeException>(() => new Longitude(198, 7, 24, LongitudePolarity.East));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Longitude(98, 77, 24, LongitudePolarity.East));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Longitude(98, 7, -24, LongitudePolarity.East));
        }

        [Fact]
        public void TestExplicitCast()
        {
            Double value = 278.123456789;

            Longitude l = value;

            Assert.Equal(98, l.Degrees);
            Assert.Equal(7, l.Minutes);
            Assert.Equal(24, l.Seconds);
            Assert.Equal(LongitudePolarity.East, l.Polarity);
            Assert.Equal(98.1233333333333, l.Value, 11);
            Assert.Equal("98E07'24\"", l.ToString());

            value = l;
            Assert.Equal(98.1233333333333, value, 11);

        }

    }
}
