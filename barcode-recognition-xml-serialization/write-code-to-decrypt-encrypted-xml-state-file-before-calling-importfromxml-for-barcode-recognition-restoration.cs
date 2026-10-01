// Title: Decrypt Encrypted XML State and Restore Barcode Reader Configuration
// Description: Demonstrates how to decrypt an AES‑encrypted XML state file and import it into Aspose.BarCode's BarCodeReader to recognize barcodes from an image.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition configuration category. It shows how to use BarCodeReader.ImportFromXml to restore a previously saved reader state, a common task when persisting recognition settings securely. Developers often need to encrypt configuration files and later decrypt them for runtime use, leveraging AES encryption and the BarCodeReader API.
// Prompt: Write code to decrypt an encrypted XML state file before calling ImportFromXml for barcode recognition restoration.
// Tags: barcode, aes, decryption, xml, importfromxml, barcoderecognition, aspose.barcode

using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that decrypts an AES‑encrypted XML state file,
/// imports the configuration into a <see cref="BarCodeReader"/>,
/// and performs barcode recognition on a supplied image.
/// </summary>
class Program
{
    // Sample AES key (32 bytes for AES‑256) and IV (16 bytes) used for decryption.
    private static readonly byte[] AesKey = new byte[32]
    {
        0x10,0x20,0x30,0x40,0x50,0x60,0x70,0x80,
        0x90,0xA0,0xB0,0xC0,0xD0,0xE0,0xF0,0x01,
        0x11,0x21,0x31,0x41,0x51,0x61,0x71,0x81,
        0x91,0xA1,0xB1,0xC1,0xD1,0xE1,0xF1,0x02
    };

    private static readonly byte[] AesIv = new byte[16]
    {
        0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,
        0x09,0x0A,0x0B,0x0C,0x0D,0x0E,0x0F,0x10
    };

    /// <summary>
    /// Entry point of the program. Decrypts the XML state file,
    /// restores the barcode reader configuration, and reads barcodes from an image.
    /// </summary>
    static void Main()
    {
        // Paths for the encrypted XML state file and the barcode image to be recognized.
        string encryptedXmlPath = "encrypted_state.xml";
        string barcodeImagePath = "barcode.png";

        // Validate that the encrypted XML file exists.
        if (!File.Exists(encryptedXmlPath))
        {
            Console.WriteLine($"Encrypted XML file not found: {encryptedXmlPath}");
            return;
        }

        // Validate that the barcode image file exists.
        if (!File.Exists(barcodeImagePath))
        {
            Console.WriteLine($"Barcode image file not found: {barcodeImagePath}");
            return;
        }

        // Decrypt the XML state file using the predefined AES key and IV.
        byte[] decryptedXmlBytes = DecryptFile(encryptedXmlPath, AesKey, AesIv);
        if (decryptedXmlBytes == null || decryptedXmlBytes.Length == 0)
        {
            Console.WriteLine("Decryption failed or resulted in empty data.");
            return;
        }

        // Import the reader configuration from the decrypted XML.
        using (var xmlStream = new MemoryStream(decryptedXmlBytes))
        {
            using (var reader = BarCodeReader.ImportFromXml(xmlStream))
            {
                // Set the image source for recognition.
                reader.SetBarCodeImage(barcodeImagePath);

                // Perform barcode reading.
                BarCodeResult[] results = reader.ReadBarCodes();

                // Output the results.
                if (results == null || results.Length == 0)
                {
                    Console.WriteLine("No barcodes detected.");
                }
                else
                {
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Code Text: {result.CodeText}");
                        Console.WriteLine($"Symbology: {result.CodeTypeName}");
                        Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                        Console.WriteLine();
                    }
                }
            }
        }
    }

    // Decrypts an AES‑encrypted file and returns the plaintext bytes.
    private static byte[] DecryptFile(string encryptedFilePath, byte[] key, byte[] iv)
    {
        try
        {
            using (var encryptedStream = new FileStream(encryptedFilePath, FileMode.Open, FileAccess.Read))
            {
                using (var aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Mode = CipherMode.CBC;

                    using (var cryptoTransform = aes.CreateDecryptor())
                    {
                        using (var cryptoStream = new CryptoStream(encryptedStream, cryptoTransform, CryptoStreamMode.Read))
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                cryptoStream.CopyTo(memoryStream);
                                return memoryStream.ToArray();
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during decryption: {ex.Message}");
            return null;
        }
    }
}