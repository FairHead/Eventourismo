namespace Eventourismo.Domain.ValueObjects;

public class OpeningHours
{
    public TimeOnly OpenTime { get; private set; }
    public TimeOnly CloseTime { get; private set; }
    public DayOfWeek DayOfWeek { get; private set; }
    public bool IsClosed { get; private set; }

    public OpeningHours(DayOfWeek dayOfWeek, TimeOnly openTime, TimeOnly closeTime)
    {
        if (openTime >= closeTime)
            throw new ArgumentException("Open time must be before close time");

        DayOfWeek = dayOfWeek;
        OpenTime = openTime;
        CloseTime = closeTime;
        IsClosed = false;
    }

    public OpeningHours(DayOfWeek dayOfWeek) // Closed day
    {
        DayOfWeek = dayOfWeek;
        IsClosed = true;
    }

    public bool IsOpenAt(TimeOnly time)
    {
        if (IsClosed) return false;
        return time >= OpenTime && time <= CloseTime;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not OpeningHours other) return false;
        return DayOfWeek == other.DayOfWeek &&
               OpenTime == other.OpenTime &&
               CloseTime == other.CloseTime &&
               IsClosed == other.IsClosed;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(DayOfWeek, OpenTime, CloseTime, IsClosed);
    }
}