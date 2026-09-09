// Title: Generate QR Code with embedded vCard for a digital business card
// Description: Creates a QR Code barcode that contains vCard contact information, suitable for use as a digital business card.
// Category-Description: This example demonstrates how to use Aspose.BarCode's BarcodeGenerator to produce a QR Code that encodes a vCard. It covers setting the code text with UTF‑8 encoding, configuring QR error correction, customizing visual appearance, and saving the result as a PNG image. Developers working with barcode generation, especially QR codes for contact sharing, will find this pattern useful for creating printable or screen‑displayed digital business cards.
// Prompt: Generate QR Code barcode and embed vCard contact information for digital business card.
// Tags: qr code, vcard, barcode generation, aspose.barcode, image output, png

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code that embeds vCard contact information using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Builds a vCard string, configures the QR Code generator,
    /// and saves the resulting image to the current directory.
    /// </summary>
    static void Main()
    {
        // Define the vCard data to be encoded in the QR Code.
        string vCard = "BEGIN:VCARD\r\n" +
                       "VERSION:3.0\r\n" +
                       "N:Doe;John;;;\r\n" +
                       "FN:John Doe\r\n" +
                       "ORG:Example Company\r\n" +
                       "TITLE:Software Engineer\r\n" +
                       "TEL;TYPE=WORK,VOICE:+1-111-555-0100\r\n" +
                       "EMAIL:john.doe@example.com\r\n" +
                       "END:VCARD";

        // Determine the full path for the output PNG file.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "vcard_qr.png");

        // Initialize the barcode generator for QR Code with an empty initial text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, ""))
        {
            // Assign the vCard string as the code text, using UTF‑8 encoding.
            generator.SetCodeText(vCard, Encoding.UTF8);

            // Configure a high error correction level to improve scan reliability.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Optional visual customizations.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;                     // Size of a single module.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;    // QR code foreground color.
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;           // Background color.

            // Save the generated QR Code as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}