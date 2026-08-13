// Title: Set Caption Color to Purple for UPC-A Barcode
// Description: Demonstrates how to generate a UPC‑A barcode and set its caption color to purple using Color.FromName.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance. It showcases the BarcodeGenerator class, its Parameters property, and the use of System.Drawing.Color for styling captions. Developers often need to modify caption text and colors when integrating barcodes into product packaging, labels, or UI displays.
// Prompt: Use Color.FromName to set caption color to "Purple" for a UPC-A barcode.
// Tags: upc-a, set-caption-color, png, barcodegenerator, parameters, color

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a UPC‑A barcode, sets a custom caption with a purple color, and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, applies caption styling, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file name
        string outputPath = "upc_a.png";

        // Initialize the barcode generator for UPC‑A with a valid 12‑digit value (including check digit)
        using (var generator = new BarcodeGenerator(EncodeTypes.UPCA, "012345678905"))
        {
            // Set the caption text that appears above the barcode
            generator.Parameters.CaptionAbove.Text = "Sample UPC‑A";

            // Apply the purple color to the caption using Color.FromName
            generator.Parameters.CaptionAbove.TextColor = Color.FromName("Purple");

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the full path of the saved barcode image
        Console.WriteLine($"Barcode saved to: {Path.GetFullPath(outputPath)}");
    }
}