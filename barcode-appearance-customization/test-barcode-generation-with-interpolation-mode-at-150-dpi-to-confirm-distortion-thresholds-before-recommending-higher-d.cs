// Title: Generate Code128 barcode at 150 dpi with Interpolation mode
// Description: Demonstrates generating a Code128 barcode image using Aspose.BarCode with interpolation auto‑size mode at 150 dpi, then reads it back to verify readability.
// Category-Description: This example belongs to the barcode generation and recognition category of Aspose.BarCode. It showcases the use of BarcodeGenerator for creating images with specific resolution and AutoSizeMode, and BarCodeReader for decoding. Developers often need to adjust DPI and interpolation settings to balance image size, quality, and scanner compatibility, making this pattern useful for testing and optimization.
// Prompt: Test barcode generation with Interpolation mode at 150 dpi to confirm distortion thresholds before recommending higher DPI.
// Tags: code128, barcode generation, barcode recognition, interpolation, dpi, aspose.barcode, png, image, resolution

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation at 150 dpi using interpolation mode and verifies readability.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// Generates a barcode, saves it, and reads it back to confirm detection.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for the generated barcode image
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeTest");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define the full path for the barcode image file
        string barcodePath = Path.Combine(outputDir, "barcode_150dpi.png");

        // Generate a Code128 barcode using interpolation auto‑size mode at 150 dpi
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test150DPI"))
        {
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation; // Enable interpolation scaling
            generator.Parameters.Resolution = 150f;                         // Set image resolution to 150 dpi
            generator.Parameters.ImageWidth.Pixels = 300f;                 // Desired image width
            generator.Parameters.ImageHeight.Pixels = 150f;                // Desired image height
            generator.Parameters.Barcode.XDimension.Pixels = 2f;           // Module (X) size
            generator.Save(barcodePath, BarCodeImageFormat.Png);           // Save as PNG
        }

        Console.WriteLine($"Barcode saved to: {barcodePath}");

        // Verify that the barcode file was created before attempting to read it
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Generated barcode file not found.");
            return;
        }

        // Set the decode type to match the generated barcode symbology
        BaseDecodeType decodeType = DecodeType.Code128;

        // Read and decode the generated barcode image
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            var results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected CodeText: {result.CodeText}");
                    Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                }
            }
        }

        Console.WriteLine("Test completed.");
    }
}