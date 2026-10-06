// Title: Decrypt Encrypted XML State for BarCodeReader Restoration
// Description: Demonstrates how to encrypt a BarCodeReader's exported XML state, then decrypt it and restore the reader using ImportFromXml.
// Category-Description: This example belongs to the Aspose.BarCode state management category, showing how to persist and restore barcode recognition settings via XML. It uses BarcodeGenerator, BarCodeReader, and cryptographic classes to secure the state file. Developers often need to save reader configurations, protect them, and later reload for consistent scanning results.
// Prompt: Write code to decrypt an encrypted XML state file before calling ImportFromXml for barcode recognition restoration.
// Tags: barcode symbology, state management, encryption, xml, importfromxml, aspose.barcode

using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates encrypting a BarCodeReader state to XML, decrypting it, and restoring the reader.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary working directory for all generated files.
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeStateDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the sample barcode image and the encrypted state file.
        string imagePath = Path.Combine(tempDir, "sample.png");
        string encryptedPath = Path.Combine(tempDir, "state.enc");

        // Generate a sample QR barcode image.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Test123"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Create a BarCodeReader, configure it, and export its state to an in‑memory XML stream.
        using (var reader = new BarCodeReader())
        {
            reader.SetBarCodeImage(imagePath);
            reader.SetBarCodeReadType(DecodeType.QR);
            reader.BarcodeSettings.StripFNC = true;
            reader.QualitySettings.XDimension = XDimensionMode.Small;

            using (var xmlStream = new MemoryStream())
            {
                // Export the configured reader state to XML.
                reader.ExportToXml(xmlStream);
                xmlStream.Position = 0;

                // Encrypt the XML using AES (256‑bit key, 128‑bit IV – all zeros for demo purposes).
                byte[] key = new byte[32];
                byte[] iv = new byte[16];

                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;

                    using (ICryptoTransform encryptor = aes.CreateEncryptor())
                    using (var encryptedMs = new MemoryStream())
                    using (var cryptoStream = new CryptoStream(encryptedMs, encryptor, CryptoStreamMode.Write))
                    {
                        xmlStream.CopyTo(cryptoStream);
                        cryptoStream.FlushFinalBlock();

                        // Write the encrypted data to a file.
                        File.WriteAllBytes(encryptedPath, encryptedMs.ToArray());
                    }
                }

                // Decrypt the XML back into a memory stream.
                using (FileStream encryptedFileStream = new FileStream(encryptedPath, FileMode.Open, FileAccess.Read))
                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;

                    using (ICryptoTransform decryptor = aes.CreateDecryptor())
                    using (var cryptoStream = new CryptoStream(encryptedFileStream, decryptor, CryptoStreamMode.Read))
                    using (var decryptedMs = new MemoryStream())
                    {
                        cryptoStream.CopyTo(decryptedMs);
                        decryptedMs.Position = 0;

                        // Import the BarCodeReader state from the decrypted XML.
                        using (var restoredReader = BarCodeReader.ImportFromXml(decryptedMs))
                        {
                            restoredReader.SetBarCodeImage(imagePath);
                            restoredReader.SetBarCodeReadType(DecodeType.QR);
                            var results = restoredReader.ReadBarCodes();

                            Console.WriteLine($"Barcodes read after state restoration: {results.Length}");
                            foreach (var result in results)
                            {
                                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                            }
                        }
                    }
                }
            }
        }

        // Optional cleanup of the temporary directory.
        // Directory.Delete(tempDir, true);
    }
}