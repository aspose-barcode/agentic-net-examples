// Title: Barcode generation with retry on temporary file system errors
// Description: Demonstrates how to generate a Code128 barcode and retry the operation when encountering transient I/O or access errors.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. It illustrates typical scenarios where developers need to handle temporary file system issues such as locked files or permission glitches while saving barcode images, and implement a simple retry mechanism.
// Prompt: Implement a retry mechanism for barcode generation when encountering temporary file system errors.
// Tags: barcode generation, retry, ioexception, unauthorizedaccessexception, code128, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation with a retry mechanism for handling temporary file system errors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode and handles success or failure messages.
    /// </summary>
    static void Main()
    {
        // Text to encode in the barcode
        string text = "Sample12345";

        // Destination path for the generated barcode image (temporary folder)
        string outputPath = Path.Combine(Path.GetTempPath(), "retry_barcode.png");

        // Maximum number of retry attempts
        int maxAttempts = 3;

        try
        {
            // Attempt to generate the barcode with retry logic
            GenerateBarcodeWithRetry(text, outputPath, maxAttempts);
            Console.WriteLine("Barcode generation completed successfully.");
        }
        catch (Exception ex)
        {
            // Report failure after exhausting retries or encountering a non-retriable error
            Console.WriteLine($"Barcode generation failed: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a barcode image, retrying on temporary I/O or access errors up to the specified number of attempts.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="filePath">The file path where the barcode image will be saved.</param>
    /// <param name="maxAttempts">Maximum number of retry attempts.</param>
    static void GenerateBarcodeWithRetry(string codeText, string filePath, int maxAttempts)
    {
        // Loop through attempts until success or maxAttempts reached
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                // Create a barcode generator for Code128 symbology
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                {
                    // Example parameter: set X dimension (pixel size of the smallest bar)
                    generator.Parameters.Barcode.XDimension.Pixels = 3;

                    // Save the generated barcode image to the specified path in PNG format
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                // Log success and exit the retry loop
                Console.WriteLine($"Barcode saved to '{filePath}' on attempt {attempt}.");
                break;
            }
            catch (IOException ioEx)
            {
                // Log I/O error and decide whether to retry
                Console.WriteLine($"Attempt {attempt} failed with I/O error: {ioEx.Message}");
                if (attempt == maxAttempts) throw;
            }
            catch (UnauthorizedAccessException uaEx)
            {
                // Log access error and decide whether to retry
                Console.WriteLine($"Attempt {attempt} failed with access error: {uaEx.Message}");
                if (attempt == maxAttempts) throw;
            }
            catch (Exception ex) when (!(ex is IOException) && !(ex is UnauthorizedAccessException))
            {
                // Non-retriable exception encountered; rethrow immediately
                Console.WriteLine($"Attempt {attempt} failed with non-retriable error: {ex.Message}");
                throw;
            }
        }
    }
}