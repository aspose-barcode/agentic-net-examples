// Title: Export MaxiCode barcode as TIFF with lossless compression
// Description: Demonstrates exporting a MaxiCode barcode to a TIFF image using lossless compression, suitable for high‑resolution printing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on complex barcode creation (e.g., MaxiCode) and image export. It showcases the use of ComplexBarcodeGenerator, MaxiCodeStandardCodetext, and image‑format classes to produce high‑resolution printable assets. Developers often need to generate barcodes for packaging, logistics, or retail, and require lossless image formats for quality‑critical workflows.
// Prompt: Export MaxiCode barcode as TIFF image with lossless compression for high‑resolution printing.
// Tags: maxicode, barcode, export, tiff, lossless, high-resolution, aspose.barcode, complexbarcode, image

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a MaxiCode barcode and saves it as a lossless TIFF image
/// for high‑resolution printing scenarios.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a MaxiCode barcode in Mode 4 and writes it to a TIFF file.
    /// </summary>
    static void Main()
    {
        // Prepare MaxiCode codetext for Mode 4 (data‑only) with a sample message.
        var maxiCodeCodetext = new MaxiCodeStandardCodetext
        {
            Mode = MaxiCodeMode.Mode4,
            Message = "Sample MaxiCode"
        };

        // Create a ComplexBarcodeGenerator using the prepared codetext.
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Set a high resolution (300 DPI) to ensure the output is suitable for high‑resolution printing.
            generator.Parameters.Resolution = 300f; // DPI

            // Enable interpolation auto‑size mode so the generator scales the image based on the resolution.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Generate the barcode image in memory.
            using (Image barcodeImage = generator.GenerateBarCodeImage())
            {
                // Save the image as a TIFF file using lossless compression.
                using (var fileStream = new FileStream("maxicode.tiff", FileMode.Create, FileAccess.Write))
                {
                    barcodeImage.Save(fileStream, ImageFormat.Tiff);
                }
            }
        }
    }
}