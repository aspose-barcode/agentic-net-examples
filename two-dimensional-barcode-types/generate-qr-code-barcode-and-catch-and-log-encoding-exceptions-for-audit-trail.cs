using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample data to encode
        const string codeText = "Hello World";

        // Output file for the QR code image
        const string outputFile = "qr.png";

        // Audit log file
        const string logFile = "audit.log";

        try
        {
            // Create QR Code generator with the specified text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Set high error correction level
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Save the generated QR Code image
                generator.Save(outputFile);
            }

            // Log successful generation
            File.AppendAllText(logFile, $"{DateTime.UtcNow:u} - QR code generated successfully: {outputFile}{Environment.NewLine}");
        }
        catch (BarCodeException ex)
        {
            // Log encoding exception details for audit trail
            string message = $"{DateTime.UtcNow:u} - BarCodeException: {ex.Message}{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}";
            File.AppendAllText(logFile, message);

            // Optionally inform the user
            Console.WriteLine("Failed to generate QR code. See audit log for details.");
        }
        catch (Exception ex)
        {
            // Log any other unexpected exceptions
            string message = $"{DateTime.UtcNow:u} - Unexpected exception: {ex.Message}{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}";
            File.AppendAllText(logFile, message);

            Console.WriteLine("An unexpected error occurred. See audit log for details.");
        }
    }
}