// Title: Export MaxiCode barcode to lossless TIFF for high‑resolution printing
// Description: Demonstrates generating a MaxiCode barcode and saving it as a TIFF image with lossless compression, suitable for high‑resolution print output.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as resolution, canvas size, and image format using the BarcodeGenerator class. Developers creating print‑ready barcodes often need to adjust DPI and output to lossless formats like TIFF. The snippet shows typical usage of EncodeTypes, BarCodeImageFormat, and generator parameters for high‑quality barcode rendering.
// Prompt: Export MaxiCode barcode as TIFF image with lossless compression for high‑resolution printing.
// Tags: maxicode, barcode generation, tiff, lossless compression, high resolution, aspose.barcode, image export

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a MaxiCode barcode and saves it as a lossless TIFF image suitable for high‑resolution printing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Configures barcode parameters, generates the barcode, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output TIFF file in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "MaxiCode.tiff");

        // Create a BarcodeGenerator for the MaxiCode symbology with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "HelloWorld"))
        {
            // Set the resolution to 300 DPI for high‑resolution printing.
            generator.Parameters.Resolution = 300f;

            // Define a large image canvas (2000x2000 pixels) to ensure sufficient detail.
            generator.Parameters.ImageWidth.Pixels = 2000f;
            generator.Parameters.ImageHeight.Pixels = 2000f;

            // Save the generated barcode as a lossless TIFF image.
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}