using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Sample QR code text containing Unicode characters that are not supported in Binary mode
        const string qrText = "こんにちは"; // Japanese greeting

        // Output file name
        const string outputFile = "qr.png";

        // Attempt to generate QR code in Binary mode (will throw for Unicode characters)
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                generator.CodeText = qrText;
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Binary;
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
                generator.Save(outputFile);
                Console.WriteLine($"QR code saved successfully to '{outputFile}' using Binary mode.");
            }
        }
        catch (InvalidCodeException ex)
        {
            // Handle specific invalid code exception
            Console.WriteLine($"InvalidCodeException: {ex.Message}");
            FallbackToAutoMode(qrText, outputFile);
        }
        catch (BarCodeException ex)
        {
            // Handle generic barcode generation exception
            Console.WriteLine($"BarCodeException: {ex.Message}");
            FallbackToAutoMode(qrText, outputFile);
        }
        catch (Exception ex)
        {
            // Handle any other unexpected exceptions
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

    // Fallback method that generates the QR code using the default Auto mode
    private static void FallbackToAutoMode(string text, string fileName)
    {
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                generator.CodeText = text;
                // Auto mode is the default; explicitly set for clarity
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Auto;
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
                generator.Save(fileName);
                Console.WriteLine($"QR code saved successfully to '{fileName}' using Auto mode.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to generate QR code in fallback mode: {ex.Message}");
        }
    }
}