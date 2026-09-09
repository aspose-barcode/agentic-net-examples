// Title: Generate QR Code with Embedded Calendar Event (iCalendar)
// Description: Creates a QR code containing iCalendar data for a doctor appointment and saves it as a PNG image.
// Category-Description: This example demonstrates how to use Aspose.BarCode to generate QR Code barcodes with custom text encoding. It covers setting QR-specific parameters such as ECI encoding, error correction level, and image dimensions. Developers working with barcode generation for data exchange (e.g., calendar events, URLs, contact info) can reference this pattern to embed structured data into QR codes using the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes.
// Prompt: Generate QR Code barcode and embed calendar event details for appointment scheduling.
// Tags: qr code, calendar, icalendar, barcode generation, png, aspose.barcode, encoding

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR Code that encodes an iCalendar event and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Builds iCalendar data, configures QR Code settings, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // iCalendar formatted event details for a doctor appointment
        string ical = "BEGIN:VCALENDAR\r\n" +
                      "VERSION:2.0\r\n" +
                      "BEGIN:VEVENT\r\n" +
                      "SUMMARY:Doctor Appointment\r\n" +
                      "DTSTART:20231001T090000Z\r\n" +
                      "DTEND:20231001T093000Z\r\n" +
                      "LOCATION:Clinic\r\n" +
                      "DESCRIPTION:Annual check-up\r\n" +
                      "END:VEVENT\r\n" +
                      "END:VCALENDAR";

        // Determine the full path for the output PNG file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "AppointmentQR.png");

        // Ensure the output directory exists before attempting to save the file
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Initialize the barcode generator for QR Code with empty initial text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, string.Empty))
        {
            // Assign the iCalendar string as the code text using UTF-8 encoding
            generator.SetCodeText(ical, Encoding.UTF8);

            // Configure QR Code specific parameters
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;          // Enable ECI to support UTF-8
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;      // Specify UTF-8 as the ECI encoding
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;    // Use high error correction for robustness

            // Optional visual setting: size of each QR module in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated QR Code as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR code image has been saved
        Console.WriteLine("QR code saved to: " + outputPath);
    }
}