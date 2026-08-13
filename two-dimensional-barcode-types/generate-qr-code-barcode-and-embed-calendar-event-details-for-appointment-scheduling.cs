// Title: Generate QR Code with Calendar Event (iCalendar) for Appointment Scheduling
// Description: Demonstrates how to create a QR Code barcode that encodes an iCalendar event, enabling recipients to add the appointment to their calendars directly from the scanned code.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation with custom data payloads. It showcases the use of BarcodeGenerator, EncodeTypes.QR, and QR-specific parameters such as error correction level and ECI encoding. Developers often need to embed structured information like contact details, URLs, or calendar events into QR codes for seamless data transfer in mobile and web applications.
// Prompt: Generate QR Code barcode and embed calendar event details for appointment scheduling.
// Tags: qr code, calendar, icalendar, barcode generation, aspose.barcode, png output, error correction, eci encoding

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Program that generates a QR Code containing an iCalendar event and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Builds the iCalendar string, configures the QR Code generator, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define calendar event details in iCalendar format
        string calendarEvent = "BEGIN:VCALENDAR\r\n" +
                               "VERSION:2.0\r\n" +
                               "BEGIN:VEVENT\r\n" +
                               "SUMMARY:Meeting with Bob\r\n" +
                               "DTSTART:20230815T090000Z\r\n" +
                               "DTEND:20230815T100000Z\r\n" +
                               "LOCATION:Conference Room\r\n" +
                               "DESCRIPTION:Discuss project status\r\n" +
                               "END:VEVENT\r\n" +
                               "END:VCALENDAR";

        // Determine output file path in the current working directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "appointment_qr.png");

        // Initialize QR Code generator with QR symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Encode the iCalendar text using UTF-8 encoding
            generator.SetCodeText(calendarEvent, Encoding.UTF8);

            // Set a high error correction level (Level H) for improved readability under adverse conditions
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Specify ECI encoding to ensure the QR code correctly represents UTF-8 characters
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            // Optional: display a human‑readable caption below the QR code
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "Meeting with Bob";

            // Save the generated QR code as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR code image has been saved
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}