using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Astro.Library.Tests
{
    public class EphemerisTimeTest
    {

        [Fact]
        public void TestCreate()
        {
            var et = new EphemerisTime();
            Assert.Equal(0, et.JulianDay);
            Assert.Equal(0, et.DeltaT);
            Assert.Equal(0, et.Value);

            var jd = new JulianDay(JulianDay.J2000);
            et = new EphemerisTime(jd, 12.34);
            Assert.Equal(2451545, et.JulianDay);
            Assert.Equal(12.34, et.DeltaT);
            Assert.Equal(2451545 + 12.34, et.Value);
        }

        [Fact]
        public void TestToString()
        {
            var jd = new JulianDay(JulianDay.J2000);
            var et = new EphemerisTime(jd, 12.34);
            Assert.Equal((2451557.34).ToString(), et.ToString());
        }

        [Fact]
        public void TestCastToDouble()
        {
            var jd = new JulianDay(JulianDay.J2000);
            var et = new EphemerisTime(jd, 12.34);
            Assert.Equal(2451545 + 12.34, (double)et);
        }

    }
}
