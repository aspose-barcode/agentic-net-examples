// Title: Barcode Recognition with Timeout
// Description: Demonstrates generating a sample Code128 barcode if missing, then reading it with a 5‑second timeout to abort long processing.
// Prompt: Set TimeOut property to five seconds to abort recognition if processing exceeds the specified limit.
// Tags: barcode, code128, timeout, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;

/// <summary>
/// Example program that creates a barcode image (if needed) and reads it using a timeout setting.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a sample barcode if it does not exist,
    /// then reads the barcode with a 5‑second timeout to prevent long‑running recognition.
    /// </summary>
    static void Main()
    {
        const string imagePath = "barcode.png";

        // ------------------------------------------------------------
        // Ensure a sample barcode image exists; create one if missing.
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            // Generate a Code128 barcode with the text "12345" and save as PNG.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
            {
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
        }

        // ------------------------------------------------------------
        // Read the barcode image using a timeout of 5 seconds (5000 ms).
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath))
        {
            // Abort recognition if it exceeds the specified time limit.
            reader.Timeout = 5000;

            // Iterate through all detected barcodes and output their type and text.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine("BarCode Type: " + result.CodeTypeName);
                Console.WriteLine("BarCode CodeText: " + result.CodeText);
            }
        }
    }
}