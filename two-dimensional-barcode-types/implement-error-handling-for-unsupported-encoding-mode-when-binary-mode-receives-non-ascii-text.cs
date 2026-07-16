// Title: QR Code Generation with Binary Mode Error Handling
// Description: Demonstrates generating a QR code in Binary mode, handling unsupported non‑ASCII characters, and falling back to Auto mode with UTF‑8 encoding.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to work with the BarcodeGenerator class to create QR codes. It illustrates typical use cases such as setting encoding modes, handling exceptions for unsupported characters, and using ECI encoding for Unicode support. Developers often need to manage encoding constraints when generating barcodes for internationalized data.
// Prompt: Implement error handling for unsupported encoding mode when Binary mode receives non‑ASCII text.
// Tags: qr, binary, error handling, fallback, eciencoding, aspose.barcode, png

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a QR code, handles unsupported characters in Binary mode,
/// and falls back to Auto mode with UTF‑8 ECI encoding when necessary.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR barcode, catches encoding errors,
    /// and retries with a compatible encoding mode.
    /// </summary>
    static void Main()
    {
        // Sample non‑ASCII text that is not allowed in Binary mode (Japanese characters)
        string nonAsciiText = "テスト";

        // Destination file for the generated barcode image
        string outputPath = "qr_binary.png";

        // Attempt to generate the QR barcode using Binary encoding mode
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Set Binary encoding mode – will throw if text contains non‑ASCII characters
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Binary;

                // Assign the non‑ASCII code text
                generator.CodeText = nonAsciiText;

                // Save the barcode image as PNG
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode saved successfully to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Handle the expected exception for unsupported characters in Binary mode
            Console.WriteLine($"Error generating barcode in Binary mode: {ex.Message}");
            Console.WriteLine("Falling back to Auto mode with the same text.");

            // Retry using Auto mode, which supports Unicode via ECI encoding
            try
            {
                using (var generator = new BarcodeGenerator(EncodeTypes.QR))
                {
                    // Set Auto encoding mode and specify UTF‑8 ECI encoding
                    generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Auto;
                    generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

                    // Reassign the same non‑ASCII text
                    generator.CodeText = nonAsciiText;

                    // Save the barcode image as PNG
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Barcode saved in Auto mode to '{outputPath}'.");
                }
            }
            catch (Exception fallbackEx)
            {
                // Report failure of the fallback attempt
                Console.WriteLine($"Fallback also failed: {fallbackEx.Message}");
            }
        }
    }
}