// Title: Generate Han Xin Barcode with Default Square Modules
// Description: Creates a Han Xin barcode for an alphanumeric string using the default square module size and saves it as a PNG file.
// Category-Description: This example demonstrates basic barcode generation using Aspose.BarCode. It focuses on the BarcodeGenerator class with EncodeTypes.HanXin, showing how to configure default settings, select output format, and write the image to disk. Developers working with various symbologies often need quick code snippets for generating barcodes in common image formats for reports, labels, or web applications.
// Prompt: Generate a Han Xin barcode with default square module size for alphanumeric input.
// Tags: hanxin, barcode, generation, png, aspose.barcode, encode types, alphanumeric

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a Han Xin barcode with default square module size
/// and save it as a PNG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary directory to store the generated barcode image.
        string outputDir = Path.Combine(Path.GetTempPath(), "HanXinDemo");
        Directory.CreateDirectory(outputDir);

        // Build the full file path for the PNG output.
        string outputPath = Path.Combine(outputDir, "hanxin.png");

        // Create a BarcodeGenerator for the Han Xin symbology with the desired alphanumeric data.
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, "ABC123"))
        {
            // Default settings provide square modules and automatic version selection.
            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Han Xin barcode saved to: {outputPath}");
    }
}