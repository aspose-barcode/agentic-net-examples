using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define the QR code content and output file
        string codeText = "https://example.com";
        string outputPath = "qr_code.png";

        // Number of attempts for saving the barcode
        const int maxAttempts = 3;
        int attempt = 0;
        bool saved = false;

        // Create the QR code generator
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the text to encode
            generator.CodeText = codeText;

            // Set high error correction level
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Retry loop for transient file system errors
            while (!saved && attempt < maxAttempts)
            {
                attempt++;
                try
                {
                    // Attempt to save the barcode image
                    generator.Save(outputPath);
                    saved = true;
                }
                catch (IOException ex)
                {
                    // If this was the last attempt, rethrow the exception
                    if (attempt >= maxAttempts)
                    {
                        Console.WriteLine($"Failed to save after {attempt} attempts: {ex.Message}");
                        throw;
                    }
                    // Otherwise, continue to next attempt
                    Console.WriteLine($"Attempt {attempt} failed with I/O error: {ex.Message}. Retrying...");
                }
            }
        }

        if (saved)
        {
            Console.WriteLine($"QR code saved successfully to '{Path.GetFullPath(outputPath)}'.");
        }
    }
}