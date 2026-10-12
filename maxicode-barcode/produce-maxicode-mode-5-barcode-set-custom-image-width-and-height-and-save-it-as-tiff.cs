// Title: Generate a MaxiCode Mode 5 barcode and save as TIFF
// Description: Demonstrates creating a MaxiCode Mode 5 barcode with custom image dimensions using Aspose.BarCode and saving it as a TIFF file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on MaxiCode symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and image format settings to produce high‑resolution barcodes. Developers often need to customize size, mode, and output format when integrating barcodes into packaging, shipping labels, or inventory systems.
// Prompt: Produce a MaxiCode Mode 5 barcode, set custom image width and height, and save it as TIFF.
// Tags: maxicode, mode5, barcode generation, image size, tiff, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a MaxiCode Mode 5 barcode,
/// applies custom image dimensions, and saves the result as a TIFF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define output directory in the temporary folder and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated TIFF file
        string outputPath = Path.Combine(outputDir, "MaxiCodeMode5.tiff");

        // Sample codetext – arbitrary text is allowed for MaxiCode Mode 5
        string codeText = "Sample MaxiCode Mode5";

        // Initialize the barcode generator with MaxiCode symbology and the sample text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
        {
            // Configure the generator to use MaxiCode Mode 5
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode5;

            // Set custom image dimensions (pixels) and enforce a fixed size
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
            generator.Parameters.ImageWidth.Pixels = 300f;
            generator.Parameters.ImageHeight.Pixels = 300f;

            // Optional: adjust the module (dot) size for better visual clarity
            generator.Parameters.Barcode.XDimension.Pixels = 10f;

            // Save the generated barcode as a TIFF image
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"MaxiCode Mode 5 barcode saved to: {outputPath}");
    }
}