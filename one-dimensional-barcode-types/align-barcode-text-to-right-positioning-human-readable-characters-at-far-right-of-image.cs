// Title: Align barcode text to the right
// Description: Demonstrates how to right‑align human‑readable text for a Code128 barcode and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to control barcode appearance. Developers often need to adjust text alignment, location, and output format when integrating barcodes into documents, labels, or UI components. The snippet shows typical steps: creating a generator, configuring CodeTextParameters, and saving the result.
// Prompt: Align barcode text to the right, positioning human‑readable characters at the far right of the image.
// Tags: code128, text-alignment, png, barcodelibrary, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Code128 barcode with right‑aligned human‑readable text and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output directory, configures the barcode generator,
    /// aligns the text to the right, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Build a unique temporary folder for the output image.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeRightAlign_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the full path for the resulting PNG file.
        string outputPath = Path.Combine(outputDir, "barcode_right.png");

        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Align the human‑readable text to the right side of the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;

            // Position the text below the barcode bars.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine("Barcode saved to: " + outputPath);
    }
}