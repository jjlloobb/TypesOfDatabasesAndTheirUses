using CBTW.Microservices.HumanResources.Application.Configurations;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CBTW.Microservices.HumanResources.Application.CallCenterService;

public static class CustomValidators
{
    private static readonly Regex regexLetters;
    private static readonly Regex regexNumbers;

    static CustomValidators()
    {
        regexLetters = new Regex($"^[a-zA-Z ]+$");
        regexNumbers = new Regex($"^[\\d]+$");
    }

    public static IRuleBuilderOptions<T, string> NotNullOrEmpty<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Must(j => !string.IsNullOrEmpty(j.NullToEmpty().Trim())).WithMessage("'{PropertyName}' debería tener un valor.");
    }

    public static IRuleBuilderOptions<T, string> NotStartWithWhiteSpace<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Must(j => j != null && !j.StartsWith(" ")).WithMessage("'{PropertyName}' debería no comenzar con espacio en blanco.");
    }

    public static IRuleBuilderOptions<T, string> NotEndWithWhiteSpace<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Must(j => j != null && !j.EndsWith(" ")).WithMessage("'{PropertyName}' debería no finalizar con espacio en blanco.");
    }

    public static IRuleBuilderOptions<T, string> NotLength<T>(this IRuleBuilder<T, string> ruleBuilder, int min, int max)
    {
        return ruleBuilder.Must(j => j != null && j.Length >= min && j.Length <= max).WithMessage("Ingrese '{PropertyName}' con un máximo de " + max + " carácteres.");
    }

    public static IRuleBuilderOptions<T, int> NotGreaterThan<T>(this IRuleBuilder<T, int> ruleBuilder, int value)
    {
        return ruleBuilder.Must(j => j > value).WithMessage("'{PropertyName}' debería ser mayor que " + value + ".");
    }

    public static IRuleBuilderOptions<T, int?> NotGreaterThan<T>(this IRuleBuilder<T, int?> ruleBuilder, int value)
    {
        return ruleBuilder.Must(j => j != null && j > value).WithMessage("'{PropertyName}' debería ser mayor que " + value + ".");
    }

    public static IRuleBuilderOptions<T, long> NotGreaterThan<T>(this IRuleBuilder<T, long> ruleBuilder, long value)
    {
        return ruleBuilder.Must(j => j > value).WithMessage("'{PropertyName}' debería ser mayor que " + value + ".");
    }

    public static IRuleBuilderOptions<T, long?> NotGreaterThan<T>(this IRuleBuilder<T, long?> ruleBuilder, long value)
    {
        return ruleBuilder.Must(j => j != null && j > value).WithMessage("'{PropertyName}' debería ser mayor que " + value + ".");
    }

    public static IRuleBuilderOptions<T, DateTime> NotLessThanOrEqualTo<T>(this IRuleBuilder<T, DateTime> ruleBuilder, DateTime dateTime)
    {
        return ruleBuilder.Must(j => j.Date <= dateTime.Date).WithMessage("'{PropertyName}' debería ser menor que " + dateTime.Date.ToString("d") + " o igual.");
    }

    public static IRuleBuilderOptions<T, string> NotLetters<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Matches(regexLetters).WithMessage("'{PropertyName}' debería ser solo letras.");
    }

    public static IRuleBuilderOptions<T, string> NotNumbers<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Matches(regexNumbers).WithMessage("'{PropertyName}' debería ser solo números.");
    }
}