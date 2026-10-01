// Title: Disable Encoding Detection and Manually Decode Unicode QR Code
// Description: Demonstrates generating a QR code with Unicode (Japanese) text, turning off automatic encoding detection during recognition, and manually decoding the raw byte data using UTF-16.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating 2D barcodes and BarCodeReader for extracting raw byte data. Developers often need to control encoding handling for multilingual content, especially when automatic detection is unsuitable. The snippet highlights key API classes (BarcodeGenerator, BarCodeReader, BarcodeSettings) and typical scenarios such as custom decoding of Unicode barcodes.
// Prompt: Disable DetectEncoding and manually decode raw byte data using Encoding.UTF16 for Unicode 2D barcodes.
// Tags: qr, unicode, encoding, detection, manual-decode, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates disabling automatic encoding detection and manually decoding Unicode QR code data using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// Generates a QR code with Japanese text, reads it without automatic encoding detection, and decodes using UTF-16.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "unicode_qr.png");

        // Sample Unicode text (Japanese)
        string unicodeText = "こんにちは世界";

        // Generate a QR code containing the Unicode text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, unicodeText))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Ensure the image was created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Read the barcode with automatic encoding detection turned off
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            // Disable automatic encoding detection
            reader.BarcodeSettings.DetectEncoding = false;

            // Iterate through all detected barcodes (only one expected)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Obtain the raw byte data from the decoded result
                byte[] rawBytes = result.CodeBytes;

                // Manually decode the bytes using UTF-16 (Unicode)
                string decodedText = Encoding.Unicode.GetString(rawBytes);

                Console.WriteLine("Decoded text (UTF-16): " + decodedText);
            }
        }

        // Clean up temporary files (ignore any errors during deletion)
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // No action needed
        }
    }
}