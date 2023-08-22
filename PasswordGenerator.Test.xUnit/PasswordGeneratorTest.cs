namespace PasswordGenerator.Test.xUnit;
using PasswordGeneratorLibrary;

public class PasswordGeneratorTest
{
    [Fact]
    public void GeneratePassword_Default_StandardPassword()
    {
        // Arrange
        var passwordGenerator = new PasswordGenerator();
        
        // Act
        var generatedPassword = passwordGenerator.GeneratePassword();
        
        // Asserts
        Assert.Equal(8, generatedPassword.Length);
        Assert.Contains(generatedPassword, char.IsUpper);
        Assert.Contains(generatedPassword, char.IsLower);
        Assert.Contains(generatedPassword, char.IsNumber);
        Assert.Contains(generatedPassword, c => "!@#$%^&*()".Contains(c));
    }
    
    [Fact]
    public void GeneratePassword_Personalized_PersonalizedPassword()
    {
        // Arrange
        var passwordGenerator = new PasswordGenerator
        {
            HasUpper = false,
            HasLower = false,
            Length = 16
        };
        
        // Act
        var generatedPassword = passwordGenerator.GeneratePassword();
        
        // Assetts
        Assert.Equal(16, generatedPassword.Length);
        Assert.DoesNotContain(generatedPassword, char.IsUpper);
        Assert.DoesNotContain(generatedPassword, char.IsLower);
        Assert.Contains(generatedPassword, char.IsNumber);
        Assert.Contains(generatedPassword, c => "!@#$%^&*()".Contains(c));
    }

    [Fact]
    public void GeneratePassword_Personalized2_PersonalizedPassword()
    {
        // Arrange
        var passwordGenerator = new PasswordGenerator
        {
            HasNumber = false,
            HasSymbol = false
        };
        
        // Act
        var generatedPassword = passwordGenerator.GeneratePassword();
        
        // Asserts
        Assert.Equal(8, generatedPassword.Length);
        Assert.Contains(generatedPassword, char.IsUpper);
        Assert.Contains(generatedPassword, char.IsLower);
        Assert.DoesNotContain(generatedPassword, char.IsNumber);
        Assert.DoesNotContain(generatedPassword, c => "!@#$%^&*()".Contains(c));
    }
    
    [Fact]
    public void GeneratePassword_PersonalizedWithConstructor_PersonalizedPassword()
    {
        // Arrange
        var passwordGenerator = new PasswordGenerator(16, false, false, true, true);

        // Act
        var generatedPassword = passwordGenerator.GeneratePassword();
        
        // Asserts
        Assert.Equal(16, generatedPassword.Length);
        Assert.DoesNotContain(generatedPassword, char.IsUpper);
        Assert.DoesNotContain(generatedPassword, char.IsLower);
        Assert.Contains(generatedPassword, char.IsNumber);
        Assert.Contains(generatedPassword, c => "!@#$%^&*()".Contains(c));
    }
    
    [Fact]
    public void GeneratePassword_AllParametersFalse_InvalidOperationException()
    {
        // Arrange
        var passwordGenerator = new PasswordGenerator
        {
            HasUpper = false,
            HasLower = false,
            HasNumber = false,
            HasSymbol = false
        };
        
        // Act & Asserts
        var exception = Assert.Throws<InvalidOperationException>(() => passwordGenerator.GeneratePassword());
        Assert.Equal("Cannot generate password. At least one character type (uppercase, lowercase, number, symbol) must be selected when initializing the password generator.", exception.Message);
    }

    [Theory]
    [MemberData(nameof(InvalidLengthValues))]
    public void GeneratePassword_InvalidLength_ValidationException(int length)
    {
        // Arrange
        var passwordGenerator = new PasswordGenerator
        {
            Length = length
        };
        
        // Act && Asserts
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => passwordGenerator.GeneratePassword());
        Assert.Equal("Invalid password length. Length must be greater than 0 and lower than 100. (Parameter 'Length')", exception.Message);
    }

    public static IEnumerable<object[]> InvalidLengthValues =>
        new List<object[]> { new object[] { 0 }, new object[] { 100 } };
}