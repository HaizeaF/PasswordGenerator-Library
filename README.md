# PasswordGenerator-Library
Library for generating customizable secure passwords in .NET 7

## Using
### Basic Using
To generate passwords using the default settings, you can follow this example:
```
using PasswordGeneratorLibrary;

// Create an instance of the PasswordGenerator class
var passwordGenerator = new PasswordGenerator();

// Generate a random password
string generatedPassword = passwordGenerator.GeneratePassword();
```
The default parameters are:
1. Length: 8
2. HasUpper, HasLower, HasNumber, HasSymbol: true


### Custom configuration
You can customize the password generation by specifying various parameters. Here's an example:
```
using PasswordGeneratorLibrary;

// Create an instance of the PasswordGenerator class with custom configuration
var passwordGenerator = new PasswordGenerator
{
    Length = 12,
    HasUpper = false,
    HasLower = true,
    HasNumber = true,
    HasSymbol = false
};

// Generate a random password with the provided configuration
string generatedPassword = passwordGenerator.GeneratePassword();
```
## Running the console test
The repository also includes a console test application that demonstrates the usage of the Password Generator Library. 
The console test will guide you through the process of generating passwords with both default and customized configurations. 
To run the console test, follow these steps:
1. Navigate to the '**PasswordGenerator.Test.Console**' directory:
```
cd PasswordGenerator.Test.Console
```

2. Run the application
```
dotnet run
```
