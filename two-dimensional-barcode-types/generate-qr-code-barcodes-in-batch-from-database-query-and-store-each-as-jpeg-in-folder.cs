using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of QR code images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates QR codes from a predefined list and saves them as JPEG files.
    /// </summary>
    static void Main()
    {
        // Simulated data source: list of URLs to encode as QR codes.
        List<string> qrData = new List<string>
        {
            "https://example.com/item/1",
            "https://example.com/item/2",
            "https://example.com/item/3",
            "https://example.com/item/4",
            "https://example.com/item/5"
        };

        // Determine the output directory relative to the current working directory.
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "QrCodes");

        // Ensure the output directory exists; create it if necessary.
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        int index = 1; // Counter for naming files sequentially.

        // Iterate over each text value and generate a corresponding QR code image.
        foreach (string codeText in qrData)
        {
            // Initialize a QR code generator with the current text.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Set a high error correction level to improve readability under adverse conditions.
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Construct the file name using a zero‑padded index (e.g., qr_001.jpg).
                string fileName = $"qr_{index:D3}.jpg";

                // Combine the folder path and file name to obtain the full output path.
                string outputPath = Path.Combine(outputFolder, fileName);

                // Save the generated QR code as a JPEG image.
                generator.Save(outputPath, BarCodeImageFormat.Jpeg);

                // Log the successful creation of the file.
                Console.WriteLine($"Saved QR code {index} to: {outputPath}");
            }

            index++; // Increment the counter for the next file.
        }

        // Indicate that the batch process has finished.
        Console.WriteLine("Batch QR code generation completed.");
    }
}