// Title: Disable DetectEncoding and manually decode Unicode QR barcode
// Description: Demonstrates generating a QR code with UTF-16 encoded text, disabling automatic encoding detection during recognition, and manually decoding the raw byte data using Encoding.Unicode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases how to use BarcodeGenerator to create 2D barcodes with custom encoding, and BarCodeReader to read them while turning off DetectEncoding. Developers often need to control encoding for Unicode barcodes, retrieve raw byte data, and perform manual decoding for precise text handling.
// Prompt: Disable DetectEncoding and manually decode raw byte data using Encoding.UTF16 for Unicode 2D barcodes.
// Tags: qr, unicode, detectencoding, manual-decoding, barcode-generation, barcode-recognition, aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR code with UTF-16 encoded text,
/// reads it back with automatic encoding detection disabled,
/// and manually decodes the raw byte data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, recognition,
    /// manual decoding, and cleanup.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary file path for the barcode image
        string tempPath = Path.Combine(Path.GetTempPath(), "UnicodeBarcode.png");

        // ------------------------------------------------------------
        // Generate a QR code containing UTF-16 encoded text (no ECI)
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set module size (pixel dimension) for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 8;

            // Encode the text using UTF-16 (Unicode) explicitly
            generator.SetCodeText("Aspose Unicode 漢字", Encoding.Unicode);

            // Set the display text that appears under the barcode (optional)
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "Aspose Unicode 漢字";

            // Save the generated barcode as a PNG image
            generator.Save(tempPath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(tempPath))
        {
            Console.WriteLine($"Failed to create barcode image at {tempPath}");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode with DetectEncoding disabled and decode manually
        // ------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(tempPath, DecodeType.QR))
        {
            // Turn off automatic encoding detection
            reader.BarcodeSettings.DetectEncoding = false;

            // Iterate through all detected barcodes (only one expected)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Code Type: {result.CodeTypeName}");

                // Retrieve the raw byte array representing the encoded text
                byte[] rawBytes = result.CodeBytes;
                Console.WriteLine($"Raw Bytes Length: {rawBytes.Length}");

                // Manually decode the raw bytes using UTF-16 (Unicode)
                string decodedText = Encoding.Unicode.GetString(rawBytes);
                Console.WriteLine($"Manually Decoded Text (UTF-16): {decodedText}");
            }
        }

        // ------------------------------------------------------------
        // Clean up the temporary barcode image file
        // ------------------------------------------------------------
        try
        {
            File.Delete(tempPath);
        }
        catch
        {
            // Suppress any exceptions during cleanup
        }
    }
}