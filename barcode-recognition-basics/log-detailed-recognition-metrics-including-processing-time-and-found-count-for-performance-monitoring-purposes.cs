// Title: Log barcode recognition metrics with Aspose.BarCode
// Description: Demonstrates generating a QR code, recognizing it, and logging processing time and found count for performance monitoring.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes and BarCodeReader to decode them. Typical scenarios include performance monitoring, batch processing, and quality tuning where developers need detailed metrics such as execution time and detected barcode count.
// Prompt: Log detailed recognition metrics, including processing time and found count, for performance monitoring purposes.
// Tags: qr, barcode, recognition, performance, metrics, aspose.barcode, generation, reading

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, recognition, and logging of performance metrics using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR code, reads it, and outputs processing time and found count.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string imagePath = Path.Combine(tempFolder, "sample.png");

        // Generate a sample QR barcode image and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // Initialize the barcode reader for all supported types
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Optional: set a quality preset for faster processing
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Start timing the recognition process
            Stopwatch sw = Stopwatch.StartNew();
            try
            {
                // Perform barcode recognition
                BarCodeResult[] results = reader.ReadBarCodes();
                sw.Stop();

                // Log performance metrics
                Console.WriteLine($"Processing time: {sw.ElapsedMilliseconds} ms");
                Console.WriteLine($"Found count: {reader.FoundCount}");

                // Output details of each recognized barcode
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
            catch (RecognitionAbortedException ex)
            {
                // Handle aborted recognition and log the elapsed time provided by the exception
                sw.Stop();
                Console.WriteLine($"Recognition aborted after {ex.ExecutionTime} ms");
            }
        }

        // Clean up temporary files and folder
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}