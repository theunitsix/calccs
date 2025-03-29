using System;
using System.Collections.Generic;
using System.Globalization;

namespace BearingDistanceTool
{
    public static class GeoUtility
    {
        public static Dictionary<string, string> MapLinks;

        public static List<GeoResult> CalculateGeoResults(List<GeoPoint> points, bool isWorldGeodetic)
        {
            var coords = new Dictionary<string, Coordinate>();
            MapLinks = new Dictionary<string, string>();

            foreach (var point in points)
            {
                var lat = Coordinate.DmsToDecimal(point.LatDeg, point.LatMin, point.LatSec);
                var lon = Coordinate.DmsToDecimal(point.LonDeg, point.LonMin, point.LonSec);
                var converted = Coordinate.ConvertToWorldGeodetic(new Coordinate { Latitude = lat, Longitude = lon }, isWorldGeodetic);
                coords[point.Label] = converted;
                MapLinks[point.Label] = GeoCalculator.GetMapUrl(converted);
            }

            var results = new List<GeoResult>();
            foreach (var target in new[] { "B", "C", "D" })
            {
                var (bearing, distance) = GeoCalculator.CalculateBearingAndDistance(coords["A"], coords[target]);
                results.Add(new GeoResult
                {
                    From = "A",
                    To = target,
                    Bearing = bearing,
                    Distance = distance
                });
            }

            return results;
        }
    }

    public class GeoPoint
    {
        public string Label { get; set; }
        public double LatDeg { get; set; }
        public double LatMin { get; set; }
        public double LatSec { get; set; }
        public double LonDeg { get; set; }
        public double LonMin { get; set; }
        public double LonSec { get; set; }
    }

    public class GeoResult
    {
        public string From { get; set; }
        public string To { get; set; }
        public double Bearing { get; set; }
        public double Distance { get; set; }
    }

    public class Coordinate
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public static double DmsToDecimal(double deg, double min, double sec)
        {
            return deg + min / 60 + sec / 3600;
        }

        public static Coordinate ConvertToWorldGeodetic(Coordinate coord, bool isWorldGeodetic)
        {
            if (!isWorldGeodetic)
            {
                double lat = coord.Latitude - (0.00010695 * coord.Latitude + 0.000017464 * coord.Longitude + 0.0046017);
                double lon = coord.Longitude - (0.000046038 * coord.Latitude + 0.000083043 * coord.Longitude + 0.010040);
                return new Coordinate { Latitude = lat, Longitude = lon };
            }
            return coord;
        }
    }

    public static class GeoCalculator
    {
        public static (double Bearing, double Distance) CalculateBearingAndDistance(Coordinate from, Coordinate to)
        {
            const double R = 6371.0;
            double lat1 = ToRadians(from.Latitude);
            double lat2 = ToRadians(to.Latitude);
            double deltaLon = ToRadians(to.Longitude - from.Longitude);

            double y = Math.Sin(deltaLon) * Math.Cos(lat2);
            double x = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(deltaLon);
            double bearing = (ToDegrees(Math.Atan2(y, x)) + 360) % 360;

            double a = Math.Pow(Math.Sin((lat2 - lat1) / 2), 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(deltaLon / 2), 2);
            double distance = R * 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return (Math.Round(bearing, 2), Math.Round(distance, 2));
        }

        public static string GetMapUrl(Coordinate coord)
        {
            return $"https://www.google.com/maps?q={coord.Latitude.ToString(CultureInfo.InvariantCulture)},{coord.Longitude.ToString(CultureInfo.InvariantCulture)}";
        }

        private static double ToRadians(double degrees) => degrees * Math.PI / 180.0;
        private static double ToDegrees(double radians) => radians * 180.0 / Math.PI;
    }
}
