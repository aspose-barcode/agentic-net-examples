// Title: Generate and Save Mailmark Barcode as JPEG
// Description: Demonstrates creating a Mailmark barcode using Aspose.BarCode and saving it as a JPEG image to a specified folder.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It showcases the use of the BarcodeGenerator class with EncodeTypes.Mailmark, configuring barcode parameters such as X‑Dimension and Bar Height, and saving the result with BarCodeImageFormat. Typical scenarios include generating shipping labels, tracking codes, or any Mailmark‑based identifiers where developers need to produce high‑quality image files for printing or digital distribution.
// Prompt: Save the generated Mailmark barcode as a JPEG file to a specified output folder.
// Tags: mailmark, barcode, generation, jpeg, aspose.barcode, encode, imageformat

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Mailmark barcode and saves it as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Mailmark barcode image and writes it to the output folder.
    /// </summary>
    /// <param name="args">Optional command‑line argument specifying the output folder.</param>
    static void Main(string[] args)
    {
        // Determine the output folder: use the first argument if provided, otherwise create an "Output" folder in the current directory.
        string outputFolder = args.Length > 0 ? args[0] : Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists.
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Build the full path for the resulting JPEG file.
        string outputPath = Path.Combine(outputFolder, "MailmarkC.jpeg");

        // Sample Mailmark C type code text.
        string codeText = "21B2254800659JW5O9QA6Y";

        // Create a barcode generator for the Mailmark symbology with the specified code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Mailmark, codeText))
        {
            // Set barcode visual parameters.
            generator.Parameters.Barcode.XDimension.Pixels = 4;   // Width of a single module.
            generator.Parameters.Barcode.BarHeight.Pixels = 50; // Height of the barcode bars.

            // Save the generated barcode as a JPEG image.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"Mailmark barcode saved to: {outputPath}");
    }
}