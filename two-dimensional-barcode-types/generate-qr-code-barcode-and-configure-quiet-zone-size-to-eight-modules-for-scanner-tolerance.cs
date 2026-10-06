// Title: Generate QR Code with Custom Quiet Zone
// Description: Demonstrates how to generate a QR Code barcode and set a quiet zone of eight modules to improve scanner tolerance.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to configure visual parameters such as module size and quiet zone padding. It uses the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create and export a QR Code image. Developers often need to adjust quiet zones for reliable scanning in various environments, making this pattern useful for QR Code customization.
// Prompt: Generate QR Code barcode and configure quiet zone size to eight modules for scanner tolerance.
// Tags: qr code, quiet zone, barcode generation, aspose.barcode, png, image output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a QR Code barcode with a custom quiet zone of eight modules.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the QR Code and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Prepare the output directory where the generated image will be stored.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Define the text to encode in the QR Code.
        string codeText = "Hello World";

        // Initialize the QR Code generator with the desired symbology and content.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set the size of a single QR module (XDimension) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Compute the quiet zone size: eight modules multiplied by the module size.
            float quietZone = 8f * generator.Parameters.Barcode.XDimension.Pixels;

            // Apply the calculated quiet zone as padding on all four sides of the barcode.
            generator.Parameters.Barcode.Padding.Left.Pixels = quietZone;
            generator.Parameters.Barcode.Padding.Top.Pixels = quietZone;
            generator.Parameters.Barcode.Padding.Right.Pixels = quietZone;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = quietZone;

            // Define the full path for the output PNG file.
            string outputPath = Path.Combine(outputDir, "QRCode_QuietZone8.png");

            // Save the generated QR Code image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the image has been saved.
            Console.WriteLine($"QR Code saved to: {outputPath}");
        }
    }
}