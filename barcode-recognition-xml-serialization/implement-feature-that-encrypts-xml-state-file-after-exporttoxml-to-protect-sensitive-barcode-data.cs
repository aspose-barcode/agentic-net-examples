// Title: Encrypt barcode configuration XML after export
// Description: Demonstrates exporting a barcode generator's configuration to XML, encrypting the XML, saving it, then decrypting and importing to generate a barcode image. Useful for protecting sensitive barcode data.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, showing how to use BarcodeGenerator.ExportToXml and ImportFromXml together with standard .NET cryptography (Aes) to secure configuration files. Developers often need to store barcode settings securely, encrypting them before persisting to disk or transmitting. The key API classes include BarcodeGenerator, BarCodeImageFormat, and the .NET Aes cryptographic classes.
// Prompt: Implement a feature that encrypts the XML state file after ExportToXml to protect sensitive barcode data.
// Tags: barcode, encryption, xml, export, import, aesencryption, aspnet, aspose.barcode, qrcode, configuration

using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that exports a barcode generator's configuration to XML,
/// encrypts the XML, saves it to a file, then decrypts and re‑imports the configuration
/// to generate a barcode image. Demonstrates secure handling of barcode state data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs export, encryption, decryption,
    /// import and image generation steps.
    /// </summary>
    static void Main()
    {
        // The data to encode in the QR code.
        string codeText = "SecureData123";

        // Create a QR code generator with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // ------------------------------------------------------------
            // Export the generator's configuration to XML (in memory).
            // ------------------------------------------------------------
            string xmlConfig;
            using (var ms = new MemoryStream())
            {
                generator.ExportToXml(ms);
                xmlConfig = Encoding.UTF8.GetString(ms.ToArray());
            }

            // ------------------------------------------------------------
            // Encrypt the XML configuration using AES.
            // ------------------------------------------------------------
            byte[] encryptedXml = EncryptXml(xmlConfig);

            // Save the encrypted XML to a temporary file for demonstration.
            string encryptedPath = Path.Combine(Path.GetTempPath(), "barcode_config.enc");
            File.WriteAllBytes(encryptedPath, encryptedXml);
            Console.WriteLine($"Encrypted XML saved to: {encryptedPath}");

            // ------------------------------------------------------------
            // Decrypt the XML back to its original plain text form.
            // ------------------------------------------------------------
            string decryptedXml = DecryptXml(encryptedXml);

            // ------------------------------------------------------------
            // Import a new BarcodeGenerator from the decrypted XML.
            // ------------------------------------------------------------
            using (var importStream = new MemoryStream(Encoding.UTF8.GetBytes(decryptedXml)))
            {
                using (var importedGenerator = BarcodeGenerator.ImportFromXml(importStream))
                {
                    // Generate and save the barcode image.
                    string imagePath = Path.Combine(Path.GetTempPath(), "barcode.png");
                    importedGenerator.Save(imagePath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Barcode image saved to: {imagePath}");
                }
            }
        }
    }

    /// <summary>
    /// Encrypts the provided XML string using AES‑256 CBC with a static key and IV.
    /// </summary>
    /// <param name="xml">Plain XML to encrypt.</param>
    /// <returns>Encrypted byte array.</returns>
    private static byte[] EncryptXml(string xml)
    {
        // 32‑byte (256‑bit) key and 16‑byte (128‑bit) IV.
        byte[] key = Encoding.UTF8.GetBytes("0123456789ABCDEF0123456789ABCDEF");
        byte[] iv = Encoding.UTF8.GetBytes("ABCDEF0123456789");
        byte[] plainBytes = Encoding.UTF8.GetBytes(xml);
        byte[] encrypted;

        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(plainBytes, 0, plainBytes.Length);
                    cs.FlushFinalBlock();
                    encrypted = ms.ToArray();
                }
            }
        }

        return encrypted;
    }

    /// <summary>
    /// Decrypts an AES‑encrypted XML byte array back to its plain string representation.
    /// </summary>
    /// <param name="encryptedData">Encrypted XML bytes.</param>
    /// <returns>Decrypted XML string.</returns>
    private static string DecryptXml(byte[] encryptedData)
    {
        // Same static key and IV used for encryption.
        byte[] key = Encoding.UTF8.GetBytes("0123456789ABCDEF0123456789ABCDEF");
        byte[] iv = Encoding.UTF8.GetBytes("ABCDEF0123456789");
        byte[] decrypted;

        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(encryptedData, 0, encryptedData.Length);
                    cs.FlushFinalBlock();
                    decrypted = ms.ToArray();
                }
            }
        }

        return Encoding.UTF8.GetString(decrypted);
    }
}