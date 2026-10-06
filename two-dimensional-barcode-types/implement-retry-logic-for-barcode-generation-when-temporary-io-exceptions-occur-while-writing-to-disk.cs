// Title: Barcode Generation with Retry Logic for Temporary I/O Failures
// Description: Demonstrates how to generate a barcode image using Aspose.BarCode and automatically retry when transient I/O exceptions occur while saving the file.
// Category-Description: This example belongs to the Aspose.BarCode file output operations category. It shows how to use the BarcodeGenerator class together with the Parameters and Save methods to create barcode images, and illustrates implementing retry logic for handling temporary file system errors. Developers working with barcode generation often need to ensure reliable file writes in environments with intermittent I/O issues, such as network shares or cloud storage.
// Prompt: Implement retry logic for barcode generation when temporary IO exceptions occur while writing to disk.
// Tags: barcode generation, retry logic, io exception, code128, png, aspose.barcode, file output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation with retry logic for handling temporary I/O errors during file saving.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Sets up parameters and invokes the retry-enabled barcode generation method.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory for the barcode image.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeRetryDemo");
        Directory.CreateDirectory(outputDir); // Ensure the directory exists.

        // Build the full file path for the generated PNG image.
        string filePath = Path.Combine(outputDir, "barcode.png");

        // Barcode content and symbology.
        string codeText = "12345678";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Maximum number of retry attempts in case of I/O failures.
        int maxAttempts = 3;

        // Generate the barcode with retry handling.
        GenerateBarcodeWithRetry(filePath, codeText, encodeType, maxAttempts);
    }

    /// <summary>
    /// Generates a barcode image and saves it to disk, retrying on temporary I/O exceptions.
    /// </summary>
    /// <param name="filePath">The full path where the barcode image will be saved.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="encodeType">The barcode symbology to use.</param>
    /// <param name="maxAttempts">Maximum number of retry attempts for I/O errors.</param>
    static void GenerateBarcodeWithRetry(string filePath, string codeText, BaseEncodeType encodeType, int maxAttempts)
    {
        // Attempt to generate and save the barcode up to the specified number of times.
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                // Create a new BarcodeGenerator with the desired symbology and data.
                using (var generator = new BarcodeGenerator(encodeType, codeText))
                {
                    // Optional: adjust barcode visual parameters (e.g., X-dimension).
                    generator.Parameters.Barcode.XDimension.Pixels = 2f;

                    // Save the generated barcode image to the target file in PNG format.
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Barcode successfully saved to '{filePath}' on attempt {attempt}.");
                break; // Exit the loop on successful save.
            }
            catch (IOException ioEx)
            {
                // Handle transient I/O errors (e.g., file lock, network hiccup).
                Console.WriteLine($"Attempt {attempt} failed with I/O error: {ioEx.Message}");
                if (attempt == maxAttempts)
                {
                    Console.WriteLine("All retry attempts exhausted. Barcode generation failed.");
                }
                // No delay is introduced; the loop proceeds to the next attempt immediately.
            }
            catch (Exception ex)
            {
                // Non-I/O exceptions are considered fatal; do not retry.
                Console.WriteLine($"Attempt {attempt} failed with unexpected error: {ex.Message}");
                break;
            }
        }
    }
}