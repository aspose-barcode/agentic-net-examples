// Title: Generate QR Code with custom module size and PNG output
// Description: Demonstrates creating a QR Code barcode, setting a manual module size of 4 pixels, and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure barcode parameters such as XDimension for QR Code symbology. It shows the use of BarcodeGenerator, EncodeTypes, and saving the result to an image file, which developers commonly need when integrating QR codes into applications for branding, marketing, or data encoding.
// Prompt: Generate a QR Code barcode with manual module size of 4 pixels and export as PNG.
// Tags: qr code, barcode generation, manual module size, png output, aspose.barcode, xdimension

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode with a manual module size
/// and saves it as a PNG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Creates a QR Code, configures its XDimension, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Initialize a QR code generator with the QR symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Define the data to encode in the QR code
            generator.CodeText = "Hello World";

            // Set the manual module size (XDimension) to 4 pixels
            generator.Parameters.Barcode.XDimension.Point = 4f;

            // Export the generated barcode as a PNG file named "qr.png"
            generator.Save("qr.png");
        }
    }
}