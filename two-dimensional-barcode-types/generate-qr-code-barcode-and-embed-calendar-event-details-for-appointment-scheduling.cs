using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR code that encodes an iCalendar event using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code image containing a calendar event.
    /// </summary>
    static void Main()
    {
        // Define a sample calendar event in iCalendar (ICS) format.
        string calendarEvent = "BEGIN:VEVENT\r\n" +
                               "UID:20230627T090000-appointment@example.com\r\n" +
                               "DTSTAMP:20230627T080000Z\r\n" +
                               "DTSTART:20230627T090000Z\r\n" +
                               "DTEND:20230627T100000Z\r\n" +
                               "SUMMARY:Project Meeting\r\n" +
                               "DESCRIPTION:Discuss project milestones.\r\n" +
                               "END:VEVENT";

        // Specify the output file path for the generated QR code image.
        string outputPath = "appointment_qr.png";

        // Create a QR code generator instance within a using block to ensure proper disposal.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Assign the iCalendar string as the text to be encoded in the QR code.
            generator.CodeText = calendarEvent;

            // Configure QR code parameters:
            // - High error correction level (Level H) to improve readability under damage.
            // - Automatic encoding mode (default) for optimal data representation.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Auto;

            // Set the image resolution to 300 DPI for higher quality output.
            generator.Parameters.Resolution = 300f;

            // Save the generated QR code as a PNG file at the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user that the QR code has been saved.
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}