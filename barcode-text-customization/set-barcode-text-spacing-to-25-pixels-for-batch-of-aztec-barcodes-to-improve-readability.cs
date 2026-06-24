using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Aztec barcodes with custom text spacing using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a set of Aztec barcodes and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Define an array of sample texts to encode in the Aztec barcodes.
        string[] codeTexts = new string[]
        {
            "AZTEC001",
            "AZTEC002",
            "AZTEC003",
            "AZTEC004",
            "AZTEC005"
        };

        // Specify the output directory for the generated barcode images.
        string outputDir = "AztecBarcodes";

        // Create the output directory if it does not already exist.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Iterate over each text value, generate a barcode, and save it to a file.
        for (int i = 0; i < codeTexts.Length; i++)
        {
            // Current text to encode.
            string codeText = codeTexts[i];

            // Build the full file path for the output image.
            string outputPath = Path.Combine(outputDir, $"aztec_{i + 1}.png");

            // Initialize the barcode generator with Aztec type and the current text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Aztec, codeText))
            {
                // Set the spacing between the barcode and the human‑readable text to 2.5 pixels.
                generator.Parameters.Barcode.CodeTextParameters.Space.Pixels = 2.5f;

                // Save the generated barcode image to the specified path.
                generator.Save(outputPath);
            }

            // Inform the user that the barcode has been saved.
            Console.WriteLine($"Saved Aztec barcode to: {outputPath}");
        }
    }
}