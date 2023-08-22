using System.Security.Cryptography;

namespace PasswordGeneratorLibrary;

/// <summary>
/// Class that generates random passwords based on configuration options.
/// By default, all properties are set to <c>true</c> to provide access to the most secure password.
/// However, each value can be modified to tailor the password according to specific needs.
/// </summary>
public class PasswordGenerator
{
    private const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
    private const string Numbers = "0123456789";
    private const string Symbols = "!@#$%^&*()";
    
    private readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();

    /// <summary>
    /// Determines the number of characters in the password.
    /// </summary>
    public int Length { get; init; } = 8;

    /// <summary>
    /// Determines whether the password should contain uppercase letters.
    /// </summary>
    public bool HasUpper { get; init; } = true;

    /// <summary>
    /// Determines whether the password should contain lowercase letters.
    /// </summary>
    public bool HasLower { get; init; } = true;

    /// <summary>
    /// Determines whether the password should contain numbers.
    /// </summary>
    public bool HasNumber { get; init; } = true;

    /// <summary>
    /// Determines whether the password should contain symbols.
    /// </summary>
    public bool HasSymbol { get; init; } = true;

    /// <summary>
    /// Parameterized constructor to initialize the password generator with specific settings.
    /// </summary>
    public PasswordGenerator(int length, bool hasUpper, bool hasLower, bool hasNumber, bool hasSymbol)
    {
        Length = length;
        HasUpper = hasUpper;
        HasLower = hasLower;
        HasNumber = hasNumber;
        HasSymbol = hasSymbol;
    }
    
    /// <summary>
    /// Default constructor for the password generator.
    /// </summary>
    public PasswordGenerator()
    {
    }

    /// <summary>
    /// Creates a string containing the allowed characters based on the configuration settings.
    /// </summary>
    /// <returns>A string containing the allowed characters.</returns>
    private string GetAllowedChars()
    {
        return string.Concat(
            HasUpper ? Uppercase : "",
            HasLower ? Lowercase : "",
            HasNumber ? Numbers : "",
            HasSymbol ? Symbols : "");
    }

    /// <summary>
    /// Selects a random character from the provided character group.
    /// </summary>
    /// <param name="charGroup">A string containing the characters to choose from.</param>
    /// <returns>A randomly selected character from the provided character group.</returns>
    private char GetRandomChar(string charGroup)
    {
        // Generate a random byte to select a character from the character group.
        var randomByte = new byte[1];
        _rng.GetBytes(randomByte);

        // Calculates the index using mod to ensure it's within the valid range of the character group.
        return charGroup[randomByte[0] % charGroup.Length];
    }

    /// <summary>
    /// Generates a random password based on the specified criteria for length and character types. It starts by ensuring at least one character of each type is present,
    /// then complements the password with random characters from selected character groups before shuffling the characters to enhance security.
    /// </summary>
    /// <returns>The randomly generated password.</returns>
    public string GeneratePassword()
    {
        // Throws an exception if the specified password length is outside the valid range.
        if (Length is < 1 or > 99) 
            throw new ArgumentOutOfRangeException(nameof(Length),"Invalid password length. Length must be greater than 0 and lower than 100.");
        
        // Throws an exception if no character types are selected for password generation.
        if (!(HasLower || HasUpper || HasNumber || HasSymbol))
            throw new InvalidOperationException("Cannot generate password. At least one character type (uppercase, lowercase, number, symbol) must be selected when initializing the password generator.");
        
        // Construct an initial password string containing at least one character of each selected type.
        var orderedPassword = string.Concat(
            HasUpper ? GetRandomChar(Uppercase) : "",
            HasLower ? GetRandomChar(Lowercase) : "",
            HasNumber ? GetRandomChar(Numbers) : "",
            HasSymbol ? GetRandomChar(Symbols) : "");

        // Generates additional random characters to complete the required password length.
        while (orderedPassword.Length < Length) orderedPassword += GetRandomChar(GetAllowedChars());
        
        // Shuffles the ordered password characters for enhanced security and returns it.
        return new string(orderedPassword.ToCharArray().OrderBy(_ => Guid.NewGuid()).ToArray());
    }
}