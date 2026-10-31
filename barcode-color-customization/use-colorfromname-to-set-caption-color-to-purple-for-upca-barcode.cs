// Title: Generate UPC-A Barcode with Purple Caption Using Aspose.BarCode
// Description: Demonstrates how to create a UPC-A barcode image and set the caption color to purple using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode captions. It uses the BarcodeGenerator class with EncodeTypes, modifies CaptionAbove parameters, and saves the result as PNG. Developers often need to adjust caption visibility, text, and color when generating barcodes for product labeling or inventory systems.
// Prompt: Use Color.FromName to set caption color to "Purple" for a UPC-A barcode.
// Tags: upc-a, barcode, caption, color, generation, png, aspose.barcode, c#, aspnet

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a UPC-A barcode with a purple caption using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a temporary output folder, generates the barcode,
    /// customizes the caption, saves the image, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary directory to store the generated barcode image.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full file path for the resulting PNG image.
        string outputPath = Path.Combine(outputDir, "upc_a_purple_caption.png");

        // Initialize the barcode generator for UPC-A symbology with the specified data.
        using (var generator = new BarcodeGenerator(EncodeTypes.UPCA, "123456789012"))
        {
            // Enable the caption above the barcode.
            generator.Parameters.CaptionAbove.Visible = true;

            // Set the caption text.
            generator.Parameters.CaptionAbove.Text = "Sample Caption";

            // Set the caption text color to purple using Color.FromName.
            generator.Parameters.CaptionAbove.TextColor = Color.FromName("Purple");

            // Save the barcode image as PNG to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}