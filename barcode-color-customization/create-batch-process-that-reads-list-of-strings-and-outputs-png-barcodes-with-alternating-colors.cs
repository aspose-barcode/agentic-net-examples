// Title: Batch barcode generation with alternating colors
// Description: Demonstrates creating PNG barcodes from a list of strings, alternating bar colors for visual distinction.
// Prompt: Create a batch process that reads a list of strings and outputs PNG barcodes with alternating colors.
// Tags: barcode symbology, batch processing, png output, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates PNG barcodes for a predefined list of strings, alternating bar colors between blue and red.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Iterates over sample values, creates a barcode for each, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define a sample list of strings to encode as barcodes
        string[] values = new string[]
        {
            "Sample001",
            "Sample002",
            "Sample003",
            "Sample004",
            "Sample005"
        };

        // Determine the output directory (current working folder)
        string outputDir = Directory.GetCurrentDirectory();

        // Process each string in the list
        for (int i = 0; i < values.Length; i++)
        {
            // Choose bar color: even index -> Blue, odd index -> Red
            Aspose.Drawing.Color barColor = (i % 2 == 0) ? Aspose.Drawing.Color.Blue : Aspose.Drawing.Color.Red;

            // Initialize the barcode generator for Code128 symbology with the current value
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, values[i]))
            {
                // Apply the selected bar color to the barcode
                generator.Parameters.Barcode.BarColor = barColor;

                // Optional: customize image dimensions if required
                // generator.Parameters.ImageWidth.Point = 300f;
                // generator.Parameters.ImageHeight.Point = 150f;

                // Construct a unique file name for the output PNG
                string fileName = $"barcode_{i + 1}.png";
                string filePath = Path.Combine(outputDir, fileName);

                // Save the generated barcode image as a PNG file
                generator.Save(filePath);
            }

            // Inform the user that the barcode has been generated
            Console.WriteLine($"Generated barcode for \"{values[i]}\" as {i + 1}.png");
        }
    }
}