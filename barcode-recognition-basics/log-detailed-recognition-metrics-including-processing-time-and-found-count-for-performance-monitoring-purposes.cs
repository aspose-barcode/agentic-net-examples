// Title: Barcode generation and recognition performance logging
// Description: Demonstrates creating a Code128 barcode, reading it back, and logging processing time and count of detected barcodes.
// Category-Description: This example belongs to the Aspose.BarCode recognition and generation category, showcasing how to use BarcodeGenerator, BarCodeReader, and DecodeType to generate a barcode image, decode it, and capture performance metrics. Developers often need to benchmark barcode processing, monitor execution time, and verify detection counts in automated workflows or CI pipelines.
// Prompt: Log detailed recognition metrics, including processing time and found count, for performance monitoring purposes.
// Tags: barcode generation, barcode recognition, performance metrics, code128, aspose.barcode, csharp

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation, recognition, and performance metric logging using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, reads it, and outputs processing time and count of detected barcodes.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Default auto-sizing is sufficient for this example
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image file was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Prepare performance measurement tools
        Stopwatch sw = new Stopwatch();
        int foundCount = 0;

        // Use DecodeType.AllSupportedTypes to detect any barcode type
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;

        // Open the barcode image for reading/recognition
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            // Start timing the recognition process
            sw.Start();

            try
            {
                // Read all barcodes present in the image
                var results = reader.ReadBarCodes();

                // Count each detected barcode
                foreach (var result in results)
                {
                    foundCount++;
                    // Optional: output each decoded text (commented out to keep focus on metrics)
                    // Console.WriteLine($"Detected: {result.CodeText} ({result.CodeTypeName})");
                }
            }
            catch (RecognitionAbortedException ex)
            {
                // Handle cases where recognition is aborted, providing execution time info
                Console.WriteLine($"Recognition aborted after {ex.ExecutionTime} ms: {ex.Message}");
            }
            catch (Exception ex)
            {
                // General error handling for unexpected issues during reading
                Console.WriteLine($"Error during barcode reading: {ex.Message}");
            }
            finally
            {
                // Stop timing regardless of success or failure
                sw.Stop();
            }
        }

        // Log detailed recognition metrics for performance monitoring
        Console.WriteLine($"Processing time: {sw.ElapsedMilliseconds} ms");
        Console.WriteLine($"Barcodes found: {foundCount}");

        // Clean up temporary files (optional)
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Suppress any cleanup errors to avoid interrupting the flow
        }
    }
}