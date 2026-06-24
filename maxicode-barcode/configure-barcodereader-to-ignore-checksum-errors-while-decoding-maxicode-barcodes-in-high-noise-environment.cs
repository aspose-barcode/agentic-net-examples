using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation and recognition of a MaxiCode barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a MaxiCode barcode, stores it in memory, and then reads it back.
    /// </summary>
    static void Main()
    {
        // Create an in‑memory stream to hold the generated barcode image.
        using (var imageStream = new MemoryStream())
        {
            // Generate a MaxiCode barcode with the specified data.
            using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "123456789012"))
            {
                // Save the generated barcode as a PNG image into the memory stream.
                generator.Save(imageStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning before reading.
            imageStream.Position = 0;

            // Initialize a barcode reader configured for MaxiCode symbology.
            using (var reader = new BarCodeReader(imageStream, DecodeType.MaxiCode))
            {
                // Disable checksum validation to tolerate noisy or damaged barcodes.
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

                // Use high‑quality settings to improve detection of damaged barcodes.
                reader.QualitySettings = QualitySettings.HighQuality;

                // Allow recognition of barcodes even if their checksums are incorrect.
                reader.QualitySettings.AllowIncorrectBarcodes = true;

                // Read all barcodes from the stream and output their text.
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected MaxiCode: {result.CodeText}");
                }
            }
        }
    }
}