// Title: QR Code UTF-8 Automatic Detection Unit Test
// Description: Demonstrates generating a QR code with multilingual text and verifies that automatic UTF-8 detection correctly decodes it.
// Prompt: Create a unit test verifying automatic UTF8 detection works for a generated QR code containing multilingual text.
// Tags: qr,utf-8,encoding,detection,barcode,generation,recognition,unit-test

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a QR code containing multilingual text,
/// then reads it back with and without automatic UTF‑8 detection enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, reads it twice,
    /// and prints the results to verify automatic UTF‑8 detection.
    /// </summary>
    static void Main()
    {
        // Multilingual text (Russian + Chinese) to be encoded in the QR code.
        string originalText = "Привет 世界";

        // Use a memory stream to avoid file I/O.
        using (var ms = new MemoryStream())
        {
            // ---------- QR code generation ----------
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Encode the text using UTF‑8 (adds BOM if needed).
                generator.SetCodeText(originalText, Encoding.UTF8);
                // Save the generated QR code as PNG into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset the stream position so it can be read from the beginning.
            ms.Position = 0;

            // ---------- Detection enabled ----------
            string detectedWithEncoding;
            using (var reader = new BarCodeReader(ms, DecodeType.QR))
            {
                // Enable automatic UTF‑8 detection.
                reader.BarcodeSettings.DetectEncoding = true;
                // Read all barcodes from the stream.
                var result = reader.ReadBarCodes();
                // Extract the decoded text if a barcode was found.
                detectedWithEncoding = result.Length > 0 ? result[0].CodeText : string.Empty;
            }

            // Reset the stream again for the second read operation.
            ms.Position = 0;

            // ---------- Detection disabled ----------
            string detectedWithoutEncoding;
            using (var reader = new BarCodeReader(ms, DecodeType.QR))
            {
                // Disable automatic UTF‑8 detection.
                reader.BarcodeSettings.DetectEncoding = false;
                var result = reader.ReadBarCodes();
                detectedWithoutEncoding = result.Length > 0 ? result[0].CodeText : string.Empty;
            }

            // Verify that detection works: with detection the text matches,
            // without detection it does not (due to encoding mismatch).
            bool detectionWorks = detectedWithEncoding == originalText && detectedWithoutEncoding != originalText;

            // Output the results to the console.
            Console.WriteLine("Original Text:                     " + originalText);
            Console.WriteLine("Detected (DetectEncoding=true):   " + detectedWithEncoding);
            Console.WriteLine("Detected (DetectEncoding=false):  " + detectedWithoutEncoding);
            Console.WriteLine("Automatic UTF-8 detection works:  " + detectionWorks);
        }
    }
}