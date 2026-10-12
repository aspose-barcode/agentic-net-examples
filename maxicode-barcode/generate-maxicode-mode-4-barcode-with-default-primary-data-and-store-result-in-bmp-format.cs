// Title: Generate MaxiCode Mode 4 Barcode and Save as BMP
// Description: Creates a MaxiCode barcode in Mode 4 using default primary data and writes the image to a BMP file.
// Category-Description: This example demonstrates how to generate a MaxiCode barcode with Aspose.BarCode for .NET. It covers the use of BarcodeGenerator, setting barcode parameters such as X‑dimension and MaxiCode mode, and saving the result in BMP format. Developers working with shipping and logistics can use MaxiCode to encode data for high‑speed scanning; typical scenarios include creating barcodes for package labeling, tracking, and inventory systems.
// Prompt: Generate a MaxiCode Mode 4 barcode with default primary data and store the result in BMP format.
// Tags: maxicode, barcode generation, bmp, aspose.barcode, encoding, image output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a MaxiCode Mode 4 barcode and saving it as a BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the output file.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output BMP file in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "MaxiCodeMode4.bmp");

        // Initialize the barcode generator for MaxiCode with default data ("Sample").
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample"))
        {
            // Set the X-dimension (module size) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 15f;

            // Configure the MaxiCode mode to Mode 4.
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode4;

            // Save the generated barcode image to the specified path in BMP format.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Output the location of the saved barcode image.
        Console.WriteLine($"MaxiCode Mode 4 barcode saved to: {outputPath}");
    }
}