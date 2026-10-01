// Title: Generate QR Code and Save as PNG with 300 DPI
// Description: This example creates a QR code containing a URL and saves it as a PNG image with a resolution of 300 DPI.
// Category-Description: Demonstrates Aspose.BarCode generation capabilities, focusing on the BarcodeGenerator class to encode QR symbology. Typical use cases include creating scannable QR codes for marketing, authentication, or product information, where developers need control over image format and resolution. This snippet belongs to a collection of examples illustrating barcode creation, format selection, and image output settings.
// Prompt: Generate a QR code and save it as a PNG file with 300 DPI resolution.
// Tags: qr code, barcode generation, png, resolution, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR code and saving it as a PNG file with 300 DPI resolution using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the QR code and writes the output file path to the console.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the QR code.
        string codeText = "https://example.com";

        // Build the full path for the output PNG file in the system's temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "qr_code.png");

        // Initialize the QR code generator with the desired symbology and content.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Configure the image resolution to 300 DPI for high-quality output.
            generator.Parameters.Resolution = 300f;

            // Save the generated QR code as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR code image has been saved.
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}