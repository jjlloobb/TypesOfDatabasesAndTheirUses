namespace CBTW.Microservices.CallCenter.Application.Configurations;

public static class StringExtensions
{
    public static string NullToEmpty(this string value)
    {
        return value ?? string.Empty;
    }
}