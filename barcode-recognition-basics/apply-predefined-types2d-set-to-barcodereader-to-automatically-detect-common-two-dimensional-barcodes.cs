using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR code and reading it using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code in memory,
    /// then reads and prints its type and text.
    /// </summary>
    static void Main()
    {
        // Create a QR code generator with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            // Set the QR error correction level to Medium (LevelM).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Generate the QR code image as a bitmap.
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Initialize a reader that can detect any 2D barcode type.
                using (var reader = new BarCodeReader(bitmap, DecodeType.Types2D))
                {
                    // Iterate through all detected barcodes.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Output the detected barcode type.
                        Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                        // Output the decoded text of the barcode.
                        Console.WriteLine($"Code Text: {result.CodeText}");
                    }
                }
            }
        }
    }
}