// Title: Generate QR Code and Save as PNG with Overwrite
// Description: Demonstrates how to generate a QR Code barcode using Aspose.BarCode and save it directly to a file path, overwriting any existing file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.QR to create QR Code images. It shows setting code text, configuring error correction, and saving to PNG format. Developers working with barcode creation, especially QR codes, often need to generate images programmatically and control file output behavior.
// Prompt: Generate a QR Code barcode and save directly to file system path with overwrite enabled.
// Tags: qr code, generation, png, aspose.barcode, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a QR Code barcode and saves it as a PNG file,
/// overwriting any existing file at the target location.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the QR Code and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path; the file will be overwritten if it already exists.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "qr.png");

        // Initialize the QR Code generator with the QR symbology.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the text that will be encoded into the QR Code.
            generator.CodeText = "Hello, Aspose QR!";

            // Optionally configure the error correction level (Medium in this case).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR Code image to the specified path.
            // The Save method overwrites any existing file at the same location.
            generator.Save(outputPath);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}