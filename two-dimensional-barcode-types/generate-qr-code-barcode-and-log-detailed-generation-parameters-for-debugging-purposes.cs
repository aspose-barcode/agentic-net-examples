// Title: Generate QR Code and log generation parameters
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, customizing its appearance, and outputting detailed settings for debugging.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to configure QR Code parameters such as error correction, encoding mode, colors, size, and human‑readable text. It uses the BarcodeGenerator class and its Parameters property, which developers commonly employ to tailor barcode images for web, print, or mobile applications. Ideal for developers needing reproducible barcode creation with full parameter visibility.
// Prompt: Generate QR Code barcode and log detailed generation parameters for debugging purposes.
// Tags: qr code, barcode generation, debugging, aspnet, aspose.barcode, image output

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates QR Code generation with detailed parameter logging using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a QR Code, saves it, and writes all generation settings to the console.
    /// </summary>
    static void Main()
    {
        // Output file name for the generated QR Code image
        const string outputFile = "qr.png";

        // Initialize the barcode generator for QR Code with sample text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // ----- Appearance settings -----
            generator.Parameters.BackColor = Color.White;                     // Background color
            generator.Parameters.Barcode.BarColor = Color.Black;             // Bar (module) color

            // Border configuration
            generator.Parameters.Border.Color = Color.Blue;                  // Border color
            generator.Parameters.Border.Width.Pixels = 2f;                    // Border width in pixels

            // Size and resolution settings
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation; // Auto‑size mode
            generator.Parameters.ImageWidth.Point = 300f;                    // Image width (points)
            generator.Parameters.ImageHeight.Point = 300f;                   // Image height (points)
            generator.Parameters.Resolution = 300f;                           // DPI resolution

            // Module (X) dimension
            generator.Parameters.Barcode.XDimension.Point = 2f;              // Size of a single QR module

            // QR‑specific options
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH; // High error correction
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Auto;  // Automatic encoding mode
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8; // UTF‑8 ECI encoding

            // Human‑readable text (code text) styling
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;
            generator.Parameters.Barcode.CodeTextParameters.Color = Color.Black;

            // Save the generated QR Code image to file
            generator.Save(outputFile);

            // ----- Log detailed generation parameters -----
            Console.WriteLine("QR Code generation parameters:");
            Console.WriteLine($"  Output file               : {outputFile}");
            Console.WriteLine($"  CodeText                  : {generator.CodeText}");
            Console.WriteLine($"  AutoSizeMode              : {generator.Parameters.AutoSizeMode}");
            Console.WriteLine($"  ImageWidth (pt)           : {generator.Parameters.ImageWidth.Point}");
            Console.WriteLine($"  ImageHeight (pt)          : {generator.Parameters.ImageHeight.Point}");
            Console.WriteLine($"  Resolution (dpi)          : {generator.Parameters.Resolution}");
            Console.WriteLine($"  XDimension (pt)           : {generator.Parameters.Barcode.XDimension.Point}");
            Console.WriteLine($"  QR ErrorLevel             : {generator.Parameters.Barcode.QR.ErrorLevel}");
            Console.WriteLine($"  QR EncodeMode             : {generator.Parameters.Barcode.QR.EncodeMode}");
            Console.WriteLine($"  QR ECIEncoding            : {generator.Parameters.Barcode.QR.ECIEncoding}");
            Console.WriteLine($"  BarColor                  : {generator.Parameters.Barcode.BarColor}");
            Console.WriteLine($"  BackColor                 : {generator.Parameters.BackColor}");
            Console.WriteLine($"  Border Color              : {generator.Parameters.Border.Color}");
            Console.WriteLine($"  Border Width (px)         : {generator.Parameters.Border.Width.Pixels}");
            Console.WriteLine($"  CodeText Font Family      : {generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName}");
            Console.WriteLine($"  CodeText Font Size (pt)   : {generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point}");
            Console.WriteLine($"  CodeText Location         : {generator.Parameters.Barcode.CodeTextParameters.Location}");
            Console.WriteLine($"  CodeText Alignment        : {generator.Parameters.Barcode.CodeTextParameters.Alignment}");
            Console.WriteLine($"  CodeText Color            : {generator.Parameters.Barcode.CodeTextParameters.Color}");
        }
    }
}