// Title: Barcode generation with retry on file system errors
// Description: Demonstrates how to generate a QR barcode using Aspose.BarCode with a simple retry mechanism to handle temporary I/O or access errors.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. It shows typical use cases such as creating barcode images while handling transient file system issues, a common requirement for batch processing or automated reporting scenarios. Developers can adapt this pattern for robust barcode creation in production pipelines.
// Prompt: Implement a retry mechanism for barcode generation when encountering temporary file system errors.
// Tags: qr, barcode, generation, retry, ioerror, unauthorizedaccess, aspose.barcode, png

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
    /// Entry point. Attempts to generate a QR barcode, retrying on I/O or access exceptions.
    /// </summary>
    static void Main()
    {
        // Maximum number of retry attempts
        const int maxAttempts = 3;

        // Prepare a temporary output folder
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeRetryDemo");
        Directory.CreateDirectory(outputFolder);

        // Full path for the generated barcode image
        string outputPath = Path.Combine(outputFolder, "qr_retry.png");

        bool success = false;

        // Retry loop: try to generate the barcode up to maxAttempts times
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                // Attempt barcode generation
                GenerateBarcode(outputPath);
                Console.WriteLine($"Barcode generated successfully on attempt {attempt}.");
                success = true;
                break; // Exit loop on success
            }
            catch (IOException ex)
            {
                // Handle temporary I/O errors (e.g., file locked)
                Console.WriteLine($"IO error on attempt {attempt}: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                // Handle access permission issues
                Console.WriteLine($"Access error on attempt {attempt}: {ex.Message}");
            }
        }

        // Report final status if all attempts failed
        if (!success)
        {
            Console.WriteLine("Failed to generate barcode after multiple attempts.");
        }
    }

    /// <summary>
    /// Generates a QR barcode and saves it to the specified file path.
    /// </summary>
    /// <param name="filePath">The full file path where the barcode image will be saved.</param>
    static void GenerateBarcode(string filePath)
    {
        // Create a barcode generator for QR code with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "RetryDemo"))
        {
            // Save the generated barcode as a PNG image
            generator.Save(filePath, BarCodeImageFormat.Png);
        }
    }
}