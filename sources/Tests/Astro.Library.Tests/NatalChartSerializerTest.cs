using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Astro.Library.Tests
{
    public class NatalChartSerializerTest
    {

        [Fact]
        public async Task TestSerializeDeserialize()
        {
            NatalChartDefinition def = new NatalChartDefinition {
                Name = "Nom",
                BirthPlaceName = "Lieu de naissance",
                BirthPlacePosition = new GeoPosition(123.45, 543.21, 999),
                Gender=Gender.Female,
                HouseSystem=HouseSystem.Placidus,
                PositionCenter=PositionCenter.Topocentric
            };
            def.BirthDate.SetDate(DateTimeOffset.Now);
            NatalChartDefinition actual;

            NatalChartSerializer ser = new NatalChartSerializer();
            Assert.Null(ser.TimeZoneProvider);
            using (var str = new MemoryStream())
            {
                await ser.Serialize(def, str);
                str.Seek(0, SeekOrigin.Begin);
                actual = await ser.Deserialize(str);
            }
            Assert.Equal(def.Name, actual.Name);
            Assert.Equal(def.BirthDate, actual.BirthDate);
            Assert.Equal(def.BirthPlaceName, actual.BirthPlaceName);
            Assert.Equal(def.BirthPlacePosition.ToString(), actual.BirthPlacePosition.ToString());
            Assert.Equal(def.Gender, actual.Gender);
            Assert.Equal(def.HouseSystem, actual.HouseSystem);
            Assert.Equal(def.PositionCenter, actual.PositionCenter);
            Assert.Equal(def.Planets.Count, actual.Planets.Count);

            var tzpMock = new Mock<ITimeZoneProvider>();
            tzpMock.Setup(p => p.FindTimeZone(It.IsAny<String>())).Returns<String>(s => TimeZoneInfo.GetSystemTimeZones().FirstOrDefault(t => t.StandardName == s) ?? TimeZoneInfo.FindSystemTimeZoneById(s));
            var tzp = tzpMock.Object;
            ser = new NatalChartSerializer(tzp);
            Assert.Same(tzp, ser.TimeZoneProvider);
            def.BirthDate.SetDate(DateTime.Now);
            using (var str = new MemoryStream())
            {
                await ser.Serialize(def, str);
                str.Seek(0, SeekOrigin.Begin);
                actual = await ser.Deserialize(str);
            }
            Assert.Equal(def.Name, actual.Name);
            Assert.Equal(def.BirthDate, actual.BirthDate);
            Assert.Equal(def.BirthPlaceName, actual.BirthPlaceName);
            Assert.Equal(def.BirthPlacePosition.ToString(), actual.BirthPlacePosition.ToString());
            Assert.Equal(def.Gender, actual.Gender);
            Assert.Equal(def.HouseSystem, actual.HouseSystem);
            Assert.Equal(def.PositionCenter, actual.PositionCenter);
            Assert.Equal(def.Planets.Count, actual.Planets.Count);

            String xml = @"
<natal-chart>
<birth-date>
<date>12/06/2015</date>
<utc-offset>None</utc-offset>
</birth-date>
</natal-chart>
";
            using (var str = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                await Assert.ThrowsAsync<FormatException>(() => ser.Deserialize(str));
            }

            xml = @"
<natal-chart>
<planets>
<planet id='Moon'/>
<planet id='18'/>
</planets>
</natal-chart>
";
            using (var str = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                actual = await ser.Deserialize(str);
            }
            Assert.Equal(1, actual.Planets.Count);
            Assert.Equal(18, actual.Planets[0].Id);

            xml = @"
<natal-chart>
<birth-place-position>
<altitude>None</altitude>
<longitude>1E</longitude>
<latitude>2N</latitude>
</birth-place-position>
</natal-chart>
";
            using (var str = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                actual = await ser.Deserialize(str);
            }
            Assert.Equal(1, actual.BirthPlacePosition.Longitude);
            Assert.Equal(2, actual.BirthPlacePosition.Latitude);
            Assert.Equal(0, actual.BirthPlacePosition.Altitude);

            await Assert.ThrowsAsync<ArgumentNullException>(() => ser.Serialize(null, null));
            await Assert.ThrowsAsync<ArgumentNullException>(() => ser.Serialize(def, null));
            await Assert.ThrowsAsync<ArgumentNullException>(() => ser.Deserialize(null));

        }

    }
}
