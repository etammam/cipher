# Fondness.Cipher

[![CI](https://github.com/etammam/cipher/actions/workflows/dotnet.yml/badge.svg)](https://github.com/etammam/cipher/actions/workflows/dotnet.yml)
[![NuGet version (Newtonsoft.Json)](https://img.&](https://www.nuget.org/packages/Fondness.Cipher/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Fondness.Cipher)](https://www.nuget.org/packages/Fondness.Cipher/)
[![License](https://img.shields.io/github/license/etammam/cipher)](https://github.com/etammam/cipher/blob/master/LICENSE)

A simple, high-performance string encryption and decryption extension library using AES (Advanced Encryption Standard) cryptography.

## 🚀 Features

- **Simple API**: Easy-to-use extension methods for string encryption/decryption
- **AES Encryption**: Uses industry-standard AES encryption algorithm
- **Custom Keys**: Support for custom encryption keys
- **High Performance**: Optimized for speed and efficiency
- **Multi-Framework**: Supports .NET 6.0, 7.0, 8.0, and 9.0
- **Secure**: Implements proper cryptographic practices

## 📦 Installation

### Package Manager Console
```
PM> install-package Fondness.Cipher
```

### .NET CLI

```bash
dotnet add package Fondness.Cipher
```

## 💡 Usage

Install this package using the NuGet package manager. You don't need any configuration on the application, just call the string extension methods:

```csharp
string input = "Hello World!";
var encryptedValue = value.ToEncrypt();
var decryptValue = encryptedValue.ToDecrypt();
```

You can also provide your custom encryption key as follows:

```csharp
string input = "Hello World!";
string encryptionKey = "abc123";
var encryptedValue = value.ToEncrypt(encryptionKey);
var decryptValue = encryptedValue.ToDecrypt(encryptionKey);
```

Make sure you provide the same encryption key for both encryption and decryption.


## ⚠️ Important Notes

- **Key Consistency**: Always use the same encryption key for both encryption and decryption
- **Key Security**: Store your custom encryption keys securely (environment variables, key vaults, etc.)
- **Key Length**: Custom keys should be sufficiently long for security (recommended: 32+ characters)
- **Exception Handling**: Handle potential exceptions for invalid inputs or wrong keys

## 🔐 Security Considerations

- Uses AES encryption with PBKDF2 key derivation
- Generates unique encrypted outputs for the same input (due to IV usage)
- Invalid decryption attempts with wrong keys will result in garbled output or exceptions
- Base64 encoding is used for the encrypted output string format

## 🧪 Exception Handling
```csharp
try
{
    string encrypted = "invalid-base64-string"; 
    string decrypted = encrypted.ToDecrypt();
} 
catch (FormatException) 
{
    // Handle invalid Base64 input 
} 
catch (ArgumentNullException) 
{
    // Handle null input 
}
catch (CryptographicException) 
{
    // Handle decryption failures 
}

```
## 🎯 Supported Frameworks

- .NET 6.0
- .NET 7.0
- .NET 8.0
- .NET 9.0

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📊 Changelog

### Version 1.0.0
- Initial release
- Basic encryption/decryption functionality
- Support for custom encryption keys
### Version 1.0.1
- Multi-framework support

---


## 🛡️ License

&copy; Fondness Open Source. Licensed under the [APACHE](LICENSE).
