// Title: Generate QR Code with automatic size based on payload length
// Description: Demonstrates creating QR Code barcodes where the symbol size automatically adjusts to the length of the input text.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator with EncodeTypes.QR. It illustrates how to configure basic parameters such as X‑Dimension and rely on the library's auto‑version feature to size the QR code appropriately. Developers working with dynamic QR code creation for varying payloads can use this pattern to produce PNG images without manually setting the QR version.
// Prompt: Generate QR Code barcode and enable automatic size to adapt to payload length.
// Tags: qr code, barcode generation, automatic sizing, png output, aspose.barcode, encode types, xdimension

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating QR Code barcodes with automatic sizing based on payload length.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates QR codes for a set of sample payloads and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Sample payloads of varying lengths to illustrate automatic QR size adjustment
        string[] payloads = new string[]
        {
            "Short",
            "A longer payload text for QR code",
            "Even longer payload text that should increase the QR code size automatically based on content length."
        };

        int index = 1;
        // Iterate over each payload, generate a QR code, and save it as a PNG file
        foreach (string text in payloads)
        {
            // Build the file path for the current QR code image
            string filePath = Path.Combine(outputDir, $"QRCode_{index}.png");

            // Create a BarcodeGenerator for QR encoding with the current payload
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                // Set the X dimension (module size) in pixels; 4 pixels per module
                generator.Parameters.Barcode.XDimension.Pixels = 4f;

                // No explicit QR version is set; the library automatically selects the appropriate size
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Output the location of the generated QR code
            Console.WriteLine($"Generated QR code {index}: {filePath}");
            index++;
        }
    }
}