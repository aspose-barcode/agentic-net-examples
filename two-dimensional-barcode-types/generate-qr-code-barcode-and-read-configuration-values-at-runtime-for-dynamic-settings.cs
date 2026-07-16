// Title: Generate QR Code and Read It with Runtime Configuration
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode with parameters supplied via command‑line arguments, then reads the barcode back to verify its content.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to use BarcodeGenerator and BarCodeReader classes. Typical use cases include dynamic barcode creation based on runtime settings and immediate validation of the generated image. Developers often need to adjust error correction level, ECI encoding, and visual properties programmatically.
// Prompt: Generate a QR Code barcode and read configuration values at runtime for dynamic settings.
// Tags: qr code, barcode generation, barcode reading, runtime configuration, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR Code barcode with runtime‑configurable settings
/// and then reads the generated image to verify its content.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts optional command‑line arguments to customize the QR Code:
    /// 0 – code text, 1 – error correction level, 2 – ECI encoding.
    /// </summary>
    /// <param name="args">Command‑line arguments.</param>
    static void Main(string[] args)
    {
        // Default barcode parameters
        string codeText = "Hello Aspose QR";
        QRErrorLevel errorLevel = QRErrorLevel.LevelM;
        ECIEncodings? eciEncoding = null;

        // --------------------------------------------------------------------
        // Parse command‑line arguments (if any) to override defaults
        // --------------------------------------------------------------------
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            codeText = args[0];
        }

        if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
        {
            if (Enum.TryParse<QRErrorLevel>(args[1], true, out var parsedLevel))
            {
                errorLevel = parsedLevel;
            }
            else
            {
                Console.WriteLine($"Invalid QR error level '{args[1]}', using default LevelM.");
            }
        }

        if (args.Length > 2 && !string.IsNullOrWhiteSpace(args[2]))
        {
            if (Enum.TryParse<ECIEncodings>(args[2], true, out var parsedEci))
            {
                eciEncoding = parsedEci;
            }
            else
            {
                Console.WriteLine($"Invalid ECI encoding '{args[2]}', ignoring.");
            }
        }

        // Output file for the generated barcode image
        string outputPath = "qr.png";

        // --------------------------------------------------------------------
        // Generate QR Code using Aspose.BarCode
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Apply dynamic settings based on parsed arguments
            generator.Parameters.Barcode.QR.ErrorLevel = errorLevel;
            if (eciEncoding.HasValue)
            {
                generator.Parameters.Barcode.QR.ECIEncoding = eciEncoding.Value;
            }

            // Optional visual customizations
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Parameters.ImageWidth.Point = 300f;   // Width in points
            generator.Parameters.ImageHeight.Point = 300f; // Height in points

            // Save the barcode image to PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Verify that the image file was created successfully
        // --------------------------------------------------------------------
        if (!File.Exists(outputPath))
        {
            Console.WriteLine($"Failed to create barcode image at '{outputPath}'.");
            return;
        }

        // --------------------------------------------------------------------
        // Read and decode the generated QR Code to confirm its content
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(outputPath, DecodeType.QR))
        {
            // Use a high‑quality preset for robust reading
            reader.QualitySettings = QualitySettings.HighQuality;

            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type : {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text  : {result.CodeText}");
                Console.WriteLine($"Confidence    : {result.Confidence}");
                Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");

                // Region bounds (optional diagnostic information)
                var bounds = result.Region.Rectangle;
                Console.WriteLine($"Region Bounds : X={bounds.X}, Y={bounds.Y}, W={bounds.Width}, H={bounds.Height}");
            }
        }
    }
}