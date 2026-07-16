// Title: Generate QR Code with embedded vCard for digital business card
// Description: Demonstrates creating a QR Code barcode that contains vCard contact information, useful for digital business cards.
// Category-Description: This example belongs to the Aspose.BarCode QR Code generation category. It shows how to use BarcodeGenerator with EncodeTypes.QR, configure error correction, and embed structured text such as vCard. Developers often need to generate QR codes for contact sharing, marketing, or authentication scenarios, and this snippet illustrates the typical API usage for those cases.
// Prompt: Generate QR Code barcode and embed vCard contact information for digital business card.
// Tags: qr code, vcard, barcode generation, aspose.barcode, png output

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a QR Code containing vCard data and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the QR Code and writes the output file.
    /// </summary>
    static void Main()
    {
        // vCard data to be encoded in the QR code
        string vCard = "BEGIN:VCARD\nVERSION:3.0\nN:Doe;John;;;\nFN:John Doe\nORG:Example Company\nTITLE:Software Engineer\nTEL;TYPE=WORK,VOICE:+1-111-555-0100\nEMAIL:john.doe@example.com\nEND:VCARD";

        // Initialize a QR code generator with the QR symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Assign the vCard string as the code text to be encoded
            generator.CodeText = vCard;

            // Set a high error correction level (Level H) for better readability under adverse conditions
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Optional: set the QR encoding mode to Auto (default)
            // generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Auto;

            // Save the generated QR code as a PNG image file
            generator.Save("vcard_qr.png");
        }

        // Inform the user that the image has been saved
        Console.WriteLine("QR code with vCard saved to vcard_qr.png");
    }
}