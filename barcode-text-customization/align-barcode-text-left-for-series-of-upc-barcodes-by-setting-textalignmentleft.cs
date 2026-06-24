using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating UPC‑A barcodes using Aspose.BarCode and saving them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a set of UPC‑A barcodes and writes them to the "Barcodes" folder.
    /// </summary>
    static void Main()
    {
        // Define a collection of sample UPC‑A codes (12 digits each, including checksum).
        string[] upcCodes = new string[]
        {
            "012345678905",
            "123456789012",
            "036000291452",
            "042100005264",
            "070123456789"
        };

        // Determine the output directory and ensure it exists.
        string outputDir = "Barcodes";
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not already exist.
            Directory.CreateDirectory(outputDir);
        }

        // Iterate over each UPC code, generate a barcode, and save it as a PNG file.
        for (int i = 0; i < upcCodes.Length; i++)
        {
            string code = upcCodes[i];

            // Initialize a BarcodeGenerator for the UPC‑A format with the current code.
            using (var generator = new BarcodeGenerator(EncodeTypes.UPCA, code))
            {
                // Align the human‑readable text to the left side of the barcode.
                generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;

                // Build the full file path for the output image.
                string filePath = Path.Combine(outputDir, $"upc_{i + 1}.png");

                // Save the generated barcode image to the specified path.
                generator.Save(filePath);

                // Output a confirmation message to the console.
                Console.WriteLine($"Saved barcode {code} to {filePath}");
            }
        }
    }
}