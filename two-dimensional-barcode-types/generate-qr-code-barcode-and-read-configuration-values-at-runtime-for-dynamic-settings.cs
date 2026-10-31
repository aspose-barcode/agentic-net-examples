using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main(string[] args)
    {
        // Default settings
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr.png");
        string codeText = "Hello World";
        QRErrorLevel errorLevel = QRErrorLevel.LevelM;
        QRVersion qrVersion = QRVersion.Auto;
        float xDimension = 4f;

        // Parse command‑line arguments (if any)
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            outputPath = args[0];
        if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            codeText = args[1];
        if (args.Length > 2 && !string.IsNullOrWhiteSpace(args[2]))
            errorLevel = ParseErrorLevel(args[2]);
        if (args.Length > 3 && !string.IsNullOrWhiteSpace(args[3]))
            qrVersion = ParseQrVersion(args[3]);
        if (args.Length > 4 && float.TryParse(args[4], out float dim) && dim > 0)
            xDimension = dim;

        // Ensure output directory exists
        string outDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir))
            Directory.CreateDirectory(outDir);

        // Generate QR Code
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = xDimension;
            generator.Parameters.Barcode.QR.ErrorLevel = errorLevel;
            generator.Parameters.Barcode.QR.QrVersion = qrVersion;
            generator.Parameters.Resolution = 300f; // optional DPI

            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"QR Code saved to: {outputPath}");

        // Read back the generated QR Code
        if (File.Exists(outputPath))
        {
            using (var reader = new BarCodeReader(outputPath, DecodeType.QR))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Decoded Text: {result.CodeText}");
                    Console.WriteLine($"Symbology: {result.CodeType}");
                }
            }
        }
        else
        {
            Console.WriteLine("Failed to locate the generated barcode image.");
        }
    }

    static QRErrorLevel ParseErrorLevel(string level)
    {
        return level.ToUpperInvariant() switch
        {
            "L" => QRErrorLevel.LevelL,
            "M" => QRErrorLevel.LevelM,
            "Q" => QRErrorLevel.LevelQ,
            "H" => QRErrorLevel.LevelH,
            _ => QRErrorLevel.LevelM
        };
    }

    static QRVersion ParseQrVersion(string versionStr)
    {
        if (string.Equals(versionStr, "auto", StringComparison.OrdinalIgnoreCase))
            return QRVersion.Auto;

        if (int.TryParse(versionStr, out int v) && v >= 1 && v <= 40)
        {
            string fieldName = v < 10 ? $"Version0{v}" : $"Version{v}";
            var field = typeof(QRVersion).GetField(fieldName);
            if (field != null)
                return (QRVersion)field.GetValue(null);
        }
        return QRVersion.Auto;
    }
}