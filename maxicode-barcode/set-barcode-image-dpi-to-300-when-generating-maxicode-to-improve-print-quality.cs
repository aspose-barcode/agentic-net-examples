// Title: Generate MaxiCode barcode with 300 DPI resolution
// Description: Demonstrates how to generate a MaxiCode barcode image at 300 DPI to ensure high‑quality printing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure image resolution and barcode dimensions using the BarcodeGenerator and its Parameters classes. Typical use cases include creating printable barcodes for shipping labels, inventory tags, and retail packaging where precise sizing and print fidelity are required. Developers often need to adjust DPI, XDimension, and output format to meet printer specifications and visual standards.
// Prompt: Set the barcode image DPI to 300 when generating a MaxiCode to improve print quality.
// Tags: maxicode, set-dpi, png, barcodegenerator, parameters

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a MaxiCode barcode image with a resolution of 300 DPI.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file in the system's temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "maxicode_300dpi.png");

        // Ensure the target directory exists; create it if it does not.
        string directory = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Create a BarcodeGenerator for the MaxiCode symbology with sample text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode Text"))
        {
            // Set the image resolution to 300 DPI for high‑quality printing.
            generator.Parameters.Resolution = 300f;

            // Optionally adjust the XDimension (module width) to improve visual quality; here set to 2 points.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}