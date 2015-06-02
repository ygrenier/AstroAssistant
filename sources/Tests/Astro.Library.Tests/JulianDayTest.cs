using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Astro.Library.Tests
{
    public class JulianDayTest
    {
        [Fact]
        public void TestCreate()
        {
            var jd = new JulianDay();
            Assert.Equal(0.0, jd.Value);
            Assert.Equal(DateCalendar.Julian, jd.Calendar);

            jd = new JulianDay(JulianDay.J2000);
            Assert.Equal(JulianDay.J2000, jd.Value);
            Assert.Equal(DateCalendar.Gregorian, jd.Calendar);

            jd = new JulianDay(JulianDay.GregorianFirstJD - 1000);
            Assert.Equal(JulianDay.GregorianFirstJD - 1000, jd.Value);
            Assert.Equal(DateCalendar.Julian, jd.Calendar);

        }

        [Fact]
        public void TestGetCalendar()
        {
            Assert.Equal(DateCalendar.Julian, JulianDay.GetCalendar(0));
            Assert.Equal(DateCalendar.Gregorian, JulianDay.GetCalendar(JulianDay.J2000));
            Assert.Equal(DateCalendar.Julian, JulianDay.GetCalendar(JulianDay.GregorianFirstJD - 1000));
            Assert.Equal(DateCalendar.Gregorian, JulianDay.GetCalendar(JulianDay.GregorianFirstJD));
            Assert.Equal(DateCalendar.Julian, JulianDay.GetCalendar(JulianDay.GregorianFirstJD - 0.1));

            Assert.Equal(DateCalendar.Julian, JulianDay.GetCalendar(0, 0, 0));
            Assert.Equal(DateCalendar.Gregorian, JulianDay.GetCalendar(2000, 1, 1));
            Assert.Equal(DateCalendar.Gregorian, JulianDay.GetCalendar(1582, 11, 15));
            Assert.Equal(DateCalendar.Julian, JulianDay.GetCalendar(1582, 11, 14));
        }

        [Fact]
        public void TestImplicitCast()
        {
            JulianDay jd = 2451545.0;
            Assert.Equal(JulianDay.J2000, jd.Value);
            Assert.Equal(DateCalendar.Gregorian, jd.Calendar);

            Double d = jd;
            Assert.Equal(JulianDay.J2000, d);
        }

        [Fact]
        public void TestToString()
        {
            JulianDay jd = JulianDay.J2000;
            Assert.Equal(JulianDay.J2000.ToString(), jd.ToString());
        }

    }
}
