namespace Bookify.Domain.Apartments;

public record Currency
{
    internal static readonly Currency None = new("");
    public static readonly Currency USD = new("USD");
    public static readonly Currency EUR = new("IDR");
    
    public string Code { get; private set; }

    public Currency(string code) => Code = code;

    public static Currency FromCode(string code) 
        => All.FirstOrDefault(c => c.Code == code) 
        ?? throw new InvalidOperationException($"Currency with code {code} not found");

    public static readonly IReadOnlyList<Currency> All = [USD, EUR];
}