// Title: Barcode generation with retry on temporary I/O errors
// Description: Demonstrates how to generate a Code128 barcode image and save it to disk with retry logic handling transient I/O exceptions.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and file I/O handling. Developers often need to create barcodes programmatically and ensure reliable saving to storage, especially when dealing with temporary file system issues. The pattern shown helps implement robust retry mechanisms for common barcode output scenarios.
// Prompt: Implement retry logic for barcode generation when temporary IO exceptions occur while writing to disk.
// Tags: barcode generation, code128, retry logic, io exception, aspose.barcode, image output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an entry point that generates a barcode image with retry logic for temporary I/O failures.
/// </summary>
class Program
{
    /// <summary>
    /// Main method – defines barcode parameters, invokes the generation routine, and reports the result.
    /// </summary>
    static void Main()
    {
        // Define barcode parameters
        string fileName = "sample_barcode.png";
        string outputPath = Path.Combine(Path.GetTempPath(), fileName);
        BaseEncodeType encodeType = EncodeTypes.Code128;
        string codeText = "123ABC";

        // Attempt to generate and save the barcode with retry logic
        bool success = GenerateBarcodeWithRetry(outputPath, encodeType, codeText, maxAttempts: 3);

        // Output the final status
        Console.WriteLine(success
            ? $"Barcode successfully saved to: {outputPath}"
            : "Failed to save barcode after multiple attempts.");
    }

    /// <summary>
    /// Generates a barcode and saves it to disk, retrying on temporary I/O exceptions.
    /// </summary>
    /// <param name="outputPath">Full file path where the barcode image will be saved.</param>
    /// <param name="type">The barcode symbology type.</param>
    /// <param name="codeText">The text to encode.</param>
    /// <param name="maxAttempts">Maximum number of retry attempts.</param>
    /// <returns>True if the barcode was saved successfully; otherwise false.</returns>
    static bool GenerateBarcodeWithRetry(string outputPath, BaseEncodeType type, string codeText, int maxAttempts)
    {
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                // Ensure the target directory exists
                string directory = Path.GetDirectoryName(outputPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Create the generator, configure optional parameters, and save the barcode
                using (var generator = new BarcodeGenerator(type, codeText))
                {
                    // Example of setting a barcode parameter (optional)
                    generator.Parameters.Barcode.XDimension.Point = 2f;

                    generator.Save(outputPath);
                }

                // Saving succeeded – exit early
                return true;
            }
            catch (IOException ioEx)
            {
                // Log the I/O exception and retry if attempts remain
                Console.WriteLine($"Attempt {attempt} failed due to I/O error: {ioEx.Message}");
                if (attempt == maxAttempts)
                {
                    // No more attempts left
                    return false;
                }

                // Optionally, introduce a short delay before the next attempt
                // (omitted for brevity)
            }
            catch (Exception ex)
            {
                // For non-I/O exceptions, log and abort retries
                Console.WriteLine($"Unexpected error on attempt {attempt}: {ex.Message}");
                return false;
            }
        }

        // Should never reach here
        return false;
    }
}