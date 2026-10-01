// Title: Read QR Code from byte array with UTF-16 detection using BarCodeReader
// Description: Demonstrates reading a QR code stored in a byte array and using DetectEncoding to correctly decode UTF‑16 text.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to generate a barcode in memory, serialize it to a byte array, and then read it back using BarCodeReader. It highlights key API classes such as BarcodeGenerator, BarCodeReader, and BarCodeResult, which developers commonly use for in‑memory barcode processing, Unicode support, and automated scanning scenarios. Ideal for applications that need to handle barcodes without persisting image files.
// Prompt: Use BarCodeReader to read a barcode from a byte array and ensure DetectEncoding correctly decodes UTF16 content.
// Tags: qr, barcode, read, byte-array, utf16, detectencoding, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR code containing UTF‑16 text,
/// stores it in a byte array, and reads it back using BarCodeReader
/// with DetectEncoding enabled to verify correct decoding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, reads it from memory,
    /// and prints the decoded text along with a verification of the original content.
    /// </summary>
    static void Main()
    {
        // Original text includes Unicode characters (emoji and Chinese) to test UTF‑16 handling.
        string originalText = "Hello 🌍 你好";

        // Create a QR code generator with empty initial text; we'll set the text with Unicode encoding.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, string.Empty))
        {
            // Assign the Unicode text to the barcode, specifying UTF‑16 (Encoding.Unicode).
            generator.SetCodeText(originalText, Encoding.Unicode);

            // Save the generated barcode image to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Convert the memory stream to a byte array for later reading.
                byte[] barcodeBytes = ms.ToArray();

                // Initialize a BarCodeReader with the byte array wrapped in a MemoryStream.
                using (var reader = new BarCodeReader(new MemoryStream(barcodeBytes), DecodeType.QR))
                {
                    // Enable automatic detection of the text encoding used in the barcode.
                    reader.BarcodeSettings.DetectEncoding = true;

                    // Read all barcodes found in the image (should be one QR code).
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // Verify that a result was obtained and that the decoded text is not empty.
                    if (results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText))
                    {
                        string decodedText = results[0].CodeText;
                        bool match = decodedText == originalText;

                        // Output the decoded text and whether it matches the original.
                        Console.WriteLine($"Decoded Text: {decodedText}");
                        Console.WriteLine($"Match Original: {match}");
                    }
                    else
                    {
                        // Inform the user if no barcode was detected or the result is empty.
                        Console.WriteLine("No barcode detected or empty result.");
                    }
                }
            }
        }
    }
}