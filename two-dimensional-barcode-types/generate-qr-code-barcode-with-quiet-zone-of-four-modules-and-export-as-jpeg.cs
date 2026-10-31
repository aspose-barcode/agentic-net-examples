// Title: Generate QR Code with custom quiet zone and export as JPEG
// Description: Demonstrates creating a QR Code barcode, configuring a quiet zone of four modules, and saving the result as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.QR to produce QR Code barcodes. Typical use cases include generating QR codes for URLs, contact information, or product data, where developers often need to control module size and quiet zone padding before exporting to common image formats such as JPEG or PNG.
// Prompt: Generate a QR Code barcode with quiet zone of four modules and export as JPEG.
// Tags: qr code, quiet zone, jpeg, aspose.barcode, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode with a custom quiet zone and saving it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, configures barcode parameters, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting JPEG file
        string outputPath = Path.Combine(outputDir, "qrcode.jpg");

        // Initialize the barcode generator for QR Code with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Set the module (X) dimension to 2 points
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Calculate padding to achieve a quiet zone of four modules on each side
            float quietModules = 4f;
            float padding = quietModules * generator.Parameters.Barcode.XDimension.Point;

            // Apply the calculated padding to all sides of the barcode
            generator.Parameters.Barcode.Padding.Left.Point = padding;
            generator.Parameters.Barcode.Padding.Right.Point = padding;
            generator.Parameters.Barcode.Padding.Top.Point = padding;
            generator.Parameters.Barcode.Padding.Bottom.Point = padding;

            // Save the generated barcode as a JPEG image
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the QR code image was saved
        Console.WriteLine($"QR code saved to {outputPath}");
    }
}