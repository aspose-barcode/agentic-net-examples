using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Paths for the generated QR image and diagnostics log
        string qrImagePath = "qr.png";
        string diagnosticsPath = "diagnostics.txt";

        // Ensure any previous files are removed to start fresh
        if (File.Exists(qrImagePath))
        {
            File.Delete(qrImagePath);
        }
        if (File.Exists(diagnosticsPath))
        {
            File.Delete(diagnosticsPath);
        }

        // Generate a QR code image with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            // Optional: set high error correction level for stronger confidence
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            generator.Save(qrImagePath);
        }

        // Verify the QR image was created before attempting to read it
        if (!File.Exists(qrImagePath))
        {
            Console.WriteLine($"Failed to create QR image at '{qrImagePath}'.");
            return;
        }

        // Read the QR code and obtain the confidence level
        using (var reader = new BarCodeReader(qrImagePath, DecodeType.QR))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Log the confidence enumeration value to the diagnostics file
                using (var writer = new StreamWriter(diagnosticsPath, true))
                {
                    writer.WriteLine($"Timestamp: {DateTime.UtcNow:u}");
                    writer.WriteLine($"Code Type: {result.CodeTypeName}");
                    writer.WriteLine($"Code Text: {result.CodeText}");
                    writer.WriteLine($"Confidence: {result.Confidence}");
                    writer.WriteLine(new string('-', 40));
                }
            }
        }

        // Indicate completion
        Console.WriteLine($"Recognition completed. Confidence details written to '{diagnosticsPath}'.");
    }
}