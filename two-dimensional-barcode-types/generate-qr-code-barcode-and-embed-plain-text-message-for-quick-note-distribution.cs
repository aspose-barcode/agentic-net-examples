using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Output file for the QR code image
        string outputFile = "qr_note.png";

        // Create a QR code generator
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Text to be encoded in the QR code
            generator.CodeText = "QuickNote: Remember to bring ID.";

            // Set QR error correction level (Medium)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Display a plain text message below the QR code
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "Quick Note";

            // Define image dimensions (optional)
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 300f;

            // Save the QR code image as PNG
            generator.Save(outputFile);
        }

        Console.WriteLine($"QR code saved to {outputFile}");
    }
}