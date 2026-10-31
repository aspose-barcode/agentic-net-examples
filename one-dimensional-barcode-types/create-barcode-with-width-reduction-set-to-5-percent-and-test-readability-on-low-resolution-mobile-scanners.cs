// Title: Generate Code128 barcode with 5% width reduction and test low‑resolution readability
// Description: This example creates a Code128 barcode, applies a 5 percent bar width reduction, saves it as a PNG image, and verifies that it can be read using a low‑resolution (72 dpi) scanner simulation.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and recognition. The example uses BarcodeGenerator to configure barcode appearance (resolution, XDimension, BarWidthReduction) and BarCodeReader to decode the image. Typical use cases include preparing barcodes for mobile devices, low‑resolution scanners, and testing readability under constrained conditions. Developers often need to adjust module size, resolution, and width reduction to meet specific printing or scanning requirements.
// Prompt: Create a barcode with width reduction set to 5 percent and test readability on low‑resolution mobile scanners.
// Tags: code128, width-reduction, low-resolution, barcode-generation, barcode-recognition, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation with width reduction and low‑resolution readability testing using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, saves it, reads it back with low‑resolution settings, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare temporary output directory and file path
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string barcodePath = Path.Combine(outputDir, "barcode.png");

        // --------------------------------------------------------------------
        // Generate a Code128 barcode with 5% bar width reduction
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Simulate a low‑resolution mobile scanner (e.g., 72 DPI)
            generator.Parameters.Resolution = 72f;

            // Set module size (XDimension) – arbitrary pixel value
            generator.Parameters.Barcode.XDimension.Pixels = 10f;

            // Apply a 5% bar width reduction (interpreted as 5 points)
            generator.Parameters.Barcode.BarWidthReduction.Point = 5f;

            // Save the generated barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Verify that the barcode image was successfully created
        // --------------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Read the barcode using low‑resolution settings to test readability
        // --------------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.Code128;
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            // Use a high‑performance quality preset for faster decoding
            reader.QualitySettings = QualitySettings.HighPerformance;

            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Detected CodeText: {result.CodeText}");
                    Console.WriteLine($"Detected Symbology: {result.CodeTypeName}");
                    Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                }
            }
        }

        // --------------------------------------------------------------------
        // Clean up generated files (optional)
        // --------------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(outputDir);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}