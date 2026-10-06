// Title: Demonstrate false positive detection with low MinimalXDimension
// Description: Generates a Code128 barcode and reads it using a deliberately low MinimalXDimension setting to show how false positives can be recorded.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader with QualitySettings (XDimensionMode and MinimalXDimension) for decoding. Developers often adjust MinimalXDimension to balance detection sensitivity and false positive rates, especially in high‑throughput scanning scenarios.
// Prompt: Record the number of false positives encountered when MinimalXDimension is set too low.
// Tags: code128, barcode-generation, barcode-recognition, minimalxdimension, false-positives, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a barcode, reads it with a low MinimalXDimension,
/// and records any false positives that occur during decoding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, attempts to read it with
    /// a minimal X-dimension setting, and reports false positive counts.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for storing the generated barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the barcode image file
        string barcodePath = Path.Combine(tempFolder, "barcode.png");

        // Generate a simple Code128 barcode and save it as a PNG file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "AsposeTest"))
        {
            // Set the X-dimension (width of the narrowest bar) to 2 pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Expected number of barcodes in the image (we generated only one)
        int expectedCount = 1;
        int falsePositives = 0;

        // Initialize the barcode reader for Code128 symbology
        BaseDecodeType decodeType = DecodeType.Code128;
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            // Configure the reader to use MinimalXDimension mode with an intentionally low value
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 0.5f; // very low to provoke false positives

            // Perform barcode detection
            BarCodeResult[] results = reader.ReadBarCodes();
            int readCount = results?.Length ?? 0;

            // Calculate false positives if more barcodes were detected than expected
            if (readCount > expectedCount)
            {
                falsePositives = readCount - expectedCount;
            }

            // Output the detection results
            Console.WriteLine($"Barcodes read: {readCount}");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Report the number of false positives detected
        Console.WriteLine($"False positives detected: {falsePositives}");

        // Clean up temporary files and directories
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any errors that occur during cleanup
        }
    }
}