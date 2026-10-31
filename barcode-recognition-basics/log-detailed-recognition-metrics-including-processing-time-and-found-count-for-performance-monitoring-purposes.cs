// Title: Barcode Generation, Recognition, and Performance Metrics Logging
// Description: Demonstrates generating a Code128 barcode, recognizing it, and logging processing time and count of detected barcodes.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and Stopwatch for measuring performance. Developers often need to generate barcodes, read them from images, and monitor recognition speed and accuracy in batch processing or real-time applications.
// Prompt: Log detailed recognition metrics, including processing time and found count, for performance monitoring purposes.
// Tags: barcode generation, barcode recognition, performance metrics, code128, aspose.barcode, stopwatch, image processing

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a barcode image, reads it back, and logs recognition metrics.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, reads it, and outputs processing time and found count.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder for storing the generated barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define barcode content and output image path
        string barcodeText = "1234567890";
        string imagePath = Path.Combine(tempFolder, "barcode.png");

        // Generate barcode image using Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, barcodeText))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Read the generated barcode and log performance metrics
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Start timing the recognition process
            Stopwatch sw = Stopwatch.StartNew();
            reader.ReadBarCodes();
            sw.Stop();

            // Retrieve the number of barcodes found
            int foundCount = reader.FoundCount;
            Console.WriteLine($"Processing time: {sw.ElapsedMilliseconds} ms");
            Console.WriteLine($"Found barcodes: {foundCount}");

            // Output details of each recognized barcode
            foreach (BarCodeResult result in reader.FoundBarCodes)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Clean up temporary files and directories
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any errors that occur during cleanup
        }
    }
}