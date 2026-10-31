// Title: Align UPC‑A barcode text to the left
// Description: Demonstrates how to generate a series of UPC‑A barcodes with the human‑readable text aligned to the left side of the barcode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to control text alignment. Typical use cases include customizing barcode appearance for retail labels or inventory systems where left‑aligned text is required. Developers often need to adjust text positioning, image format, and output paths when automating barcode creation.
// Prompt: Align barcode text left for a series of UPC‑A barcodes by setting TextAlignment.Left.
// Tags: upc-a, barcode, text alignment, left alignment, generation, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates multiple UPC‑A barcodes with left‑aligned text and saves them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the output directory, generates barcodes, and writes status messages.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the generated barcode images
        string outputDir = Path.Combine(Path.GetTempPath(), "UPCSeries_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define a set of sample UPC‑A codes (each must contain exactly 12 digits)
        string[] upcCodes = new string[]
        {
            "012345678905",
            "036000291452",
            "123456789012",
            "042100005264",
            "070123456789"
        };

        // Iterate over each UPC‑A code, generate a barcode, and save it as a PNG file
        for (int i = 0; i < upcCodes.Length; i++)
        {
            string code = upcCodes[i];
            string filePath = Path.Combine(outputDir, $"UPC_{i + 1}_{code}.png");

            // Initialize the barcode generator with UPC‑A symbology and the current code
            using (var generator = new BarcodeGenerator(EncodeTypes.UPCA, code))
            {
                // Set the human‑readable text alignment to the left side of the barcode
                generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;

                // Save the generated barcode image in PNG format
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Output the location of the saved barcode image
            Console.WriteLine($"Saved UPC-A barcode {i + 1} to: {filePath}");
        }

        // Indicate that all barcodes have been generated successfully
        Console.WriteLine("All barcodes generated successfully.");
    }
}