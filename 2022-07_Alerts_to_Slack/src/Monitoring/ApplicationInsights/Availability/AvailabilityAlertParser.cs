namespace Monitoring.ApplicationInsights.Availability;

public static class AvailabilityAlertParser
{
    public static string GetWebTestName(this AvailabilityAlert alert)
        => alert!.data.alertContext.condition.allOf[0].webTestName;
}