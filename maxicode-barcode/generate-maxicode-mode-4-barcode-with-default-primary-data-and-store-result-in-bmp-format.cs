// Title: Generate MaxiCode Mode 4 barcode and save as BMP
// Description: Demonstrates creating a MaxiCode Mode 4 barcode using Aspose.BarCode and saving it to a BMP image file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to configure MaxiCode parameters such as mode and X‑dimension. It uses the BarcodeGenerator class with EncodeTypes.MaxiCode and illustrates typical use cases like generating shipping labels or inventory tags where MaxiCode Mode 4 is required. Developers often need to customize dimensions and output formats, and this snippet provides a concise reference.
// Prompt: Generate a MaxiCode Mode 4 barcode with default primary data and store the result in BMP format.
// Tags: maxicode, barcode generation, bmp output, aspnet, aspose.barcode, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a MaxiCode Mode 4 barcode and saving it as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures parameters, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Determine output file path in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "MaxiCodeMode4.bmp");

        // Initialize the barcode generator for MaxiCode with sample data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode Mode 4"))
        {
            // Set the X-dimension (module size) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 15f;
            // Specify the MaxiCode mode to use (Mode 4)
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode4;
            // Save the generated barcode image as BMP
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"MaxiCode Mode 4 barcode saved to: {outputPath}");
    }
}