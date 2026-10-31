// Title: Generate QR Code barcode with runtime configuration
// Description: Demonstrates creating a QR Code using Aspose.BarCode, allowing dynamic settings via command‑line arguments.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the BarcodeGenerator and BarCodeReader classes for QR Code creation, customization of error correction level, version, and module size, and subsequent decoding. Developers often need to generate barcodes with variable parameters and verify them programmatically, making this pattern useful for batch processing, reporting, or automated testing.
// Prompt: Generate a QR Code barcode and read configuration values at runtime for dynamic settings.
// Tags: qr code, barcode generation, barcode recognition, runtime configuration, aspose.barcode, csharp

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR Code barcode using Aspose.BarCode,
/// saves it as an image, and then reads it back to verify the encoded text.
/// Configuration values such as the code text, error correction level,
/// QR version, and module size can be supplied via command‑line arguments.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses optional arguments, creates the QR Code, saves it,
    /// and decodes it to display the result.
    /// </summary>
    /// <param name="args">
    /// Optional command‑line arguments:
    /// 0 – code text,
    /// 1 – error correction level (L, M, Q, H),
    /// 2 – QR version (Auto or 1‑40),
    /// 3 – X‑dimension in pixels.
    /// </param>
    static void Main(string[] args)
    {
        // Default configuration values
        string codeText = "Aspose";
        QRErrorLevel errorLevel = QRErrorLevel.LevelM;
        QRVersion version = QRVersion.Auto;
        float xDimensionPixels = 4f;
        string outputPath = "qr.png";

        // -----------------------------------------------------------------
        // Parse command‑line arguments (fallback to defaults)
        // -----------------------------------------------------------------
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            codeText = args[0];

        if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
        {
            switch (args[1].ToUpperInvariant())
            {
                case "L": errorLevel = QRErrorLevel.LevelL; break;
                case "M": errorLevel = QRErrorLevel.LevelM; break;
                case "Q": errorLevel = QRErrorLevel.LevelQ; break;
                case "H": errorLevel = QRErrorLevel.LevelH; break;
            }
        }

        if (args.Length > 2 && !string.IsNullOrWhiteSpace(args[2]))
        {
            string vStr = args[2].Trim();
            if (vStr.Equals("Auto", StringComparison.OrdinalIgnoreCase))
            {
                version = QRVersion.Auto;
            }
            else if (int.TryParse(vStr, out int vNum) && vNum >= 1 && vNum <= 40)
            {
                // Map numeric version to the corresponding QRVersion field
                string fieldName = vNum < 10 ? $"Version0{vNum}" : $"Version{vNum}";
                var field = typeof(QRVersion).GetField(fieldName);
                if (field != null)
                    version = (QRVersion)field.GetValue(null);
            }
        }

        if (args.Length > 3 && float.TryParse(args[3], out float xDim) && xDim > 0)
            xDimensionPixels = xDim;

        // -----------------------------------------------------------------
        // Ensure the output directory exists
        // -----------------------------------------------------------------
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // -----------------------------------------------------------------
        // Generate QR Code using BarcodeGenerator
        // -----------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = xDimensionPixels;
            generator.Parameters.Barcode.QR.ErrorLevel = errorLevel;
            generator.Parameters.Barcode.QR.Version = version;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"QR code saved to: {outputPath}");

        // -----------------------------------------------------------------
        // Read back the generated barcode to verify its content
        // -----------------------------------------------------------------
        if (File.Exists(outputPath))
        {
            using (var reader = new BarCodeReader(outputPath, DecodeType.QR))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Decoded Text: {result.CodeText}");
                }
            }
        }
        else
        {
            Console.WriteLine("Failed to locate the generated barcode image.");
        }
    }
}