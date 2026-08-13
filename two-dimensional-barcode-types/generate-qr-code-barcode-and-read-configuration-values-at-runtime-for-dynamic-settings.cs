// Title: Generate QR Code with Runtime Configuration
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode, with parameters supplied via command‑line arguments for dynamic configuration.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It shows how to use the BarcodeGenerator class together with QR‑specific parameters such as error correction level and ECI encoding. Typical use cases include generating QR codes on the fly in web services or desktop applications where settings are provided at runtime. Developers often need to adjust dimensions, padding, and encoding without recompiling.
// Prompt: Generate a QR Code barcode and read configuration values at runtime for dynamic settings.
// Tags: qr code, barcode generation, runtime configuration, aspose.barcode, encode types, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code barcode using Aspose.BarCode.
/// Configuration values (code text, error level, ECI encoding, dimensions, padding) can be supplied
/// via command‑line arguments, allowing dynamic runtime settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Parses optional command‑line arguments, configures the QR Code generator, and saves the image as PNG.
    /// </summary>
    /// <param name="args">
    /// Optional arguments:
    /// <list type="bullet">
    ///   <item><description>args[0] – Code text to encode.</description></item>
    ///   <item><description>args[1] – Error correction level (L, M, Q, H).</description></item>
    ///   <item><description>args[2] – ECI encoding (e.g., UTF8, UTF16BE, Win1251).</description></item>
    ///   <item><description>args[3] – X dimension in points (float).</description></item>
    ///   <item><description>args[4] – Padding in points (float).</description></item>
    /// </list>
    /// </param>
    static void Main(string[] args)
    {
        // Default configuration values
        string codeText = "Hello, World!";
        QRErrorLevel errorLevel = QRErrorLevel.LevelM;
        ECIEncodings eciEncoding = ECIEncodings.UTF8;
        float xDimension = 2f; // points
        float padding = 5f; // points

        // -----------------------------------------------------------------
        // Parse command‑line arguments if provided
        // -----------------------------------------------------------------
        // args[0] = code text
        // args[1] = error level (L, M, Q, H)
        // args[2] = ECI encoding (UTF8, UTF16BE, Win1251, etc.)
        // args[3] = X dimension (float)
        // args[4] = padding (float)
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            codeText = args[0];

        if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            errorLevel = ParseErrorLevel(args[1], errorLevel);

        if (args.Length > 2 && !string.IsNullOrWhiteSpace(args[2]))
            eciEncoding = ParseEciEncoding(args[2], eciEncoding);

        if (args.Length > 3 && float.TryParse(args[3], out float xDimParsed))
            xDimension = xDimParsed;

        if (args.Length > 4 && float.TryParse(args[4], out float padParsed))
            padding = padParsed;

        // -----------------------------------------------------------------
        // Prepare output file path
        // -----------------------------------------------------------------
        string outputPath = Path.Combine(Path.GetTempPath(), "qr_generated.png");

        // -----------------------------------------------------------------
        // Create and configure the QR Code generator
        // -----------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the data to encode
            generator.CodeText = codeText;

            // QR‑specific settings
            generator.Parameters.Barcode.QR.ErrorLevel = errorLevel;
            generator.Parameters.Barcode.QR.ECIEncoding = eciEncoding;

            // General barcode settings
            generator.Parameters.Barcode.XDimension.Point = xDimension;
            generator.Parameters.Barcode.Padding.Left.Point = padding;
            generator.Parameters.Barcode.Padding.Top.Point = padding;
            generator.Parameters.Barcode.Padding.Right.Point = padding;
            generator.Parameters.Barcode.Padding.Bottom.Point = padding;

            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"QR Code generated at: {outputPath}");
    }

    // Convert string like "L", "M", "Q", "H" to QRErrorLevel enum
    private static QRErrorLevel ParseErrorLevel(string value, QRErrorLevel fallback)
    {
        return value.ToUpperInvariant() switch
        {
            "L" => QRErrorLevel.LevelL,
            "M" => QRErrorLevel.LevelM,
            "Q" => QRErrorLevel.LevelQ,
            "H" => QRErrorLevel.LevelH,
            _ => fallback,
        };
    }

    // Convert string to ECIEncodings enum; fallback to provided default if unknown
    private static ECIEncodings ParseEciEncoding(string value, ECIEncodings fallback)
    {
        // Supported names are the enum member names (case‑insensitive)
        foreach (ECIEncodings enc in Enum.GetValues(typeof(ECIEncodings)))
        {
            if (enc.ToString().Equals(value, StringComparison.OrdinalIgnoreCase))
                return enc;
        }
        return fallback;
    }
}