// Title: Generate Compact Mailmark Barcode with Adjusted Module Size
// Description: Demonstrates how to create a Mailmark barcode with a reduced X-dimension, producing a compact image suitable for small labels.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure barcode parameters such as XDimension for specific symbologies like Mailmark. Developers often need to generate high‑density barcodes for packaging, shipping, or inventory labels, and this snippet illustrates the typical workflow using BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes.
// Prompt: Adjust barcode module size to produce a compact Mailmark barcode suitable for small labels.
// Tags: mailmark, barcode, generation, compact, png, aspose.barcode, xdimension

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a compact Mailmark barcode by reducing the module (X‑dimension) size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a temporary folder, generates the barcode, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Build a unique temporary directory to store the output image.
        string tempDir = Path.Combine(Path.GetTempPath(), "MailmarkDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the full path for the resulting PNG file.
        string outputPath = Path.Combine(tempDir, "MailmarkCompact.png");

        // The data to encode in the Mailmark barcode.
        string codeText = "21B2254800659JW5O9QA6Y";

        // Initialize the barcode generator for the Mailmark symbology with the provided text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Mailmark, codeText))
        {
            // Reduce the X‑dimension (module size) to 2 pixels for a more compact barcode.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Mailmark barcode saved to: {outputPath}");
    }
}