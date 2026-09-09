// Title: Generate QR Code Barcode with Runtime Configuration
// Description: Demonstrates creating a QR Code using Aspose.BarCode, allowing dynamic settings via command‑line arguments, and then decoding the generated image.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for QR Code creation and BarCodeReader for decoding. Typical scenarios include generating QR codes on the fly with configurable error correction and version, then validating them programmatically. Developers often need to adjust barcode parameters at runtime and verify the output using the same library.
// Prompt: Generate a QR Code barcode and read configuration values at runtime for dynamic settings.
// Tags: qr code, barcode generation, barcode recognition, runtime configuration, aspose.barcode, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR Code barcode using Aspose.BarCode,
/// saves it to a PNG file, and then reads the barcode back.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses optional command‑line arguments to configure the QR Code,
    /// generates the image, and decodes it.
    /// </summary>
    /// <param name="args">Optional parameters: code text, error correction level, version, output path.</param>
    static void Main(string[] args)
    {
        // Default configuration values
        string codeText = "Aspose.BarCode QR";
        string errorLevelStr = "H";
        string versionStr = "Auto";
        string outputPath = Path.Combine(Path.GetTempPath(), "qr_generated.png");

        // Override defaults with command‑line arguments, if supplied
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            codeText = args[0];
        if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            errorLevelStr = args[1];
        if (args.Length > 2 && !string.IsNullOrWhiteSpace(args[2]))
            versionStr = args[2];
        if (args.Length > 3 && !string.IsNullOrWhiteSpace(args[3]))
            outputPath = args[3];

        // Map the error correction level string to the corresponding enum value
        QRErrorLevel errorLevel = QRErrorLevel.LevelM; // default to Medium
        switch (errorLevelStr.ToUpperInvariant())
        {
            case "L":
                errorLevel = QRErrorLevel.LevelL;
                break;
            case "M":
                errorLevel = QRErrorLevel.LevelM;
                break;
            case "Q":
                errorLevel = QRErrorLevel.LevelQ;
                break;
            case "H":
                errorLevel = QRErrorLevel.LevelH;
                break;
        }

        // Resolve the QR version; allow "Auto" or explicit version strings like "Version05"
        QRVersion qrVersion = QRVersion.Auto;
        if (!string.Equals(versionStr, "Auto", StringComparison.OrdinalIgnoreCase))
        {
            string enumName = versionStr.StartsWith("Version", StringComparison.OrdinalIgnoreCase)
                ? versionStr
                : "Version" + versionStr.PadLeft(2, '0');

            if (Enum.TryParse(typeof(QRVersion), enumName, out object verObj))
                qrVersion = (QRVersion)verObj;
        }

        // Ensure the output directory exists before saving the image
        string outDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir))
            Directory.CreateDirectory(outDir);

        // Generate the QR Code image using BarcodeGenerator
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.ErrorLevel = errorLevel;
            generator.Parameters.Barcode.QR.Version = qrVersion;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"QR Code saved to: {outputPath}");

        // Decode the generated QR Code image using BarCodeReader
        if (File.Exists(outputPath))
        {
            using (var reader = new BarCodeReader(outputPath, DecodeType.QR))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Decoded text: {result.CodeText}");
                }
            }
        }
        else
        {
            Console.WriteLine("Failed to generate the QR code image.");
        }
    }
}