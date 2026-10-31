// Title: Generate Code128 barcode PNG at 250 DPI and check file size
// Description: Demonstrates setting barcode resolution to 250 DPI, creating a Code128 barcode, saving it as a PNG image, and measuring the resulting file size for storage considerations.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode image resolution using the BarcodeGenerator class, select an output format (PNG) via BarCodeImageFormat, and retrieve file information with System.IO. Developers working on barcode creation, image quality tuning, and storage optimization commonly use these APIs to produce high‑resolution barcodes suitable for printing or digital use.
// Prompt: Set barcode resolution to 250 DPI, generate PNG, and evaluate file size for storage optimization.
// Tags: code128, barcode, resolution, png, file size, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode image at 250 DPI,
/// saves it as PNG, and reports the file size for storage optimization.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts optional barcode text argument, creates a temporary
    /// output folder, generates the barcode, and prints the file location and size.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument is used as barcode text.</param>
    static void Main(string[] args)
    {
        // Determine barcode text: use first argument if provided, otherwise default.
        string codeText = args.Length > 0 ? args[0] : "12345678";

        // Create a unique temporary folder for the generated image.
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Build the full path for the PNG output file.
        string outputPath = Path.Combine(outputFolder, "barcode.png");

        // Generate the barcode with 250 DPI resolution and save it as PNG.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Resolution = 250f;               // Set image resolution.
            generator.Save(outputPath, BarCodeImageFormat.Png);   // Save as PNG.
        }

        // Evaluate the size of the generated file.
        long fileSize = new FileInfo(outputPath).Length;

        // Output the results to the console.
        Console.WriteLine($"Barcode generated at: {outputPath}");
        Console.WriteLine($"File size: {fileSize} bytes");
    }
}