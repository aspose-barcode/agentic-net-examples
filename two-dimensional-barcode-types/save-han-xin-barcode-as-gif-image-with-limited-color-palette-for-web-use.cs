// Title: Save Han Xin barcode as GIF with web‑friendly palette
// Description: Demonstrates generating a Han Xin 2‑D barcode and saving it as a GIF image, which uses a limited color palette suitable for web pages.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure barcode parameters (symbology, error correction, version) and output settings (colors, resolution, image format) using the BarcodeGenerator class. Typical use cases include creating compact, web‑ready barcode images for e‑commerce, ticketing, or inventory systems. Developers often need to produce GIF or PNG files with specific color constraints, and this snippet shows the essential steps.
// Prompt: Save Han Xin barcode as GIF image with limited color palette for web use.
// Tags: hanxin, barcode generation, gif, color palette, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Han Xin barcode and saving it as a GIF image suitable for web use.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, configures visual settings, and writes the GIF file.
    /// </summary>
    static void Main()
    {
        // Define a temporary output folder and ensure it exists
        string outputFolder = Path.Combine(Path.GetTempPath(), "HanXinGifDemo");
        Directory.CreateDirectory(outputFolder);

        // Full path for the resulting GIF file
        string outputPath = Path.Combine(outputFolder, "HanXinBarcode.gif");

        // Text to encode in the barcode
        string codeText = "Sample Han Xin Code for Web";

        // Initialize the barcode generator for Han Xin symbology with the provided text
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            // Configure error correction level (L2) to improve readability
            generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;

            // Let the library choose the optimal version automatically (square format)
            generator.Parameters.Barcode.HanXin.Version = HanXinVersion.Auto;

            // Set visual colors: black bars on a white background
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Define resolution (96 DPI) typical for web images
            generator.Parameters.Resolution = 96;

            // Save the barcode as a GIF; GIF format inherently uses a limited color palette ideal for web delivery
            generator.Save(outputPath, BarCodeImageFormat.Gif);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Han Xin barcode saved to: {outputPath}");
    }
}