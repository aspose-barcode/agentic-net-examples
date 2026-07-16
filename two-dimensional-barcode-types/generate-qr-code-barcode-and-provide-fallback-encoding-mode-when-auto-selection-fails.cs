// Title: Generate QR Code with fallback encoding mode
// Description: Demonstrates creating a QR Code barcode containing Unicode characters and handling auto‑selection failures by switching to explicit ECI UTF‑8 encoding.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation and encoding mode management. It showcases the use of BarcodeGenerator, EncodeTypes, QREncodeMode, and ECIEncodings classes to produce QR codes, a common requirement for applications needing to embed multilingual data. Developers often need to handle cases where automatic mode selection cannot encode the input, requiring a fallback to a specific encoding.
// Prompt: Generate a QR Code barcode and provide fallback encoding mode when auto selection fails.
// Tags: qr code, fallback encoding, eci, unicode, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode with Unicode text and providing a fallback encoding mode when automatic selection fails.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code, first using Auto mode, and falls back to ECI UTF‑8 mode if needed.
    /// </summary>
    static void Main()
    {
        // Text to encode; includes Unicode characters to test encoding handling.
        string codeText = "Sample QR with Unicode 漢字";

        // Output file path for the generated QR code image.
        string outputPath = "qr.png";

        // Try generating the QR code using the default Auto encoding mode.
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Set the QR encoding mode explicitly to Auto for clarity.
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Auto;

                // Save the generated barcode image to the specified path.
                generator.Save(outputPath);
                Console.WriteLine($"QR code generated with Auto mode: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Auto mode failed (e.g., due to unsupported characters). Log the error.
            Console.WriteLine($"Auto mode failed: {ex.Message}");

            // Fallback: generate the QR code using explicit ECI UTF‑8 encoding.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Switch to ECI encoding mode.
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECIEncoding;

                // Specify UTF‑8 as the ECI encoding to support Unicode characters.
                generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

                // Save the fallback barcode image.
                generator.Save(outputPath);
                Console.WriteLine($"QR code generated with fallback ECI UTF-8 mode: {outputPath}");
            }
        }
    }
}