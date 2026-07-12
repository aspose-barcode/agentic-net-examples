// Title: Decode barcodes from encrypted images in memory
// Description: Demonstrates decrypting AES‑encrypted image files in memory and using Aspose.BarCode to read any barcodes they contain.
// Category-Description: This example belongs to the Aspose.BarCode image processing and barcode recognition category. It shows how to work with the BarCodeReader, DecodeType, and QualitySettings classes to extract barcode data from images that are first decrypted in memory. Typical use cases include secure storage of barcode images and on‑the‑fly decoding without writing decrypted files to disk.
// Prompt: Decode a set of barcodes from encrypted image files after decrypting them in memory.
// Tags: barcode, decryption, aes, memory, aspose.barcode, decode, image

using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that decrypts AES‑encrypted image files in memory
/// and decodes any barcodes they contain using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Performs simple AES‑CBC decryption. This method is for demonstration
    /// purposes only and uses a hard‑coded key/IV.
    /// </summary>
    /// <param name="cipherData">Encrypted byte array.</param>
    /// <param name="key">AES key (256‑bit).</param>
    /// <param name="iv">AES initialization vector (128‑bit).</param>
    /// <returns>Decrypted byte array.</returns>
    private static byte[] DecryptAes(byte[] cipherData, byte[] key, byte[] iv)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (var decryptor = aes.CreateDecryptor())
            using (var msInput = new MemoryStream(cipherData))
            using (var msOutput = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(msInput, decryptor, CryptoStreamMode.Read))
                {
                    cryptoStream.CopyTo(msOutput);
                }
                return msOutput.ToArray();
            }
        }
    }

    /// <summary>
    /// Entry point. Decrypts each encrypted image file, loads it into a bitmap,
    /// and uses <see cref="BarCodeReader"/> to detect and output barcode information.
    /// </summary>
    static void Main()
    {
        // Paths to sample encrypted image files (replace with actual file locations)
        string[] encryptedFiles = new string[]
        {
            "encrypted1.bin",
            "encrypted2.bin",
            "encrypted3.bin"
        };

        // Hard‑coded AES key and IV for the example (must match the encryption side)
        byte[] aesKey = new byte[32]; // 256‑bit key (all zeros for demo)
        byte[] aesIv  = new byte[16]; // 128‑bit IV (all zeros for demo)

        foreach (var encPath in encryptedFiles)
        {
            // Verify that the encrypted file exists before attempting to process it
            if (!File.Exists(encPath))
            {
                Console.WriteLine($"File not found: {encPath}");
                continue;
            }

            try
            {
                // Read the encrypted file bytes from disk
                byte[] encryptedBytes = File.ReadAllBytes(encPath);

                // Decrypt the bytes to obtain the original image (e.g., PNG)
                byte[] imageBytes = DecryptAes(encryptedBytes, aesKey, aesIv);

                // Load the decrypted image into a bitmap using a memory stream
                using (var imageStream = new MemoryStream(imageBytes))
                using (var bitmap = new Bitmap(imageStream))
                using (var reader = new BarCodeReader())
                {
                    // Configure the reader to detect all supported barcode symbologies
                    reader.BarCodeReadType = DecodeType.AllSupportedTypes;

                    // Use high‑quality settings to improve recognition on low‑quality images
                    reader.QualitySettings = QualitySettings.HighQuality;

                    // Assign the bitmap image to the reader
                    reader.SetBarCodeImage(bitmap);

                    // Perform the barcode decoding operation
                    var results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcodes detected in file: {encPath}");
                    }
                    else
                    {
                        Console.WriteLine($"Barcodes found in file: {encPath}");
                        foreach (var result in results)
                        {
                            Console.WriteLine($"  Type: {result.CodeTypeName}");
                            Console.WriteLine($"  Text: {result.CodeText}");

                            // Output the region bounds of the detected barcode
                            var bounds = result.Region.Rectangle;
                            Console.WriteLine($"  Region: X={bounds.X}, Y={bounds.Y}, W={bounds.Width}, H={bounds.Height}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing of the current file
                Console.WriteLine($"Error processing file {encPath}: {ex.Message}");
            }
        }
    }
}