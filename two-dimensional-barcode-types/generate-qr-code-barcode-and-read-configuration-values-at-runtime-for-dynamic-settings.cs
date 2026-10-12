// Title: Generate QR Code barcode with runtime configuration
// Description: Demonstrates creating a QR Code using Aspose.BarCode, where the barcode text, error correction level, version, and module size are supplied via command‑line arguments or defaults.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator to produce a QR Code, configure QR‑specific parameters such as error correction level and version, save the image, and then read it back with BarCodeReader. Developers working with dynamic barcode creation and validation often need to adjust settings at runtime, making this pattern useful for batch processing, automated testing, or user‑driven applications.
// Prompt: Generate a QR Code barcode and read configuration values at runtime for dynamic settings.
// Tags: qr code, barcode generation, barcode recognition, runtime configuration, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a QR Code barcode using runtime‑provided settings
/// and then reads the generated barcode to verify its content.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts optional command‑line arguments to customize the QR Code
    /// and demonstrates both generation and recognition using Aspose.BarCode.
    /// </summary>
    /// <param name="args">
    /// args[0] – barcode text (default: "Aspose")
    /// args[1] – error correction level (L, M, Q, H; default: "H")
    /// args[2] – QR version number (0‑40; 0 = auto; default: 0)
    /// args[3] – X‑dimension in pixels (float > 0; default: 4)
    /// </param>
    static void Main(string[] args)
    {
        // --------------------------------------------------------------------
        // Default configuration values
        // --------------------------------------------------------------------
        string codeText = "Aspose";
        string errorLevelStr = "H";
        int versionNumber = 0; // 0 means Auto
        float xDimensionPixels = 4f;

        // --------------------------------------------------------------------
        // Override defaults with command‑line arguments, if supplied
        // --------------------------------------------------------------------
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            codeText = args[0];

        if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            errorLevelStr = args[1].ToUpperInvariant();

        if (args.Length > 2 && int.TryParse(args[2], out int v) && v >= 0 && v <= 40)
            versionNumber = v;

        if (args.Length > 3 && float.TryParse(args[3], out float xd) && xd > 0)
            xDimensionPixels = xd;

        // --------------------------------------------------------------------
        // Resolve QR error correction level from string to enum
        // --------------------------------------------------------------------
        QRErrorLevel errorLevel = errorLevelStr switch
        {
            "L" => QRErrorLevel.LevelL,
            "M" => QRErrorLevel.LevelM,
            "Q" => QRErrorLevel.LevelQ,
            "H" => QRErrorLevel.LevelH,
            _   => QRErrorLevel.LevelM
        };

        // --------------------------------------------------------------------
        // Resolve QR version (use Auto when versionNumber is 0 or not found)
        // --------------------------------------------------------------------
        QRVersion qrVersion = QRVersion.Auto;
        if (versionNumber > 0)
        {
            string fieldName = versionNumber <= 9
                ? $"Version0{versionNumber}"
                : $"Version{versionNumber}";

            var field = typeof(QRVersion).GetField(fieldName);
            if (field?.GetValue(null) is QRVersion vEnum)
                qrVersion = vEnum;
        }

        // --------------------------------------------------------------------
        // Determine output file path (temporary folder)
        // --------------------------------------------------------------------
        string outputPath = Path.Combine(Path.GetTempPath(), "qr_generated.png");

        // --------------------------------------------------------------------
        // Generate QR Code with the configured parameters
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = xDimensionPixels;
            generator.Parameters.Barcode.QR.ErrorLevel = errorLevel;
            generator.Parameters.Barcode.QR.Version = qrVersion;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"QR Code saved to: {outputPath}");

        // --------------------------------------------------------------------
        // Read and decode the generated QR Code to verify its content
        // --------------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.QR;
        using (var reader = new BarCodeReader(outputPath, decodeType))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Decoded Text: {result.CodeText}");
                Console.WriteLine($"Symbology: {result.CodeType}");
            }
        }
    }
}