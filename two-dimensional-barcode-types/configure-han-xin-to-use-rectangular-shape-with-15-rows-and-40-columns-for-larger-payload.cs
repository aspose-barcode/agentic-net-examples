// Title: Generate Han Xin Barcode with Automatic Version Selection
// Description: Demonstrates how to generate a Han Xin 2‑D barcode using Aspose.BarCode, automatically selecting the appropriate version for a large payload.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and HanXinVersion to create 2‑D barcodes. Typical use cases include encoding large numeric strings or data sets where the barcode size must adapt automatically. Developers often need to configure barcode parameters, select image formats, and save the result to a file.
// Prompt: Configure Han Xin to use rectangular shape with 15 rows and 40 columns for larger payload.
// Tags: hanxin, barcode, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Han Xin barcode image with automatic version selection.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Han Xin barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "HanXin.png");

        // Prepare a sample payload that requires a larger barcode version (200 numeric characters).
        string codeText = new string('9', 200); // 200 digits

        // Initialize the barcode generator for the Han Xin symbology with the given payload.
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            // Han Xin supports only square symbols; setting Version to Auto lets the library choose the smallest fitting version.
            generator.Parameters.Barcode.HanXin.Version = HanXinVersion.Auto;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Han Xin barcode saved to: {outputPath}");
    }
}