// Title: Display Human-Readable Text for Code128 Barcodes
// Description: Generates several Code128 barcodes and saves them as PNG files with the barcode text shown below the bars.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to control human‑readable text visibility using the CodeTextParameters class. Developers commonly need to render barcodes with accompanying text for scanning verification, packaging labels, or inventory systems. The key API classes demonstrated are BarcodeGenerator, EncodeTypes, and CodeTextParameters, which are frequently used in batch barcode creation scenarios.
// Prompt: Show main barcode text for all generated Code128 barcodes by setting CodetextParameters.Visible to true.
// Tags: code128, barcode, text-visibility, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate Code128 barcodes with visible human‑readable text using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates sample Code128 barcodes, saves them as PNG images, and writes the output paths to the console.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for output images
        string outputFolder = Path.Combine(Path.GetTempPath(), "Code128Demo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Sample Code128 texts to encode
        string[] texts = { "ABC123", "9876543210", "CODE128-EXAMPLE" };

        // Iterate over each sample text and generate a barcode
        foreach (string text in texts)
        {
            // Initialize the barcode generator for Code128 with the current text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Set the human‑readable text location to Below the barcode.
                // This makes the main barcode text visible in the generated image.
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

                // Build the full file path for the PNG output
                string filePath = Path.Combine(outputFolder, $"Code128_{text}.png");

                // Save the barcode image in PNG format
                generator.Save(filePath, BarCodeImageFormat.Png);

                // Inform the user where the file was saved
                Console.WriteLine($"Generated barcode saved to: {filePath}");
            }
        }

        // Explain why CodetextParameters.Visible is not used
        Console.WriteLine("Note: CodetextParameters.Visible is not a supported property in the current Aspose.BarCode API. Text visibility is controlled via CodeTextParameters.Location.");
    }
}