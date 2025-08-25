namespace ConSeeker.Api.Models
{
    /// <summary>
    /// Represents a geographic location and related sensor info.
    /// </summary>
    public class Location
    {
        // Latitude in degrees
        public double Latitude { get; set; }

        // Longitude in degrees
        public double Longitude { get; set; }

        // Accuracy of latitude/longitude in meters (optional)
        public double? Accuracy { get; set; }

        // Altitude in meters (optional)
        public double? Altitude { get; set; }

        // Accuracy of altitude in meters (optional)
        public double? AltitudeAccuracy { get; set; }

        // Speed in meters per second (optional)
        public double? Speed { get; set; }

        // Direction/heading in degrees (0-360, optional)
        public double? Course { get; set; }

        // Timestamp of the reading in UTC
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
