// Title: Read UTF8 PDF417 Barcodes with Automatic Encoding Detection
// Description: Demonstrates generating a PDF417 barcode containing UTF‑8 text and reading it using BarCodeReader with DetectEncoding enabled, eliminating manual character conversion.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator to create a PDF417 barcode with ECI UTF‑8 encoding and how to employ BarCodeReader to decode the barcode automatically. Developers working with multi‑language data, QR codes, or PDF417 symbologies often need to handle character encoding correctly; the DetectEncoding property simplifies this by detecting and converting encoded text without extra code.
// Prompt: Enable BarCodeReader.DetectEncoding to true and read UTF8 encoded PDF417 barcodes without manual conversion.
// Tags: pdf417, barcode, encoding, detection, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a PDF417 barcode with UTF‑8 encoded text and reads it back using
/// Aspose.BarCode's automatic encoding detection feature.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, generates a barcode,
    /// and reads it twice – once with automatic encoding detection and once without.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary directory for the barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposePdf417Demo");
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "pdf417_utf8.png");

        // ------------------------------------------------------------
        // Generate a PDF417 barcode that contains UTF‑8 encoded text
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417))
        {
            string unicodeText = "Aspose常に先を行く";
            // Set the code text with explicit UTF‑8 encoding
            generator.SetCodeText(unicodeText, Encoding.UTF8);
            // Ensure the barcode embeds the UTF‑8 ECI identifier
            generator.Parameters.Barcode.Pdf417.ECIEncoding = ECIEncodings.UTF8;
            // Adjust image resolution (optional)
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            // Save the barcode as a PNG file
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode with DetectEncoding set to true
        // ------------------------------------------------------------
        Console.WriteLine("Reading with DetectEncoding = true:");
        BaseDecodeType decodeType = DecodeType.Pdf417;
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            // Enable automatic detection of the encoded character set
            reader.BarcodeSettings.DetectEncoding = true;
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }

        // ------------------------------------------------------------
        // Read the barcode with DetectEncoding set to false (manual conversion)
        // ------------------------------------------------------------
        Console.WriteLine("\nReading with DetectEncoding = false:");
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            // Disable automatic encoding detection
            reader.BarcodeSettings.DetectEncoding = false;
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"Raw CodeText: {result.CodeText}");
                // Example of manual conversion if needed:
                // string decoded = Encoding.UTF8.GetString(Encoding.Default.GetBytes(result.CodeText));
                // Console.WriteLine($"Decoded CodeText: {decoded}");
            }
        }
    }
}