using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR code, saving it to a memory stream,
/// and then reading it back using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code, writes it to a memory stream,
    /// and reads it back to verify its contents.
    /// </summary>
    static void Main()
    {
        // Text to encode in the QR code.
        string qrText = "Hello Aspose QR Code";

        // Create a memory stream to hold the generated QR code image.
        using (var ms = new MemoryStream())
        {
            // Initialize the barcode generator for QR codes with the specified text.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
            {
                // Set the QR code error correction level (optional).
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Save the generated QR code as a PNG image into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning for reading.
            ms.Position = 0;

            // Initialize a barcode reader to decode QR codes from the memory stream.
            using (var reader = new BarCodeReader(ms, DecodeType.QR))
            {
                // Attempt to read all barcodes present in the stream.
                var results = reader.ReadBarCodes();

                // If no barcodes were detected, inform the user.
                if (results.Length == 0)
                {
                    Console.WriteLine("No QR code detected.");
                }
                else
                {
                    // Iterate through each detected barcode and display its details.
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Detected QR Code Type: {result.CodeTypeName}");
                        Console.WriteLine($"Decoded Text: {result.CodeText}");
                        Console.WriteLine($"Confidence: {result.Confidence}");
                        Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                    }
                }
            }
        }
    }
}