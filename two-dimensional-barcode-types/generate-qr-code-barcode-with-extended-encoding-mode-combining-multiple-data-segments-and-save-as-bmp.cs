// Title: Generate QR Code with Extended Encoding and Save as BMP
// Description: Demonstrates creating a QR Code barcode using the Extended encoding mode, combining multiple data segments, and saving the result as a BMP image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation with advanced encoding options. It showcases the use of BarcodeGenerator, QrExtCodetextBuilder, and QR encoding settings (QREncodeMode, QRErrorLevel) to build multi‑segment QR codes. Developers often need such patterns for encoding mixed data types, applying ECI, or inserting function characters in QR symbols.
// Prompt: Generate a QR Code barcode with Extended encoding mode combining multiple data segments and save as BMP.
// Tags: qr code, extended encoding, bmp, aspose.barcode, barcode generation, qrextcodetextbuilder, qrencondemode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a QR Code using the Extended encoding mode,
/// combines several data segments (plain text, function characters, ECI),
/// and saves the barcode as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Builds an extended QR code text,
    /// configures the generator, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Build an extended QR code text with multiple segments:
        // - FNC1 in first position
        // - Plain text segment "ABC123"
        // - Group separator (GS)
        // - Plain text segment "XYZ"
        // - ECI segment (UTF‑8) containing Cyrillic text "Привет"
        var extBuilder = new QrExtCodetextBuilder();
        extBuilder.AddFNC1FirstPosition();                     // <FNC1> at the start
        extBuilder.AddPlainCodetext("ABC123");                 // first plain data segment
        extBuilder.AddFNC1GroupSeparator();                   // group separator (0x1D)
        extBuilder.AddPlainCodetext("XYZ");                    // second plain data segment
        extBuilder.AddECICodetext(ECIEncodings.UTF8, "Привет"); // ECI segment with Cyrillic text

        // Retrieve the combined extended codetext string.
        string extendedCodeText = extBuilder.GetExtendedCodetext();

        // Create a QR barcode generator and configure it for Extended mode.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Assign the extended codetext to the generator.
            generator.CodeText = extendedCodeText;

            // Set QR encoding mode to Extended (supports multi‑segment codetext).
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Extended;

            // Optional: set error correction level (e.g., LevelM).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated barcode image as a BMP file.
            generator.Save("qr_extended.bmp");
        }

        // Inform the user that the file has been created.
        Console.WriteLine("QR code saved to 'qr_extended.bmp'.");
    }
}