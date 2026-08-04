// Title: Generate GS1 Code 128 barcode with AI data and save as PNG
// Description: Demonstrates creating a GS1 Code 128 barcode that includes Application Identifiers (AI) for GTIN‑14 and batch/lot number, then saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.GS1Code128 to embed AI data in a 1D barcode. Typical use cases include product labeling, inventory tracking, and compliance with GS1 standards. Developers often need to configure visual parameters such as X‑dimension, bar height, and colors before exporting the barcode to common image formats.
// Prompt: Generate a GS1 Code 128 barcode with AI data and save as a PNG image file.
// Tags: gs1, code128, barcode, generation, png, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a GS1 Code 128 barcode containing AI data
/// and saves it as a PNG image file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures visual settings,
    /// and writes the result to "gs1code128.png".
    /// </summary>
    static void Main()
    {
        // Define GS1 data string with Application Identifiers:
        // (01) – GTIN‑14, (10) – Batch/Lot number
        string codeText = "(01)00123456789012(10)ABC123";

        // Initialize the barcode generator for GS1 Code 128 with the data string
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Configure visual appearance of the barcode
            generator.Parameters.Barcode.XDimension.Point = 2f;          // Module (X) size in points
            generator.Parameters.Barcode.BarHeight.Point = 100f;        // Height of the bars for 1D barcode
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black; // Bar color
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;        // Background color

            // Save the generated barcode as a PNG image file
            generator.Save("gs1code128.png");
        }
    }
}