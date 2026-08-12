// Title: Generate QR Code with Extended Encoding and Save as BMP
// Description: Demonstrates how to build an extended QR Code containing multiple data segments using Aspose.BarCode, set the QR encode mode to Extended, and save the resulting barcode as a BMP image.
// Category-Description: This example belongs to the Aspose.BarCode QR Code generation category. It shows how to use the QrExtCodetextBuilder to combine plain text, ECI encoded text, and function characters into a single QR Code, configure QR parameters such as encode mode and error correction level, and output the barcode as a BMP file. Developers working with 2‑D barcodes can reference this pattern for creating complex QR codes with mixed data types.
// Prompt: Generate a QR Code barcode with Extended encoding mode combining multiple data segments and save as BMP.
// Tags: qr code, extended encoding, bmp, aspose.barcode, barcode generation, qrextcodetextbuilder, qrencondemode, error correction

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.Generation; // QrExtCodetextBuilder resides here

/// <summary>
/// Example program that creates a QR Code with extended encoding mode,
/// combines several data segments, and saves the result as a BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Builds an extended QR code text, configures the generator, and saves the barcode.
    /// </summary>
    static void Main()
    {
        // Initialize a builder for extended QR code text that can hold multiple data segments.
        QrExtCodetextBuilder builder = new QrExtCodetextBuilder();

        // Add a plain text segment.
        builder.AddPlainCodetext("Hello");

        // Add an ECI (UTF-8) encoded segment.
        builder.AddECICodetext(ECIEncodings.UTF8, "World");

        // Insert the FNC1 first position function character.
        builder.AddFNC1FirstPosition();

        // Add another plain text segment.
        builder.AddPlainCodetext("12345");

        // Insert the FNC1 group separator function character.
        builder.AddFNC1GroupSeparator();

        // Add a final plain text segment.
        builder.AddPlainCodetext("End");

        // Retrieve the combined extended codetext string.
        string extendedCodetext = builder.GetExtendedCodetext();

        // Create a QR Code generator and configure its parameters.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Assign the extended codetext to the generator.
            generator.CodeText = extendedCodetext;

            // Set QR specific parameters: use Extended encode mode and medium error correction level.
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Extended;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Optionally set a human‑readable display text for the 2‑D barcode.
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "Sample QR";

            // Save the generated QR Code as a BMP image.
            generator.Save("qr_extended.bmp");
        }

        // Inform the user that the image has been saved.
        Console.WriteLine("QR code saved to 'qr_extended.bmp'.");
    }
}