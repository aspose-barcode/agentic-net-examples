// Title: Generate Han Xin Barcode with Automatic Version Selection
// Description: Demonstrates creating a Han Xin barcode using Aspose.BarCode, automatically selecting the version based on payload size.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on Han Xin symbology. It shows how to configure barcode parameters such as version and error correction level using the BarcodeGenerator and its Parameters API. Developers commonly use these APIs to generate QR-like barcodes for data encoding, customizing size, shape, and error resilience.
// Prompt: Configure Han Xin to use rectangular shape with 15 rows and 40 columns for larger payload.
// Tags: hanxin, barcode, generation, automatic version, error correction, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Han Xin barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Han Xin barcode with automatic version selection
    /// and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define a sample payload that requires a larger Han Xin barcode.
        string codeText = "This is a sample payload that requires a larger Han Xin barcode.";

        // Create a temporary folder to store the generated barcode image.
        string outputFolder = Path.Combine(Path.GetTempPath(), "HanXinExample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        string outputPath = Path.Combine(outputFolder, "HanXin.png");

        // Initialize the barcode generator for Han Xin symbology with the provided payload.
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            // Han Xin supports only square versions; rectangular shapes are not available.
            // Set the version to Auto so the library selects the appropriate size based on the payload.
            generator.Parameters.Barcode.HanXin.Version = HanXinVersion.Auto;

            // Optional: configure error correction level to improve data recovery capability.
            generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;

            // Save the generated barcode image to the specified path in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the generated barcode image.
        Console.WriteLine("Han Xin barcode generated at:");
        Console.WriteLine(outputPath);
    }
}