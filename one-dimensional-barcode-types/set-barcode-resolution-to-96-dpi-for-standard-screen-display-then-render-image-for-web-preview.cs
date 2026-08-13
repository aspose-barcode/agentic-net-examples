// Title: Generate Code128 barcode image with 96 DPI resolution for web preview
// Description: Demonstrates how to set the barcode image resolution to 96 DPI, suitable for standard screen display, and save it as a PNG file for web usage.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator and its Parameters to control output quality. Typical scenarios include creating barcodes for e‑commerce sites, online tickets, or any web‑based application where screen‑optimized images are required. Developers often need to adjust resolution, format, and encoding to meet UI and performance constraints.
// Prompt: Set barcode resolution to 96 DPI for standard screen display, then render image for web preview.
// Tags: code128, resolution, png, barcodegenerator, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode, sets its resolution to 96 DPI,
/// and saves the result as a PNG image suitable for web preview.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.png";

        // Ensure the target directory exists; create it if necessary.
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Initialize a BarcodeGenerator for Code128 symbology with sample data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Configure the generator to use a screen‑friendly resolution of 96 DPI.
            generator.Parameters.Resolution = 96f;

            // Save the generated barcode as a PNG file, ideal for web display.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the absolute path of the saved image for verification.
        Console.WriteLine($"Barcode image saved to: {Path.GetFullPath(outputPath)}");
    }
}