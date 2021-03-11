using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WeatherHistoryViewer.Core.Models.Weather
{
    #region Enums

    public enum HttpStatusTypes
    {
        not_found = 404,
        invalid_acces_key = 101,
        missing_query = 601,
        no_results = 602,
        invalid_unit = 606,
        request_failed = 615
    }

    #endregion

    #region Classes

    public class HttpStatusModel
    {
        public short Code { get; set; }
        public string Type { get; set; }
        public string Info { get; set; }
    }

    public class Request
    {
        [JsonPropertyName("type")] public string Type { get; set; }

        [JsonPropertyName("query")] public string Query { get; set; }

        [JsonPropertyName("language")] public string Language { get; set; }

        [JsonPropertyName("unit")] public string Unit { get; set; }
    }

    public class Location
    {
        [JsonPropertyName("name")] public string Name { get; set; }

        [JsonPropertyName("country")] public string Country { get; set; }

        [JsonPropertyName("region")] public string Region { get; set; }

        [JsonPropertyName("lat")] public string Lat { get; set; }

        [JsonPropertyName("lon")] public string Lon { get; set; }

        [JsonPropertyName("timezone_id")] public string TimezoneId { get; set; }

        [NotMapped]
        [JsonPropertyName("localtime")]
        public string Localtime { get; set; }

        [JsonPropertyName("localtime_epoch")]
        [NotMapped]
        public int LocaltimeEpoch { get; set; }

        [JsonPropertyName("utc_offset")] public string UtcOffset { get; set; }
    }

    public class Current
    {
        [JsonPropertyName("observation_time")] public string ObservationTime { get; set; }

        [JsonPropertyName("temperature")] public int Temperature { get; set; }

        [JsonPropertyName("weather_code")] public int WeatherCode { get; set; }

        [JsonPropertyName("weather_icons")]
        [NotMapped]
        public List<string> WeatherIcons { get; set; }

        [JsonPropertyName("weather_descriptions")]
        [NotMapped]
        public List<string> WeatherDescriptions { get; set; }

        [JsonPropertyName("wind_speed")] public int WindSpeed { get; set; }

        [JsonPropertyName("wind_degree")] public int WindDegree { get; set; }

        [JsonPropertyName("wind_dir")] public string WindDir { get; set; }

        [JsonPropertyName("pressure")] public int Pressure { get; set; }

        [JsonPropertyName("precip")] public double Precip { get; set; }

        [JsonPropertyName("humidity")] public int Humidity { get; set; }

        [JsonPropertyName("cloudcover")] public int Cloudcover { get; set; }

        [JsonPropertyName("feelslike")] public int Feelslike { get; set; }

        [JsonPropertyName("uv_index")] public int UvIndex { get; set; }

        [JsonPropertyName("visibility")] public int Visibility { get; set; }

        [JsonPropertyName("is_day")] public string IsDay { get; set; }
    }

    public class Astro
    {
        [JsonPropertyName("sunrise")] public string Sunrise { get; set; }

        [JsonPropertyName("sunset")] public string Sunset { get; set; }

        [JsonPropertyName("moonrise")] public string Moonrise { get; set; }

        [JsonPropertyName("moonset")] public string Moonset { get; set; }

        [JsonPropertyName("moon_phase")] public string MoonPhase { get; set; }

        [JsonPropertyName("moon_illumination")]
        public int MoonIllumination { get; set; }
    }

    public class WeatherSnapshot
    {
        public int Id { get; set; }
        public int HistoricalWeatherId { get; set; }
        public HistoricalWeather HistoricalWeather { get; set; }
        public string FullDate { get; set; }
        [JsonPropertyName("time")] public string Time { get; set; }

        [JsonPropertyName("temperature")] public int Temperature { get; set; }

        [JsonPropertyName("wind_speed")] public int WindSpeed { get; set; }

        [JsonPropertyName("wind_degree")] public int WindDegree { get; set; }

        [JsonPropertyName("wind_dir")] public string WindDir { get; set; }

        [JsonPropertyName("weather_code")] public int WeatherCode { get; set; }

        [JsonPropertyName("weather_icons")]
        [NotMapped]
        public List<string> WeatherIcons { get; set; }

        [JsonPropertyName("weather_descriptions")]
        [NotMapped]
        public List<string> WeatherDescriptions { get; set; }

        [JsonPropertyName("precip")] public double Precip { get; set; }

        [JsonPropertyName("humidity")] public int Humidity { get; set; }

        [JsonPropertyName("visibility")] public int Visibility { get; set; }

        [JsonPropertyName("pressure")] public int Pressure { get; set; }

        [JsonPropertyName("cloudcover")] public int Cloudcover { get; set; }

        [JsonPropertyName("heatindex")] public int Heatindex { get; set; }

        [JsonPropertyName("dewpoint")] public int Dewpoint { get; set; }

        [JsonPropertyName("windchill")] public int Windchill { get; set; }

        [JsonPropertyName("windgust")] public int Windgust { get; set; }

        [JsonPropertyName("feelslike")] public int Feelslike { get; set; }

        [JsonPropertyName("chanceofrain")] public int Chanceofrain { get; set; }

        [JsonPropertyName("chanceofremdry")] public int Chanceofremdry { get; set; }

        [JsonPropertyName("chanceofwindy")] public int Chanceofwindy { get; set; }

        [JsonPropertyName("chanceofovercast")] public int Chanceofovercast { get; set; }

        [JsonPropertyName("chanceofsunshine")] public int Chanceofsunshine { get; set; }

        [JsonPropertyName("chanceoffrost")] public int Chanceoffrost { get; set; }

        [JsonPropertyName("chanceofhightemp")] public int Chanceofhightemp { get; set; }

        [JsonPropertyName("chanceoffog")] public int Chanceoffog { get; set; }

        [JsonPropertyName("chanceofsnow")] public int Chanceofsnow { get; set; }

        [JsonPropertyName("chanceofthunder")] public int Chanceofthunder { get; set; }

        [JsonPropertyName("uv_index")] public int UvIndex { get; set; }
    }

    public class Day
    {
        [JsonPropertyName("date")] public string Date { get; set; }

        [JsonPropertyName("date_epoch")] public int DateEpoch { get; set; }

        [JsonPropertyName("astro")] public Astro Astro { get; set; }

        [JsonPropertyName("mintemp")] public int Mintemp { get; set; }

        [JsonPropertyName("maxtemp")] public int Maxtemp { get; set; }

        [JsonPropertyName("avgtemp")] public int Avgtemp { get; set; }

        [JsonPropertyName("totalsnow")] public double Totalsnow { get; set; }

        [JsonPropertyName("sunhour")] public double Sunhour { get; set; }

        [JsonPropertyName("uv_index")] public int UvIndex { get; set; }

        [JsonPropertyName("hourly")] public List<WeatherSnapshot> HourlyModels { get; set; }
    }

    public class Historical
    {
        public Day Day { get; set; }
    }

    #endregion
}