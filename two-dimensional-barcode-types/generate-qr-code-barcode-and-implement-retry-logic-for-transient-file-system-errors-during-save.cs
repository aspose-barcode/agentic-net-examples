// Title: Generate QR Code with Retry Logic for File Save
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode and saving it to a PNG file with retry handling for transient I/O errors.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce QR Code images. Typical scenarios include generating QR codes for URLs, contact information, or product data, where developers often need to handle occasional file system glitches by implementing retry logic.
// Prompt: Generate QR Code barcode and implement retry logic for transient file system errors during save.
// Tags: qr code, barcode generation, retry, io, png, aspose.barcode, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a QR Code barcode and saves it to a PNG file
/// with retry logic for handling transient file system errors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code and attempts to save it,
    /// retrying up to three times if an IOException occurs.
    /// </summary>
    static void Main()
    {
        // Define a unique temporary output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "QrDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting PNG file
        string outputPath = Path.Combine(outputDir, "qr_code.png");

        // Text to encode in the QR Code (e.g., a URL)
        string qrText = "https://www.example.com";

        // Initialize the barcode generator for QR encoding
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Optional: configure visual appearance and error correction level
            generator.Parameters.Barcode.XDimension.Pixels = 4f;               // Module size
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH; // Highest error correction

            const int maxAttempts = 3; // Maximum number of save attempts

            // Attempt to save the image, retrying on transient I/O failures
            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                try
                {
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"QR code saved successfully to: {outputPath}");
                    break; // Exit loop on success
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Attempt {attempt} failed with I/O error: {ex.Message}");
                    if (attempt == maxAttempts)
                    {
                        Console.WriteLine("All retry attempts exhausted. Operation failed.");
                        throw; // Re‑throw the exception after final attempt
                    }
                }
            }
        }
    }
}