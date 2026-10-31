using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Calendar event details in iCalendar format
        string vEvent = "BEGIN:VCALENDAR\r\n" +
                        "VERSION:2.0\r\n" +
                        "BEGIN:VEVENT\r\n" +
                        "SUMMARY:Meeting with John\r\n" +
                        "DTSTART:20231101T100000Z\r\n" +
                        "DTEND:20231101T110000Z\r\n" +
                        "LOCATION:Conference Room\r\n" +
                        "DESCRIPTION:Discuss project status\r\n" +
                        "END:VEVENT\r\n" +
                        "END:VCALENDAR";

        // Prepare output directory and file path
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeQR");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "AppointmentQR.png");

        // Generate QR Code barcode with event details
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, vEvent))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}