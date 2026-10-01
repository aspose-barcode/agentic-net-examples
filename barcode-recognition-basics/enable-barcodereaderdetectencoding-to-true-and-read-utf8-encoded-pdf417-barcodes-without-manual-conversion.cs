// Title: Read UTF-8 PDF417 Barcodes with Automatic Encoding Detection
// Description: Demonstrates creating a PDF417 barcode containing UTF‑8 text and reading it back using BarCodeReader with DetectEncoding enabled, eliminating manual conversion.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, focusing on PDF417 symbology. Developers often need to handle non‑ASCII data, and enabling DetectEncoding simplifies reading UTF‑8 encoded barcodes without extra conversion steps.
// Prompt: Enable BarCodeReader.DetectEncoding to true and read UTF8 encoded PDF417 barcodes without manual conversion.
// Tags: pdf417, barcode, encoding, detection, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample program that creates a PDF417 barcode with UTF‑8 text and reads it back
/// using automatic encoding detection.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode if needed and decodes it
    /// with <c>BarCodeReader.DetectEncoding</c> set to <c>true</c>.
    /// </summary>
    static void Main()
    {
        // Define a temporary file path for the barcode image.
        string imagePath = Path.Combine(Path.GetTempPath(), "sample_pdf417.png");

        // Create a PDF417 barcode with UTF‑8 text if the file does not already exist.
        if (!File.Exists(imagePath))
        {
            // Sample UTF‑8 text containing non‑ASCII characters (Japanese).
            string utf8Text = "こんにちは世界"; // "Hello World" in Japanese

            // Generate the barcode and save it as a PNG image.
            using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, utf8Text))
            {
                generator.Save(imagePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode image created at: {imagePath}");
            }
        }
        else
        {
            Console.WriteLine($"Using existing barcode image at: {imagePath}");
        }

        // Verify the image file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Error: Barcode image file not found.");
            return;
        }

        // Read the barcode with automatic encoding detection enabled.
        using (var reader = new BarCodeReader(imagePath, DecodeType.Pdf417))
        {
            // Enable detection of the text encoding (e.g., UTF‑8) automatically.
            reader.BarcodeSettings.DetectEncoding = true;

            bool found = false;
            // Iterate through all detected barcodes in the image.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine("Decoded Text: " + result.CodeText);
                Console.WriteLine("Symbology   : " + result.CodeTypeName);
                found = true;
            }

            if (!found)
            {
                Console.WriteLine("No barcode detected in the image.");
            }
        }
    }
}