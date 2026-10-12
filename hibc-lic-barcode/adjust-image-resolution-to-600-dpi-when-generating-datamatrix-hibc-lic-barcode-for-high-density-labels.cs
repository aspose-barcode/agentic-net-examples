// Title: Generate DataMatrix HIBC LIC Barcode at 600 DPI
// Description: Demonstrates how to create a DataMatrix HIBC LIC barcode with a 600 DPI image resolution for high‑density label printing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and resolution settings to produce high‑resolution barcodes. Typical scenarios include printing dense labels for medical, pharmaceutical, or logistics applications where barcode readability at small sizes is critical. Developers often need to adjust image DPI, module size, and output format to meet label specifications.
// Prompt: Adjust the image resolution to 600 DPI when generating a DataMatrix HIBC LIC barcode for high‑density labels.
// Tags: datamatrix, hibc, barcode, resolution, png, aspose.barcode, image generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a DataMatrix HIBC LIC barcode image with a 600 DPI resolution,
/// suitable for high‑density label printing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the HIBC DataMatrix code text (must start with '+')
        string codeText = "+A123B456C789";

        // Build a temporary output directory and file path
        string outputDir = Path.Combine(Path.GetTempPath(), "HIBC_DataMatrix");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "HIBC_DataMatrix_600dpi.png");

        // Initialize the barcode generator for DataMatrix with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Set the image resolution to 600 DPI for high‑density output
            generator.Parameters.Resolution = 600f;

            // Optionally define the module size (X‑dimension) in millimeters
            generator.Parameters.Barcode.XDimension.Millimeters = 0.5f;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}