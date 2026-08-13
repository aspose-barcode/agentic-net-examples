// Title: Read UTF-8 PDF417 barcode with automatic encoding detection
// Description: Demonstrates enabling DetectEncoding on BarCodeReader to correctly decode UTF-8 encoded PDF417 barcodes.
// Category-Description: This example belongs to the Aspose.BarCode barcode reading category, focusing on decoding PDF417 symbology with Unicode text. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for scanning, and the DetectEncoding setting to handle UTF-8 data automatically. Developers often need to read barcodes containing non‑ASCII characters without manual byte‑to‑string conversion.
// Prompt: Enable BarCodeReader.DetectEncoding to true and read UTF8 encoded PDF417 barcodes without manual conversion.
// Tags: pdf417, barcode, encoding detection, utf8, read, generation, aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a PDF417 barcode with UTF‑8 text and reading it back using automatic encoding detection.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode image, verifies its creation, and reads the encoded text.
    /// </summary>
    static void Main()
    {
        // Path for the generated barcode image
        string imagePath = "pdf417.png";

        // Create a PDF417 barcode with UTF-8 encoded text
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, string.Empty))
        {
            // Set Unicode text using UTF-8 encoding
            generator.SetCodeText("Привет мир", Encoding.UTF8);
            // Save the barcode image as PNG
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image '{imagePath}' was not created.");
            return;
        }

        // Read the barcode and enable automatic encoding detection
        using (var reader = new BarCodeReader(imagePath, DecodeType.Pdf417))
        {
            // Ensure DetectEncoding is enabled (default is true, but set explicitly)
            reader.BarcodeSettings.DetectEncoding = true;

            // Process detected barcodes
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Decoded CodeText: {result.CodeText}");
            }
        }
    }
}