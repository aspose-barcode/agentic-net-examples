// Title: Read UTF8 PDF417 Barcodes with DetectEncoding Enabled
// Description: Demonstrates generating a PDF417 barcode containing UTF‑8 text and reading it using BarCodeReader with DetectEncoding set to true, eliminating the need for manual character conversion.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator to create PDF417 symbols, BarCodeReader to decode them, and the DetectEncoding setting to automatically handle UTF‑8 encoded data. Developers working with multilingual barcodes, especially PDF417, often need to generate and read encoded text without manual byte‑to‑string conversions.
// Prompt: Enable BarCodeReader.DetectEncoding to true and read UTF8 encoded PDF417 barcodes without manual conversion.
// Tags: pdf417, barcode, encoding, detection, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a PDF417 barcode with UTF‑8 text and reading it using DetectEncoding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Define a temporary directory to store the generated barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposePdf417Demo");
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "pdf417_utf8.png");

        // Generate a PDF417 barcode that encodes UTF‑8 text
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417))
        {
            generator.SetCodeText("Пример UTF8 текста", Encoding.UTF8);
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // -----------------------------------------------------------------
        // Read the barcode with DetectEncoding set to true (automatic UTF‑8 handling)
        // -----------------------------------------------------------------
        Console.WriteLine("Reading with DetectEncoding = true:");
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Pdf417))
        {
            reader.BarcodeSettings.DetectEncoding = true;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }

        // -----------------------------------------------------------------
        // Read the barcode with DetectEncoding set to false (manual conversion may be required)
        // -----------------------------------------------------------------
        Console.WriteLine("\nReading with DetectEncoding = false:");
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Pdf417))
        {
            reader.BarcodeSettings.DetectEncoding = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"Raw CodeText: {result.CodeText}");
                // Example of manual decoding if needed:
                // string decoded = Encoding.UTF8.GetString(Encoding.Default.GetBytes(result.CodeText));
                // Console.WriteLine($"Manually decoded: {decoded}");
            }
        }
    }
}