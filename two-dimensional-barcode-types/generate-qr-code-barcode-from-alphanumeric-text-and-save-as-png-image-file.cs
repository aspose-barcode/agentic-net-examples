// Title: Generate QR Code barcode and save as PNG
// Description: Demonstrates creating a QR Code from alphanumeric text using Aspose.BarCode and writing it to a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.QR to produce QR Code barcodes. Typical use cases include encoding URLs, product IDs, or any alphanumeric data for mobile scanning. Developers often need to set the CodeText, choose an image format, and save the result to a file.
// Prompt: Generate a QR Code barcode from alphanumeric text and save as PNG image file.
// Tags: qr code, barcode generation, png output, aspose.barcode, encode types, barcodegenerator

using System;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode from alphanumeric text and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a BarcodeGenerator for QR Code, sets the text, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define the output file path where the PNG image will be written
        string outputPath = "qr.png";

        // Initialize a BarcodeGenerator with QR Code symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Assign the alphanumeric text to be encoded in the QR Code
            generator.CodeText = "Sample123";

            // Persist the generated barcode as a PNG image file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user of the successful operation
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}