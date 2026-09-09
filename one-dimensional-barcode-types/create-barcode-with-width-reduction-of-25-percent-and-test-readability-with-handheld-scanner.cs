// Title: Create Code128 barcode with 25% width reduction and verify readability
// Description: Demonstrates generating a Code128 barcode image with a 25 percent width reduction and then reading it back to confirm it can be scanned.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator to customize barcode dimensions (XDimension and BarWidthReduction) and BarCodeReader to decode the image. Typical use cases include preparing barcodes for limited space labels and validating that the produced barcodes are readable by handheld scanners. Developers often need to adjust module size and width reduction while ensuring compatibility with scanning devices.
// Prompt: Create a barcode with width reduction of 25 percent and test readability with a handheld scanner.
// Tags: code128, width reduction, barcode generation, barcode recognition, handheld scanner, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a Code128 barcode with a 25 percent width reduction,
/// saves it as a PNG file, and then verifies that it can be read by a barcode scanner.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, and validates readability.
    /// </summary>
    static void Main()
    {
        // Define output directory in the temporary folder and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated barcode image
        string barcodePath = Path.Combine(outputDir, "barcode.png");

        // ------------------------------------------------------------
        // Generate a Code128 barcode with a 25% width reduction
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Base module size (X-dimension) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 10f;

            // Reduce the barcode width by 25% of the X-dimension (10 * 0.25 = 2.5)
            generator.Parameters.Barcode.BarWidthReduction.Pixels = 2.5f;

            // Save the barcode image as PNG
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Verify that the generated barcode image exists
        // ------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode back using BarCodeReader to test scanner readability
        // ------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.Code128;
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            var results = reader.ReadBarCodes();

            // Check if any barcode was successfully decoded
            if (results != null && results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText))
            {
                Console.WriteLine("Barcode read successfully.");
                Console.WriteLine($"Decoded text: {results[0].CodeText}");
                Console.WriteLine($"Symbology: {results[0].CodeTypeName}");
            }
            else
            {
                Console.WriteLine("Failed to read barcode.");
            }
        }
    }
}