// Title: Generate QR Code with iCalendar Event for Appointment Scheduling
// Description: Demonstrates creating a QR Code that encodes an iCalendar (ICS) event, useful for embedding meeting details in a scannable barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator with EncodeTypes.QR to embed structured text such as iCalendar data. Developers often need to create QR codes for event invitations, tickets, or calendar syncing, and typically work with classes like BarcodeGenerator, EncodeTypes, QRErrorLevel, and BarCodeImageFormat to customize dimensions, error correction, and output format.
// Prompt: Generate QR Code barcode and embed calendar event details for appointment scheduling.
// Tags: qr code, barcode generation, ics, calendar event, png, aspose.barcode, encode types, qrcode, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code containing iCalendar event data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates an iCalendar string, encodes it into a QR Code, and saves the image as PNG.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for the generated barcode image
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "AppointmentQR.png");

        // iCalendar (ICS) event details to be embedded in the QR Code
        string ics = "BEGIN:VCALENDAR\r\n" +
                     "VERSION:2.0\r\n" +
                     "BEGIN:VEVENT\r\n" +
                     "SUMMARY:Team Meeting\r\n" +
                     "DTSTART:20231101T100000Z\r\n" +
                     "DTEND:20231101T110000Z\r\n" +
                     "LOCATION:Conference Room\r\n" +
                     "DESCRIPTION:Discuss project status\r\n" +
                     "END:VEVENT\r\n" +
                     "END:VCALENDAR";

        // Initialize the barcode generator for QR Code with the iCalendar payload
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, ics))
        {
            // Set QR Code module size (pixel dimension) and error correction level
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR Code as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image has been saved
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}