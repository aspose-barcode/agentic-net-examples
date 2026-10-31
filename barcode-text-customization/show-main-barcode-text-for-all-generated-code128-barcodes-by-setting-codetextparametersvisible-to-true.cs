// Title: Generate Code128 Barcodes with Visible Text
// Description: Demonstrates creating Code128 barcodes, placing the human‑readable text below the bars, and saving them as PNG files.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce barcodes for labeling, inventory, and shipping. Developers often need to render barcodes with readable text for scanning devices and visual verification, and this snippet illustrates typical configuration steps.
// Prompt: Show main barcode text for all generated Code128 barcodes by setting CodetextParameters.Visible to true.
// Tags: code128, barcode, generation, png, aspose.barcode, codetext, visible, textlocation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a set of Code128 barcodes, displays the main barcode text below each barcode,
/// and saves the images as PNG files in a temporary directory.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates barcodes for predefined texts, configures text visibility,
    /// and writes the resulting images to disk.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the output images
        string outputDir = Path.Combine(Path.GetTempPath(), "Code128Demo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Sample texts to encode into Code128 barcodes
        string[] sampleTexts = { "ABC123", "9876543210", "HelloWorld" };

        // Iterate over each sample text and generate a corresponding barcode image
        for (int i = 0; i < sampleTexts.Length; i++)
        {
            string text = sampleTexts[i];
            string filePath = Path.Combine(outputDir, $"Code128_{i + 1}.png");

            // Initialize the barcode generator with Code128 symbology and the current text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Position the human‑readable text below the barcode bars
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

                // Optional: increase the spacing between the barcode and the text for better readability
                generator.Parameters.Barcode.CodeTextParameters.Space.Point = 5f;

                // Save the generated barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Output the location of the saved image
            Console.WriteLine($"Generated barcode saved to: {filePath}");
        }

        // Indicate that all barcodes have been generated successfully
        Console.WriteLine("All Code128 barcodes generated successfully.");
    }
}