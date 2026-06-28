using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code with extended codetext using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that builds an extended codetext with multiple segments and generates a QR code image.
    /// </summary>
    static void Main()
    {
        // Create a builder for constructing an extended QR codetext with multiple data segments.
        var builder = new QrExtCodetextBuilder();

        // Add an FNC1 character at the first position (required for certain GS1 applications).
        builder.AddFNC1FirstPosition();

        // Append a plain alphanumeric segment.
        builder.AddPlainCodetext("ABC123");

        // Insert a group separator (FNC1) to delimit segments.
        builder.AddFNC1GroupSeparator();

        // Append another plain alphanumeric segment.
        builder.AddPlainCodetext("DEF456");

        // Add an ECI segment with UTF‑8 encoding containing Japanese text.
        builder.AddECICodetext(ECIEncodings.UTF8, "こんにちは");

        // Retrieve the fully constructed extended codetext string.
        string extendedCode = builder.GetExtendedCodetext();

        // Generate the QR code using the extended encoding mode.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Assign the extended codetext to the generator.
            generator.CodeText = extendedCode;

            // Set QR-specific parameters: use Extended mode and medium error correction level.
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Extended;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM; // optional error correction

            // Save the generated QR code as a BMP image file.
            generator.Save("qr_extended.bmp");
        }

        // Inform the user that the QR code image has been saved.
        Console.WriteLine("QR code saved as qr_extended.bmp");
    }
}