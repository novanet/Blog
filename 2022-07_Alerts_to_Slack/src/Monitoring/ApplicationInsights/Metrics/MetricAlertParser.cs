namespace Monitoring.ApplicationInsights.Metrics;

public static class MetricAlertParser
{
    public static float GetMetric(this MetricAlert alert)
        => alert!.data.alertContext.condition.allOf[0].metricValue;

    public static string? GetCloudRoleName(this MetricAlert alert)
    => alert!.GetDimension("cloud_RoleName");

    public static string? GetHealthCheckResource(this MetricAlert alert)
        => alert!.data.essentials.configurationItems[0];

    public static string? GetDlqEntityName(this MetricAlert alert)
        => alert!.GetDimension("EntityName");

    private static string? GetDimension(this MetricAlert alert, string dimensionName)
        => alert!.data.alertContext.condition.allOf[0].dimensions.FirstOrDefault(x => x.name == dimensionName)?.value;
}