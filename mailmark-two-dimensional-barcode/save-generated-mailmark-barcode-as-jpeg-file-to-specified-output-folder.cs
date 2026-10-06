// Title: Generate and Save Mailmark C-Type Barcode as JPEG
// Description: Demonstrates creating a Mailmark C-type barcode and saving it as a JPEG image file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.Mailmark to produce Mailmark barcodes. Typical use cases include encoding postal information for mail sorting and tracking. Developers often need to customize dimensions and export the barcode to common image formats such as JPEG for integration into documents or web services.
// Prompt: Save the generated Mailmark barcode as a JPEG file to a specified output folder.
// Tags: mailmark, barcode, jpeg, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Mailmark C-type barcode and saves it as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and writes it to the output folder.
    /// </summary>
    /// <param name="args">Optional command‑line argument specifying the output folder path.</param>
    static void Main(string[] args)
    {
        // Determine the output folder: use the first argument if provided; otherwise create a temporary folder.
        string outputFolder;
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            outputFolder = args[0];
        }
        else
        {
            outputFolder = Path.Combine(Path.GetTempPath(), "MailmarkOutput_" + Guid.NewGuid().ToString("N"));
        }

        // Ensure the output directory exists.
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Build the full path for the JPEG file.
        string outputFile = Path.Combine(outputFolder, "MailmarkCType.jpg");

        // Sample Mailmark C-type code text (26 characters).
        string mailmarkCode = "21B2254800659JW5O9QA6Y";

        // Generate the barcode using Aspose.BarCode.
        using (var generator = new BarcodeGenerator(EncodeTypes.Mailmark, mailmarkCode))
        {
            // Optional appearance settings: set module size and bar height.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;

            // Save the generated barcode as a JPEG image.
            generator.Save(outputFile, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"Mailmark barcode saved to: {outputFile}");
    }
}