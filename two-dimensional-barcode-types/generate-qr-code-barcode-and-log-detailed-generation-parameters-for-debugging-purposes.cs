// Title: Generate QR Code and Log Parameters
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, customizing its appearance and QR‑specific settings, saving it as PNG, and outputting all generation parameters for debugging.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with QR Code symbology. It shows typical use cases such as setting module size, error correction level, version, encoding mode, ECI encoding, colors, padding, resolution, and rotation. Developers working with barcode creation often need to fine‑tune these parameters and log them for troubleshooting or audit purposes.
// Prompt: Generate QR Code barcode and log detailed generation parameters for debugging purposes.
// Tags: qr code, barcode generation, debugging, parameters, aspose.barcode, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Sample program that generates a QR Code barcode, saves it as a PNG file,
/// and writes detailed generation parameters to the console for debugging.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Determine a temporary file path for the output image.
        string outputPath = Path.Combine(Path.GetTempPath(), "SampleQrCode.png");

        // Initialize the QR Code generator with the desired text to encode.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // -----------------------------------------------------------------
            // QR Code specific configuration
            // -----------------------------------------------------------------

            // Set the size of a single QR module (pixel dimension).
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Configure QR error correction level, version, encoding mode, and ECI encoding.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
            generator.Parameters.Barcode.QR.Version = QRVersion.Version05;
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Auto;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            // -----------------------------------------------------------------
            // Visual appearance settings
            // -----------------------------------------------------------------

            // Define foreground (barcode) and background colors.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Set padding around the barcode in points.
            generator.Parameters.Barcode.Padding.Left.Point = 5f;
            generator.Parameters.Barcode.Padding.Top.Point = 5f;
            generator.Parameters.Barcode.Padding.Right.Point = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;

            // Define image resolution (DPI) and rotation angle.
            generator.Parameters.Resolution = 300f;
            generator.Parameters.RotationAngle = 0f;

            // -----------------------------------------------------------------
            // Save the generated barcode image to the file system.
            // -----------------------------------------------------------------
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // -----------------------------------------------------------------
            // Log all relevant generation parameters for debugging purposes.
            // -----------------------------------------------------------------
            Console.WriteLine("QR Code Generation Parameters:");
            Console.WriteLine($"  CodeText               : {generator.CodeText}");
            Console.WriteLine($"  XDimension (Pixels)    : {generator.Parameters.Barcode.XDimension.Pixels}");
            Console.WriteLine($"  QR ErrorLevel          : {generator.Parameters.Barcode.QR.ErrorLevel}");
            Console.WriteLine($"  QR Version             : {generator.Parameters.Barcode.QR.Version}");
            Console.WriteLine($"  QR EncodeMode          : {generator.Parameters.Barcode.QR.EncodeMode}");
            Console.WriteLine($"  QR ECIEncoding         : {generator.Parameters.Barcode.QR.ECIEncoding}");
            Console.WriteLine($"  BarColor               : {generator.Parameters.Barcode.BarColor}");
            Console.WriteLine($"  BackColor              : {generator.Parameters.BackColor}");
            Console.WriteLine($"  Padding Left (Point)   : {generator.Parameters.Barcode.Padding.Left.Point}");
            Console.WriteLine($"  Padding Top (Point)    : {generator.Parameters.Barcode.Padding.Top.Point}");
            Console.WriteLine($"  Padding Right (Point)  : {generator.Parameters.Barcode.Padding.Right.Point}");
            Console.WriteLine($"  Padding Bottom (Point) : {generator.Parameters.Barcode.Padding.Bottom.Point}");
            Console.WriteLine($"  Resolution (DPI)       : {generator.Parameters.Resolution}");
            Console.WriteLine($"  RotationAngle (degrees): {generator.Parameters.RotationAngle}");
            Console.WriteLine($"  Output file            : {outputPath}");
        }
    }
}