// Title: Generate QR Code with Zero Margin and Save as JPEG
// Description: This example creates a QR Code barcode with no padding and saves it as a JPEG image.
// Category-Description: Demonstrates Aspose.BarCode barcode generation for QR Code symbology, focusing on margin (padding) configuration and image export. Uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical use cases include creating compact QR codes for web or print where extra whitespace must be avoided. Developers often need to adjust padding and choose output formats when integrating barcode generation into .NET applications.
// Prompt: Generate a QR Code barcode with margin set to zero and save as JPEG image.
// Tags: qr code, barcode generation, margin, padding, jpeg, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code with zero margins and saves it as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_zero_margin.jpg");

        // Initialize the barcode generator for QR Code symbology with the desired text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Set all padding (margin) values to zero to eliminate whitespace around the QR Code.
            generator.Parameters.Barcode.Padding.Left.Point = 0f;
            generator.Parameters.Barcode.Padding.Top.Point = 0f;
            generator.Parameters.Barcode.Padding.Right.Point = 0f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 0f;

            // Save the generated barcode as a JPEG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}