// Title: Generate High‑Resolution PNG DataMatrix Barcode with Interpolation AutoSizeMode
// Description: Demonstrates how to configure AutoSizeMode to Interpolation, set image dimensions and resolution, and save a high‑resolution PNG barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and parameter settings such as AutoSizeMode, ImageWidth, ImageHeight, and Resolution. Developers commonly need to produce high‑quality barcodes for print media, packaging, or digital documents, and this snippet shows the typical API calls required.
// Prompt: Configure AutoSizeMode to Interpolation, set ImageWidth and ImageHeight, and generate a high‑resolution PNG barcode.
// Tags: datamatrix, barcode generation, high resolution, png, autosizemode, interpolation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a high‑resolution PNG DataMatrix barcode with interpolation auto‑size mode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output folder, configures barcode generator, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define a temporary directory for the output file.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Build the full path for the generated PNG barcode.
        string outputPath = Path.Combine(outputDir, "HighResBarcode.png");

        // Initialize the barcode generator for a DataMatrix symbology with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "ASPOSE"))
        {
            // Set auto‑size mode to use interpolation for smoother scaling.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Define the target image dimensions in pixels.
            generator.Parameters.ImageWidth.Pixels = 1200f;
            generator.Parameters.ImageHeight.Pixels = 1200f;

            // Adjust the X‑dimension (module size) for better visual quality.
            generator.Parameters.Barcode.XDimension.Pixels = 3f;

            // Set the output resolution (DPI) for high‑resolution printing.
            generator.Parameters.Resolution = 300f;

            // Save the barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}