// Title: Barcode generation with retry on transient save errors
// Description: Demonstrates how to generate a Code128 barcode image and implement a simple retry mechanism when saving the image fails due to transient errors.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create barcode images. Typical use cases include generating barcodes for inventory, shipping, or point‑of‑sale systems where occasional I/O failures may occur, and developers need a retry strategy to ensure reliable image output. The snippet shows best practices for handling transient exceptions during the Save operation.
/// Prompt: Implement a retry mechanism for barcode generation when transient errors occur during image saving.
/// Tags: barcode, code128, retry, image saving, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation with a retry mechanism for transient save errors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Sets up output path, barcode data, and invokes the retry logic.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeRetryDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated barcode image
        string outputPath = Path.Combine(outputDir, "sample_code128.png");

        // Barcode content and symbology
        string codeText = "12345678";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Maximum number of retry attempts for saving the image
        int maxAttempts = 3;

        // Generate the barcode with retry handling
        GenerateBarcodeWithRetry(outputPath, codeText, encodeType, maxAttempts);
    }

    /// <summary>
    /// Generates a barcode image and retries the save operation if a transient exception occurs.
    /// </summary>
    /// <param name="outputPath">File path where the barcode image will be saved.</param>
    /// <param name="codeText">The data to encode in the barcode.</param>
    /// <param name="encodeType">The barcode symbology to use.</param>
    /// <param name="maxAttempts">Maximum number of save attempts before giving up.</param>
    static void GenerateBarcodeWithRetry(string outputPath, string codeText, BaseEncodeType encodeType, int maxAttempts)
    {
        // Loop through attempts up to the specified maximum
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                // Create a barcode generator with the desired symbology and data
                using (var generator = new BarcodeGenerator(encodeType, codeText))
                {
                    // Additional barcode parameters can be set here if needed

                    // Attempt to save the barcode image to the specified path
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                }

                // If save succeeds, log success and exit the retry loop
                Console.WriteLine($"Barcode saved successfully to '{outputPath}' on attempt {attempt}.");
                break;
            }
            catch (Exception ex)
            {
                // Log the failure of the current attempt
                Console.WriteLine($"Attempt {attempt} failed: {ex.Message}");

                // If this was the final allowed attempt, inform the user
                if (attempt == maxAttempts)
                {
                    Console.WriteLine("All retry attempts exhausted. Barcode generation failed.");
                }
            }
        }
    }
}