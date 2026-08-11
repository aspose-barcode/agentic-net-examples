// Title: Apply custom background color to ITF‑14 barcode and export as JPEG
// Description: Demonstrates how to set a custom background color for an ITF‑14 barcode using Aspose.BarCode, then render and save it as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical use cases include customizing barcode appearance for branding or printing requirements, where developers need to modify colors and export to common image formats.
/// Prompt: Apply custom background color to ITF barcodes before rendering, export JPEG.
/// Tags: itf, background-color, jpeg, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates applying a custom background color to an ITF‑14 barcode and saving it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, customizes colors, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file name and path
        string outputPath = "itf_barcode.jpg";

        // Sample 14‑digit code for the ITF‑14 barcode
        string codeText = "12345678901231";

        // Initialize the barcode generator with ITF‑14 symbology and the sample code
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.ITF14, codeText))
        {
            // Set a custom background color for the entire image
            generator.Parameters.BackColor = Color.LightGray;

            // Optionally set the bar (foreground) color
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Render and save the barcode as a JPEG image
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"ITF barcode saved to {Path.GetFullPath(outputPath)}");
    }
}