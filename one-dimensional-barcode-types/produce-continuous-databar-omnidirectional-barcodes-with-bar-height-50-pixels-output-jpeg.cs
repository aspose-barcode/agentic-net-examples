// Title: Generate DataBar Omnidirectional Barcode as JPEG
// Description: Demonstrates creating a continuous DataBar Omnidirectional barcode with a specific bar height and saving it as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.DatabarOmniDirectional to produce linear barcodes. Typical use cases include encoding GS1-128 data for retail or logistics, customizing barcode dimensions, and exporting to common image formats. Developers often need to adjust size parameters and choose appropriate output formats for integration into documents or web services.
// Prompt: Produce continuous DataBar Omnidirectional barcodes with bar height 50 pixels, output JPEG.
// Tags: databar, omnidirectional, barcode, generation, jpeg, aspnet, barcodgenerator, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a DataBar Omnidirectional barcode and saving it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, sets its height, saves the image, and writes the output path.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the system's temporary directory
        string outputPath = Path.Combine(Path.GetTempPath(), "DataBarOmniDirectional.jpg");

        // Set the barcode text (example GS1-128 data)
        string codeText = "(01)12345678901231";

        // Initialize the barcode generator with the DataBar Omnidirectional symbology and the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarOmniDirectional, codeText))
        {
            // Configure the bar height to 50 pixels
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;

            // Save the generated barcode as a JPEG image
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Output the location of the saved barcode image
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}