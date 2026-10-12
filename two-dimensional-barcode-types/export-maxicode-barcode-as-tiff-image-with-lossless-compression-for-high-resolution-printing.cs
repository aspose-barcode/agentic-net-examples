// Title: Export MaxiCode barcode to TIFF with lossless compression
// Description: Demonstrates generating a MaxiCode barcode and saving it as a TIFF image using lossless compression, suitable for high‑resolution printing.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to configure barcode parameters such as symbology mode, resolution, and colors, and how to export the result to a TIFF file with lossless compression. Developers working with barcode creation for print media often need to produce high‑quality raster images; the key classes used are BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and related parameter objects. The snippet serves as a reference for generating printable barcodes in .NET applications.
// Prompt: Export MaxiCode barcode as TIFF image with lossless compression for high‑resolution printing.
// Tags: maxicode, barcode, tiff, lossless compression, image generation, aspnet, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a MaxiCode barcode and saving it as a TIFF image with lossless compression.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, configures parameters, and saves the image.
    /// </summary>
    static void Main()
    {
        // Determine the output file path in the system's temporary folder
        string outputPath = Path.Combine(Path.GetTempPath(), "maxicode.tiff");
        string directory = Path.GetDirectoryName(outputPath);

        // Ensure the target directory exists
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Create a barcode generator for MaxiCode with the desired text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Hello MaxiCode"))
        {
            // Set MaxiCode mode (Mode4), resolution (300 DPI), and bar color (black)
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode4;
            generator.Parameters.Resolution = 300f;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Save the generated barcode as a TIFF image (lossless compression by default)
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}