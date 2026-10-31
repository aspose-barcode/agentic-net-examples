using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeQrDemo");
        Directory.CreateDirectory(outputDir);
        string logPath = Path.Combine(outputDir, "audit.log");

        // Helper to log messages
        void Log(string message)
        {
            Console.WriteLine(message);
            File.AppendAllText(logPath, $"{DateTime.UtcNow:u} - {message}{Environment.NewLine}");
        }

        // 1. Generate a valid QR Code
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
            {
                string validPath = Path.Combine(outputDir, "ValidQr.png");
                generator.Save(validPath, BarCodeImageFormat.Png);
                Log($"Successfully generated QR code: {validPath}");
            }
        }
        catch (Exception ex)
        {
            Log($"Error generating valid QR code: {ex.Message}");
        }

        // 2. Attempt to generate an invalid QR Code (Binary mode with Unicode text)
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Force exception on incorrect code text
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;
                // Set encode mode to Binary (will reject Unicode characters)
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Binary;
                // Unicode text that cannot be encoded in Binary mode
                generator.SetCodeText("こんにちは"); // Japanese greeting
                string invalidPath = Path.Combine(outputDir, "InvalidQr.png");
                generator.Save(invalidPath, BarCodeImageFormat.Png);
                Log($"Unexpectedly generated QR code: {invalidPath}");
            }
        }
        catch (Exception ex)
        {
            Log($"Caught expected encoding exception: {ex.Message}");
        }

        Log("Processing completed.");
    }
}