// Title: Generate Code 16K barcode and save as high‑resolution TIFF
// Description: Creates a Code 16K barcode containing the maximum 77 characters and saves it as a 300 dpi TIFF image.
// Category-Description: This example demonstrates Aspose.BarCode barcode generation using the BarcodeGenerator class with EncodeTypes.Code16K. It shows how to configure resolution, module size, and aspect ratio for high‑resolution output, a common requirement for printing barcodes on labels, packaging, or documents. Developers working with barcode creation, especially for Code 16K symbology, can use this pattern to produce TIFF files suitable for high‑quality print workflows.
// Prompt: Generate Code 16K barcode with maximum 77 characters, save high‑resolution TIFF.
// Tags: code16k, barcode, generation, tiff, highresolution, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code 16K barcode with the maximum allowed data length
/// and saving it as a high‑resolution TIFF image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, configures image settings,
    /// and writes the resulting file to a temporary directory.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary output directory
        string outputDir = Path.Combine(Path.GetTempPath(), "Code16KDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Sample Code 16K data (exactly 77 characters, the maximum for this symbology)
        string codeText = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJ";

        // Full path for the resulting TIFF file
        string outputPath = Path.Combine(outputDir, "Code16K.tiff");

        // Initialize the barcode generator with Code 16K symbology and the data string
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            // Set image resolution to 300 dpi for high‑quality output
            generator.Parameters.Resolution = 300f;

            // Define the module (bar) width in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Adjust the aspect ratio; values > 8 are recommended for Code 16K
            generator.Parameters.Barcode.Code16K.AspectRatio = 10;

            // Save the barcode as a TIFF image with the configured settings
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine("Barcode saved to: " + outputPath);
    }
}