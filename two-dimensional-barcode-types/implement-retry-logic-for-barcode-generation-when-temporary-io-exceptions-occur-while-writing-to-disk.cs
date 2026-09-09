// Title: Barcode Generation with Retry Logic for Temporary IO Failures
// Description: Demonstrates how to generate a barcode image using Aspose.BarCode and implement retry logic when temporary I/O exceptions occur while saving the file to disk.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, encoding types, and image format classes. It shows typical scenarios where developers need to handle transient file system errors during barcode image creation, employing retry loops to improve robustness. Useful for applications that generate barcodes in batch processes or cloud environments where I/O reliability may vary.
// Prompt: Implement retry logic for barcode generation when temporary IO exceptions occur while writing to disk.
// Tags: barcode, generation, retry, ioexception, png, code128, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation with retry logic for handling temporary I/O errors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary output folder and initiates barcode generation with retry handling.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeRetryDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Define the full path for the barcode image file
        string outputPath = Path.Combine(outputFolder, "barcode.png");
        string codeText = "12345678";
        BaseEncodeType encodeType = EncodeTypes.Code128;
        int maxAttempts = 3;

        // Generate the barcode image with retry logic
        GenerateBarcodeWithRetry(outputPath, codeText, encodeType, maxAttempts);
    }

    static void GenerateBarcodeWithRetry(string filePath, string codeText, BaseEncodeType encodeType, int maxAttempts)
    {
        // Initialize the barcode generator with the specified symbology and data
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Example of setting a parameter (optional)
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Attempt to save the barcode image, retrying on temporary I/O failures
            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                try
                {
                    generator.Save(filePath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Barcode successfully saved to: {filePath}");
                    break; // Exit loop on success
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Attempt {attempt} failed with IO exception: {ex.Message}");
                    if (attempt == maxAttempts)
                    {
                        Console.WriteLine("All retry attempts exhausted. Barcode generation failed.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Attempt {attempt} failed with unexpected exception: {ex.Message}");
                    // Non-IO exceptions are not retried
                    break;
                }
            }
        }
    }
}