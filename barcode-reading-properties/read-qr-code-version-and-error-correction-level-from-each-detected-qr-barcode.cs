// Title: Read QR Code version and error correction level from detected QR barcodes
// Description: Demonstrates generating QR codes with specific version and error correction level, then reading them back to retrieve those properties.
// Prompt: Read QR Code version and error correction level from each detected QR barcode.
// Tags: qr, barcode, version, error-correction, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates QR codes with specific version and error correction level,
/// then reads them back to display the detected version and error level.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample QR codes, reads them, and prints version and error level.
    /// </summary>
    static void Main()
    {
        // Define sample QR code configurations: version and error correction level
        var samples = new (int version, QRErrorLevel level)[]
        {
            (5, QRErrorLevel.LevelL),
            (10, QRErrorLevel.LevelM),
            (15, QRErrorLevel.LevelH)
        };

        // Iterate over each sample configuration
        foreach (var (version, level) in samples)
        {
            // Create a QR code generator with the sample text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR"))
            {
                // Set the desired QR version and error correction level
                generator.Parameters.Barcode.QR.Version = (QRVersion)version;
                generator.Parameters.Barcode.QR.ErrorLevel = level;

                // Save the generated QR code to a memory stream in PNG format
                using (MemoryStream ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0; // Reset stream position for reading

                    // Initialize a QR code reader on the memory stream
                    using (BarCodeReader reader = new BarCodeReader(ms, DecodeType.QR))
                    {
                        // Read all detected barcodes
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            // Ensure the detected barcode is a QR code
                            if (result.CodeTypeName.Equals("QR", StringComparison.OrdinalIgnoreCase))
                            {
                                // Output the detected QR version and error correction level
                                Console.WriteLine($"Detected QR - Version: {result.Extended.QR.Version}, ErrorLevel: {result.Extended.QR.ErrorLevel}");
                            }
                        }
                    }
                }
            }
        }
    }
}