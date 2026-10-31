// Title: Generate QR Code barcode and save as PNG
// Description: Demonstrates creating a QR Code from alphanumeric text using Aspose.BarCode and writing the image to a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.QR to produce QR Code barcodes. Typical use cases include encoding URLs, contact information, or any alphanumeric data for mobile scanning. Developers often need to generate barcode images programmatically for web, desktop, or mobile applications, and this snippet shows the essential steps: instantiate the generator, set the data, and save the image.
// Prompt: Generate a QR Code barcode from alphanumeric text and save as PNG image file.
// Tags: qr code, barcode generation, png output, aspose.barcode, encode types, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode from provided text and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts optional command‑line argument for the QR Code text; otherwise uses a default string.
    /// Generates the QR Code and writes it to "qr.png" in the current directory.
    /// </summary>
    static void Main(string[] args)
    {
        // Determine the text to encode; use first argument if supplied, else default.
        string codeText = "Hello World 123";
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            codeText = args[0];
        }

        // Build the full output file path in the current working directory.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "qr.png");

        // Create a BarcodeGenerator for QR Code with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}