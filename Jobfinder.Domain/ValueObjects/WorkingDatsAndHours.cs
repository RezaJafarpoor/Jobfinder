namespace Jobfinder.Domain.ValueObjects;

public class WorkingDatsAndHours
{
    public DayOfWeek From { get; set; }
    public DayOfWeek To { get; set; }
    public int StartingHour { get; set; }
    public int FinishingHour { get; set; }
    
}