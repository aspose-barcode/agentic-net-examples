// Title: Generate GS1 Code 128 barcode with custom font and save as PNG
// Description: Demonstrates creating a GS1 Code 128 barcode, applying a custom Helvetica font to the human‑readable text, and saving the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure barcode parameters such as X‑dimension, code‑text location, and custom fonts using the BarcodeGenerator class. Typical use cases include producing GS1‑compliant barcodes for product labeling, inventory systems, and retail applications where a readable text representation is required. Developers often need to customize appearance and export formats, making this pattern a common reference.
// Prompt: Create a GS1 Code 128 barcode, apply a custom font for human‑readable text, and save as PNG.
// Tags: gs1 code128, barcode generation, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a GS1 Code 128 barcode with custom human‑readable font and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures appearance, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "GS1Code128.png");

        // GS1 Code 128 barcode text (Application Identifier 01 with 14‑digit GTIN).
        string codeText = "(01)12345678901231";

        // Initialize the barcode generator for GS1 Code 128 with the specified text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Optional: set the module (X) size to 2 pixels for better resolution.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Position the human‑readable text below the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Apply a custom Helvetica font with a size of 12 points to the human‑readable text.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}