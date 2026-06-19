using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a high‑resolution DataMatrix barcode,
/// saving it to a memory stream, and then recognizing it from that stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a DataMatrix barcode at 600 DPI, writes it to a memory stream,
    /// and reads it back using a high‑quality recognition preset.
    /// </summary>
    static void Main()
    {
        // Create a memory stream to hold the generated barcode image.
        using (var ms = new MemoryStream())
        {
            // Initialize the barcode generator for a DataMatrix with the text "ABC123".
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "ABC123"))
            {
                // Set a high resolution (600 DPI) to improve detection of small symbols.
                generator.Parameters.Resolution = 600f;

                // Save the generated barcode as a PNG image into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning before reading.
            ms.Position = 0;

            // Initialize a barcode reader to decode DataMatrix codes from the stream.
            using (var reader = new BarCodeReader(ms, DecodeType.DataMatrix))
            {
                // Apply a high‑quality preset to aid detection of small barcodes.
                reader.QualitySettings = QualitySettings.HighQuality;

                // Iterate through all detected barcodes and output their text.
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected DataMatrix code text: {result.CodeText}");
                }
            }
        }
    }
}