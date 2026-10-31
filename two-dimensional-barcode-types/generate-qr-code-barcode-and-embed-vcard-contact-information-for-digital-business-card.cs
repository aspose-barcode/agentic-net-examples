// Title: Generate QR Code with embedded vCard for a digital business card
// Description: Demonstrates creating a QR Code barcode that contains vCard contact information, useful for generating digital business cards.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to encode structured text (vCard) into a QR Code using the BarcodeGenerator class. Typical use cases include embedding contact details in QR codes for easy scanning by mobile devices. Developers often need to set QR error correction level and module size via Parameters.Barcode.QR and Parameters.Barcode.XDimension.
// Prompt: Generate QR Code barcode and embed vCard contact information for digital business card.
// Tags: qr code, vcard, barcode generation, aspnet, aspose.barcode, image output, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code that encodes a vCard contact for a digital business card.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the QR Code image and saves it to the current directory.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "vcard_qr.png");

        // vCard data to be embedded in the QR Code.
        string vcard = "BEGIN:VCARD\r\nVERSION:3.0\r\nN:Doe;John;;;\r\nFN:John Doe\r\nORG:Example Company\r\nTITLE:Software Engineer\r\nTEL;TYPE=WORK,VOICE:+1-111-555-0100\r\nEMAIL:john.doe@example.com\r\nEND:VCARD";

        // Initialize the barcode generator for QR type with the vCard content.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, vcard))
        {
            // Set QR error correction level to Medium (Level M).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Define the module size (X dimension) as 2 points.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated QR Code as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR code image has been saved.
        Console.WriteLine($"QR code saved to {outputPath}");
    }
}