// Title: Generate GS1 Code 128 barcode with custom human‑readable text and save as JPEG
// Description: This example creates a GS1 Code 128 barcode, adds readable text below the bars, and writes the image to a JPEG file.
// Category-Description: Demonstrates Aspose.BarCode barcode generation for GS1 Code 128 symbology. Shows how to configure barcode parameters such as X‑dimension, code‑text placement, font settings, resolution, and anti‑aliasing, then save the result using BarCodeImageFormat. Useful for developers needing to produce GS1‑compliant barcodes for product labeling, inventory, or logistics.
// Prompt: Generate a GS1 Code 128 barcode, embed custom human‑readable text below, and save as JPEG.
// Tags: gs1,code128,barcode,generation,jpeg,human‑readable text,aspose.barcode,aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a GS1 Code 128 barcode, adds custom human‑readable text,
/// and saves the result as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and writes it to a temporary JPEG file.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output JPEG file in the system's temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "Gs1Code128.jpg");

        // Ensure the output directory exists; create it if necessary.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // GS1 Code 128 data string (Application Identifier 01 with a 14‑digit GTIN).
        string gs1Code = "(01)12345678901231";

        // Initialize the barcode generator for GS1 Code 128 using the provided data.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, gs1Code))
        {
            // Set the module (X‑dimension) size to 2 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Position the human‑readable text below the barcode and configure its font.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Reduce the image resolution to 72 DPI to keep the JPEG file size small.
            generator.Parameters.Resolution = 72f;
            // Disable anti‑aliasing for a sharper barcode edge in the JPEG output.
            generator.Parameters.UseAntiAlias = false;

            // Save the generated barcode as a JPEG file at the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"GS1 Code 128 barcode saved to: {outputPath}");
    }
}