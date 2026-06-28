using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a GS1 QR code using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a GS1 QR code with sample data and saves it as an image file.
    /// </summary>
    static void Main()
    {
        // Sample GS1 fields
        string gtin = "12345678901231"; // (01) GTIN
        string lot = "LOT123";          // (10) Lot number
        string serial = "SER456";       // (21) Serial number

        // Group separator (ASCII 29) for GS1 QR
        string gs = ((char)29).ToString();

        // Construct GS1 QR codetext with parentheses for AI and group separator between fields
        string codeText = $"(01){gtin}(10){lot}{gs}(21){serial}";

        // Determine output file path in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "gs1qr.png");

        // Generate QR Code with GS1 encoding
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1QR, codeText))
        {
            // Set QR error correction level (optional, Level M provides a good balance)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated barcode image to the specified path
            generator.Save(outputPath);
        }

        // Inform the user where the QR code image was saved
        Console.WriteLine($"GS1 QR code saved to: {outputPath}");
    }
}