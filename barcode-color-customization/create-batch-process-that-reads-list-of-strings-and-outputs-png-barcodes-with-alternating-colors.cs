// Title: Batch Generation of PNG Barcodes with Alternating Colors
// Description: Demonstrates how to generate a series of Code128 barcodes from a list of strings, saving each as a PNG file with alternating black and red colors.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and barcode parameter settings. Typical use cases include bulk barcode creation for inventory, shipping labels, or product catalogs where visual differentiation (e.g., alternating colors) is desired. Developers often need to automate barcode output to image files while customizing appearance via the Parameters API.
// Prompt: Create a batch process that reads a list of strings and outputs PNG barcodes with alternating colors.
// Tags: barcode symbology, generation, png, color, aspose.barcode, batch processing

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch creation of PNG barcodes with alternating colors using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates Code128 barcodes from a predefined list,
    /// saves each as a PNG file, and alternates the bar color between black and red.
    /// </summary>
    static void Main()
    {
        // Define a sample list of strings to encode as barcodes.
        List<string> codes = new List<string>
        {
            "Sample001",
            "Sample002",
            "Sample003",
            "Sample004",
            "Sample005"
        };

        // Set up the output directory for generated barcode images.
        string outputDir = "Barcodes";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Iterate over each code, generate a barcode, and save it with the appropriate color.
        for (int i = 0; i < codes.Count; i++)
        {
            string codeText = codes[i];
            string filePath = Path.Combine(outputDir, $"barcode_{i + 1}.png");

            // Determine bar color: even index → Black, odd index → Red.
            Aspose.Drawing.Color barColor = (i % 2 == 0) ? Aspose.Drawing.Color.Black : Aspose.Drawing.Color.Red;

            // Create and configure the barcode generator.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = codeText;                     // Set the text to encode.
                generator.Parameters.Barcode.BarColor = barColor; // Apply the selected bar color.
                generator.Save(filePath);                          // Save the barcode as a PNG file.
            }

            // Log the generation result to the console.
            Console.WriteLine($"Generated barcode for \"{codeText}\" at \"{filePath}\" with color {(barColor == Aspose.Drawing.Color.Black ? "Black" : "Red")}");
        }
    }
}