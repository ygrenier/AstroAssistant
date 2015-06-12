using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Astro
{

    /// <summary>
    /// Outils de sérialisation d'un thème
    /// </summary>
    public class NatalChartSerializer
    {
        /// <summary>
        /// Création d'un nouveau sérialiseur de thème
        /// </summary>
        public NatalChartSerializer(ITimeZoneProvider timeZoneProvider = null)
        {
            this.TimeZoneProvider = timeZoneProvider;
        }

        /// <summary>
        /// Sérialisation d'une définition de thème
        /// </summary>
        public Task Serialize(NatalChartDefinition definition, Stream stream)
        {
            if (definition == null) throw new ArgumentNullException("definition");
            if (stream == null) throw new ArgumentNullException("stream");
            return Task.Factory.StartNew(() => {
                XmlWriterSettings settings = new XmlWriterSettings {
                    CloseOutput = false,
                    Encoding = Encoding.UTF8,
                    Indent = true,
                    IndentChars = "  ",
                    OmitXmlDeclaration = false
                };
                using (XmlWriter writer = XmlWriter.Create(stream, settings))
                {
                    writer.WriteStartDocument();
                    //writer.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
                    writer.WriteStartElement("natal-chart");
                    writer.WriteElementString("name", definition.Name);

                    writer.WriteElementString("gender", definition.Gender.ToString());

                    writer.WriteStartElement("birth-date");
                    writer.WriteElementString("date", String.Format("{0:D4}-{1:D2}-{2:D2}", definition.BirthDate.Year, definition.BirthDate.Month, definition.BirthDate.Day));
                    writer.WriteElementString("time", String.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}", definition.BirthDate.Hour, definition.BirthDate.Minute, definition.BirthDate.Second, definition.BirthDate.MilliSecond));
                    writer.WriteElementString("timezone", definition.BirthDate.TimeZone != null ? definition.BirthDate.TimeZone.StandardName : String.Empty);
                    writer.WriteElementString("utc-offset", (definition.BirthDate.TimeZone != null ? definition.BirthDate.TimeZone.BaseUtcOffset : definition.BirthDate.UtcOffset).TotalHours.ToString(System.Globalization.CultureInfo.InvariantCulture));
                    writer.WriteElementString("daylight", definition.BirthDate.DayLight.ToString());
                    writer.WriteEndElement();

                    writer.WriteElementString("birth-place-name", definition.BirthPlaceName);

                    writer.WriteStartElement("birth-place-position");
                    writer.WriteElementString("longitude", definition.BirthPlacePosition.Longitude.ToString());
                    writer.WriteElementString("latitude", definition.BirthPlacePosition.Latitude.ToString());
                    writer.WriteElementString("altitude", definition.BirthPlacePosition.Altitude.ToString());
                    writer.WriteEndElement();

                    writer.WriteElementString("position-center", definition.PositionCenter.ToString());
                    writer.WriteElementString("house-system", definition.HouseSystem.ToString());

                    writer.WriteStartElement("planets");
                    foreach (var planet in definition.Planets)
                    {
                        writer.WriteStartElement("planet");
                        writer.WriteAttributeString("id", planet.Id.ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Flush();
                }
            });
        }

        void Deserialize(DateDefinition date, XmlReader reader)
        {
            while (reader.Read())
            {
                while (reader.IsStartElement())
                {
                    try
                    {
                        switch (reader.Name)
                        {
                            case "date":
                                var matchD = Regex.Match(reader.ReadElementContentAsString(), @"(?<y>\d{4})-(?<m>\d{1,2})-(?<d>\d{1,2})");
                                if (matchD.Success)
                                {
                                    date.Year = int.Parse(matchD.Groups["y"].Value);
                                    date.Month = int.Parse(matchD.Groups["m"].Value);
                                    date.Day = int.Parse(matchD.Groups["d"].Value);
                                }
                                break;
                            case "time":
                                var matchT = Regex.Match(reader.ReadElementContentAsString(), @"(?<h>\d{1,2}):(?<m>\d{1,2})(:(?<s>\d{1,2}))?(\.(?<l>\d{1,4}))?");
                                if (matchT.Success)
                                {
                                    date.Hour = int.Parse(matchT.Groups["h"].Value);
                                    date.Minute = int.Parse(matchT.Groups["m"].Value);
                                    date.Second = String.IsNullOrWhiteSpace(matchT.Groups["s"].Value) ? 0 : int.Parse(matchT.Groups["s"].Value);
                                    date.MilliSecond = String.IsNullOrWhiteSpace(matchT.Groups["l"].Value) ? 0 : int.Parse(matchT.Groups["l"].Value);
                                }
                                break;
                            case "timezone":
                                String content = reader.ReadElementContentAsString();
                                if (!String.IsNullOrWhiteSpace(content) && TimeZoneProvider != null)
                                {
                                    date.TimeZone = TimeZoneProvider.FindTimeZone(content);
                                }
                                break;
                            case "utc-offset":
                                if (date.TimeZone == null)
                                {
                                    date.UtcOffset = TimeSpan.FromHours(Double.Parse(reader.ReadElementContentAsString(), System.Globalization.CultureInfo.InvariantCulture));
                                }
                                else
                                {
                                    reader.Read();
                                }
                                break;
                            case "daylight":
                                DayLightDefinition dld;
                                if (Enum.TryParse<DayLightDefinition>(reader.ReadElementContentAsString(), true, out dld))
                                    date.DayLight = dld;
                                break;
                            default:
                                reader.Read();
                                break;
                        }
                    }
                    catch
                    {
                        reader.Read();
                        throw;
                    }
                }
            }
        }

        void Deserialize(IList<Planet> planets, XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    try
                    {
                        switch (reader.Name)
                        {
                            case "planet":
                                int pId = int.MinValue;
                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "id")
                                    {
                                        pId = int.Parse(reader.Value);
                                    }
                                }
                                reader.MoveToElement();
                                if (pId != int.MinValue)
                                    planets.Add(new Planet(pId));
                                break;
                            default:
                                break;
                        }
                    }
                    catch { }
                }
            }
        }

        GeoPosition DeserializeGeoPosition(XmlReader reader)
        {
            GeoPosition result=new GeoPosition();
            while (reader.Read())
            {
                while (reader.IsStartElement())
                {
                    try
                    {
                        switch (reader.Name)
                        {
                            case "longitude":
                                result.Longitude = Longitude.Parse(reader.ReadElementContentAsString());
                                break;
                            case "latitude":
                                result.Latitude = Latitude.Parse(reader.ReadElementContentAsString());
                                break;
                            case "altitude":
                                result.Altitude = Double.Parse(reader.ReadElementContentAsString(), System.Globalization.CultureInfo.InvariantCulture);
                                break;
                            default:
                                reader.Read();
                                break;
                        }
                    }
                    catch { }
                }
            }
            return result;
        }

        /// <summary>
        /// Désérialisation d'une définition de thème
        /// </summary>
        public Task<NatalChartDefinition> Deserialize(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            return Task.Factory.StartNew<NatalChartDefinition>(() => {
                var result = new NatalChartDefinition();
                result.Planets.Clear();
                XmlReaderSettings settings = new XmlReaderSettings {
                    CloseInput = false,
                    IgnoreComments = true,
                    IgnoreProcessingInstructions = true,
                    IgnoreWhitespace = true
                };
                using (var reader = XmlReader.Create(stream, settings))
                {
                    while (reader.Read())
                    {
                        while (reader.IsStartElement())
                        {
                            switch (reader.Name)
                            {
                                case "name":
                                    result.Name = reader.ReadElementContentAsString();
                                    break;
                                case "gender":
                                    Gender gender;
                                    if (Enum.TryParse<Gender>(reader.ReadElementContentAsString(), true, out gender))
                                        result.Gender = gender;
                                    break;
                                case "birth-date":
                                    using (var sTree = reader.ReadSubtree())
                                        Deserialize(result.BirthDate, sTree);
                                    break;
                                case "birth-place-name":
                                    result.BirthPlaceName = reader.ReadElementContentAsString();
                                    break;
                                case "birth-place-position":
                                    using (var sTree = reader.ReadSubtree())
                                        result.BirthPlacePosition = DeserializeGeoPosition(sTree);
                                    break;
                                case "position-center":
                                    PositionCenter posCent;
                                    if (Enum.TryParse<PositionCenter>(reader.ReadElementContentAsString(), true, out posCent))
                                        result.PositionCenter = posCent;
                                    break;
                                case "house-system":
                                    HouseSystem hSys;
                                    if (Enum.TryParse<HouseSystem>(reader.ReadElementContentAsString(), true, out hSys))
                                        result.HouseSystem = hSys;
                                    break;
                                case "planets":
                                    result.Planets.Clear();
                                    using (var sTree = reader.ReadSubtree())
                                        Deserialize(result.Planets, sTree);
                                    break;
                                default:
                                    reader.Read();
                                    break;
                            }
                        }
                    }
                }
                return result;
            });
        }

        /// <summary>
        /// Fournisseur de timezone
        /// </summary>
        public ITimeZoneProvider TimeZoneProvider { get; set; }

    }

}
