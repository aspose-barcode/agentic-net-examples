// Title: Generate Code128 barcode with 30% width reduction and verify readability
// Description: This example creates a Code128 barcode where the bar width is reduced by 30 percent, saves it as a PNG file, and then reads it back to confirm that scanners can still decode the symbol.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition APIs. It uses BarcodeGenerator to customize bar dimensions (XDimension and BarWidthReduction) and BarCodeReader to validate the output. Typical for developers who need to fine‑tune barcode appearance while ensuring scan reliability, such as packaging, labeling, or inventory systems.
// Prompt: Create a barcode with width reduction set to 30 percent and verify scanner readability.
// Tags: barcode, code128, width reduction, generation, recognition, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates creating a Code128 barcode with a 30 percent width reduction
/// and then verifying that the barcode can be read by a scanner.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, and validates readability.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output directory
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define file path and barcode content
        string barcodePath = Path.Combine(outputDir, "Code128_WidthReduction30.png");
        string codeText = "ASPOSE";

        // --------------------------------------------------------------------
        // Generate barcode with 30% width reduction (XDimension = 10 px, reduction = 3 px)
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 10;               // Base module width
            generator.Parameters.Barcode.BarWidthReduction.Pixels = 3;        // 30% of 10 px
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {barcodePath}");

        // --------------------------------------------------------------------
        // Verify readability using BarCodeReader
        // --------------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Generated barcode file not found.");
            return;
        }

        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            var results = reader.ReadBarCodes();
            if (results != null && results.Length > 0)
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"Decoded Text: {result.CodeText}");
                    Console.WriteLine($"Symbology: {result.CodeTypeName}");
                }
            }
            else
            {
                Console.WriteLine("No barcode detected or unreadable.");
            }
        }
    }
}