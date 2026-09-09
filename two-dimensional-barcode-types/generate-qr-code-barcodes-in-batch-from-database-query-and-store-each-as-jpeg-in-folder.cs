// Title: Batch QR Code Generation from Database Records to JPEG Files
// Description: Demonstrates generating QR Code barcodes for multiple records retrieved from a database and saving each as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.QR to create QR codes in bulk. Typical use cases include batch processing of data rows, exporting barcodes to image files, and integrating barcode creation into automated workflows. Developers often need to configure dimensions, error correction levels, and output formats, which this sample showcases.
// Prompt: Generate QR Code barcodes in batch from database query and store each as JPEG in folder.
// Tags: qr code, batch generation, jpeg, aspose.barcode, barcodegenerator, encode types, image export

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates QR Code barcodes for a set of records and saves each as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Simulates a database query, creates a unique output folder,
    /// generates QR codes for each record, and saves them as JPEG images.
    /// </summary>
    static void Main()
    {
        // Simulated database query result: a list of record IDs and associated QR code text.
        List<(int Id, string CodeText)> records = new List<(int, string)>
        {
            (1, "https://example.com/1"),
            (2, "https://example.com/2"),
            (3, "https://example.com/3"),
            (4, "https://example.com/4"),
            (5, "https://example.com/5")
        };

        // Create a unique temporary output folder for the generated images.
        string outputFolder = Path.Combine(Path.GetTempPath(), "QrBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Output folder: {outputFolder}");

        // Iterate over each record and generate a QR code image.
        foreach (var record in records)
        {
            try
            {
                // Initialize the barcode generator with QR encoding and the record's text.
                using (var generator = new BarcodeGenerator(EncodeTypes.QR, record.CodeText))
                {
                    // Optional appearance adjustments.
                    generator.Parameters.Barcode.XDimension.Pixels = 4f;               // Set module size.
                    generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM; // Set error correction level.
                    generator.Parameters.Resolution = 300f;                           // Set image resolution.

                    // Build a safe file name by replacing invalid characters.
                    string safeText = record.CodeText.Replace(Path.GetInvalidFileNameChars(), '_');
                    string fileName = $"{record.Id}_{safeText}.jpg";
                    string filePath = Path.Combine(outputFolder, fileName);

                    // Save the generated QR code as a JPEG image.
                    generator.Save(filePath, BarCodeImageFormat.Jpeg);
                    Console.WriteLine($"Saved QR code for record {record.Id} to {filePath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during generation for this record.
                Console.WriteLine($"Failed to generate QR code for record {record.Id}: {ex.Message}");
            }
        }

        Console.WriteLine("Batch generation completed.");
    }
}

// Extension method to replace invalid filename characters with a specified replacement character.
static class StringExtensions
{
    /// <summary>
    /// Replaces each character in the provided array with the specified replacement character.
    /// </summary>
    /// <param name="str">The original string.</param>
    /// <param name="chars">Array of characters to replace.</param>
    /// <param name="replacement">The character to insert in place of each invalid character.</param>
    /// <returns>A new string with invalid characters replaced.</returns>
    public static string Replace(this string str, char[] chars, char replacement)
    {
        foreach (char c in chars)
        {
            str = str.Replace(c, replacement);
        }
        return str;
    }
}