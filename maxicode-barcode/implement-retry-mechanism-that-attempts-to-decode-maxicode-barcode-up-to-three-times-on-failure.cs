// Title: MaxiCode barcode generation and retry decoding example
// Description: Demonstrates generating a MaxiCode barcode, saving it as PNG, and attempting to decode it with up to three retries on failure.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating MaxiCode symbols and BarCodeReader for decoding them. Typical use cases include batch processing of shipping labels or inventory tags where decoding may need retries due to image quality. Developers often need to handle transient read errors and implement retry logic using DecodeType and EncodeTypes classes.
// Prompt: Implement a retry mechanism that attempts to decode a MaxiCode barcode up to three times on failure.
// Tags: maxicode, barcode generation, barcode recognition, retry, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides an example of generating a MaxiCode barcode, saving it to a temporary file,
/// and decoding it with a retry mechanism that attempts up to three times on failure.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a MaxiCode barcode, then tries to read it
    /// up to three times, reporting success or failure to the console.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "MaxiCodeRetry_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "maxicode.png");

        // Generate a simple MaxiCode barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Test"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 10f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        const int maxAttempts = 3;
        bool decoded = false;

        // Attempt to decode the barcode up to maxAttempts times
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                using (var reader = new BarCodeReader(imagePath, DecodeType.MaxiCode))
                {
                    var results = reader.ReadBarCodes();
                    if (results != null && results.Length > 0)
                    {
                        // Successful decode – output each result
                        foreach (var result in results)
                        {
                            Console.WriteLine($"Decoded on attempt {attempt}: {result.CodeText}");
                        }
                        decoded = true;
                        break; // Exit the retry loop
                    }
                    else
                    {
                        Console.WriteLine($"Attempt {attempt}: No barcode detected.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception and continue to next attempt
                Console.WriteLine($"Attempt {attempt} failed: {ex.Message}");
            }
        }

        if (!decoded)
        {
            Console.WriteLine("Failed to decode the MaxiCode barcode after 3 attempts.");
        }

        // Clean up temporary files and folder
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}