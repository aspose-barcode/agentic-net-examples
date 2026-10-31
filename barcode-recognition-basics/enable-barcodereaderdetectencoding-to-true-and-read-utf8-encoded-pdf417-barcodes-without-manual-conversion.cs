// Title: Read UTF-8 PDF417 Barcodes with DetectEncoding Enabled
// Description: Demonstrates generating a PDF417 barcode encoded in UTF-8 and reading it back using BarCodeReader with DetectEncoding set to true, eliminating the need for manual text conversion.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating PDF417 symbols with ECI UTF-8 encoding and BarCodeReader for decoding them, highlighting typical scenarios such as international text handling and automatic character set detection. Developers working with multi-language barcodes often need to enable DetectEncoding to correctly interpret encoded data without extra conversion steps.
// Prompt: Enable BarCodeReader.DetectEncoding to true and read UTF8 encoded PDF417 barcodes without manual conversion.
// Tags: pdf417,utf-8,detectencoding,barcode generation,barcode recognition,aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides a sample that generates a PDF417 barcode with UTF‑8 text and reads it using DetectEncoding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary directory, generates a PDF417 barcode image, and reads it back with automatic encoding detection.
    /// </summary>
    static void Main()
    {
        // Define a temporary folder to store the generated barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "pdf417_utf8.png");

        // Generate a PDF417 barcode with UTF-8 encoded text ("こんにちは世界")
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417))
        {
            // Set PDF417 to use ECI mode with UTF-8 encoding
            generator.Parameters.Barcode.Pdf417.EncodeMode = Pdf417EncodeMode.ECI;
            generator.Parameters.Barcode.Pdf417.ECIEncoding = ECIEncodings.UTF8;

            // Assign the Unicode text and specify UTF-8 encoding explicitly
            generator.SetCodeText("こんにちは世界", Encoding.UTF8);

            // Save the barcode as a PNG image
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode image with DetectEncoding enabled to auto‑detect UTF-8 text
        using (var reader = new BarCodeReader(imagePath, DecodeType.Pdf417))
        {
            // Enable automatic character set detection
            reader.BarcodeSettings.DetectEncoding = true;

            // Perform the recognition
            BarCodeResult[] results = reader.ReadBarCodes();

            // Handle case where no barcode was found
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            // Output the type and decoded text of each detected barcode
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }
    }
}