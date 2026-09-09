// Title: Generate DataBar Omnidirectional barcode as JPEG
// Description: Demonstrates creating a continuous DataBar Omnidirectional barcode with a 50‑pixel bar height and saving it as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.DatabarOmniDirectional. Typical use cases include encoding GS1 data for retail, inventory, or logistics applications where high‑density, omnidirectional barcodes are required. Developers often need to set dimensions such as X‑dimension and bar height before exporting to common image formats like JPEG.
// Prompt: Produce continuous DataBar Omnidirectional barcodes with bar height 50 pixels, output JPEG.
// Tags: databars, omnidirectional, barcode, generation, jpeg, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a DataBar Omnidirectional barcode and saves it as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates output directory, generates the barcode, and saves it.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the output directory relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        // Ensure the directory exists.
        Directory.CreateDirectory(outputDir);

        // Build the full file path for the resulting JPEG image.
        string filePath = Path.Combine(outputDir, "DataBarOmniDirectional.jpg");

        // Initialize the barcode generator with the desired symbology and data.
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarOmniDirectional, "(01)12345678901231"))
        {
            // Set the X‑dimension (module width) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2;
            // Set the bar height to 50 pixels as required.
            generator.Parameters.Barcode.BarHeight.Pixels = 50;

            // Save the generated barcode image as a JPEG file.
            generator.Save(filePath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {filePath}");
    }
}