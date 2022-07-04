namespace Monitoring.ApplicationInsights.Logs;

public static class LogAlertParser
{
    public static string GetLinkToSearchResults(this LogAlert alert)
    {
        var allOf = alert.data.alertContext.condition.allOf[0];
        return allOf.linkToSearchResultsUI;
    }

    public static int GetNumberOfErrors(this LogAlert alert)
    {
        var allOf = alert.data.alertContext.condition.allOf[0];
        return Convert.ToInt32(allOf.metricValue);
    }

    public static string? GetExceptionType(this LogAlert alert)
        => alert!.GetDimension("type");

    public static string? GetExceptionMessage(this LogAlert alert)
        => alert!.GetDimension("outerMessage");

    public static string? GetCloudRoleName(this LogAlert alert)
        => alert!.GetDimension("cloud_RoleName");

    public static string? GetTracesMessage(this LogAlert alert)
        => alert!.GetDimension("message");

    private static string? GetDimension(this LogAlert alert, string dimensionName)
        => alert!.data.alertContext.condition.allOf[0].dimensions.FirstOrDefault(x => x.name == dimensionName)?.value;

}