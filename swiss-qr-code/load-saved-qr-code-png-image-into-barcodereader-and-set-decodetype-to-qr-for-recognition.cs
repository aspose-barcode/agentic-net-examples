// Title: QR Code recognition from PNG using BarCodeReader
// Description: Demonstrates loading a saved QR Code PNG file and decoding it with Aspose.BarCode's BarCodeReader.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating how to use BarCodeReader with a specific DecodeType (QR) to read QR code images. It showcases key classes such as BarCodeReader, DecodeType, and BarCodeResult, which are commonly used for extracting data from scanned barcodes in applications like inventory, ticketing, or authentication.
// Prompt: Load a saved QR Code PNG image into BarCodeReader and set DecodeType to QR for recognition.
// Tags: qr code, barcode recognition, decode type, aspose.barcode, png, reader

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates loading a QR code image file and decoding it using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that reads a QR code PNG file and prints decoded information.
    /// </summary>
    static void Main()
    {
        // Build the full path to the QR code image located in the current directory.
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "qr_code.png");

        // Verify that the file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize BarCodeReader with the image path and specify QR as the decode type.
        // DecodeType.QR returns a BaseDecodeType configured for QR code recognition.
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Perform the barcode reading operation.
            BarCodeResult[] results = reader.ReadBarCodes();

            // Check whether any QR codes were detected.
            if (results.Length == 0)
            {
                Console.WriteLine("No QR code detected.");
            }
            else
            {
                // Iterate through all detected results and output their details.
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Decoded Text: {result.CodeText}");
                }
            }
        }
    }
}