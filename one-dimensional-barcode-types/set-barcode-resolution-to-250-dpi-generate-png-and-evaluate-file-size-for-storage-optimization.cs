// Title: Generate high‑resolution Code128 barcode PNG and check file size
// Description: Demonstrates setting barcode resolution to 250 DPI, creating a PNG image, and retrieving its file size for storage optimization.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to configure barcode rendering parameters such as resolution, select a symbology (Code128), and save the result in PNG format. Developers commonly use these APIs (BarcodeGenerator, EncodeTypes, BarCodeImageFormat) to produce barcodes for packaging, inventory, or mobile scanning, and often need to assess file size for efficient storage or transmission.
// Prompt: Set barcode resolution to 250 DPI, generate PNG, and evaluate file size for storage optimization.
// Tags: code128, resolution, png, file size, barcode generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode PNG with a custom resolution and reporting its file size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary folder, generates the barcode image, and outputs its location and size.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the output PNG file
        string filePath = Path.Combine(tempFolder, "barcode.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Set the rendering resolution to 250 DPI for higher quality output
            generator.Parameters.Resolution = 250f;

            // Save the generated barcode as a PNG image
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Verify that the file was created and report its size
        if (File.Exists(filePath))
        {
            long fileSize = new FileInfo(filePath).Length;
            Console.WriteLine($"Generated barcode saved to: {filePath}");
            Console.WriteLine($"File size: {fileSize} bytes");
        }
        else
        {
            Console.WriteLine("Failed to generate barcode image.");
        }
    }
}