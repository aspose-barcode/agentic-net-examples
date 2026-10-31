// Title: Barcode reading with retry on low reading quality
// Description: Demonstrates generating a Code128 barcode, reading it, and retrying when the reading quality is reported as None.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Developers often need to handle cases where the scanner reports poor quality (ReadingQuality = None) and implement retry logic to improve reliability in automated processing pipelines.
// Prompt: Implement a retry mechanism that re‑reads a barcode image when ReadingQuality is reported as None.
// Tags: code128, barcode generation, barcode recognition, readingquality, retry, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a barcode image, reading it with Aspose.BarCode,
/// and retrying the read operation when the reported <c>ReadingQuality</c> is None.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary barcode image,
    /// attempts to read it up to a maximum number of retries,
    /// and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the demo
        string tempFolder = Path.Combine(Path.GetTempPath(), "RetryDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        const int maxAttempts = 3;
        bool success = false;

        // Retry loop: attempt to read the barcode up to maxAttempts times
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
            {
                BarCodeResult[] results;
                try
                {
                    // Attempt to read all barcodes from the image
                    results = reader.ReadBarCodes();
                }
                catch (Exception ex)
                {
                    // Log any exception and abort further attempts
                    Console.WriteLine($"Attempt {attempt}: Exception during reading - {ex.Message}");
                    break;
                }

                if (results == null || results.Length == 0)
                {
                    // No barcodes detected; treat as low quality and possibly retry
                    Console.WriteLine($"Attempt {attempt}: No barcodes detected.");
                }
                else
                {
                    bool anyNone = false;

                    // Output details for each detected barcode
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Attempt {attempt}: Type={result.CodeTypeName}, Text={result.CodeText}, ReadingQuality={result.ReadingQuality}");
                        if (result.ReadingQuality == 0) // 0 corresponds to ReadingQuality.None
                            anyNone = true;
                    }

                    // If none of the results have ReadingQuality.None, stop retrying
                    if (!anyNone)
                    {
                        Console.WriteLine("Reading quality acceptable, stopping retries.");
                        success = true;
                        break;
                    }
                }

                // Provide feedback about retry status
                if (attempt < maxAttempts)
                {
                    Console.WriteLine("ReadingQuality is None, retrying...");
                }
                else
                {
                    Console.WriteLine("Maximum attempts reached.");
                }
            }
        }

        // Cleanup temporary files and directories
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }

        // Final outcome message
        Console.WriteLine(success ? "Barcode read successfully." : "Failed to obtain acceptable reading quality.");
    }
}