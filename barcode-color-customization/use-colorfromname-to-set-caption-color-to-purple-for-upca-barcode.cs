// Title: Generate a UPC-A barcode with a purple caption using Aspose.BarCode
// Description: Demonstrates how to create a UPC-A barcode image and set the caption color to purple via Color.FromName.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and caption parameters. Developers often need to customize barcode appearance, such as adding captions with specific colors, for product labeling and inventory systems. The snippet shows typical steps: initializing the generator, configuring caption visibility, text, and color, and saving the image.
// Prompt: Use Color.FromName to set caption color to "Purple" for a UPC-A barcode.
// Tags: upc-a, barcode generation, caption color, color.fromname, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a UPC-A barcode with a purple caption and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, configures caption settings, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working folder
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not already exist
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the resulting PNG image
        string outputPath = Path.Combine(outputDir, "UPCA_PurpleCaption.png");

        // UPC-A requires exactly 12 numeric characters
        string codeText = "012345678905";

        // Initialize the barcode generator with UPC-A symbology and the data string
        using (var generator = new BarcodeGenerator(EncodeTypes.UPCA, codeText))
        {
            // Enable the caption above the barcode and set its displayed text
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Sample Caption";

            // Apply a purple color to the caption text using Color.FromName
            generator.Parameters.CaptionAbove.TextColor = Color.FromName("Purple");

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}