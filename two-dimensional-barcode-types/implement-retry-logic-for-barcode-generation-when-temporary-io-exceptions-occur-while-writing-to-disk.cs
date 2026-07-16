// Title: Barcode generation with retry logic for temporary IO errors
// Description: Demonstrates generating a Code128 barcode image and saving it to disk with retry handling for transient I/O exceptions.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator and related classes to create barcodes, handle file system errors, and implement simple retry mechanisms. Developers often need to generate barcode images in various formats (PNG, JPEG) and ensure robustness against temporary disk access issues.
// Prompt: Implement retry logic for barcode generation when temporary IO exceptions occur while writing to disk.
// Tags: code128, generation, png, aspose.barcode, barcodegenerator, i/o, retry

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation with retry logic for temporary I/O exceptions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode and saves it with retry handling.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the generated barcode image
        string outputPath = Path.Combine(outputDir, "code128.png");

        // Create a barcode generator for Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "123ABC";

            // Save the barcode image with retry logic (max 3 attempts)
            SaveBarcodeWithRetry(generator, outputPath, maxAttempts: 3);
        }

        Console.WriteLine("Barcode generation completed.");
    }

    /// <summary>
    /// Saves the barcode image to the specified file path, retrying on transient I/O exceptions.
    /// </summary>
    /// <param name="generator">The BarcodeGenerator instance configured with the desired symbology and text.</param>
    /// <param name="filePath">Full path where the barcode image will be saved.</param>
    /// <param name="maxAttempts">Maximum number of retry attempts before giving up.</param>
    static void SaveBarcodeWithRetry(BarcodeGenerator generator, string filePath, int maxAttempts)
    {
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                // Attempt to save the barcode image to disk
                generator.Save(filePath);
                Console.WriteLine($"Saved barcode to '{filePath}' on attempt {attempt}.");
                return; // Success – exit the method
            }
            catch (IOException ex)
            {
                // Log the I/O exception and decide whether to retry
                Console.WriteLine($"IO exception on attempt {attempt}: {ex.Message}");
                if (attempt == maxAttempts)
                {
                    Console.WriteLine("Maximum retry attempts reached. Operation failed.");
                    throw; // Re‑throw the exception after final attempt
                }
                // Immediate retry without delay (as per constraints)
            }
        }
    }
}