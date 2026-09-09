// Title: Generate QR Code and Log Generation Parameters
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, configuring key settings, and outputting detailed generation parameters for debugging.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and related parameter classes. Typical use cases include generating QR codes for URLs, contact information, or product data while needing to inspect or log the exact generation settings for troubleshooting or audit purposes. Developers often need to adjust dimensions, error correction levels, colors, and padding, then verify those settings programmatically.
// Prompt: Generate QR Code barcode and log detailed generation parameters for debugging purposes.
// Tags: qr, barcode, generation, debugging, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR Code barcode and logs detailed generation parameters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the QR Code, logs parameters, and saves the image.
    /// </summary>
    static void Main()
    {
        // Prepare output directory and file path
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "qr_code.png");

        // Sample QR code text
        string codeText = "Sample QR Code for debugging";

        // Create and configure the barcode generator
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set generation parameters
            generator.Parameters.Barcode.XDimension.Pixels = 4f;                     // Size of a single module in pixels
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;      // Error correction level
            generator.Parameters.Resolution = 300f;                                 // Image resolution (DPI)
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;              // Auto-size mode for optimal dimensions
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;    // Color of the QR modules
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;           // Background color
            generator.Parameters.RotationAngle = 0f;                                // No rotation
            generator.Parameters.Barcode.Padding.Left.Point = 5f;                  // Left padding
            generator.Parameters.Barcode.Padding.Top.Point = 5f;                   // Top padding
            generator.Parameters.Barcode.Padding.Right.Point = 5f;                // Right padding
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;               // Bottom padding

            // Log detailed parameters for debugging
            Console.WriteLine("Generating QR Code with the following parameters:");
            Console.WriteLine($"Encode Type: {EncodeTypes.QR}");
            Console.WriteLine($"Code Text: {codeText}");
            Console.WriteLine($"XDimension (Pixels): {generator.Parameters.Barcode.XDimension.Pixels}");
            Console.WriteLine($"Error Level: {generator.Parameters.Barcode.QR.ErrorLevel}");
            Console.WriteLine($"Resolution (DPI): {generator.Parameters.Resolution}");
            Console.WriteLine($"AutoSizeMode: {generator.Parameters.AutoSizeMode}");
            Console.WriteLine($"Bar Color: {generator.Parameters.Barcode.BarColor}");
            Console.WriteLine($"Background Color: {generator.Parameters.BackColor}");
            Console.WriteLine($"Rotation Angle: {generator.Parameters.RotationAngle}");
            Console.WriteLine($"Padding (L,T,R,B) Points: {generator.Parameters.Barcode.Padding.Left.Point}, {generator.Parameters.Barcode.Padding.Top.Point}, {generator.Parameters.Barcode.Padding.Right.Point}, {generator.Parameters.Barcode.Padding.Bottom.Point}");

            // Save the barcode image to PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"QR Code saved to: {outputPath}");
        }
    }
}