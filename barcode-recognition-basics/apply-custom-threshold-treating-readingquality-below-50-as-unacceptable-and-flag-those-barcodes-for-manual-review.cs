using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating Code128 barcodes, saving them to memory,
/// and then recognizing them using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates sample barcodes, reads them back, and reports their reading quality.
    /// </summary>
    static void Main()
    {
        // Sample barcode texts to process
        string[] samples = new string[] { "1234567890", "ABCDEFGHIJ", "12345" };

        // Iterate over each sample text
        foreach (string text in samples)
        {
            // Generate barcode image in a memory stream
            using (var ms = new MemoryStream())
            {
                // Create a barcode generator for Code128 with the current text
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
                {
                    // Save the generated barcode as PNG into the memory stream
                    generator.Save(ms, BarCodeImageFormat.Png);
                }

                // Reset stream position to the beginning before reading
                ms.Position = 0;

                // Load the PNG image from the memory stream as a Bitmap for recognition
                using (var bitmap = new Bitmap(ms))
                {
                    // Initialize a barcode reader that can decode all supported symbologies
                    using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                    {
                        // Read all barcodes found in the image
                        foreach (var result in reader.ReadBarCodes())
                        {
                            double quality = result.ReadingQuality;

                            // Flag low-quality readings for manual review
                            if (quality < 50.0)
                            {
                                Console.WriteLine($"[FLAGGED] CodeText: {result.CodeText}, ReadingQuality: {quality:F2}% – requires manual review.");
                            }
                            else
                            {
                                Console.WriteLine($"[ACCEPTED] CodeText: {result.CodeText}, ReadingQuality: {quality:F2}%");
                            }
                        }
                    }
                }
            }
        }
    }
}