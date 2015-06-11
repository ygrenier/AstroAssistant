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

        [Fact]
        public void TestParse()
        {
            Latitude lat = Latitude.Parse("1N");
            Assert.Equal(1.0, (Double)lat);
            lat = Latitude.Parse("1N23'");
            Assert.Equal(1.38333333333333, (Double)lat, 14);
            lat = Latitude.Parse("1N23\"");
            Assert.Equal(1.00638888888889, (Double)lat, 14);
            lat = Latitude.Parse("1N23'45\"");
            Assert.Equal(1.39583333333333, (Double)lat, 14);
            lat = Latitude.Parse("2s");
            Assert.Equal(-2.0, (Double)lat);

            Assert.Throws<FormatException>(() => Latitude.Parse(null));
            Assert.Throws<FormatException>(() => Latitude.Parse(""));
            Assert.Throws<FormatException>(() => Latitude.Parse("1"));
            Assert.Throws<FormatException>(() => Latitude.Parse("1N88'"));
            Assert.Throws<FormatException>(() => Latitude.Parse("1N22'88\""));
        }

        [Fact]
        public void TestTryParse()
        {
            Latitude lat;
            Assert.True(Latitude.TryParse("1N", out lat));
            Assert.Equal(1.0, (Double)lat);
            Assert.True(Latitude.TryParse("1N23'", out lat));
            Assert.Equal(1.38333333333333, (Double)lat, 14);
            Assert.True(Latitude.TryParse("1N23\"", out lat));
            Assert.Equal(1.00638888888889, (Double)lat, 14);
            Assert.True(Latitude.TryParse("1N23'45\"", out lat));
            Assert.Equal(1.39583333333333, (Double)lat, 14);
            Assert.True(Latitude.TryParse("2s", out lat));
            Assert.Equal(-2.0, (Double)lat);

            Assert.False(Latitude.TryParse(null, out lat));
            Assert.False(Latitude.TryParse("", out lat));
            Assert.False(Latitude.TryParse("1", out lat));
            Assert.False(Latitude.TryParse("1N88'", out lat));
            Assert.False(Latitude.TryParse("1N22'88\"", out lat));
        }

    }
}
