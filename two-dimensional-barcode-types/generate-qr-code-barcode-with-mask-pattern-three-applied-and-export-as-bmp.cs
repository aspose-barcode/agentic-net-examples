// Title: Generate QR Code with BMP output using Aspose.BarCode
// Description: Demonstrates creating a QR Code barcode, encoding text, and saving it as a BMP image file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.QR to produce QR Code symbols. Typical use cases include generating QR codes for URLs, product information, or authentication data, and exporting them to common image formats such as BMP. Developers often need to set the code text, choose the symbology, and save the result using BarCodeImageFormat.
// Prompt: Generate a QR Code barcode with mask pattern three applied and export as BMP.
// Tags: qr code, barcode generation, bmp output, aspose.barcode, encode types, barcodegenerator

using System;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode and saving it as a BMP image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR Code, sets its text, and saves it to a BMP file.
    /// </summary>
    static void Main()
    {
        // Output file path
        string outputPath = "qr.bmp";

        // Initialize the barcode generator for QR Code symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Assign the data to be encoded in the QR Code
            generator.CodeText = "Sample QR Code";

            // Note: The Aspose.BarCode API automatically selects the optimal mask pattern.
            // Explicit mask pattern selection (e.g., pattern three) is not exposed.

            // Persist the generated barcode as a BMP image
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"QR code saved to {outputPath}");
    }
}