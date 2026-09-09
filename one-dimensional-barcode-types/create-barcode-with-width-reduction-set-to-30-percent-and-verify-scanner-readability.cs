// Title: Generate Code128 barcode with 30% bar width reduction and verify readability
// Description: Demonstrates how to create a Code128 barcode with a 30 percent bar‑width reduction using Aspose.BarCode, save it as PNG, and confirm that a scanner can decode it.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows usage of BarcodeGenerator for customizing bar dimensions (XDimension and BarWidthReduction) and BarCodeReader for validating the generated image. Developers often need to adjust bar width for printing constraints while ensuring scanner compatibility, making this pattern common in packaging, inventory, and label‑printing solutions.
// Prompt: Create a barcode with width reduction set to 30 percent and verify scanner readability.
// Tags: code128, bar width reduction, barcode generation, barcode recognition, png, aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation with bar‑width reduction and subsequent readability verification.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode with 30 % width reduction, saves it, and checks readability using BarCodeReader.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary output directory
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(outputDir, "Code128_BarWidthReduction30.png");

        // Generate a Code128 barcode with a 30% bar width reduction
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ASPOSE"))
        {
            // Set the base module (X) size in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 10f;

            // Apply a 30% reduction to the bar width (10 * 0.3 = 3 pixels)
            generator.Parameters.Barcode.BarWidthReduction.Pixels = 3f;

            // Save the barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {barcodePath}");

        // Verify that the generated file exists before attempting to read it
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Generated barcode file not found.");
            return;
        }

        // Set the expected decode type for the reader
        BaseDecodeType decodeType = DecodeType.Code128;

        // Use BarCodeReader to attempt decoding the saved image
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            var results = reader.ReadBarCodes();

            // If decoding succeeded, output each decoded text value
            if (results != null && results.Length > 0)
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"Decoded Text: {result.GetCodeText(Encoding.UTF8)}");
                }
            }
            else
            {
                Console.WriteLine("No barcode detected or unreadable.");
            }
        }
    }
}