// Title: Generate QR Code with Square Aspect Ratio
// Description: Creates a QR Code barcode and forces a square aspect ratio to ensure consistent sizing across outputs.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.QR to produce QR Code images. Developers commonly use these APIs to embed data such as URLs, product identifiers, or contact information into QR codes, and may need to control dimensions like XDimension and aspect ratio for uniform appearance in UI or print layouts. The example demonstrates setting pixel density and a square aspect ratio, then saving the result as a PNG file.
/// Prompt: Generate QR Code barcode and set aspect ratio to square for consistent sizing.
// Tags: qr code, barcode generation, aspect ratio, square, aspose.barcode, png, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode with a square aspect ratio using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the QR code and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarCodeDemo");
        Directory.CreateDirectory(outputDir);

        // Build the full path for the resulting PNG image
        string outputPath = Path.Combine(outputDir, "QrSquare.png");

        // Initialize the barcode generator for QR code with the desired text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "ASPOSE"))
        {
            // Set the module size (pixel dimension) for the QR code
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Force a square aspect ratio (1:1) for consistent sizing
            generator.Parameters.Barcode.QR.AspectRatio = 1;

            // Save the generated QR code as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR code image has been saved
        Console.WriteLine($"QR code saved to {outputPath}");
    }
}