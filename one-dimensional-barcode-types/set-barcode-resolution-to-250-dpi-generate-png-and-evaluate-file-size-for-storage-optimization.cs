// Title: Generate high‑resolution Code128 barcode PNG and check file size
// Description: Demonstrates setting barcode resolution to 250 DPI, creating a PNG image, and retrieving its file size for storage considerations.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode rendering parameters such as resolution, choose an output format, and use the BarcodeGenerator class. Typical use cases include creating high‑quality barcodes for printing or digital media and evaluating file size for storage optimization. Developers often need to adjust resolution and format to meet quality and size requirements.
// Prompt: Set barcode resolution to 250 DPI, generate PNG, and evaluate file size for storage optimization.
// Tags: code128, resolution, png, file size, barcode generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode at 250 DPI, saving as PNG, and reporting the file size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, saves it, and outputs the file size.
    /// </summary>
    static void Main()
    {
        // Define the temporary output file path for the generated PNG barcode.
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode_250dpi.png");

        // Create a BarcodeGenerator for Code128 with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the rendering resolution to 250 DPI for higher image quality.
            generator.Parameters.Resolution = 250f;

            // Save the barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the file was created and retrieve its size.
        if (File.Exists(outputPath))
        {
            long fileSize = new FileInfo(outputPath).Length;
            Console.WriteLine($"Generated barcode saved to: {outputPath}");
            Console.WriteLine($"File size: {fileSize} bytes");
        }
        else
        {
            Console.WriteLine("Failed to generate barcode image.");
        }
    }
}