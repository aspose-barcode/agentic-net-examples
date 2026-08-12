// Title: Generate and Rotate QR Code Barcode to PNG
// Description: Demonstrates creating a QR Code barcode, rotating it 90 degrees clockwise, and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class to produce QR Code symbologies, apply image transformations such as rotation, and export the result in common raster formats like PNG. Typical use cases include creating printable QR codes for marketing, product labeling, or mobile app linking, where developers often need to adjust orientation for layout constraints.
// Prompt: Generate a QR Code barcode rotated ninety degrees clockwise and export as PNG.
// Tags: qr code, rotation, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a QR Code, rotates it, and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Build the full path for the output PNG file in the system's temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "qr_rotated.png");

        // Initialize a BarcodeGenerator for QR Code with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Apply a 90-degree clockwise rotation to the generated barcode image.
            generator.Parameters.RotationAngle = 90f;

            // Persist the rotated barcode to disk in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the PNG file has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}