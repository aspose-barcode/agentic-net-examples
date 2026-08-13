// Title: UTF-8 Detection Test for QR Code
// Description: Demonstrates generating a QR code with multilingual text and verifying automatic UTF-8 detection during recognition.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator (EncodeTypes.QR) to create a QR code, and BarCodeReader (DecodeType.QR) to read it back. Developers often need to ensure correct character encoding handling for multilingual data, especially when automatic UTF-8 detection is required. The snippet highlights key API classes, typical use cases, and serves as a reference for building unit tests around encoding detection.
// Prompt: Create a unit test verifying automatic UTF8 detection works for a generated QR code containing multilingual text.
// Tags: qr, utf8 detection, barcode generation, barcode recognition, multilingual, aspose.barcode, unit test

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Contains the entry point that generates a QR code with multilingual text,
/// then reads it back to verify that automatic UTF-8 detection works correctly.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a QR code containing Latin, Chinese, and Cyrillic characters,
    /// saves it to a memory stream, and validates that the reader automatically
    /// detects UTF-8 encoding and returns the original text.
    /// </summary>
    static void Main()
    {
        // Multilingual text containing Latin, Chinese, and Cyrillic characters.
        string originalText = "Hello 世界 Привет";

        // Use a memory stream to avoid file I/O.
        using (var memoryStream = new MemoryStream())
        {
            // Create a QR code generator.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Encode the text using UTF-8. This inserts the appropriate ECI identifier.
                generator.SetCodeText(originalText, Encoding.UTF8);

                // Save the barcode image to the memory stream in PNG format.
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning for reading.
            memoryStream.Position = 0;

            // Initialize a reader for QR codes.
            using (var reader = new BarCodeReader(memoryStream, DecodeType.QR))
            {
                // Ensure automatic UTF-8 detection is enabled (default is true).
                reader.BarcodeSettings.DetectEncoding = true;

                bool detectionSucceeded = false;

                // Iterate through all detected barcodes (should be only one).
                foreach (var result in reader.ReadBarCodes())
                {
                    if (result.CodeText == originalText)
                    {
                        detectionSucceeded = true;
                        Console.WriteLine("UTF-8 detection succeeded: " + result.CodeText);
                    }
                    else
                    {
                        Console.WriteLine($"Mismatch detected. Expected: '{originalText}', Got: '{result.CodeText}'");
                    }
                }

                // Report overall test outcome.
                if (!detectionSucceeded)
                {
                    Console.WriteLine("UTF-8 detection failed.");
                }
            }
        }
    }
}