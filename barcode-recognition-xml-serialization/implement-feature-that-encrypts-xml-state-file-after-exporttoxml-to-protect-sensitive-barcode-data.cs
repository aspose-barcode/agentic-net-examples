// Title: Encrypt and Decrypt Barcode Generator XML State
// Description: Demonstrates exporting a barcode generator's state to XML, encrypting it with AES, and then decrypting to recreate the barcode image.
// Category-Description: This example belongs to the Aspose.BarCode state management category, showcasing how to use ExportToXml and ImportFromXml together with .NET cryptography to protect sensitive barcode data. It highlights key API classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, and is useful for developers needing secure storage or transmission of barcode configurations.
// Prompt: Implement a feature that encrypts the XML state file after ExportToXml to protect sensitive barcode data.
// Tags: barcode symbology, export, import, encryption, aes, xml, aspose.barcode

using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates encrypting the XML state of a barcode generator and restoring it.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a QR code, encrypts its generator state, then decrypts and regenerates the image.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary output directory
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeXmlEnc_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string encryptedFilePath = Path.Combine(outputDir, "generator_encrypted.bin");
        string barcodeImagePath = Path.Combine(outputDir, "barcode.png");

        // Generate a random AES key and IV (for demonstration only; store securely in production)
        byte[] aesKey = new byte[32]; // 256‑bit key
        byte[] aesIv = new byte[16];  // 128‑bit IV
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(aesKey);
            rng.GetBytes(aesIv);
        }

        // Create a QR barcode generator and export its state to an in‑memory XML stream
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SensitiveData123"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            using (var xmlStream = new MemoryStream())
            {
                generator.ExportToXml(xmlStream);
                byte[] xmlBytes = xmlStream.ToArray();

                // Encrypt the XML bytes with AES and write the ciphertext to a file
                using (var aes = Aes.Create())
                {
                    aes.Key = aesKey;
                    aes.IV = aesIv;
                    using (var encryptor = aes.CreateEncryptor())
                    using (var encryptedStream = new MemoryStream())
                    using (var cryptoStream = new CryptoStream(encryptedStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(xmlBytes, 0, xmlBytes.Length);
                        cryptoStream.FlushFinalBlock();
                        File.WriteAllBytes(encryptedFilePath, encryptedStream.ToArray());
                    }
                }
            }
        }

        // Read the encrypted file and decrypt the XML back into a memory stream
        byte[] encryptedData = File.ReadAllBytes(encryptedFilePath);
        using (var aes = Aes.Create())
        {
            aes.Key = aesKey;
            aes.IV = aesIv;
            using (var decryptor = aes.CreateDecryptor())
            using (var decryptedStream = new MemoryStream())
            using (var cryptoStream = new CryptoStream(new MemoryStream(encryptedData), decryptor, CryptoStreamMode.Read))
            {
                cryptoStream.CopyTo(decryptedStream);
                decryptedStream.Position = 0;

                // Import the barcode generator state from the decrypted XML and save the barcode image
                using (var importedGenerator = BarcodeGenerator.ImportFromXml(decryptedStream))
                {
                    importedGenerator.Save(barcodeImagePath, BarCodeImageFormat.Png);
                }
            }
        }

        Console.WriteLine("Encrypted XML saved to: " + encryptedFilePath);
        Console.WriteLine("Generated barcode image saved to: " + barcodeImagePath);
    }
}