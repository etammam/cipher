# Cipher

a simple high-performance string encryption and decryption extension  
using aes advance encryption standard behind the sence.

## Usage

install this pakage using nuget package manager
`PM> install-package Fondness.Cipher`
you don't need any configuration on application just call the string extension method

```cs
string input = "Hello World!";
var encryptedValue = value.ToEncrypt();
var decryptValue = encryptedValue.ToDecrypt();
```

also you can provide your custom encryption key as following

```cs
string input = "Hello World!";
string encryptionKey = "abc123";
var encryptedValue = value.ToEncrypt(encryptionKey);
var decryptValue = encryptedValue.ToDecrypt(encryptionKey);
```

make sure you provide the same encryption key over encryption and decryption.

&copy;Fondness Open Source.
