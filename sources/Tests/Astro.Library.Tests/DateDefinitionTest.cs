using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Astro.Library.Tests
{
    public class DateDefinitionTest
    {
        [Fact]
        public void TestCreate()
        {
            var dd = new DateDefinition();
            Assert.Equal(0, dd.Year);
            Assert.Equal(0, dd.Month);
            Assert.Equal(0, dd.Day);
            Assert.Equal(0, dd.Hour);
            Assert.Equal(0, dd.Minute);
            Assert.Equal(0, dd.Second);
            Assert.Equal(0, dd.MilliSecond);
            Assert.Equal(TimeSpan.Zero, dd.UtcOffset);
            Assert.Equal(null, dd.TimeZone);
            Assert.Equal(DayLightDefinition.FromDotNet, dd.DayLight);

            dd = new DateDefinition(new DateTimeOffset(2015, 6, 3, 6, 6, 23, 123, TimeSpan.FromHours(3)), DayLightDefinition.Off);
            Assert.Equal(2015, dd.Year);
            Assert.Equal(6, dd.Month);
            Assert.Equal(3, dd.Day);
            Assert.Equal(6, dd.Hour);
            Assert.Equal(6, dd.Minute);
            Assert.Equal(23, dd.Second);
            Assert.Equal(123, dd.MilliSecond);
            Assert.Equal(TimeSpan.FromHours(3), dd.UtcOffset);
            Assert.Equal(null, dd.TimeZone);
            Assert.Equal(DayLightDefinition.Off, dd.DayLight);

            dd = new DateDefinition(new DateTime(2015, 6, 3, 6, 6, 23, 123, DateTimeKind.Utc), DayLightDefinition.On);
            Assert.Equal(2015, dd.Year);
            Assert.Equal(6, dd.Month);
            Assert.Equal(3, dd.Day);
            Assert.Equal(6, dd.Hour);
            Assert.Equal(6, dd.Minute);
            Assert.Equal(23, dd.Second);
            Assert.Equal(123, dd.MilliSecond);
            Assert.Equal(TimeSpan.FromHours(0), dd.UtcOffset);
            Assert.Same(TimeZoneInfo.Utc, dd.TimeZone);
            Assert.Equal(DayLightDefinition.On, dd.DayLight);
        }

        [Theory]
        [InlineData(0, 0, 0, 0d, DayLightDefinition.FromDotNet, 0d)]
        [InlineData(0, 0, 0, 0d, DayLightDefinition.On, 1d)]
        [InlineData(0, 0, 0, 0d, DayLightDefinition.Off, 0d)]
        [InlineData(0, 0, 0, 2d, DayLightDefinition.FromDotNet, 2d)]
        [InlineData(0, 0, 0, 2d, DayLightDefinition.On, 3d)]
        [InlineData(0, 0, 0, 2d, DayLightDefinition.Off, 2d)]
        [InlineData(1000, 0, 0, 0d, DayLightDefinition.FromDotNet, 0d)]
        [InlineData(1000, 0, 0, 0d, DayLightDefinition.On, 1d)]
        [InlineData(1000, 0, 0, 0d, DayLightDefinition.Off, 0d)]
        [InlineData(1000, 0, 0, 2d, DayLightDefinition.FromDotNet, 2d)]
        [InlineData(1000, 0, 0, 2d, DayLightDefinition.On, 3d)]
        [InlineData(1000, 0, 0, 2d, DayLightDefinition.Off, 2d)]
        [InlineData(2015, 7, 14, 0d, DayLightDefinition.FromDotNet, 0d)]
        [InlineData(2015, 7, 14, 0d, DayLightDefinition.On, 1d)]
        [InlineData(2015, 7, 14, 0d, DayLightDefinition.Off, 0d)]
        [InlineData(2015, 7, 14, 2d, DayLightDefinition.FromDotNet, 2d)]
        [InlineData(2015, 7, 14, 2d, DayLightDefinition.On, 3d)]
        [InlineData(2015, 7, 14, 2d, DayLightDefinition.Off, 2d)]
        public void TestGetDateOffset_ByOffset(int y, int m, int d, Double off, DayLightDefinition daylight, Double expected)
        {
            var dd = new DateDefinition {
                Year = y,
                Month = m,
                Day = d,
                UtcOffset = TimeSpan.FromHours(off),
                DayLight = daylight
            };
            Assert.Equal(TimeSpan.FromHours(expected), dd.GetDateOffset());
        }

        [Theory]
        [InlineData(0, 0, 0, "UTC", DayLightDefinition.FromDotNet, 0d)]
        [InlineData(0, 0, 0, "UTC", DayLightDefinition.On, 1d)]
        [InlineData(0, 0, 0, "UTC", DayLightDefinition.Off, 0d)]
        [InlineData(0, 0, 0, "Romance Standard Time", DayLightDefinition.FromDotNet, 1d)]
        [InlineData(0, 0, 0, "Romance Standard Time", DayLightDefinition.On, 2d)]
        [InlineData(0, 0, 0, "Romance Standard Time", DayLightDefinition.Off, 1d)]
        [InlineData(1000, 0, 0, "UTC", DayLightDefinition.FromDotNet, 0d)]
        [InlineData(1000, 0, 0, "UTC", DayLightDefinition.On, 1d)]
        [InlineData(1000, 0, 0, "UTC", DayLightDefinition.Off, 0d)]
        [InlineData(1000, 0, 0, "Romance Standard Time", DayLightDefinition.FromDotNet, 1d)]
        [InlineData(1000, 0, 0, "Romance Standard Time", DayLightDefinition.On, 2d)]
        [InlineData(1000, 0, 0, "Romance Standard Time", DayLightDefinition.Off, 1d)]
        [InlineData(2015, 7, 14, "UTC", DayLightDefinition.FromDotNet, 0d)]
        [InlineData(2015, 7, 14, "UTC", DayLightDefinition.On, 1d)]
        [InlineData(2015, 7, 14, "UTC", DayLightDefinition.Off, 0d)]
        [InlineData(2015, 7, 14, "Romance Standard Time", DayLightDefinition.FromDotNet, 2d)]
        [InlineData(2015, 7, 14, "Romance Standard Time", DayLightDefinition.On, 2d)]
        [InlineData(2015, 7, 14, "Romance Standard Time", DayLightDefinition.Off, 1d)]
        public void TestGetDateOffset_ByTimeZone(int y, int m, int d, String tz, DayLightDefinition daylight, Double expected)
        {
            var dd = new DateDefinition {
                Year = y,
                Month = m,
                Day = d,
                TimeZone = TimeZoneInfo.FindSystemTimeZoneById(tz),
                DayLight = daylight
            };
            Assert.Equal(TimeSpan.FromHours(expected), dd.GetDateOffset());
        }

        [Theory]
        [InlineData(0, 0, 0, 0d, DayLightDefinition.FromDotNet, null)]
        [InlineData(0, 0, 0, 0d, DayLightDefinition.On, null)]
        [InlineData(0, 0, 0, 0d, DayLightDefinition.Off, null)]
        [InlineData(0, 0, 0, 2d, DayLightDefinition.FromDotNet, null)]
        [InlineData(0, 0, 0, 2d, DayLightDefinition.On, null)]
        [InlineData(0, 0, 0, 2d, DayLightDefinition.Off, null)]
        [InlineData(1000, 0, 0, 0d, DayLightDefinition.FromDotNet, null)]
        [InlineData(1000, 0, 0, 0d, DayLightDefinition.On, null)]
        [InlineData(1000, 0, 0, 0d, DayLightDefinition.Off, null)]
        [InlineData(1000, 0, 0, 2d, DayLightDefinition.FromDotNet, null)]
        [InlineData(1000, 0, 0, 2d, DayLightDefinition.On, null)]
        [InlineData(1000, 0, 0, 2d, DayLightDefinition.Off, null)]
        [InlineData(2015, 7, 14, 0d, DayLightDefinition.FromDotNet, 0d)]
        [InlineData(2015, 7, 14, 0d, DayLightDefinition.On, 1d)]
        [InlineData(2015, 7, 14, 0d, DayLightDefinition.Off, 0d)]
        [InlineData(2015, 7, 14, 2d, DayLightDefinition.FromDotNet, 2d)]
        [InlineData(2015, 7, 14, 2d, DayLightDefinition.On, 3d)]
        [InlineData(2015, 7, 14, 2d, DayLightDefinition.Off, 2d)]
        public void TestToDateTimeOffset_ByOffset(int y, int m, int d, Double off, DayLightDefinition daylight, Double? expected)
        {
            var dd = new DateDefinition {
                Year = y,
                Month = m,
                Day = d,
                UtcOffset = TimeSpan.FromHours(off),
                DayLight = daylight
            };
            if (expected.HasValue)
            {
                var dto = dd.ToDateTimeOffset();
                Assert.Equal(y, dto.Year);
                Assert.Equal(m, dto.Month);
                Assert.Equal(d, dto.Day);
                Assert.Equal(expected, dto.Offset.TotalHours);
            }
            else
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => dd.ToDateTimeOffset());
            }
        }

        [Theory]
        [InlineData(0, 0, 0, "UTC", DayLightDefinition.FromDotNet, null)]
        [InlineData(0, 0, 0, "UTC", DayLightDefinition.On, null)]
        [InlineData(0, 0, 0, "UTC", DayLightDefinition.Off, null)]
        [InlineData(0, 0, 0, "Romance Standard Time", DayLightDefinition.FromDotNet, null)]
        [InlineData(0, 0, 0, "Romance Standard Time", DayLightDefinition.On, null)]
        [InlineData(0, 0, 0, "Romance Standard Time", DayLightDefinition.Off, null)]
        [InlineData(1000, 0, 0, "UTC", DayLightDefinition.FromDotNet, null)]
        [InlineData(1000, 0, 0, "UTC", DayLightDefinition.On, null)]
        [InlineData(1000, 0, 0, "UTC", DayLightDefinition.Off, null)]
        [InlineData(1000, 0, 0, "Romance Standard Time", DayLightDefinition.FromDotNet, null)]
        [InlineData(1000, 0, 0, "Romance Standard Time", DayLightDefinition.On, null)]
        [InlineData(1000, 0, 0, "Romance Standard Time", DayLightDefinition.Off, null)]
        [InlineData(2015, 7, 14, "UTC", DayLightDefinition.FromDotNet, 0d)]
        [InlineData(2015, 7, 14, "UTC", DayLightDefinition.On, 1d)]
        [InlineData(2015, 7, 14, "UTC", DayLightDefinition.Off, 0d)]
        [InlineData(2015, 7, 14, "Romance Standard Time", DayLightDefinition.FromDotNet, 2d)]
        [InlineData(2015, 7, 14, "Romance Standard Time", DayLightDefinition.On, 2d)]
        [InlineData(2015, 7, 14, "Romance Standard Time", DayLightDefinition.Off, 1d)]
        public void TestToDateTimeOffset_ByTimeZone(int y, int m, int d, String tz, DayLightDefinition daylight, Double? expected)
        {
            var dd = new DateDefinition {
                Year = y,
                Month = m,
                Day = d,
                TimeZone = TimeZoneInfo.FindSystemTimeZoneById(tz),
                DayLight = daylight
            };
            if (expected.HasValue)
            {
                var dto = dd.ToDateTimeOffset();
                Assert.Equal(y, dto.Year);
                Assert.Equal(m, dto.Month);
                Assert.Equal(d, dto.Day);
                Assert.Equal(expected, dto.Offset.TotalHours);
            }
            else
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => dd.ToDateTimeOffset());
            }
        }

        [Theory]
        [InlineData(0, 0, 0, 0d, DayLightDefinition.FromDotNet, null)]
        [InlineData(0, 0, 0, 0d, DayLightDefinition.On, null)]
        [InlineData(0, 0, 0, 0d, DayLightDefinition.Off, null)]
        [InlineData(0, 0, 0, 2d, DayLightDefinition.FromDotNet, null)]
        [InlineData(0, 0, 0, 2d, DayLightDefinition.On, null)]
        [InlineData(0, 0, 0, 2d, DayLightDefinition.Off, null)]
        [InlineData(1000, 0, 0, 0d, DayLightDefinition.FromDotNet, null)]
        [InlineData(1000, 0, 0, 0d, DayLightDefinition.On, null)]
        [InlineData(1000, 0, 0, 0d, DayLightDefinition.Off, null)]
        [InlineData(1000, 0, 0, 2d, DayLightDefinition.FromDotNet, null)]
        [InlineData(1000, 0, 0, 2d, DayLightDefinition.On, null)]
        [InlineData(1000, 0, 0, 2d, DayLightDefinition.Off, null)]
        [InlineData(2015, 7, 14, 0d, DayLightDefinition.FromDotNet, 0d)]
        [InlineData(2015, 7, 14, 0d, DayLightDefinition.On, 1d)]
        [InlineData(2015, 7, 14, 0d, DayLightDefinition.Off, 0d)]
        [InlineData(2015, 7, 14, 2d, DayLightDefinition.FromDotNet, 2d)]
        [InlineData(2015, 7, 14, 2d, DayLightDefinition.On, 3d)]
        [InlineData(2015, 7, 14, 2d, DayLightDefinition.Off, 2d)]
        public void TestToDateTime_ByOffset(int y, int m, int d, Double off, DayLightDefinition daylight, Double? expected)
        {
            var dd = new DateDefinition {
                Year = y,
                Month = m,
                Day = d,
                UtcOffset = TimeSpan.FromHours(off),
                DayLight = daylight
            };
            if (expected.HasValue)
            {
                var dto = dd.ToDateTime();
                Assert.Equal(y, dto.Year);
                Assert.Equal(m, dto.Month);
                Assert.Equal(d, dto.Day);
                //Assert.Equal(expected, dto.Offset.TotalHours);
            }
            else
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => dd.ToDateTime());
            }
        }

        [Theory]
        [InlineData(0, 0, 0, "UTC", DayLightDefinition.FromDotNet, null)]
        [InlineData(0, 0, 0, "UTC", DayLightDefinition.On, null)]
        [InlineData(0, 0, 0, "UTC", DayLightDefinition.Off, null)]
        [InlineData(0, 0, 0, "Romance Standard Time", DayLightDefinition.FromDotNet, null)]
        [InlineData(0, 0, 0, "Romance Standard Time", DayLightDefinition.On, null)]
        [InlineData(0, 0, 0, "Romance Standard Time", DayLightDefinition.Off, null)]
        [InlineData(1000, 0, 0, "UTC", DayLightDefinition.FromDotNet, null)]
        [InlineData(1000, 0, 0, "UTC", DayLightDefinition.On, null)]
        [InlineData(1000, 0, 0, "UTC", DayLightDefinition.Off, null)]
        [InlineData(1000, 0, 0, "Romance Standard Time", DayLightDefinition.FromDotNet, null)]
        [InlineData(1000, 0, 0, "Romance Standard Time", DayLightDefinition.On, null)]
        [InlineData(1000, 0, 0, "Romance Standard Time", DayLightDefinition.Off, null)]
        [InlineData(2015, 7, 14, "UTC", DayLightDefinition.FromDotNet, 0d)]
        [InlineData(2015, 7, 14, "UTC", DayLightDefinition.On, 1d)]
        [InlineData(2015, 7, 14, "UTC", DayLightDefinition.Off, 0d)]
        [InlineData(2015, 7, 14, "Romance Standard Time", DayLightDefinition.FromDotNet, 2d)]
        [InlineData(2015, 7, 14, "Romance Standard Time", DayLightDefinition.On, 2d)]
        [InlineData(2015, 7, 14, "Romance Standard Time", DayLightDefinition.Off, 1d)]
        public void TestToDateTime_ByTimeZone(int y, int m, int d, String tz, DayLightDefinition daylight, Double? expected)
        {
            var dd = new DateDefinition {
                Year = y,
                Month = m,
                Day = d,
                TimeZone = TimeZoneInfo.FindSystemTimeZoneById(tz),
                DayLight = daylight
            };
            if (expected.HasValue)
            {
                var dto = dd.ToDateTime();
                Assert.Equal(y, dto.Year);
                Assert.Equal(m, dto.Month);
                Assert.Equal(d, dto.Day);
                //Assert.Equal(expected, dto.Offset.TotalHours);
            }
            else
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => dd.ToDateTime());
            }
        }

        [Fact]
        public void TestSetDateTimeOffset()
        {
            DateDefinition def = new DateDefinition(DateTime.Now);
            Assert.Equal(TimeSpan.FromHours(0), def.UtcOffset);
            Assert.Same(TimeZoneInfo.Local, def.TimeZone);

            def.SetDate(new DateTimeOffset(2015, 6, 3, 6, 6, 23, 123, TimeSpan.FromHours(3)));
            Assert.Equal(2015, def.Year);
            Assert.Equal(6, def.Month);
            Assert.Equal(3, def.Day);
            Assert.Equal(6, def.Hour);
            Assert.Equal(6, def.Minute);
            Assert.Equal(23, def.Second);
            Assert.Equal(123, def.MilliSecond);
            Assert.Equal(TimeSpan.FromHours(3), def.UtcOffset);
            Assert.Equal(null, def.TimeZone);
            Assert.Equal(DayLightDefinition.FromDotNet, def.DayLight);
        }

        [Fact]
        public void TestSetDateTime()
        {
            DateDefinition def = new DateDefinition(DateTimeOffset.Now);
            Assert.Equal(TimeZoneInfo.Local.GetUtcOffset(DateTimeOffset.Now), def.UtcOffset);
            Assert.Equal(null, def.TimeZone);

            def.SetDate(new DateTime(2015, 6, 3, 6, 6, 23, 123,DateTimeKind.Unspecified));
            Assert.Equal(2015, def.Year);
            Assert.Equal(6, def.Month);
            Assert.Equal(3, def.Day);
            Assert.Equal(6, def.Hour);
            Assert.Equal(6, def.Minute);
            Assert.Equal(23, def.Second);
            Assert.Equal(123, def.MilliSecond);
            Assert.Equal(TimeSpan.FromHours(0), def.UtcOffset);
            Assert.Same(TimeZoneInfo.Local, def.TimeZone);
            Assert.Equal(DayLightDefinition.FromDotNet, def.DayLight);
        }

        [Fact]
        public void TestEquals()
        {
            DateTimeOffset now = DateTimeOffset.Now;
            DateDefinition def = new DateDefinition(now);

            Assert.False(def.Equals((DateDefinition)null));
            Assert.True(def.Equals(def));
            Assert.True(def.Equals(new DateDefinition(now)));
            Assert.False(def.Equals(new DateDefinition(now, DayLightDefinition.On)));
            Assert.False(def.Equals(new DateDefinition(now.ToUniversalTime())));
            Assert.False(def.Equals(new DateDefinition(now.DateTime)));
            Assert.False(def.Equals(new DateDefinition(now.DateTime.ToUniversalTime())));

            def = new DateDefinition(now.DateTime);
            Assert.False(def.Equals(new DateDefinition(now)));
            Assert.False(def.Equals(new DateDefinition(now, DayLightDefinition.On)));
            Assert.False(def.Equals(new DateDefinition(now.ToUniversalTime())));
            Assert.True(def.Equals(new DateDefinition(now.DateTime)));
            Assert.False(def.Equals(new DateDefinition(now.DateTime.ToUniversalTime())));

        }
    }
}
