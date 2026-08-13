// Title: Generate QR Code with Multilingual Display Text
// Description: Demonstrates how to create a QR Code barcode using Aspose.BarCode, assign a multilingual TwoDDisplayText for visual representation, and save the image as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.QR. It shows how to set human‑readable text (TwoDDisplayText) in multiple languages, a common requirement when QR codes need to convey readable information alongside the encoded data. Developers working with 2‑D barcodes often need to customize display text, choose output formats, and manage file paths.
// Prompt: Generate QR Code barcode and set TwoDDisplayText to multilingual phrase for display.
// Tags: qr code, generation, png, aspose.barcode, twoddisplaytext, multilingual

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a QR Code barcode, sets multilingual display text,
/// and saves the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the QR Code and writes the output file path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the system temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "qr_multilingual.png");

        // Ensure the target directory exists before attempting to save the image.
        string directory = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Initialize the QR code generator with the QR symbology.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the data that will be encoded in the QR code.
            generator.CodeText = "SampleData";

            // Assign a multilingual string to be displayed alongside the QR code.
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "Hello 世界 مرحبا";

            // Save the generated QR code image to the specified path (default format is PNG).
            generator.Save(outputPath);
        }

        // Inform the user where the QR code image has been saved.
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}