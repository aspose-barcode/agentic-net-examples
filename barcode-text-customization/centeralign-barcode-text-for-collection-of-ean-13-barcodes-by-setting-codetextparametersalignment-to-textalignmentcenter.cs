// Title: Center-align text for EAN‑13 barcodes using Aspose.BarCode
// Description: Demonstrates how to generate EAN‑13 barcodes with the human‑readable text centered beneath the bars.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodetextParameters to control barcode appearance. Typical use cases include creating product labels or inventory tags where the text must be aligned for readability. Developers often need to adjust X‑dimension, text alignment, and output format when producing batches of barcodes.
// Prompt: Center-align barcode text for a collection of EAN‑13 barcodes by setting CodetextParameters.Alignment to TextAlignment.Center.
// Tags: ean-13, barcode, text-alignment, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a set of EAN‑13 barcodes with centered human‑readable text and saves them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates an output folder, iterates over sample EAN‑13 codes,
    /// configures barcode generation parameters, and writes each barcode image to disk.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the generated barcode images.
        string outputDir = Path.Combine(Path.GetTempPath(), "Ean13Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Sample EAN‑13 codes to be encoded.
        string[] ean13Codes = new string[]
        {
            "1234567890128",
            "4006381333931",
            "5901234123457"
        };

        int index = 1;
        // Generate a barcode for each code in the collection.
        foreach (string code in ean13Codes)
        {
            // Initialize the generator with the EAN‑13 symbology and the current code.
            using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, code))
            {
                // Set the module (X) dimension to 2 pixels for better visual quality.
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Center‑align the human‑readable text beneath the barcode.
                generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

                // Build the full file path for the PNG image.
                string filePath = Path.Combine(outputDir, $"Ean13_{index}.png");

                // Save the generated barcode image to the specified path.
                generator.Save(filePath, BarCodeImageFormat.Png);

                // Output a confirmation message to the console.
                Console.WriteLine($"Saved barcode {code} to {filePath}");
            }
            index++;
        }
    }
}