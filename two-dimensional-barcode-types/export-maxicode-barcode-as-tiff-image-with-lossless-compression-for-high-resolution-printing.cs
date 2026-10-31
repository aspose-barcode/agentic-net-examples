// Title: Export MaxiCode barcode to TIFF with lossless compression
// Description: Demonstrates generating a MaxiCode barcode and saving it as a TIFF image using lossless compression, suitable for high‑resolution printing.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure barcode parameters such as resolution and X‑dimension and export the result to a TIFF file with lossless compression. It uses the BarcodeGenerator class together with EncodeTypes and BarCodeImageFormat to create barcodes for printing and packaging applications. Developers often need to produce high‑quality printable barcodes, and this snippet shows the typical steps for such use cases.
// Prompt: Export MaxiCode barcode as TIFF image with lossless compression for high‑resolution printing.
// Tags: maxicode, barcode generation, tiff, lossless compression, high resolution printing, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a MaxiCode barcode and saves it as a TIFF image with lossless compression.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output folder, configures the barcode generator,
    /// and writes the resulting TIFF file to disk.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working folder.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the generated TIFF file.
        string outputPath = Path.Combine(outputDir, "MaxiCode.tiff");

        // Text to encode in the MaxiCode barcode.
        string codeText = "Sample MaxiCode";

        // Initialize the barcode generator for MaxiCode symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
        {
            // Set a high resolution (300 DPI) for high‑quality printing.
            generator.Parameters.Resolution = 300f;

            // Define the X‑dimension (module width) in points.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the barcode as a TIFF image using lossless compression.
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}