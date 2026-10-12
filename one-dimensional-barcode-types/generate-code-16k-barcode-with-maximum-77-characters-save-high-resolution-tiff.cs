// Title: Generate Code 16K barcode and save as high‑resolution TIFF
// Description: Demonstrates creating a Code 16K barcode with the maximum 77‑character payload and exporting it to a 300 dpi TIFF image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as resolution, X‑dimension, and aspect ratio using the BarcodeGenerator class. Typical use cases include producing high‑quality printable barcodes for inventory, shipping, or compliance labeling. Developers often need to adjust image resolution and dimensions to meet printing standards, and this snippet shows the essential API calls.
// Prompt: Generate Code 16K barcode with maximum 77 characters, save high‑resolution TIFF.
// Tags: code16k, barcode generation, tiff output, high resolution, aspnet.barcode, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Code 16K barcode with a 77‑character payload
/// and saves it as a high‑resolution TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, configures image settings, and writes the file.
    /// </summary>
    static void Main()
    {
        // Determine output folder and ensure it exists
        string outputDirectory = Path.Combine(Environment.CurrentDirectory, "Output");
        Directory.CreateDirectory(outputDirectory);

        // Build full path for the output TIFF file
        string outputPath = Path.Combine(outputDirectory, "Code16K.tiff");

        // Prepare a 77‑character string for the barcode data
        string baseStr = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string codeText = (baseStr + baseStr + baseStr).Substring(0, 77);

        // Initialize the generator with Code16K symbology and the prepared data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            // Set image resolution to 300 DPI for high‑quality output
            generator.Parameters.Resolution = 300f;

            // Set X‑dimension (module width) to 2 pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Adjust the Code16K specific aspect ratio
            generator.Parameters.Barcode.Code16K.AspectRatio = 10f;

            // Save the generated barcode as a TIFF image
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Code 16K barcode saved to: {outputPath}");
    }
}