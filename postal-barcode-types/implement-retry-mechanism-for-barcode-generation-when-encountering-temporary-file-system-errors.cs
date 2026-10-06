// Title: Barcode Generation with Retry on Temporary File System Errors
// Description: Demonstrates how to generate a Code128 barcode image while implementing a retry mechanism to handle transient file system errors such as I/O or access violations.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create barcode images. Typical use cases include generating barcodes for inventory, shipping labels, or point‑of‑sale systems where file write operations may occasionally fail. Developers often need to implement retry logic to ensure reliable barcode creation in environments with intermittent storage issues.
// Prompt: Implement a retry mechanism for barcode generation when encountering temporary file system errors.
// Tags: barcode generation, code128, retry, ioexception, unauthorizedaccessexception, aspnet, aspose.barcode, png, temporary files

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation with retry logic for handling temporary file system errors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Prepares the output directory, invokes the barcode generation with retry,
    /// and reports the result to the console.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for the barcode image
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeRetryDemo");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define the full path for the generated PNG file and the barcode text
        string outputPath = Path.Combine(outputDir, "code128.png");
        string codeText = "1234567890";

        // Set the maximum number of retry attempts
        int maxAttempts = 3;

        // Attempt to generate the barcode with retry logic
        bool success = GenerateBarcodeWithRetry(outputPath, codeText, maxAttempts);
        Console.WriteLine(success ? "Barcode generated successfully." : "Failed to generate barcode after retries.");
    }

    static bool GenerateBarcodeWithRetry(string outputPath, string codeText, int maxAttempts)
    {
        // Loop through the allowed number of attempts
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                // Create a BarcodeGenerator for Code128 with the specified text
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                {
                    // Optional parameter configuration for visual appearance
                    generator.Parameters.Barcode.XDimension.Pixels = 2f;
                    generator.Parameters.Barcode.WideNarrowRatio = 2;

                    // Save the generated barcode as a PNG file
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Attempt {attempt}: Barcode saved to '{outputPath}'.");
                return true; // Success, exit the method
            }
            catch (IOException ex)
            {
                // Handle temporary I/O errors (e.g., file locked, disk full)
                Console.WriteLine($"Attempt {attempt}: IOException encountered - {ex.Message}");
                // Continue to next attempt
            }
            catch (UnauthorizedAccessException ex)
            {
                // Handle transient access errors (e.g., permission issues that may resolve)
                Console.WriteLine($"Attempt {attempt}: UnauthorizedAccessException encountered - {ex.Message}");
                // Continue to next attempt
            }
            catch (Exception ex)
            {
                // Handle unexpected errors that are not retriable
                Console.WriteLine($"Attempt {attempt}: Unexpected error - {ex.Message}");
                break; // Abort retry loop
            }
        }

        // All attempts failed
        return false;
    }
}