// Title: Generate QR Code and Log Parameters
// Description: Demonstrates how to create a QR Code barcode with Aspose.BarCode, configure its properties, and output detailed generation settings for debugging.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes.QR, and related parameter classes (e.g., QRErrorLevel, QRVersion, ECIEncodings). Typical scenarios include creating QR codes for URLs, product data, or authentication tokens, where developers need to fine‑tune dimensions, error correction, and output resolution. The snippet shows how to retrieve and log these settings, a common requirement for troubleshooting and ensuring consistent barcode quality across deployments.
// Prompt: Generate QR Code barcode and log detailed generation parameters for debugging purposes.
// Tags: qr code, barcode generation, debugging, parameters, aspnet, aspose.barcode, image output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code using Aspose.BarCode,
/// logs all relevant generation parameters, and saves the image to disk.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Prepare the output directory where barcode images will be saved.
        string outputDir = Path.Combine(Environment.CurrentDirectory, "Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define file paths and the text to encode in the QR Code.
        string outputPath = Path.Combine(outputDir, "qr.png");
        string codeText = "Aspose QR Code Example";

        // Initialize the QR Code generator with the desired symbology and data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Configure generation parameters for size, error correction, version, encoding, resolution, rotation, and padding.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
            generator.Parameters.Barcode.QR.Version = QRVersion.Auto;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;
            generator.Parameters.Resolution = 300f;
            generator.Parameters.RotationAngle = 0f;
            generator.Parameters.Barcode.Padding.Left.Point = 2f;
            generator.Parameters.Barcode.Padding.Top.Point = 2f;
            generator.Parameters.Barcode.Padding.Right.Point = 2f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 2f;

            // Log all configured parameters to the console for debugging purposes.
            Console.WriteLine("QR Code Generation Parameters:");
            Console.WriteLine($"  CodeText: {generator.CodeText}");
            Console.WriteLine($"  XDimension (Pixels): {generator.Parameters.Barcode.XDimension.Pixels}");
            Console.WriteLine($"  ErrorLevel: {generator.Parameters.Barcode.QR.ErrorLevel}");
            Console.WriteLine($"  Version: {generator.Parameters.Barcode.QR.Version}");
            Console.WriteLine($"  ECIEncoding: {generator.Parameters.Barcode.QR.ECIEncoding}");
            Console.WriteLine($"  Resolution (dpi): {generator.Parameters.Resolution}");
            Console.WriteLine($"  RotationAngle: {generator.Parameters.RotationAngle}");
            Console.WriteLine($"  Padding (Points) - Left: {generator.Parameters.Barcode.Padding.Left.Point}, Top: {generator.Parameters.Barcode.Padding.Top.Point}, Right: {generator.Parameters.Barcode.Padding.Right.Point}, Bottom: {generator.Parameters.Barcode.Padding.Bottom.Point}");

            // Generate the QR Code bitmap and log its dimensions.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                Console.WriteLine($"Generated bitmap size: {bitmap.Width}x{bitmap.Height}");
                // Save the bitmap to a PNG file.
                bitmap.Save(outputPath, ImageFormat.Png);
            }

            // Additionally, save the QR Code using the generator's built‑in Save method.
            string alternatePath = Path.Combine(outputDir, "qr_generator_save.png");
            generator.Save(alternatePath, BarCodeImageFormat.Png);
            Console.WriteLine($"QR code saved to: {outputPath}");
            Console.WriteLine($"QR code also saved via generator.Save to: {alternatePath}");
        }
    }
}