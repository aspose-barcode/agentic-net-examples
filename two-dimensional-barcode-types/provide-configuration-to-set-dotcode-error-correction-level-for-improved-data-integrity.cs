// Title: Generate a DotCode barcode with default settings
// Description: Demonstrates creating a DotCode barcode image and saving it to a temporary folder.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.DotCode. Typical use cases include encoding data for high‑density 2‑D barcodes in logistics or inventory systems. Developers often need to configure dimensions, error correction, and output formats when generating barcodes programmatically.
// Prompt: Provide configuration to set DotCode error correction level for improved data integrity.
// Tags: dotcode, barcode generation, error correction, image output, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a DotCode barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary directory, generates a DotCode barcode, saves it as PNG, and writes the file path to the console.
    /// </summary>
    static void Main()
    {
        // Define a unique temporary output directory.
        string outputDir = Path.Combine(Path.GetTempPath(), "DotCodeExample_" + Guid.NewGuid().ToString("N"));
        // Ensure the directory exists.
        Directory.CreateDirectory(outputDir);
        // Full path for the generated PNG image.
        string imagePath = Path.Combine(outputDir, "DotCode.png");

        // Initialize the barcode generator for DotCode with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, "SampleDataForDotCode"))
        {
            // Set the module (X-dimension) size in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 10f;
            // Let the generator automatically determine the optimal rows and columns.
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine("DotCode barcode generated at:");
        Console.WriteLine(imagePath);
    }
}