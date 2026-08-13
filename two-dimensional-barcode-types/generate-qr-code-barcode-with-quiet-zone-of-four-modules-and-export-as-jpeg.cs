// Title: Generate QR Code with Default Quiet Zone and Save as JPEG
// Description: This example creates a QR Code barcode, uses the default quiet zone of four modules, and saves the image as a JPEG file.
// Category-Description: Demonstrates Aspose.BarCode barcode generation for QR Code symbology, covering configuration of encoding, quiet zone handling, and image export. The key API classes are BarcodeGenerator, EncodeTypes, and BarCodeImageFormat. Typical use cases include creating QR codes for URLs or product information and exporting them to common image formats for web or print.
// Prompt: Generate a QR Code barcode with quiet zone of four modules and export as JPEG.
// Tags: qr code,quiet zone,jpeg,barcode generation,aspose.barcode,encode types,barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode and saves it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path (saved in the current working directory)
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qrcode.jpg");

        // Initialize a QR Code generator with the QR symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the data to be encoded in the QR Code
            generator.CodeText = "https://example.com";

            // The default quiet zone for QR Code is 4 modules, which meets the requirement.
            // Export the generated barcode to a JPEG image file
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the QR Code image has been saved
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}