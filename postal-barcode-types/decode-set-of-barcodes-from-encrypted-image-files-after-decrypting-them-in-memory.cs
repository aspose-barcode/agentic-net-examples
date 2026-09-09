// Title: Decrypt and Decode Barcodes from Encrypted Image Files
// Description: Demonstrates generating barcode images, encrypting them in memory, then decrypting and decoding the barcodes without persisting plain images to disk.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating various symbologies, BarCodeReader for decoding, and standard .NET cryptography (AES) for securing image data. Developers often need to protect barcode assets in transit or storage and later extract the encoded information, making this pattern common in secure document workflows.
// Prompt: Decode a set of barcodes from encrypted image files after decrypting them in memory.
// Tags: barcode, encryption, decryption, generation, recognition, aes, aspose.barcode, symbology, qr, code128

using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates creating barcode images, encrypting them, then decrypting and decoding the barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    // Fixed AES key and IV for demonstration (32 bytes key for AES-256, 16 bytes IV)
    private static readonly byte[] aesKey = new byte[32];
    private static readonly byte[] aesIv = new byte[16];

    /// <summary>
    /// Entry point that generates sample barcodes, encrypts them, decrypts them in memory, and reads the barcode data.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for encrypted files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeDecryptDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define sample barcodes to generate (text and symbology)
        var samples = new (string Text, BaseEncodeType Type)[]
        {
            ("HelloWorld", EncodeTypes.Code128),
            ("https://example.com", EncodeTypes.QR)
        };

        // Generate barcode images, encrypt them, and write encrypted files to the temp folder
        for (int i = 0; i < samples.Length; i++)
        {
            var (text, type) = samples[i];
            using (var generator = new BarcodeGenerator(type, text))
            {
                using (var ms = new MemoryStream())
                {
                    // Save the barcode image to a memory stream in PNG format
                    generator.Save(ms, BarCodeImageFormat.Png);
                    byte[] plainBytes = ms.ToArray();

                    // Encrypt the image bytes
                    byte[] encryptedBytes = Encrypt(plainBytes);

                    // Write the encrypted bytes to a file
                    string filePath = Path.Combine(tempDir, $"barcode_{i}.enc");
                    File.WriteAllBytes(filePath, encryptedBytes);
                }
            }
        }

        // Decrypt each encrypted file and decode the barcode(s) it contains
        string[] encryptedFiles = Directory.GetFiles(tempDir, "*.enc");
        foreach (string encFile in encryptedFiles)
        {
            byte[] encryptedBytes = File.ReadAllBytes(encFile);
            byte[] decryptedBytes = Decrypt(encryptedBytes);

            using (var ms = new MemoryStream(decryptedBytes))
            {
                // Initialize the reader with the desired symbologies
                using (var reader = new BarCodeReader(ms, DecodeType.QR, DecodeType.Code128, DecodeType.Pdf417, DecodeType.DataMatrix, DecodeType.Aztec))
                {
                    // Iterate through all detected barcodes and output their type and text
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"File: {Path.GetFileName(encFile)} - Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }
                }
            }
        }

        // Clean up the temporary folder and its contents
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignore cleanup errors (e.g., files still in use)
        }
    }

    private static byte[] Encrypt(byte[] data)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = aesKey;
            aes.IV = aesIv;
            using (ICryptoTransform encryptor = aes.CreateEncryptor())
            {
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.FlushFinalBlock();
                        return ms.ToArray();
                    }
                }
            }
        }
    }

    private static byte[] Decrypt(byte[] data)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = aesKey;
            aes.IV = aesIv;
            using (ICryptoTransform decryptor = aes.CreateDecryptor())
            {
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.FlushFinalBlock();
                        return ms.ToArray();
                    }
                }
            }
        }
    }
}