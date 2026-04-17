using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Build extended codetext with multiple segments (FNC1, plain text, group separator, ECI)
        var textBuilder = new QrExtCodetextBuilder();
        textBuilder.AddFNC1FirstPosition();                     // FNC1 in first position
        textBuilder.AddPlainCodetext("Hello");                  // Plain segment
        textBuilder.AddFNC1GroupSeparator();                   // Group separator (GS)
        textBuilder.AddPlainCodetext("World<FNC1>");            // Plain segment with escaped FNC1
        textBuilder.AddECICodetext(ECIEncodings.UTF8, "Привет"); // ECI segment (UTF‑8)

        // Generate QR code in Extended mode and save as BMP
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.CodeText = textBuilder.GetExtendedCodetext();
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Extended; // use Extended mode
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM; // medium error correction
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "Sample QR"; // visible text
            generator.Save("qr_extended.bmp"); // output file
        }
    }
}