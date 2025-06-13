namespace Eventourismo.Domain.ValueObjects;

public class Location
{
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
    public string Address { get; private set; }
    public string City { get; private set; }
    public string? Country { get; private set; }

    public Location(double latitude, double longitude, string address, string city, string? country = null)
    {
        if (latitude < -90 || latitude > 90)
            throw new ArgumentException("Latitude must be between -90 and 90 degrees");
        
        if (longitude < -180 || longitude > 180)
            throw new ArgumentException("Longitude must be between -180 and 180 degrees");

        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Address cannot be empty");

        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("City cannot be empty");

        Latitude = latitude;
        Longitude = longitude;
        Address = address;
        City = city;
        Country = country;
    }

    public double DistanceTo(Location other)
    {
        const double earthRadius = 6371; // Earth's radius in kilometers

        var dLat = DegreesToRadians(other.Latitude - Latitude);
        var dLon = DegreesToRadians(other.Longitude - Longitude);

        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(DegreesToRadians(Latitude)) * Math.Cos(DegreesToRadians(other.Latitude)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return earthRadius * c;
    }

    private static double DegreesToRadians(double degrees) => degrees * Math.PI / 180;

    public override bool Equals(object? obj)
    {
        if (obj is not Location other) return false;
        return Math.Abs(Latitude - other.Latitude) < 0.0001 &&
               Math.Abs(Longitude - other.Longitude) < 0.0001 &&
               Address == other.Address &&
               City == other.City &&
               Country == other.Country;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Latitude, Longitude, Address, City, Country);
    }
}