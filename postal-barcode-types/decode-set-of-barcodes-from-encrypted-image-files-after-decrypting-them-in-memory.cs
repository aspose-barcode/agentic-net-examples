// Title: Decrypt and Decode Barcodes from Encrypted Image Files
// Description: Demonstrates generating barcode images, encrypting them in memory, decrypting the data, and decoding the barcodes using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating QR, Code128, and DataMatrix barcodes, and BarCodeReader for extracting barcode information from image streams. Typical scenarios include secure storage of barcode images, batch processing of encrypted assets, and on‑the‑fly decoding without persisting plaintext files. Developers often need to combine image manipulation, simple encryption, and barcode APIs to meet security and automation requirements.
// Prompt: Decode a set of barcodes from encrypted image files after decrypting them in memory.
// Tags: barcode, encryption, decryption, generation, recognition, aspose.barcode, qr, code128, datamatrix, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates barcode images, encrypts them, decrypts them in memory,
/// and then decodes the barcodes using Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Prepare a temporary folder to store encrypted barcode image files.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "EncryptedBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // --------------------------------------------------------------
        // 2. Define sample barcode data (symbology and corresponding text).
        // --------------------------------------------------------------
        var samples = new List<(BaseEncodeType encodeType, string codeText)>
        {
            (EncodeTypes.QR, "Sample QR Code"),
            (EncodeTypes.Code128, "CODE12812345"),
            (EncodeTypes.DataMatrix, "DM1234567890")
        };

        // --------------------------------------------------------------
        // 3. Simple XOR key used for encrypting the image byte arrays.
        // --------------------------------------------------------------
        byte xorKey = 0x5A;

        // --------------------------------------------------------------
        // 4. Generate barcode images, encrypt them, and write to disk.
        // --------------------------------------------------------------
        foreach (var (encodeType, codeText) in samples)
        {
            string filePath = Path.Combine(tempFolder, $"{Guid.NewGuid().ToString("N")}.png");
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                using (var ms = new MemoryStream())
                {
                    // Save the barcode image to a memory stream in PNG format.
                    generator.Save(ms, BarCodeImageFormat.Png);
                    byte[] imageBytes = ms.ToArray();

                    // Encrypt the image bytes using XOR.
                    for (int i = 0; i < imageBytes.Length; i++)
                    {
                        imageBytes[i] ^= xorKey;
                    }

                    // Persist the encrypted bytes to a file.
                    File.WriteAllBytes(filePath, imageBytes);
                }
            }
        }

        // --------------------------------------------------------------
        // 5. Locate all encrypted PNG files for decryption and decoding.
        // --------------------------------------------------------------
        var encryptedFiles = new List<string>(Directory.GetFiles(tempFolder, "*.png"));
        foreach (string encryptedPath in encryptedFiles)
        {
            if (!File.Exists(encryptedPath))
            {
                Console.WriteLine($"File not found: {encryptedPath}");
                continue;
            }

            // ----------------------------------------------------------
            // 6. Read and decrypt the file contents back into a memory stream.
            // ----------------------------------------------------------
            byte[] encryptedBytes = File.ReadAllBytes(encryptedPath);
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                encryptedBytes[i] ^= xorKey; // Reverse XOR encryption.
            }

            using (var decryptedStream = new MemoryStream(encryptedBytes))
            {
                // ------------------------------------------------------
                // 7. Initialize a BarCodeReader for the supported symbologies.
                // ------------------------------------------------------
                using (var reader = new BarCodeReader(decryptedStream, DecodeType.QR, DecodeType.Code128, DecodeType.DataMatrix))
                {
                    // --------------------------------------------------
                    // 8. Iterate over detected barcodes and output results.
                    // --------------------------------------------------
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"File: {Path.GetFileName(encryptedPath)}");
                        Console.WriteLine($"  Detected Type: {result.CodeTypeName}");
                        Console.WriteLine($"  Code Text: {result.CodeText}");
                    }
                }
            }
        }

        // --------------------------------------------------------------
        // 9. Clean up the temporary folder and its contents.
        // --------------------------------------------------------------
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any exceptions during cleanup (e.g., file locks).
        }
    }
}