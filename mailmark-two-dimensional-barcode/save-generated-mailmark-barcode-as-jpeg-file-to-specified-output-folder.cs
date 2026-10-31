// Title: Generate and Save Mailmark Barcode as JPEG
// Description: Creates a Mailmark C-type barcode and saves it as a JPEG image file in a specified output folder.
// Category-Description: This example demonstrates the Aspose.BarCode generation workflow for Mailmark symbology. It uses the BarcodeGenerator class with EncodeTypes.Mailmark to produce a barcode, configures visual parameters, and saves the result in JPEG format. Developers working with postal barcodes or needing to embed Mailmark codes in documents can follow this pattern to generate images programmatically.
// Prompt: Save the generated Mailmark barcode as a JPEG file to a specified output folder.
// Tags: mailmark, barcode, generation, jpeg, save, aspose.barcode, encode-types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a Mailmark barcode and save it as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Mailmark C-type barcode and writes it to a JPEG file.
    /// </summary>
    /// <param name="args">Optional command‑line argument specifying the output folder.</param>
    static void Main(string[] args)
    {
        // Determine the output folder:
        // Use the first command‑line argument if provided; otherwise, fall back to a temporary folder.
        string outputFolder = args.Length > 0
            ? args[0]
            : Path.Combine(Path.GetTempPath(), "MailmarkOutput");

        // Ensure the output directory exists.
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Build the full path for the output JPEG file.
        string outputFile = Path.Combine(outputFolder, "MailmarkC.jpeg");

        // Sample Mailmark C‑type code text to encode.
        string mailmarkCode = "21B2254800659JW5O9QA6Y";

        // Create a BarcodeGenerator for Mailmark symbology and configure visual parameters.
        using (var generator = new BarcodeGenerator(EncodeTypes.Mailmark, mailmarkCode))
        {
            // Set the X‑dimension (module width) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            // Set the barcode height in pixels.
            generator.Parameters.Barcode.BarHeight.Pixels = 50;
            // Save the generated barcode as a JPEG image.
            generator.Save(outputFile, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"Mailmark barcode saved to: {outputFile}");
    }
}