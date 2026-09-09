// Title: Generate MaxiCode Mode 4 barcode and save as BMP
// Description: Demonstrates creating a MaxiCode Mode 4 barcode with default data and saving it as a BMP image file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.MaxiCode to produce MaxiCode symbols. Typical use cases include encoding shipping information or package tracking data in MaxiCode format. Developers often need to set specific MaxiCode modes and export the barcode to common image formats such as BMP, PNG, or JPEG.
// Prompt: Generate a MaxiCode Mode 4 barcode with default primary data and store the result in BMP format.
// Tags: maxicode, barcode generation, bmp, aspnet, aspose.barcode, encode types, image export

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a MaxiCode Mode 4 barcode and saving it as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, configures parameters, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output BMP file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "MaxiCodeMode4.bmp");

        // Initialize the barcode generator for MaxiCode with default sample data
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample"))
        {
            // Set the X-dimension (module size) in pixels
            gen.Parameters.Barcode.XDimension.Pixels = 15;

            // Configure the MaxiCode mode to Mode 4
            gen.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode4;

            // Save the generated barcode as a BMP image
            gen.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Output the location of the saved barcode image
        Console.WriteLine($"MaxiCode Mode 4 barcode saved to: {outputPath}");
    }
}