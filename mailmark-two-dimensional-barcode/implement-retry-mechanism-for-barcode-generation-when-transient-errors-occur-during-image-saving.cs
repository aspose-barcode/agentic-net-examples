// Title: Barcode Generation with Retry on Transient Errors
// Description: Demonstrates generating a Code128 barcode and saving it as a PNG file while automatically retrying on transient I/O errors.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class to create barcodes, persist them to disk, and implement robust error‑handling patterns such as retry loops for transient failures (e.g., file‑system access issues). Developers working with barcode creation, image output, or automated batch processing often need to ensure reliability when saving files, making this pattern a common requirement.
/// Prompt: Implement a retry mechanism for barcode generation when transient errors occur during image saving.
/// Tags: barcode symbology, generation, retry, png, aspose.barcode, code128, io, error handling

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a simple console demo that generates a Code128 barcode,
/// saves it to a temporary PNG file, and retries the save operation
/// when transient I/O errors occur.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Sets up a temporary folder,
    /// defines barcode parameters, and invokes the retry logic.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeRetryDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define output file path and barcode content
        string outputPath = Path.Combine(tempFolder, "barcode.png");
        string codeText = "12345678";

        // Maximum number of retry attempts for transient failures
        int maxAttempts = 3;

        // Generate the barcode with retry handling
        GenerateBarcodeWithRetry(outputPath, codeText, maxAttempts);
    }

    /// <summary>
    /// Attempts to generate and save a barcode image, retrying on transient errors.
    /// </summary>
    /// <param name="outputPath">Full file path where the PNG image will be saved.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="maxAttempts">Maximum number of retry attempts.</param>
    static void GenerateBarcodeWithRetry(string outputPath, string codeText, int maxAttempts)
    {
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                // Create a barcode generator for Code128 symbology
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                {
                    // Save the barcode image to the specified path in PNG format
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Barcode saved to '{outputPath}' on attempt {attempt}.");
                break; // Success – exit the retry loop
            }
            // Retry only when the exception is considered transient and attempts remain
            catch (Exception ex) when (IsTransient(ex) && attempt < maxAttempts)
            {
                Console.WriteLine($"Attempt {attempt} failed with transient error: {ex.Message}. Retrying...");
            }
            // Any non‑transient error or exhausted attempts are reported and stop retrying
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to generate barcode: {ex.Message}");
                break;
            }
        }
    }

    /// <summary>
    /// Determines whether an exception is considered transient for the purpose of retrying.
    /// </summary>
    /// <param name="ex">The exception to evaluate.</param>
    /// <returns>True if the exception is an I/O‑related transient error; otherwise, false.</returns>
    static bool IsTransient(Exception ex)
    {
        // Consider I/O related exceptions as transient for this example
        return ex is IOException || ex is UnauthorizedAccessException;
    }
}