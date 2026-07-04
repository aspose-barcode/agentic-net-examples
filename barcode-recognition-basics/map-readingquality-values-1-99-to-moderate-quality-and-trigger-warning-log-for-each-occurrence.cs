// Title: Barcode Generation, Reading, and Quality Mapping
// Description: Generates a Code128 barcode, reads it, and maps reading quality values 1‑99 to moderate quality, logging a warning for each occurrence.
// Prompt: Map ReadingQuality values 1‑99 to moderate quality and trigger a warning log for each occurrence.
// Tags: barcode, generation, recognition, readingquality, warning, console

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, recognition, and mapping of ReadingQuality values to moderate quality with warning logs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it, and processes the reading quality.
    /// </summary>
    static void Main()
    {
        // Generate a sample barcode and store it in a memory stream
        using (var ms = new MemoryStream())
        {
            // Create a barcode generator for Code128 with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Save the generated barcode as PNG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset stream position before reading the image
            ms.Position = 0;

            // Load the image from the memory stream into a Bitmap (Aspose.Drawing)
            using (var bitmap = new Bitmap(ms))
            {
                // Create a reader that detects all supported barcode types
                using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                {
                    // Iterate through each detected barcode result
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Output basic barcode information
                        Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                        Console.WriteLine($"Code Text: {result.CodeText}");
                        Console.WriteLine($"Reading Quality: {result.ReadingQuality}");

                        // Map ReadingQuality values 1‑99 to moderate quality and log a warning
                        if (result.ReadingQuality >= 1 && result.ReadingQuality <= 99)
                        {
                            Console.WriteLine($"Warning: ReadingQuality {result.ReadingQuality} is considered moderate.");
                        }

                        Console.WriteLine(); // Blank line for readability between results
                    }
                }
            }
        }
    }
}