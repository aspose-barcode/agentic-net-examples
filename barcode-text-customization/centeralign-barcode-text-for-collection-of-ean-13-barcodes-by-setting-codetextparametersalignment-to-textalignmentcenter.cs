// Title: Center-align text for EAN‑13 barcodes using Aspose.BarCode
// Description: Demonstrates how to generate a set of EAN‑13 barcodes with the human‑readable text centered beneath each barcode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodetextParameters to control text layout. Developers often need to customize the appearance of barcode captions for printing labels, receipts, or packaging, and this snippet shows the typical steps: creating a generator, configuring X‑dimension, setting TextAlignment, and saving to an image format.
// Prompt: Center-align barcode text for a collection of EAN‑13 barcodes by setting CodetextParameters.Alignment to TextAlignment.Center.
// Tags: ean-13, barcode, text-alignment, center, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates multiple EAN‑13 barcodes with centered human‑readable text and saves them as PNG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates an output folder, validates EAN‑13 codes, configures barcode generation,
    /// and writes the resulting images to disk.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the generated barcode images.
        string outputDir = Path.Combine(Path.GetTempPath(), "Ean13Center_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Sample set of EAN‑13 codes to be rendered.
        string[] ean13Codes = new string[]
        {
            "1234567890128",
            "4006381333931",
            "5901234123457",
            "9780306406157",
            "73513537" // shorter example will cause checksum error, so use full 13-digit
        };

        // Iterate through each code, validate length, and generate the barcode image.
        for (int i = 0; i < ean13Codes.Length; i++)
        {
            string code = ean13Codes[i];

            // Ensure the code is exactly 13 digits; otherwise, skip it with a warning.
            if (code.Length != 13)
            {
                Console.WriteLine($"Skipping invalid EAN-13 code (must be 13 digits): {code}");
                continue;
            }

            // Build the full file path for the output PNG image.
            string filePath = Path.Combine(outputDir, $"Ean13_{i + 1}.png");

            // Initialize the barcode generator with the EAN‑13 symbology and the current code.
            using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, code))
            {
                // Center-align the human‑readable text beneath the barcode.
                generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

                // Optional: increase the module (bar) size for better visual clarity.
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Save the generated barcode as a PNG file.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Generated barcode saved to: {filePath}");
        }

        Console.WriteLine("Processing completed.");
    }
}