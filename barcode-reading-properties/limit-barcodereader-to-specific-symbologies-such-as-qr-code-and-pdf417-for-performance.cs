using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Generate a QR Code image
        using (var qrGenerator = new BarcodeGenerator(EncodeTypes.QR, "Hello QR"))
        {
            qrGenerator.Save("qr.png");
        }

        // Generate a PDF417 barcode image
        using (var pdfGenerator = new BarcodeGenerator(EncodeTypes.Pdf417, "Hello PDF417"))
        {
            pdfGenerator.Save("pdf417.png");
        }

        // Read the QR Code image, limiting detection to QR and PDF417 symbologies
        using (var readerQr = new BarCodeReader("qr.png", DecodeType.QR, DecodeType.Pdf417))
        {
            foreach (var result in readerQr.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }

        // Read the PDF417 image, limiting detection to QR and PDF417 symbologies
        using (var readerPdf = new BarCodeReader("pdf417.png", DecodeType.QR, DecodeType.Pdf417))
        {
            foreach (var result in readerPdf.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }
    }
}