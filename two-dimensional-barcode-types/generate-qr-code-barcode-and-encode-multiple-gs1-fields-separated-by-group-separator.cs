using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Build GS1 QR codetext with multiple AI fields separated by the GS (group separator) character.
        var builder = new QrExtCodetextBuilder();
        builder.AddFNC1FirstPosition(); // Insert FNC1 at the first position for GS1 QR.
        builder.AddPlainCodetext("(01)01234567890123"); // GTIN
        builder.AddFNC1GroupSeparator(); // GS separator
        builder.AddPlainCodetext("(21)ABC123"); // Serial number
        builder.AddFNC1GroupSeparator();
        builder.AddPlainCodetext("(10)LOT987"); // Batch/lot number

        string gs1Codetext = builder.GetExtendedCodetext();

        // Generate QR code with GS1 extended mode.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.CodeText = gs1Codetext;
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Extended;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
            // Optional: set visible text without control characters.
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "GS1 QR Example";

            generator.Save("gs1qr.png");
        }
    }
}