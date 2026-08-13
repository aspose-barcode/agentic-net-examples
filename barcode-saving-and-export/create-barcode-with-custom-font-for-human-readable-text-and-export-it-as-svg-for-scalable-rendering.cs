// Title: Create Code39 barcode with custom font and export as SVG
// Description: Demonstrates how to generate a Code39 barcode, apply a custom font to the human‑readable text, and save it as an SVG file for scalable rendering.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical use cases include creating barcodes with styled captions and exporting them to vector formats for web or print. Developers often need to customize appearance and choose scalable output formats like SVG.
// Prompt: Create a barcode with custom font for human‑readable text and export it as SVG for scalable rendering.
// Tags: code39, custom font, svg, barcode generation, aspose.barcode, vector output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a Code39 barcode with a custom font for the human‑readable text and exporting it as an SVG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the SVG image
        string outputPath = "barcode.svg";

        // Resolve the full directory path and ensure it exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Initialize a BarcodeGenerator for Code39 with the desired data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, "12345"))
        {
            // Set the barcode's foreground (bars) and background colors
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Customize the font of the human‑readable (code text) portion
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Optionally adjust the module (X) dimension for finer control over size
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Attempt to save the barcode as an SVG file; handle evaluation‑license restrictions
            try
            {
                generator.Save(outputPath, BarCodeImageFormat.Svg);
                Console.WriteLine($"Barcode saved to {outputPath}");
            }
            catch (Exception ex) when (ex.Message.Contains("evaluation"))
            {
                Console.WriteLine("SVG export requires a full license for this symbology.");
                Console.WriteLine(ex.Message);
            }
        }
    }
}