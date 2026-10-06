// Title: Generate Continuous DataBar Omnidirectional Barcode as JPEG
// Description: Demonstrates creating a DataBar Omnidirectional (GS1 DataBar) barcode with a fixed bar height of 50 pixels and saving it as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.DatabarOmniDirectional to produce continuous GS1 DataBar barcodes. Typical use cases include retail product labeling and inventory systems where high‑density linear barcodes are required. Developers often need to configure dimensions such as X‑dimension and bar height before exporting to common image formats like JPEG.
// Prompt: Produce continuous DataBar Omnidirectional barcodes with bar height 50 pixels, output JPEG.
// Tags: databar, omnidirectional, barcode, generation, jpeg, aspnet, barcodgenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a continuous DataBar Omnidirectional barcode and saving it as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, configures dimensions, saves the image, and outputs the file path.
    /// </summary>
    static void Main()
    {
        // Define the barcode content (GS1 Application Identifier 01 with GTIN)
        string codeText = "(01)12345678901231";

        // Build a unique temporary file path for the output JPEG
        string outputPath = Path.Combine(Path.GetTempPath(), "DataBarOmni_" + Guid.NewGuid().ToString("N") + ".jpg");

        // Initialize the barcode generator with the desired symbology and text
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarOmniDirectional, codeText))
        {
            // Set the X-dimension (module width) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Set the bar height to 50 pixels as required
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;

            // Save the generated barcode image to the specified path in JPEG format
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the file was saved
        Console.WriteLine("Barcode saved to: " + outputPath);
    }
}