// Title: Read UTF-8 PDF417 Barcodes with Automatic Encoding Detection
// Description: Demonstrates generating a PDF417 barcode containing UTF-8 Cyrillic text and reading it back using BarCodeReader with DetectEncoding enabled, eliminating manual conversion.
// Prompt: Enable BarCodeReader.DetectEncoding to true and read UTF8 encoded PDF417 barcodes without manual conversion.
// Tags: pdf417, barcode, encoding, detection, aspnet, csharp

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a PDF417 barcode with UTF‑8 encoded text
/// and reads it back using automatic encoding detection.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, then decodes it while automatically detecting the text encoding.
    /// </summary>
    static void Main()
    {
        // Sample UTF-8 text containing Cyrillic characters
        const string utf8Text = "Пример UTF-8 текста";

        // Generate a PDF417 barcode with UTF-8 encoding and store it in a memory stream
        using (var barcodeStream = new MemoryStream())
        {
            // Create a barcode generator for PDF417 symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417))
            {
                // Encode the text using UTF-8 (adds BOM if needed)
                generator.SetCodeText(utf8Text, Encoding.UTF8);
                // Save the barcode image to the stream in PNG format
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset stream position before reading
            barcodeStream.Position = 0;

            // Create a reader for PDF417 barcodes from the stream
            using (var reader = new BarCodeReader(barcodeStream, DecodeType.Pdf417))
            {
                // Enable automatic detection of the text encoding
                reader.BarcodeSettings.DetectEncoding = true;

                // Read all barcodes found in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                    Console.WriteLine($"Decoded Text: {result.CodeText}");
                }
            }
        }
    }
}