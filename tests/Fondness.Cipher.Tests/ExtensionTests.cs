using FluentAssertions;

namespace Fondness.Cipher.Tests;

public class ExtensionTests
{
    /// <summary>
    /// Tests that a simple string can be encrypted and then decrypted back to its original value
    /// using the default encryption key. This verifies the basic round-trip functionality.
    /// </summary>
    [Fact]
    public void ToEncrypt_And_ToDecrypt_WithDefaultKey_ShouldReturnOriginalString()
    {
        // Arrange
        const string originalText = "Hello World";

        // Act
        var encrypted = originalText.ToEncrypt();
        var decrypted = encrypted.ToDecrypt();

        // Assert
        decrypted.Should().Be(originalText);
        encrypted.Should().NotBe(originalText);
    }

    /// <summary>
    /// Tests that encryption and decryption work correctly with a custom encryption key.
    /// This ensures the optional parameter functionality works as expected.
    /// </summary>
    [Fact]
    public void ToEncrypt_And_ToDecrypt_WithCustomKey_ShouldReturnOriginalString()
    {
        // Arrange
        const string originalText = "Secret Message";
        const string customKey = "myCustomKey123456789012345678901";

        // Act
        var encrypted = originalText.ToEncrypt(customKey);
        var decrypted = encrypted.ToDecrypt(customKey);

        // Assert
        decrypted.Should().Be(originalText);
        encrypted.Should().NotBe(originalText);
    }

    /// <summary>
    /// Tests that an empty string can be encrypted and decrypted successfully.
    /// This ensures the extension methods handle edge cases with empty inputs.
    /// </summary>
    [Fact]
    public void ToEncrypt_And_ToDecrypt_WithEmptyString_ShouldReturnEmptyString()
    {
        // Arrange
        const string originalText = "";

        // Act
        var encrypted = originalText.ToEncrypt();
        var decrypted = encrypted.ToDecrypt();

        // Assert
        decrypted.Should().Be(originalText);
        encrypted.Should().NotBeNullOrEmpty();
    }

    /// <summary>
    /// Tests that strings containing special characters and unicode can be properly
    /// encrypted and decrypted. This verifies handling of non-ASCII characters.
    /// </summary>
    [Fact]
    public void ToEncrypt_And_ToDecrypt_WithSpecialCharacters_ShouldReturnOriginalString()
    {
        // Arrange
        const string originalText = "Hello 世界! @#$%^&*()_+-=[]{}|;':\",./<>?";

        // Act
        var encrypted = originalText.ToEncrypt();
        var decrypted = encrypted.ToDecrypt();

        // Assert
        decrypted.Should().Be(originalText);
        encrypted.Should().NotBe(originalText);
    }

    /// <summary>
    /// Tests that a very long string can be encrypted and decrypted successfully.
    /// This ensures the extension methods can handle large amounts of data.
    /// </summary>
    [Fact]
    public void ToEncrypt_And_ToDecrypt_WithLongString_ShouldReturnOriginalString()
    {
        // Arrange
        var originalText = new string('A', 10000) + "Some text in the middle" + new string('Z', 10000);

        // Act
        var encrypted = originalText.ToEncrypt();
        var decrypted = encrypted.ToDecrypt();

        // Assert
        decrypted.Should().Be(originalText);
        encrypted.Should().NotBe(originalText);
    }

    /// <summary>
    /// Tests that strings with only whitespace characters are handled correctly.
    /// This ensures proper handling of strings containing spaces, tabs, and newlines.
    /// </summary>
    [Fact]
    public void ToEncrypt_And_ToDecrypt_WithWhitespaceString_ShouldReturnOriginalString()
    {
        // Arrange
        const string originalText = "   \t\n\r   ";

        // Act
        var encrypted = originalText.ToEncrypt();
        var decrypted = encrypted.ToDecrypt();

        // Assert
        decrypted.Should().Be(originalText);
        encrypted.Should().NotBe(originalText);
    }

    /// <summary>
    /// Tests that different strings produce different encrypted outputs when using the same key.
    /// This verifies that the encryption is working correctly and not producing identical results.
    /// </summary>
    [Fact]
    public void ToEncrypt_WithDifferentStrings_ShouldProduceDifferentEncryptedValues()
    {
        // Arrange
        const string text1 = "First message";
        const string text2 = "Second message";

        // Act
        var encrypted1 = text1.ToEncrypt();
        var encrypted2 = text2.ToEncrypt();

        // Assert
        encrypted1.Should().NotBe(encrypted2);
    }

