// Title: Generate Han Xin Barcode with Default Square Modules
// Description: Demonstrates creating a Han Xin barcode from alphanumeric text using Aspose.BarCode with default square module size.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.HanXin. Typical use cases include encoding alphanumeric data for inventory, tracking, or authentication purposes where a compact, high-capacity 2‑D barcode is required. Developers often need to generate such barcodes quickly with default settings for square modules and then save them in common image formats.
// Prompt: Generate a Han Xin barcode with default square module size for alphanumeric input.
// Tags: hanxin, barcode, generation, png, aspose.barcode, alphanumeric, default-modules

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Han Xin barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Han Xin barcode from a sample alphanumeric string and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Sample alphanumeric text to encode
        string codeText = "ABC123";

        // Create a unique temporary folder for the output to avoid filename conflicts
        string outputFolder = Path.Combine(Path.GetTempPath(), "HanXinDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Full path of the generated barcode image
        string outputPath = Path.Combine(outputFolder, "HanXinBarcode.png");

        // Initialize the barcode generator with Han Xin symbology and the sample text
        // Default settings produce a square barcode (default XDimension and version)
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Han Xin barcode saved to: {outputPath}");
    }
}