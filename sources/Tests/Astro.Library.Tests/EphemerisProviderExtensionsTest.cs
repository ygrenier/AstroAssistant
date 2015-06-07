using Astro;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Astro.Library.Tests
{
    public class EphemerisProviderExtensionsTest
    {
        [Fact]
        public void TestToJulianDay()
        {
            var mockProvider = new Mock<IEphemerisProvider>();
            var provider = mockProvider.Object;

            DateDefinition dd = new DateDefinition {
                Year = 2015,
                Month = 6,
                Day = 7
            };
            var jd = provider.ToJulianDay(dd, DateCalendar.Gregorian);
            Assert.Equal(0, jd);
            jd = provider.ToJulianDay(dd, DateCalendar.Julian);
            Assert.Equal(0, jd);

            mockProvider.Verify(p => p.ToJulianDay(It.IsAny<DateTimeOffset>(), DateCalendar.Gregorian), Times.Once());
            mockProvider.Verify(p => p.ToJulianDay(It.IsAny<DateTimeOffset>(), DateCalendar.Julian), Times.Once());

            Assert.Throws<ArgumentNullException>(() => EphemerisProviderExtensions.ToJulianDay(null, null));
            Assert.Throws<ArgumentNullException>(() => EphemerisProviderExtensions.ToJulianDay(provider, null));
        }

        [Fact]
        public void TestToSideralTime()
        {
            var mockProvider = new Mock<IEphemerisProvider>();
            mockProvider.Setup(p => p.ToSideralTime(It.IsAny<JulianDay>())).Returns<JulianDay>(j => j.Value);
            var provider = mockProvider.Object;

            var jd = new JulianDay(20.67);
            var sd = provider.ToSideralTime(jd, 12);
            Assert.Equal(21.47, sd.Value, 2);

            sd = provider.ToSideralTime(jd, new Longitude(130, 12, 11));
            Assert.Equal(5.35020370370371, sd.Value, 14);

            jd = new JulianDay(4.67);
            sd = provider.ToSideralTime(jd, new Longitude(-130, 12, 11));
            Assert.Equal(19.9897962962963, sd.Value, 14);

            Assert.Throws<ArgumentNullException>(() => EphemerisProviderExtensions.ToSideralTime(null, 0, 0));
        }

    }
}
