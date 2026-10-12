// Title: Generate QR Code with embedded vCard for a digital business card
// Description: Demonstrates creating a QR Code barcode that contains vCard contact information, useful for sharing digital business cards.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code symbology and embedding structured data such as vCard. It showcases the use of BarcodeGenerator, EncodeTypes, and QRErrorLevel classes to produce a PNG image. Developers often need to generate QR codes for contact sharing, event tickets, or product information, and this snippet illustrates the typical workflow.
// Prompt: Generate QR Code barcode and embed vCard contact information for digital business card.
// Tags: qr code, vcard, barcode generation, aspose.barcode, png output, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR Code containing vCard data and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a vCard string, encodes it into a QR Code, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the vCard content to be embedded in the QR Code.
        string vCard = "BEGIN:VCARD\nVERSION:3.0\nN:Doe;John;;;\nFN:John Doe\nORG:Example Company\nTITLE:Software Engineer\nTEL;TYPE=WORK,VOICE:+1-111-555-0100\nEMAIL:john.doe@example.com\nEND:VCARD";

        // Determine the full path for the output PNG file in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "vcard_qr.png");

        // Initialize the barcode generator with QR Code symbology and the vCard data.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, vCard))
        {
            // Set the QR Code error correction level to Medium (Level M) for a balance of data capacity and resilience.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR Code as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR code saved to {outputPath}");
    }
}