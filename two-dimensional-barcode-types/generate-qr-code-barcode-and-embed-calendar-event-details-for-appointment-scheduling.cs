// Title: Generate QR Code with Calendar Event for Appointment Scheduling
// Description: Demonstrates creating a QR Code that encodes an iCalendar event, useful for embedding appointment details in a scannable format.
// Category-Description: This example belongs to the Aspose.BarCode QR code generation category, showcasing how to use BarcodeGenerator, EncodeTypes, and QR-specific parameters such as error correction and ECI encoding. Developers often need to embed structured data like calendar events, URLs, or contact information into QR codes for mobile scanning and automated processing.
// Prompt: Generate QR Code barcode and embed calendar event details for appointment scheduling.
// Tags: qr code, calendar event, ics, barcode generation, aspnet, aspose.barcode, png output

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR Code that contains an iCalendar event for appointment scheduling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a QR Code with calendar details and saves it as a PNG image.
    /// </summary>
    static void Main()
    {
        // Define the iCalendar formatted event details
        string calendarEvent = "BEGIN:VCALENDAR\r\n" +
                               "VERSION:2.0\r\n" +
                               "BEGIN:VEVENT\r\n" +
                               "SUMMARY:Appointment with Dr. Smith\r\n" +
                               "DTSTART:20230715T090000Z\r\n" +
                               "DTEND:20230715T093000Z\r\n" +
                               "LOCATION:Clinic Room 101\r\n" +
                               "DESCRIPTION:Regular check‑up\r\n" +
                               "END:VEVENT\r\n" +
                               "END:VCALENDAR";

        // Initialize the QR code generator with the calendar event as the payload
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, calendarEvent))
        {
            // Set a high error correction level to improve readability after printing or scanning
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Ensure the payload is encoded using UTF‑8 (ECI) for proper character representation
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            // Optionally increase the module (dot) size for a clearer image
            generator.Parameters.Barcode.XDimension.Point = 3f;

            // Save the generated QR code as a PNG file
            generator.Save("appointment_qr.png");
        }

        // Inform the user that the QR code has been generated
        Console.WriteLine("QR code generated: appointment_qr.png");
    }
}