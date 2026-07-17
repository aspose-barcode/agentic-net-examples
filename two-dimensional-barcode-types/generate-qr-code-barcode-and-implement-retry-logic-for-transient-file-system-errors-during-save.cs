// Title: Generate QR Code with Retry Logic for File Save
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode and saving it with retry handling for transient I/O errors.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use the BarcodeGenerator class with QR symbology, configure error correction, and implement robust file system operations. Developers often need to generate barcodes in various formats (PNG, JPEG, etc.) and ensure reliable saving in environments where I/O errors may occur, such as CI pipelines or network shares.
// Prompt: Generate QR Code barcode and implement retry logic for transient file system errors during save.
// Tags: qr code, barcode generation, png, retry logic, aspose.barcode, encode types, filesystem

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode and saves it to disk with retry logic for transient I/O errors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output directory and file name for the generated QR code image.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        string outputFile = Path.Combine(outputDir, "qr_code.png");

        // Ensure the output directory exists; create it if it does not.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Initialize the QR code generator with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Configure a high error correction level to improve readability under damage.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Define retry parameters for handling transient file system errors.
            const int maxAttempts = 3;
            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                try
                {
                    // Attempt to save the generated QR code image to the specified file.
                    generator.Save(outputFile);
                    Console.WriteLine($"QR code saved successfully to '{outputFile}'.");
                    break; // Exit the loop on successful save.
                }
                catch (IOException ex)
                {
                    // If this was the final attempt, log the failure and rethrow the exception.
                    if (attempt == maxAttempts)
                    {
                        Console.WriteLine($"Failed to save after {maxAttempts} attempts: {ex.Message}");
                        throw;
                    }

                    // Log the transient error and continue to the next retry attempt.
                    Console.WriteLine($"Attempt {attempt} failed with I/O error: {ex.Message}. Retrying...");
                }
            }
        }
    }
}