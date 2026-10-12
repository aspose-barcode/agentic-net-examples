// Title: Generate Code128 barcode with width reduction and verify readability
// Description: Demonstrates creating a Code128 barcode with a 5 percent width reduction and testing its readability on low‑resolution mobile scanners.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator to customize bar dimensions (e.g., width reduction) and BarCodeReader to validate the resulting image. Developers working with barcode printing, mobile scanning, or inventory systems often need to fine‑tune barcode appearance for specific scanner capabilities, making this pattern a common requirement.
// Prompt: Create a barcode with width reduction set to 5 percent and test readability on low‑resolution mobile scanners.
// Tags: code128, width-reduction, barcode-generation, barcode-recognition, png, aspose.barcode, low-resolution-scanner

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode with a width reduction
/// and validates its readability using Aspose.BarCode's recognition engine.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, saves it, and attempts to read it back.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare output directory and file path
        // ------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }
        string barcodePath = Path.Combine(outputDir, "BarcodeWidthReduction.png");

        // ------------------------------------------------------------
        // Generate a Code128 barcode with a 5% width reduction
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set X dimension (module width) for better visibility
            generator.Parameters.Barcode.XDimension.Pixels = 10f;

            // Apply bar width reduction (5 points ≈ 5% of typical bar width)
            generator.Parameters.Barcode.BarWidthReduction.Point = 5f;

            // Save the generated barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {barcodePath}");

        // ------------------------------------------------------------
        // Verify that the saved barcode can be read
        // ------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Barcode file not found. Exiting.");
            return;
        }

        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Use high‑performance quality settings for faster scanning
            reader.QualitySettings = QualitySettings.HighPerformance;

            bool found = false;
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
                Console.WriteLine($"Detected CodeType: {result.CodeTypeName}");
                found = true;
            }

            if (!found)
            {
                Console.WriteLine("No barcode detected. It may be unreadable on low‑resolution scanners.");
            }
        }
    }
}