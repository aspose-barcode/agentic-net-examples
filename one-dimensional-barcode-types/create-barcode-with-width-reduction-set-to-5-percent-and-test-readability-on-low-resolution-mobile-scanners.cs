// Title: Generate Code128 barcode and verify readability on low‑resolution scanners
// Description: This example creates a Code128 barcode image at 72 dpi to simulate a low‑resolution mobile scanner capture, saves it as PNG, and then attempts to read it back using Aspose.BarCode APIs.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and recognition workflows. It showcases the use of BarcodeGenerator for creating barcodes, configuring image resolution, and BarCodeReader for decoding. Typical use cases include preparing barcodes for mobile applications, testing scanner compatibility, and validating image quality. Developers often need to adjust generation parameters and verify readability across devices.
// Prompt: Create a barcode with width reduction set to 5 percent and test readability on low‑resolution mobile scanners.
// Tags: code128, barcode generation, barcode recognition, low resolution, width reduction, aspose.barcode, png, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode creation with low resolution and subsequent readability verification.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, saves it, and validates its readability.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary output directory
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string barcodePath = Path.Combine(outputDir, "barcode.png");

        // Generate a Code128 barcode with low resolution (72 dpi) to simulate a mobile scanner capture
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ASPOSE"))
        {
            // Set image resolution; lower DPI mimics low‑resolution scanner output
            generator.Parameters.Resolution = 72f;

            // Save the generated barcode as a PNG file
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {barcodePath}");

        // Verify that the barcode image file was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Initialize a reader for Code128 barcodes
        BaseDecodeType decodeType = DecodeType.Code128;
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            // Attempt to read all barcodes from the image
            var results = reader.ReadBarCodes();
            if (results != null && results.Length > 0)
            {
                // Output details of each decoded barcode
                foreach (var result in results)
                {
                    Console.WriteLine($"Decoded Text: {result.CodeText}");
                    Console.WriteLine($"Symbology: {result.CodeTypeName}");
                    Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                }
            }
            else
            {
                // Inform the user if the barcode could not be read
                Console.WriteLine("Barcode could not be read. It may be unreadable on low‑resolution scanners.");
            }
        }
    }
}