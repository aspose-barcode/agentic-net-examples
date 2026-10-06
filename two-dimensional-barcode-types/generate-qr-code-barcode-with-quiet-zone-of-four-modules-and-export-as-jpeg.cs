// Title: Generate QR Code with Custom Quiet Zone and Save as JPEG
// Description: This example creates a QR Code barcode, configures a quiet zone of four modules, and exports the result as a JPEG image.
// Category-Description: Demonstrates Aspose.BarCode barcode generation using the BarcodeGenerator class with EncodeTypes.QR. Typical scenarios include creating QR codes for URLs, product information, or contact data, where developers need to control module size, quiet zone, and output image format. This example belongs to the barcode creation and image export category, showcasing common API members such as Parameters.Barcode.XDimension, Padding, and Save.
// Prompt: Generate a QR Code barcode with quiet zone of four modules and export as JPEG.
// Tags: qr code, quiet zone, jpeg, aspose.barcode, barcode generation, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a QR Code with a specific quiet zone and save it as a JPEG file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the QR Code and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr.jpeg");

        // Text to encode in the QR Code (e.g., a URL).
        string codeText = "https://example.com";

        // Initialize the barcode generator for QR Code symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set the module (X) dimension to 2 pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Calculate quiet zone size: four modules * X dimension.
            float quietZone = 4f * generator.Parameters.Barcode.XDimension.Pixels;

            // Apply the calculated quiet zone to all sides of the barcode.
            generator.Parameters.Barcode.Padding.Left.Pixels = quietZone;
            generator.Parameters.Barcode.Padding.Right.Pixels = quietZone;
            generator.Parameters.Barcode.Padding.Top.Pixels = quietZone;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = quietZone;

            // Save the generated barcode as a JPEG image.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the QR Code image was saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}