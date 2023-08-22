using PasswordGeneratorLibrary;

// Display welcome message
Console.WriteLine("Welcome to the Password Generator Console Test!");
Console.WriteLine("This is a simple demonstration of the library.");
Console.WriteLine("==============================================");

// Ask user whether to manually configure the password or not
Console.Write("Do you want to configure the password manually? (y/n): ");
var manualConfig = ReadYesNoInput();

Console.Clear();
var passwordGenerator = new PasswordGenerator();

// Prompt user for custom configuration
if (manualConfig)
{
    Console.Write("Enter password length: ");
    var length = ReadIntInput();

    Console.Write("Include uppercase letters? (y/n): ");
    var hasUpper = ReadYesNoInput();

    Console.Write("Include lowercase letters? (y/n): ");
    var hasLower = ReadYesNoInput();

    Console.Write("Include numbers? (y/n): ");
    var hasNumber = ReadYesNoInput();
    
    Console.Write("Include symbols? (y/n): ");
    var hasSymbol = ReadYesNoInput();
    
    // Create a new instance of PasswordGenerator with the provided configuration
    passwordGenerator = new PasswordGenerator
    {
        Length = length,
        HasUpper = hasUpper,
        HasLower = hasLower,
        HasNumber = hasNumber,
        HasSymbol = hasSymbol
    };
}

// Generate the password using the generator
var generatedPassword = passwordGenerator.GeneratePassword();

Console.Clear();

// Display the generated password to the user
Console.WriteLine("Generated Password: " + generatedPassword);

// Define a function to read "y" or "n" responses and ensure valid inputs
static bool ReadYesNoInput()
{
    while (true)
    {
        var input = Console.ReadLine()?.ToLower();
        if (!string.IsNullOrEmpty(input) && input is "y" or "n")
        {
            return input == "y";
        }
        Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
    }
}

// Define a function to read integer inputs and ensure valid inputs
static int ReadIntInput()
{
    while (true)
    {
        var input = Console.ReadLine();
        if (int.TryParse(input, out var result))
        {
            return result;
        }
        Console.WriteLine("Invalid input. Please enter a integer.");
    }
}
