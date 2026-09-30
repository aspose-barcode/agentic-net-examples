// Title: Set caption color to Purple for a UPC-A barcode using Aspose.BarCode
// Description: Demonstrates how to generate a UPC-A barcode and change the caption text color to purple using Color.FromName.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and caption parameters to customize barcode appearance. Typical use cases include creating product barcodes with styled captions for packaging or inventory systems. Developers often need to adjust caption text, font, and color to match branding guidelines, and this snippet shows the essential API calls.
// Prompt: Use Color.FromName to set caption color to "Purple" for a UPC-A barcode.
// Tags: upc-a, barcode, caption, color, fromname, aspnet, aspose.barcode, png, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a UPC-A barcode with a purple caption using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, sets caption color, and saves the image.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for the generated image
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Define the full file path where the PNG barcode will be saved
        string outputPath = Path.Combine(outputDir, "upc_a.png");

        // Initialize the barcode generator for UPC-A symbology with a 12‑digit sample value
        using (var generator = new BarcodeGenerator(EncodeTypes.UPCA, "123456789012"))
        {
            // Add a caption above the barcode
            generator.Parameters.CaptionAbove.Text = "Sample UPC-A";

            // Set the caption text color to purple using Color.FromName
            generator.Parameters.CaptionAbove.TextColor = Color.FromName("Purple");

            // Render and save the barcode image in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}