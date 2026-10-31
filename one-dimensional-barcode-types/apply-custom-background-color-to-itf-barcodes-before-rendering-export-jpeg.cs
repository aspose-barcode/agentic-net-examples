// Title: Apply custom background color to ITF14 barcode and export as JPEG
// Description: Demonstrates how to generate an ITF14 barcode with a custom background and bar colors, then save it as a JPEG image. Useful for creating printable barcodes with branding.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and image format settings. Developers often need to customize visual appearance of barcodes—such as background and bar colors—before rendering them to common image formats like JPEG for web or print use. The snippet shows typical steps: setting dimensions, colors, and saving the image.
// Prompt: Apply custom background color to ITF barcodes before rendering, export JPEG.
// Tags: itf14, barcode, background color, jpeg, aspose.barcode, generation, colors

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates an ITF14 barcode with a custom background and bar colors, then saves it as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output folder, configures the barcode generator,
    /// applies visual customizations, and writes the resulting image to disk.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarCodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated JPEG image.
        string outputPath = Path.Combine(outputDir, "ITF14_CustomBackground.jpg");

        // The data to encode in the ITF14 barcode.
        string codeText = "12345678901231";

        // Initialize the barcode generator for ITF14 symbology with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, codeText))
        {
            // Set the X-dimension (module width) to 2 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Apply a light gray background to the entire image.
            generator.Parameters.BackColor = Color.LightGray;

            // Set the barcode bars to a dark blue color.
            generator.Parameters.Barcode.BarColor = Color.DarkBlue;

            // Render and save the barcode as a JPEG file.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}