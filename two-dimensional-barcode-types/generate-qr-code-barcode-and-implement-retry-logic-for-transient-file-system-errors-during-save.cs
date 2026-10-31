// Title: Generate QR Code with retry logic for file save
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode and saving it as PNG with retry handling for transient file system errors.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure QR Code parameters (EncodeTypes.QR, error correction level) and implement robust file saving with retry logic for IOException and UnauthorizedAccessException. Developers commonly use these APIs to produce QR codes for URLs, product information, or authentication, and need to handle occasional file access issues in automated or cloud environments.
// Prompt: Generate QR Code barcode and implement retry logic for transient file system errors during save.
// Tags: qr code, barcode generation, retry logic, file io, aspose.barcode, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode and saving it with retry logic for transient file system errors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code and attempts to save it with retry handling.
    /// </summary>
    static void Main()
    {
        // Prepare output directory and file path
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);
        string filePath = Path.Combine(outputDir, "qr.png");

        // QR code content (e.g., a URL)
        string codeText = "https://example.com";

        // Maximum number of save attempts
        const int maxAttempts = 3;

        // Create and configure the QR code generator
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set module size (pixel dimension) for the QR code
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            // Use high error correction level to improve readability
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Attempt to save with retry logic for transient file system errors
            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                try
                {
                    generator.Save(filePath, BarCodeImageFormat.Png);
                    Console.WriteLine($"QR code successfully saved to: {filePath}");
                    break; // Exit loop on successful save
                }
                catch (IOException) when (attempt < maxAttempts)
                {
                    Console.WriteLine($"Attempt {attempt} failed with IOException. Retrying...");
                }
                catch (UnauthorizedAccessException) when (attempt < maxAttempts)
                {
                    Console.WriteLine($"Attempt {attempt} failed with UnauthorizedAccessException. Retrying...");
                }
                catch (Exception ex)
                {
                    // Unexpected exception – log and rethrow
                    Console.WriteLine($"Failed to save QR code: {ex.Message}");
                    throw;
                }
            }
        }
    }
}