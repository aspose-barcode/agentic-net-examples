// Title: Generate QR Code with Transparent Background
// Description: Creates a QR Code barcode and saves it as a PNG with a fully transparent background, suitable for overlay on colored images.
// Category-Description: This example demonstrates Aspose.BarCode barcode generation focusing on QR Code creation and background transparency. It uses the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce a PNG image where the background alpha channel is set to zero. Developers often need transparent barcodes to embed them over UI elements, reports, or graphics without obscuring underlying colors.
// Prompt: Generate QR Code barcode and set background transparency to allow overlay on colored backgrounds.
// Tags: qr code, barcode generation, transparent background, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a QR Code barcode with a fully transparent background
/// and save it as a PNG file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the QR Code and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the output file
        string tempDir = Path.Combine(Path.GetTempPath(), "QrTransparent_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the full path for the PNG image to be saved
        string outputPath = Path.Combine(tempDir, "qr_transparent.png");

        // Initialize the barcode generator for a QR Code with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Aspose QR with transparent background"))
        {
            // Set the barcode (foreground) color to black
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Set the background color to fully transparent (alpha = 0)
            generator.Parameters.BackColor = Color.FromArgb(0, 255, 255, 255);

            // Optional: adjust the size of each QR module (pixel size)
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode as a PNG image preserving transparency
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the generated image
        Console.WriteLine("QR Code generated with transparent background at:");
        Console.WriteLine(outputPath);
    }
}