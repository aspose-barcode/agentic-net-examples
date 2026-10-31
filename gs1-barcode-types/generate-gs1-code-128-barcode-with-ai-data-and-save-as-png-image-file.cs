// Title: Generate GS1 Code 128 barcode with AI data and save as PNG
// Description: Demonstrates creating a GS1 Code 128 barcode that includes Application Identifiers (AI) for GTIN‑14 and serial number, then saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.GS1Code128 to embed AI data. Typical use cases include product labeling, inventory tracking, and compliance with GS1 standards. Developers often need to configure barcode dimensions and export the image in common formats such as PNG.
// Prompt: Generate a GS1 Code 128 barcode with AI data and save as a PNG image file.
// Tags: gs1,code128,barcode,generation,png,aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a GS1 Code 128 barcode containing AI data and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates output folder, generates the barcode, and writes the result path to the console.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);
        // Full path for the resulting PNG image.
        string outPath = Path.Combine(outputDir, "GS1Code128.png");

        // GS1 Code 128 barcode text with AI (01) GTIN‑14 and AI (21) Serial Number.
        string codeText = "(01)12345678901231(21)ASPOSE";

        // Initialize the barcode generator with GS1 Code 128 symbology and the specified code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Set the X-dimension (module width) to 2 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            // Save the generated barcode as a PNG image.
            generator.Save(outPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"GS1 Code 128 barcode saved to: {outPath}");
    }
}