using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation of a QR barcode with UTF‑8 text and compares automatic
/// encoding detection with manual decoding using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code, reads it with automatic encoding detection,
    /// then reads it again with detection disabled and manually decodes the result.
    /// </summary>
    static void Main()
    {
        // Sample UTF‑8 text to encode in the QR barcode.
        const string originalText = "Слово";

        // Create an in‑memory stream to hold the generated barcode image.
        using (var imageStream = new MemoryStream())
        {
            // ---------- Barcode generation ----------
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Set the code text and explicitly specify UTF‑8 encoding.
                generator.SetCodeText(originalText, Encoding.UTF8);
                // Save the barcode as PNG into the memory stream.
                generator.Save(imageStream, BarCodeImageFormat.Png);
            }

            // Reset stream position so it can be read from the beginning.
            imageStream.Position = 0;

            // ---------- Automatic encoding detection ----------
            string autoDetectedText;
            using (var autoReader = new BarCodeReader(imageStream, DecodeType.QR))
            {
                // Enable automatic detection of the text encoding.
                autoReader.BarcodeSettings.DetectEncoding = true;
                // Read all barcodes from the stream.
                var result = autoReader.ReadBarCodes();
                // Extract the decoded text if a barcode was found.
                autoDetectedText = result.Length > 0 ? result[0].CodeText : null;
            }

            // Reset stream again for the second read operation.
            imageStream.Position = 0;

            // ---------- Manual decoding (automatic detection disabled) ----------
            string manualDecodedText;
            using (var manualReader = new BarCodeReader(imageStream, DecodeType.QR))
            {
                // Disable automatic encoding detection.
                manualReader.BarcodeSettings.DetectEncoding = false;
                var result = manualReader.ReadBarCodes();

                if (result.Length > 0 && result[0].CodeText != null)
                {
                    // When detection is off, the engine returns characters using ISO‑8859‑1 mapping.
                    // Convert those characters back to the original byte sequence and decode as UTF‑8.
                    var isoEncoding = Encoding.GetEncoding("ISO-8859-1");
                    byte[] rawBytes = isoEncoding.GetBytes(result[0].CodeText);
                    manualDecodedText = Encoding.UTF8.GetString(rawBytes);
                }
                else
                {
                    manualDecodedText = null;
                }
            }

            // ---------- Result comparison ----------
            bool isEqual = string.Equals(autoDetectedText, manualDecodedText, StringComparison.Ordinal);
            Console.WriteLine(isEqual
                ? "Test passed: manual UTF-8 decoding matches automatic detection."
                : "Test failed: results differ.");

            // Output both decoded strings for verification.
            Console.WriteLine($"Automatic detection result: {autoDetectedText}");
            Console.WriteLine($"Manual decoding result:   {manualDecodedText}");
        }
    }
}