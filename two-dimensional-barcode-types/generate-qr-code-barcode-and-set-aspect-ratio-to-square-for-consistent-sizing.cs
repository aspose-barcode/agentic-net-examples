// Title: Generate a QR Code with a square aspect ratio
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode and forcing a 1:1 aspect ratio for consistent square sizing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure QR Code parameters such as X‑Dimension and AspectRatio. Developers commonly use these APIs to produce QR codes for web URLs, product IDs, or authentication tokens, requiring precise control over size and shape for UI consistency. The key classes shown are BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, which are typical in barcode creation workflows.
// Prompt: Generate QR Code barcode and set aspect ratio to square for consistent sizing.
// Tags: qr code, barcode generation, aspect ratio, square, aspose.barcode, png output

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
    /// Entry point. Creates output directory, generates QR code, saves as PNG, and writes the file path.
    /// </summary>
    static void Main(string[] args)
    {
        // Define a temporary output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeQrDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated PNG file
        string outputPath = Path.Combine(outputDir, "QrSquare.png");

        // Initialize the barcode generator for QR code with sample data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR"))
        {
            // Set the size of each QR module (pixel dimension)
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Force a square aspect ratio (1:1) for consistent sizing
            generator.Parameters.Barcode.QR.AspectRatio = 1;

            // Save the generated QR code as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR code image was saved
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}