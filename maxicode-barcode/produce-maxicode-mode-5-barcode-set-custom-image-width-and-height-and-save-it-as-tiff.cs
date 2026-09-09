// Title: Generate MaxiCode Mode 5 Barcode and Save as TIFF
// Description: Creates a MaxiCode Mode 5 barcode with custom dimensions and saves it as a TIFF image.
// Category-Description: This example demonstrates how to use Aspose.BarCode for .NET to generate a specific barcode symbology (MaxiCode) with custom image size settings. It covers the BarcodeGenerator class, setting barcode parameters such as MaxiCode mode and image dimensions, and saving the result in TIFF format. Developers working with shipping labels, logistics, or any application requiring MaxiCode can use this pattern to produce high‑quality barcode images.
// Prompt: Produce a MaxiCode Mode 5 barcode, set custom image width and height, and save it as TIFF.
// Tags: maxicode, barcode generation, tiff, aspose.barcode, image size, mode5

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a MaxiCode Mode 5 barcode with custom dimensions and saving it as a TIFF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures its appearance, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output TIFF file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "MaxiCodeMode5.tiff");

        // Text to encode in the MaxiCode barcode.
        string codeText = "Sample MaxiCode Mode5";

        // Initialize the barcode generator with MaxiCode symbology and the desired text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
        {
            // Set the MaxiCode mode to Mode 5.
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode5;

            // Specify custom image width and height in pixels.
            generator.Parameters.ImageWidth.Pixels = 500f;
            generator.Parameters.ImageHeight.Pixels = 300f;

            // Choose the nearest auto‑size mode to fit the barcode within the specified dimensions.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Save the generated barcode as a TIFF image to the defined path.
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Output the location of the saved barcode image.
        Console.WriteLine($"MaxiCode Mode 5 barcode saved to: {outputPath}");
    }
}