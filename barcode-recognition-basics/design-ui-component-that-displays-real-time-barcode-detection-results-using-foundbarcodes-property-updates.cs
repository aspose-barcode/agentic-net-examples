// Title: Real‑time barcode detection demo using Aspose.BarCode
// Description: Generates a Code128 barcode image, then reads it multiple times to simulate real‑time detection, displaying detection results and the FoundBarCodes count.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category. It demonstrates how to use BarcodeGenerator to create barcodes and BarCodeReader with QualitySettings to detect barcodes in images. Typical use cases include scanning images in a loop, updating UI components with detection results, and handling multiple symbologies. Developers often need to retrieve the FoundBarCodes property and barcode region information for UI overlays.
// Prompt: Design a UI component that displays real‑time barcode detection results using FoundBarCodes property updates.
// Tags: barcode, code128, detection, realtime, foundbarcodes, qualitysettings, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode and performing repeated detection to simulate real‑time updates.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, reads it multiple times,
    /// and outputs detection details including the FoundBarCodes count.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory for generated files
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define barcode parameters
        string codeText = "ABC1234567890";
        string barcodePath = Path.Combine(tempDir, "sample.png");

        // Generate a Code128 barcode image using default sizing
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Simulate real‑time detection by reading the image multiple times
        const int readAttempts = 3;
        for (int attempt = 1; attempt <= readAttempts; attempt++)
        {
            Console.WriteLine($"--- Detection attempt {attempt} ---");

            // Verify the image file exists before attempting to read
            if (!File.Exists(barcodePath))
            {
                Console.WriteLine("Barcode image not found.");
                break;
            }

            // Create a reader for all supported symbologies
            using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
            {
                // Optional: improve performance with a preset quality setting
                reader.QualitySettings = QualitySettings.HighPerformance;

                // Perform the detection
                BarCodeResult[] results = reader.ReadBarCodes();

                // Display the number of barcodes found (FoundBarCodes property)
                Console.WriteLine($"FoundBarCodes: {reader.FoundBarCodes}");

                // Iterate over each detected barcode and output details
                int index = 0;
                foreach (var result in results)
                {
                    Console.WriteLine($"Result #{++index}:");
                    Console.WriteLine($"  CodeText      : {result.CodeText}");
                    Console.WriteLine($"  Symbology     : {result.CodeTypeName}");
                    Console.WriteLine($"  ReadingQuality: {result.ReadingQuality}");
                    var bounds = result.Region.Rectangle;
                    Console.WriteLine($"  Region (X,Y,W,H): {bounds.X}, {bounds.Y}, {bounds.Width}, {bounds.Height}");
                    Console.WriteLine($"  Angle (degrees): {result.Region.Angle}");
                }

                if (results.Length == 0)
                {
                    Console.WriteLine("No barcode detected.");
                }
            }

            Console.WriteLine();
        }

        // Clean up temporary files
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup warning: {ex.Message}");
        }
    }
}