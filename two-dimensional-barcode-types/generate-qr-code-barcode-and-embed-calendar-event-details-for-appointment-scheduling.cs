using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // iCalendar event details to be encoded in the QR code
        string ical = "BEGIN:VCALENDAR\r\n" +
                      "VERSION:2.0\r\n" +
                      "BEGIN:VEVENT\r\n" +
                      "SUMMARY:Appointment with Dr. Smith\r\n" +
                      "DTSTART:20260420T090000Z\r\n" +
                      "DTEND:20260420T100000Z\r\n" +
                      "LOCATION:123 Main St, Anytown\r\n" +
                      "DESCRIPTION:Annual check‑up\r\n" +
                      "END:VEVENT\r\n" +
                      "END:VCALENDAR";

        // Create a QR code generator with the iCalendar text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, ical))
        {
            // Use high error correction level for robustness
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Set image dimensions (optional)
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 300f;

            // Set colors (optional)
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Save the QR code image to a file
            generator.Save("qr_calendar.png");
        }
    }
}