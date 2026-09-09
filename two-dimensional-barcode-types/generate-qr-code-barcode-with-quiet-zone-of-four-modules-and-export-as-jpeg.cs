// Title: Generate QR Code with custom quiet zone and save as JPEG
// Description: Demonstrates creating a QR Code barcode, setting a quiet zone of four modules, and exporting the image as a JPEG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes to produce QR Code barcodes. It shows how to adjust barcode parameters such as XDimension and padding (quiet zone) before saving the result in a specific image format (JPEG). Developers commonly need to customize quiet zones for scanner compatibility and export barcodes for web or print use.
// Prompt: Generate a QR Code barcode with quiet zone of four modules and export as JPEG.
// Tags: qr code, quiet zone, jpeg, generation, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a QR Code barcode, configures a quiet zone of four modules,
/// and saves the barcode as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_code.jpeg");

        // Initialize the QR Code generator with the desired text (e.g., a URL).
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set the size of a single module (XDimension) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Calculate quiet zone size: 4 modules * XDimension.
            float quietZonePixels = 4f * generator.Parameters.Barcode.XDimension.Pixels;

            // Apply the calculated quiet zone to all four sides of the barcode.
            generator.Parameters.Barcode.Padding.Left.Pixels = quietZonePixels;
            generator.Parameters.Barcode.Padding.Right.Pixels = quietZonePixels;
            generator.Parameters.Barcode.Padding.Top.Pixels = quietZonePixels;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = quietZonePixels;

            // Save the generated barcode as a JPEG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}