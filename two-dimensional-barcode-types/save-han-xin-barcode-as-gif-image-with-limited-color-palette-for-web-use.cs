// Title: Generate Han Xin barcode and save as GIF with web‑friendly palette
// Description: Demonstrates creating a Han Xin 2‑D barcode, configuring error correction, and saving it as a GIF image suitable for web use.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator with Han Xin symbology, set encoding parameters, and export to common web image formats. Developers often need to produce compact, high‑density barcodes for URLs or product data and require GIF output with limited colors for faster loading. The key API classes shown are BarcodeGenerator, EncodeTypes, HanXinErrorLevel, HanXinEncodeMode, and BarCodeImageFormat.
// Prompt: Save Han Xin barcode as GIF image with limited color palette for web use.
// Tags: hanxin, barcode, generation, gif, color-palette, aspose.barcode, encoding

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Han Xin barcode and saving it as a GIF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the barcode, configures parameters, and writes the image to a temporary file.
    /// </summary>
    static void Main()
    {
        // Determine output file path in the system temporary folder
        string outputPath = Path.Combine(Path.GetTempPath(), "HanXinBarcode.gif");

        // Initialize the barcode generator with Han Xin symbology and the desired text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.HanXin, "Hello World"))
        {
            // Set error correction level to L2 (higher reliability)
            generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;
            // Use automatic mode to let the library choose the optimal encoding
            generator.Parameters.Barcode.HanXin.EncodeMode = HanXinEncodeMode.Auto;
            // Define foreground (barcode) and background colors for the GIF
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode as a GIF image
            generator.Save(outputPath, BarCodeImageFormat.Gif);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Han Xin barcode saved to: {outputPath}");
    }
}