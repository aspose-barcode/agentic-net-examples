using System;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code image using Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code and saves it to a file while measuring execution time.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated QR code image.
        string outputPath = "qr_code.png";

        // The text/content to encode in the QR code.
        string codeText = "https://example.com";

        // Start a stopwatch to measure the generation time.
        Stopwatch sw = Stopwatch.StartNew();

        // Create a BarcodeGenerator for QR type with the specified content.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set the QR error correction level to high (Level H) for better resilience.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated QR code image to the specified file path.
            generator.Save(outputPath);
        }

        // Stop the stopwatch after generation is complete.
        sw.Stop();

        // Output the elapsed time and location of the saved QR code.
        Console.WriteLine($"QR code generated and saved to '{outputPath}' in {sw.ElapsedMilliseconds} ms.");
    }
}