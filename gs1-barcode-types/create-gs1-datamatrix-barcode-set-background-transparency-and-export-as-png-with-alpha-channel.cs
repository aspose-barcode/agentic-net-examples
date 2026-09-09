// Title: Create GS1 DataMatrix barcode with transparent background and PNG output
// Description: Demonstrates generating a GS1 DataMatrix barcode, applying a transparent background, and saving it as a PNG image that retains the alpha channel.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on image rendering options such as background transparency and format selection. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce high‑quality barcodes for printing or digital display. Developers often need to create barcodes with transparent backgrounds for overlaying on UI elements or documents.
// Prompt: Create a GS1 DataMatrix barcode, set background transparency, and export as PNG with an alpha channel.
// Tags: gs1datamatrix, barcode, background-transparency, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a GS1 DataMatrix barcode with a transparent background and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the output file path to the console.
    /// </summary>
    static void Main()
    {
        // Prepare the output directory and define the full file path for the PNG image
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "gs1datamatrix.png");

        // Define the GS1 DataMatrix code text (example data)
        string codeText = "(01)12345678901231(21)ASPOSE(30)9876";

        // Create a BarcodeGenerator for GS1 DataMatrix with the specified text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Set the background color to transparent to retain the alpha channel in the PNG
            generator.Parameters.BackColor = Aspose.Drawing.Color.Transparent;

            // Optionally adjust the X-dimension (module size) in pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 8;

            // Save the generated barcode as a PNG image preserving transparency
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"GS1 DataMatrix barcode saved to: {outputPath}");
    }
}