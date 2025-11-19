namespace PubsModern.Domain.ValueObjects;

using PubsModern.Domain.Common;
using System.Text.RegularExpressions;

/// <summary>
/// ISBN value object with validation
/// </summary>
public class ISBN : ValueObject
{
    public string Value { get; private set; }

    private ISBN() 
    {
        Value = string.Empty;
    }

    public ISBN(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("ISBN cannot be empty", nameof(value));

        var cleanValue = value.Replace("-", "").Replace(" ", "");
        
        if (!IsValidISBN(cleanValue))
            throw new ArgumentException("Invalid ISBN format", nameof(value));

        Value = cleanValue;
    }

    private static bool IsValidISBN(string isbn)
    {
        // ISBN-10 or ISBN-13 validation
        if (isbn.Length == 10)
            return ValidateISBN10(isbn);
        if (isbn.Length == 13)
            return ValidateISBN13(isbn);
        
        return false;
    }

    private static bool ValidateISBN10(string isbn)
    {
        if (!Regex.IsMatch(isbn, @"^\d{9}[\dX]$"))
            return false;

        int sum = 0;
        for (int i = 0; i < 9; i++)
        {
            sum += (isbn[i] - '0') * (10 - i);
        }
        
        char lastChar = isbn[9];
        sum += lastChar == 'X' ? 10 : (lastChar - '0');

        return sum % 11 == 0;
    }

    private static bool ValidateISBN13(string isbn)
    {
        if (!Regex.IsMatch(isbn, @"^\d{13}$"))
            return false;

        int sum = 0;
        for (int i = 0; i < 12; i++)
        {
            sum += (isbn[i] - '0') * (i % 2 == 0 ? 1 : 3);
        }
        
        int checkDigit = (10 - (sum % 10)) % 10;
        return checkDigit == (isbn[12] - '0');
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;

    public static implicit operator string(ISBN isbn) => isbn.Value;
}