    /// <summary>
    /// Tests that the same string encrypted with different keys produces different encrypted outputs.
    /// This verifies that the custom encryption key parameter is actually being used.
    /// </summary>
    [Fact]
    public void ToEncrypt_WithDifferentKeys_ShouldProduceDifferentEncryptedValues()
    {
        // Arrange
        const string originalText = "Same message";
        const string key1 = "key1234567890123456789012345678901";
        const string key2 = "key9876543210987654321098765432109";

        // Act
        var encrypted1 = originalText.ToEncrypt(key1);
        var encrypted2 = originalText.ToEncrypt(key2);

        // Assert
        encrypted1.Should().NotBe(encrypted2);
    }

    /// <summary>
    /// Tests that attempting to decrypt with a wrong key fails to produce the original text.
    /// This verifies that the encryption is secure and requires the correct key for decryption.
    /// </summary>
    [Fact]
    public void ToDecrypt_WithWrongKey_ShouldNotReturnOriginalString()
    {
        // Arrange
        const string originalText = "Secret message";
        const string correctKey = "correctkey123456789012345678901234";
        const string wrongKey = "wrongkey1234567890123456789012345";

        // Act
        var encrypted = originalText.ToEncrypt(correctKey);
        var decryptedWithWrongKey = encrypted.ToDecrypt(wrongKey);

        // Assert
        decryptedWithWrongKey.Should().NotBe(originalText);
    }

    /// <summary>
    /// Tests that the encrypted output is always a valid Base64 string.
    /// This ensures the encryption method produces properly formatted output.
    /// </summary>
    [Fact]
    public void ToEncrypt_ShouldReturnValidBase64String()
    {
        // Arrange
        const string originalText = "Test message for Base64 validation";

        // Act
        var encrypted = originalText.ToEncrypt();

        // Assert
        encrypted.Should().NotBeNullOrEmpty();
        var isValidBase64 = IsValidBase64String(encrypted);
        isValidBase64.Should().BeTrue();
    }

    /// <summary>
    /// Tests that decryption handles Base64 strings with spaces correctly by replacing them with plus signs.
    /// This tests the specific logic in ToDecrypt that handles space-to-plus conversion.
    /// </summary>
    [Fact]
    public void ToDecrypt_WithSpacesInBase64_ShouldHandleCorrectly()
    {
        // Arrange
        const string originalText = "Message with spaces handling";

        // Act
        var encrypted = originalText.ToEncrypt();
        var encryptedWithSpaces = encrypted.Replace("+", " ");
        var decrypted = encryptedWithSpaces.ToDecrypt();

        // Assert
        decrypted.Should().Be(originalText);
    }

    /// <summary>
    /// Tests that null input throws an appropriate exception during encryption.
    /// This verifies proper error handling for invalid inputs.
    /// </summary>
    [Fact]
    public void ToEncrypt_WithNullInput_ShouldThrowException()
    {
        // Arrange
        string? nullString = null;

        // Act & Assert
        var action = () => nullString!.ToEncrypt();
        action.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    /// Tests that null input throws an appropriate exception during decryption.
    /// This verifies proper error handling for invalid inputs during decryption.
    /// </summary>
    [Fact]
    public void ToDecrypt_WithNullInput_ShouldThrowException()
    {
        // Arrange
        string? nullString = null;

        // Act & Assert
        var action = () => nullString!.ToDecrypt();
        action.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    /// Tests that invalid Base64 input throws an appropriate exception during decryption.
    /// This verifies proper error handling for malformed encrypted strings.
    /// </summary>
    [Fact]
    public void ToDecrypt_WithInvalidBase64_ShouldThrowException()
    {
        // Arrange
        const string invalidBase64 = "This is not a valid Base64 string!@#$";

        // Act & Assert
        var action = () => invalidBase64.ToDecrypt();
        action.Should().Throw<FormatException>();
    }

    /// <summary>
    /// Tests round-trip encryption/decryption with numeric strings to ensure
    /// numeric data is handled correctly without type conversion issues.
    /// </summary>
    [Fact]
    public void ToEncrypt_And_ToDecrypt_WithNumericString_ShouldReturnOriginalString()
    {
        // Arrange
        const string originalText = "1234567890.123456789";

        // Act
        var encrypted = originalText.ToEncrypt();
        var decrypted = encrypted.ToDecrypt();

        // Assert
        decrypted.Should().Be(originalText);
    }

    private static bool IsValidBase64String(string base64String)
    {
        if (string.IsNullOrEmpty(base64String))
            return false;

        try
        {
            _ = Convert.FromBase64String(base64String);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
