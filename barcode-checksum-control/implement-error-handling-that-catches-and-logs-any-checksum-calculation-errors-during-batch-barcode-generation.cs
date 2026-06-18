using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a set of barcodes and saves them to disk.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output directory where barcode images will be saved.
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // --------------------------------------------------------------------
        // Define batch data: each tuple contains a barcode symbology and the text to encode.
        // --------------------------------------------------------------------
        var batch = new List<(BaseEncodeType type, string text)>
        {
            (EncodeTypes.Code39FullASCII, "ABC-123"),
            (EncodeTypes.EAN13, "123456789012"), // 12 digits, checksum will be calculated
            (EncodeTypes.ITF14, "1234567890123"), // 13 digits, checksum will be calculated
            (EncodeTypes.Codabar, "A12345B"), // valid Codabar
            (EncodeTypes.Code128, "Invalid|*?") // intentionally invalid characters for demonstration
        };

        int index = 1; // Counter for naming output files

        // --------------------------------------------------------------------
        // Iterate over each barcode definition, generate the image, and handle errors.
        // --------------------------------------------------------------------
        foreach (var item in batch)
        {
            // Build the full file path for the current barcode image.
            string filePath = Path.Combine(outputDir, $"barcode_{index}.png");

            try
            {
                // Create a generator for the specified symbology and text.
                using (var generator = new BarcodeGenerator(item.type, item.text))
                {
                    // Enable checksum calculation where the symbology supports it.
                    generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

                    // Save the generated barcode image to the file system.
                    generator.Save(filePath);

                    // Inform the user that the barcode was generated successfully.
                    Console.WriteLine($"Generated barcode {index}: {filePath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during barcode generation (e.g., invalid characters).
                Console.WriteLine($"Error generating barcode {index} (Symbology: {item.type.TypeName}): {ex.Message}");
            }

            index++; // Increment the counter for the next barcode.
        }

        // --------------------------------------------------------------------
        // Indicate that the batch processing has finished.
        // --------------------------------------------------------------------
        Console.WriteLine("Batch processing completed.");
    }
}