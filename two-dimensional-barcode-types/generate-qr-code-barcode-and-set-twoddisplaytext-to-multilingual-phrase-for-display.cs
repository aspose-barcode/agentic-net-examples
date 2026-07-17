// Title: Generate QR Code with multilingual display text
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode and setting a multilingual TwoDDisplayText for visual representation while preserving the encoded data.
// Category-Description: This example belongs to the Aspose.BarCode 2D barcode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.QR, configure QR encoding options, and customize the displayed text via CodeTextParameters.TwoDDisplayText. Developers often need to show user‑friendly or localized text alongside the actual encoded value in QR codes for marketing, multilingual signage, or documentation purposes.
// Prompt: Generate QR Code barcode and set TwoDDisplayText to multilingual phrase for display.
// Tags: qr code, two-dimensional, display text, multilingual, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode and sets a multilingual display text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a QR Code, assigns a localized display string,
    /// and saves the image to disk.
    /// </summary>
    static void Main()
    {
        // The actual data encoded in the QR code (can be any string)
        const string codeText = "Sample QR Content";

        // Text shown to users when the QR code is rendered; supports multiple languages
        const string displayText = "Hello 世界 مرحبا";

        // Destination file for the generated QR code image
        const string outputPath = "qr_multilingual.png";

        // Initialize the QR barcode generator with the encoded data
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Explicitly set QR encoding mode (Auto is the default)
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Auto;

            // Assign the multilingual text that will be displayed on the barcode image
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = displayText;

            // Persist the generated QR code as a PNG file
            generator.Save(outputPath);
        }

        // Inform the user where the QR code image was saved
        Console.WriteLine($"QR code saved to '{outputPath}'.");
    }
}