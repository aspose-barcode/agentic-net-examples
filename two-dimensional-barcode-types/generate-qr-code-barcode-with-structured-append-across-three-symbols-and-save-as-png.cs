// Title: Generate QR Code with Structured Append across Multiple Symbols
// Description: Demonstrates how to create a QR Code barcode split into three structured‑append symbols and save each as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and QR-specific parameters such as StructuredAppend. Developers often need to split large data across multiple QR symbols for better readability or scanning reliability; this snippet illustrates configuring total count, sequence indicator, and parity byte. It serves as a reference for creating multi‑symbol QR codes in .NET applications.
// Prompt: Generate a QR Code barcode with structured append across three symbols and save as PNG.
// Tags: qr code, structured append, png, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates three QR Code symbols using Structured Append
/// and saves each symbol as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary folder, encodes data into three QR symbols,
    /// and writes the resulting PNG images to disk.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for the generated QR symbols
        string outputFolder = Path.Combine(Path.GetTempPath(), "QrStructuredAppend_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Data to be encoded (same for all symbols; Structured Append will split it automatically)
        string data = "This is a sample text that will be split across three QR symbols using Structured Append.";

        // Loop to generate each part of the structured‑append QR code
        for (int index = 0; index < 3; index++)
        {
            // Initialize the QR generator with the data to encode
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, data))
            {
                // Configure Structured Append parameters
                generator.Parameters.Barcode.QR.StructuredAppend.TotalCount = 3;               // total number of symbols
                generator.Parameters.Barcode.QR.StructuredAppend.SequenceIndicator = index; // zero‑based index of the current symbol
                generator.Parameters.Barcode.QR.StructuredAppend.ParityByte = 0;             // optional parity byte (0 = not used)

                // Optional: set error correction level and other QR settings if desired
                // generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Build the output file name for the current QR part
                string filePath = Path.Combine(outputFolder, $"qr_part_{index + 1}.png");

                // Save the generated QR symbol as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);

                Console.WriteLine($"Saved QR part {index + 1} to: {filePath}");
            }
        }

        Console.WriteLine("All QR symbols generated successfully.");
    }
}